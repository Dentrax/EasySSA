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
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace EasySSA.Core.Network {
    public abstract class TCPServer {

        internal bool IsActive;

        private Socket m_listenerSocket;

        private bool m_wasConnectionAllowed;

        private SecurityFlags m_SecurityFlags;

        private ObjectPool<SocketContext> m_SocketContextPool;


        public volatile int ConnectionCount;

        public ObjectPool<SocketContext> SocketContextPool => m_SocketContextPool;

        private readonly ManualResetEvent m_manualResetEvent = new ManualResetEvent(false);

        private Thread m_accepterThread = null;


        public void DOBind(IPEndPoint endpoint) {

            if (m_listenerSocket != null) {
                throw new Exception("[TCPServer::DOBind()] -> Trying to start server on socket which is already in use!");
            }

            m_listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try {
                m_listenerSocket.Bind(endpoint);
                m_listenerSocket.Listen(100);

                m_accepterThread = new Thread(DOBeginAccepter);
                m_accepterThread.Start();
            } catch (SocketException e) {
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

            Client c = new Client(socket);



            try {
                switch (m_ServerType) {
                    case E_ServerType.GatewayServer: {
                            //pass socket to gateway context handler
                            new GatewayContext(client, OnClientDisconnect);
                            //m_ClientCount_Gateway++;
                            Console.Title = string.Format("Client count [GatewayServer: {0}] [AgentServer1: {1}] [AgentServer2: {2}]", FilterMain.gateway, FilterMain.agent1, FilterMain.agent2);
                        }
                        break;
                    case E_ServerType.AgentServer: {
                            //pass socket to agent context handler
                            new AgentContext(client, OnClientDisconnect);
                            //m_ClientCount_Agent++;
                            //FilterMain.cur_players++;
                            Console.Title = string.Format("Client count [GatewayServer: {0}] [AgentServer1: {1}] [AgentServer2: {2}]", FilterMain.gateway, FilterMain.agent1, FilterMain.agent2); ;
                        }
                        break;
                    case E_ServerType.AgentServer2: {
                            //pass socket to agent context handler
                            new AgentContext2(client, OnClientDisconnect);
                            //m_ClientCount_Agent2++;
                            //FilterMain.cur_players++;
                            Console.Title = string.Format("Client count [GatewayServer: {0}] [AgentServer1: {1}] [AgentServer2: {2}]", FilterMain.gateway, FilterMain.agent1, FilterMain.agent2);
                        }
                        break;
                    default: {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(FilterMain.FILTER + "AcceptConnectionCallback()::Unknown server type");
                            Console.ResetColor();
                            //Environment.Exit(0);
                        }
                        break;
                }
            } catch (SocketException SocketEx) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(FilterMain.FILTER + "AcceptConnectionCallback()::Error while starting context. Exception: {0}", SocketEx.ToString());
                Console.ResetColor();
            }
        }

        void OnClientDisconnect(ref Socket client, E_ServerType HandlerType) {
            // Check
            if (client == null) {
                return;
            }

            switch (HandlerType) {
                case E_ServerType.GatewayServer: {
                        //m_ClientCount_Gateway--;
                        Console.Title = string.Format("Client count [GatewayServer: {0}] [AgentServer1: {1}] [AgentServer2: {2}]", FilterMain.gateway, FilterMain.agent1, FilterMain.agent2);
                    }
                    break;
                case E_ServerType.AgentServer: {
                        //m_ClientCount_Agent--;
                        //FilterMain.cur_players--;
                        Console.Title = string.Format("Client count [GatewayServer: {0}] [AgentServer1: {1}] [AgentServer2: {2}]", FilterMain.gateway, FilterMain.agent1, FilterMain.agent2);
                    }
                    break;
                case E_ServerType.AgentServer2: {
                        //m_ClientCount_Agent2--;
                        //FilterMain.cur_players--;
                        Console.Title = string.Format("Client count [GatewayServer: {0}] [AgentServer1: {1}] [AgentServer2: {2}]", FilterMain.gateway, FilterMain.agent1, FilterMain.agent2);
                    }
                    break;
            }

            try {
                client.Close();
            } catch (SocketException SocketEx) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(FilterMain.FILTER + "OnClientDisconnect()::Error closing socket. Exception: {0}", SocketEx.ToString());
                Console.ResetColor();
            } catch (ObjectDisposedException ObjDispEx) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(FilterMain.FILTER + "OnClientDisconnect()::Error closing socket (socket already disposed?). Exception: {0}", ObjDispEx.ToString());
                Console.ResetColor();
            } catch {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(FilterMain.FILTER + "Something went wrong with Async systems.");
                Console.ResetColor();
            }


            client = null;
            GC.Collect();
        }
    }
}
