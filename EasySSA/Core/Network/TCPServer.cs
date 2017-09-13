#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using EasySSA.Common;
using EasySSA.Core.Network.Securities;
using EasySSA.Core.Utils;
using EasySSA.Server;
using EasySSA.Server.Services;
using EasySSA.Services;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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


        public void DOBind(Action<bool> callback = null) {

            if (m_listenerSocket != null || IsActive) {
                throw new Exception("[TCPServer::DOBind()] -> Trying to start server on socket which is already in use!");
            }

            m_listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try {
                m_listenerSocket.Bind(m_serviceComponent.LocalEndPoint);
                m_listenerSocket.Listen(100);

                m_accepterThread = new Thread(DOBeginAccepter);
                m_accepterThread.Start();

                this.IsActive = true;

                this.m_serviceComponent.OnLocalSocketStatusChanged?.Invoke(SocketError.Success);
                
                callback?.Invoke(true);
            } catch (SocketException e) {

                this.IsActive = false;

                this.m_serviceComponent.OnLocalSocketStatusChanged?.Invoke(SocketError.Fault);

                callback?.Invoke(false);
                throw new Exception("[TCPServer::DOBind()] -> Could not bind/listen/BeginAccept socket! " + e.ToString());
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

            Client client = new Client(socket);

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

        private void BindClient(Client client) {
            try {

                new SROServiceContext(client, this.m_serviceComponent).DOBind( this.m_serviceComponent.OnServiceSocketStatusChanged);

            } catch {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.ResetColor();
            }
        }
    }
}
