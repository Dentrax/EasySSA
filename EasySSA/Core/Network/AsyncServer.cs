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

        internal bool IsActive;

        private Socket m_Socket;

        private bool m_wasConnectionAllowed;

        private SecurityFlags m_SecurityFlags;

        private ObjectPool<SocketContext> m_SocketContextPool;



        public volatile int ConnectionCount;

        public ObjectPool<SocketContext> SocketContextPool => m_SocketContextPool;

        private ManualResetEvent m_manualResetEvent = new ManualResetEvent(false);

        private Thread m_accepterThread = null;


        public abstract void Start(IPEndPoint endpoint);

        //ANTI-CPU LOAD RELAX
        //        var delay = TimeSpan.FromMilliseconds(50);
        //while (true) {
        // await Task.Delay(delay);
        //        await SendMessageAsync(mySocket, someData);
        //        await ReceiveReplyAsync(mySocket);
        //    }


        //TODO
        //    Check if client is connected
        //    Poll the client
        //Check if data is available
        //Read the data
        //Go back to step 1

        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/20f737aa-6c45-4e9f-b627-d3128d31de61/multi-threaded-server-socket-for-multiple-clients-threading-and-send-receive-issue?forum=csharpgeneral

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    }
}
