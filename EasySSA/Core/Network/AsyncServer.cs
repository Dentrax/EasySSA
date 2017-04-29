#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System.Net;
using System.Net.Sockets;
using System.Threading;
using EasySSA.Core.Network.Securities;
using EasySSA.Core.Utils;

namespace EasySSA.Core.Network {
    public abstract class AsyncServer {

        private Socket m_Socket;

        private bool m_wasConnectionAllowed;

        private SecurityFlags m_SecurityFlags;

        private ObjectPool<SocketContext> m_SocketContextPool;



        public volatile int ConnectionCount;

        public ObjectPool<SocketContext> SocketContextPool => m_SocketContextPool;

        private ManualResetEvent m_manualResetEvent = new ManualResetEvent(false);

        private Thread m_accepterThread = null;


        public abstract void Start(IPEndPoint endpoint);

    }
}
