using System;
using System.Net;
using System.Net.Sockets;

namespace EasySSA.Core.Network {
    public static class NetworkHelper {
        public static Socket TryConnect(IPEndPoint endpoint, int timeout, Action<SocketError> callback = null) {
            if (endpoint == null || endpoint.Address == null || endpoint.Port <= 0 || endpoint.Port >= 65535 || timeout <= 0) {
                if (callback != null) callback(SocketError.NotInitialized);
            }

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Blocking = false;
            socket.NoDelay = true;

            try {
                IAsyncResult iar = socket.BeginConnect(endpoint, null, null);

                if (iar.AsyncWaitHandle.WaitOne(timeout)) {
                    socket.EndConnect(iar);

                    if (callback != null) callback(SocketError.Success);
                    return socket;
                } else {
                    socket.Close();
                }
            } catch { }

            if (callback != null) callback(SocketError.Fault);
            return null;
        }
    }
}
