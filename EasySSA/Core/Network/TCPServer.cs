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

        private bool m_wasConnectionAllowed;

        private SecurityFlags m_SecurityFlags;

        private ObjectPool<SocketContext> m_SocketContextPool;


        public volatile int ConnectionCount;

        public ObjectPool<SocketContext> SocketContextPool => m_SocketContextPool;

        private readonly ManualResetEvent m_manualResetEvent = new ManualResetEvent(false);

        private Thread m_accepterThread = null;

        private SROServiceComponent m_serviceComponent;

        public TCPServer(SROServiceComponent serviceComponent) {
            this.m_serviceComponent = serviceComponent;
        }


        public void DOBind(Action<bool> callback = null) {

            if (m_listenerSocket != null) {
                throw new Exception("[TCPServer::DOBind()] -> Trying to start server on socket which is already in use!");
            }

            m_listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try {
                m_listenerSocket.Bind(m_serviceComponent.EndPoint);
                m_listenerSocket.Listen(100);

                m_accepterThread = new Thread(DOBeginAccepter);
                m_accepterThread.Start();

                if (callback != null) callback(true);
            } catch (SocketException e) {
                if (callback != null) callback(false);
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

            try {
                switch (this.m_serviceComponent.ServiceType) {
                    case ServerServiceType.GATEWAY:
                        new GatewayServer(client, this.m_serviceComponent);
                        break;
                    case ServerServiceType.AGENT:
                        new AgentServer(client, this.m_serviceComponent);
                        break;
                    default:
                        throw new NotSupportedException("[SROServiceComponent::DOBind()] -> NotSupportedService handled : " + this.m_serviceComponent.ServiceType);
                }
            } catch (SocketException SocketEx) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.ResetColor();
            }
        }

        void OnClientDisconnect(ref Socket client) {
            // Check
            //if (client == null) {
            //    return;
            //}

            //try {
            //    client.Close();
            //} catch (SocketException SocketEx) {
            //    Console.ForegroundColor = ConsoleColor.DarkRed;
            //    Console.WriteLine(FilterMain.FILTER + "OnClientDisconnect()::Error closing socket. Exception: {0}", SocketEx.ToString());
            //    Console.ResetColor();
            //} catch (ObjectDisposedException ObjDispEx) {
            //    Console.ForegroundColor = ConsoleColor.DarkRed;
            //    Console.WriteLine(FilterMain.FILTER + "OnClientDisconnect()::Error closing socket (socket already disposed?). Exception: {0}", ObjDispEx.ToString());
            //    Console.ResetColor();
            //} catch {
            //    Console.ForegroundColor = ConsoleColor.DarkRed;
            //    Console.WriteLine(FilterMain.FILTER + "Something went wrong with Async systems.");
            //    Console.ResetColor();
            //}


            //client = null;
            //GC.Collect();
        }
    }
}
