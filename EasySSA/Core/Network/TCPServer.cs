#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Net.Sockets;
using System.Security;
using System.Threading;

using EasySSA.Common;
using EasySSA.Context;
using EasySSA.Component;
using System.Net;

namespace EasySSA.Core.Network {
    public sealed class TCPServer {

        internal bool IsActive;

        private Socket m_listenerSocket;

        public volatile int ConnectionCount;

        private readonly ManualResetEvent m_manualResetEvent = new ManualResetEvent(false);

        private Thread m_accepterThread = null;

        private SROServiceComponent m_serviceComponent;

        public TCPServer(SROServiceComponent serviceComponent) {
            this.m_serviceComponent = serviceComponent;
            this.IsActive = false;
        }

        public void DOBind(Action<bool, BindErrorType> callback = null) {

            if(m_listenerSocket != null) {
                callback?.Invoke(false, BindErrorType.SERVER_BIND_SOCKET_NOT_NULL);
                return;
            }

            if (this.IsActive) {
                callback?.Invoke(false, BindErrorType.SERVER_BIND_SOCKET_ALREADY_ACTIVE);
                return;
            }

            this.m_listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try {

                this.m_listenerSocket.Bind(m_serviceComponent.LocalEndPoint);
                this.m_listenerSocket.Listen(100);
              
                this.m_accepterThread = new Thread(DOBeginAccepter);
                this.m_accepterThread.Start();

                bool flag1 = m_listenerSocket.Poll(1000, SelectMode.SelectRead);
                bool flag2 = m_listenerSocket.Available == 0;

                if (flag1 && flag2) {
                    this.IsActive = false;
                    this.m_serviceComponent.OnLocalSocketStatusChanged?.Invoke(SocketError.HostUnreachable);
                    callback?.Invoke(false, BindErrorType.SERVER_BIND_SOCKET_NON_AVAILABLE);
                } else {
                    this.IsActive = true;
                    this.m_serviceComponent.OnLocalSocketStatusChanged?.Invoke(SocketError.Success);
                    callback?.Invoke(true, BindErrorType.SUCCESS);
                }
                
                return;

            } catch (ArgumentNullException e) {
                Logger.TCP.Print(LogLevel.Error, e.ToString());
                this.m_serviceComponent.OnLocalSocketStatusChanged?.Invoke(SocketError.InvalidArgument);
                callback?.Invoke(false, BindErrorType.SERVER_BIND_ARGUMENT_NULL_EXCEPTION);
            } catch (SocketException e) {
                Logger.TCP.Print(LogLevel.Error, e.ToString());
                this.m_serviceComponent.OnLocalSocketStatusChanged?.Invoke(SocketError.AddressNotAvailable);
                callback?.Invoke(false, BindErrorType.SERVER_BIND_SOCKET_EXCEPTION);
            } catch (ObjectDisposedException e) {
                Logger.TCP.Print(LogLevel.Error, e.ToString());
                this.m_serviceComponent.OnLocalSocketStatusChanged?.Invoke(SocketError.NotSocket);
                callback?.Invoke(false, BindErrorType.SERVER_BIND_OBJECT_DISPOSED_EXCEPTION);
            } catch (SecurityException e) {
                Logger.TCP.Print(LogLevel.Error, e.ToString());
                this.m_serviceComponent.OnLocalSocketStatusChanged?.Invoke(SocketError.AccessDenied);
                callback?.Invoke(false, BindErrorType.SERVER_BIND_SECURITY_EXCEPTION);
            }

        }

        private void DOBeginAccepter() {
            while (m_listenerSocket != null) {
                m_manualResetEvent.Reset();
                try {
                    m_listenerSocket.BeginAccept(new AsyncCallback(DOConnectionAccepter), m_listenerSocket);
                } catch { }
                m_manualResetEvent.WaitOne();
            }
        }

        private void DOConnectionAccepter(IAsyncResult iar) {
            Socket socket = null;

            m_manualResetEvent.Set();

            try {
                socket = m_listenerSocket.EndAccept(iar);
            } catch (SocketException e) {
                throw new Exception("[TCPServer::DOConnectionAccepter()] -> Could not bind/listen/BeginAccept socket! " + e.ToString());

            } catch (ObjectDisposedException e) {
                throw new Exception("[TCPServer::DOConnectionAccepter()] -> ObjectDisposedException while EndAccept " + e.ToString());
            }

            SROClient client = new SROClient(socket);

            if(this.m_serviceComponent.OnClientConnected != null) {
                bool result = this.m_serviceComponent.OnClientConnected(client);
                if (result) {
                    this.BindClient(client);
                } else {
                    //TODO: Do not bind implements
                }
            } else {
                this.BindClient(client);
            }
           
        }

        private void BindClient(SROClient client) {
            try {
                new SROServiceContext(client, this.m_serviceComponent).DOBind( this.m_serviceComponent.OnServiceSocketStatusChanged);
            } catch { }
        }
    }
}
