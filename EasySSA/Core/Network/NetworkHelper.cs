using System;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Threading;

namespace EasySSA.Core.Network {
    public static class NetworkHelper {
        public static Socket TryConnect(IPEndPoint endpoint, int timeout, Action<SocketError> callback = null) {
            if (endpoint == null || endpoint.Address == null || endpoint.Port <= 0 || endpoint.Port >= 65535 || timeout <= 0) {
                callback?.Invoke(SocketError.NotInitialized);
                return null;
            }

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Blocking = false;
            socket.NoDelay = true;

            try {
                IAsyncResult iar = socket.BeginConnect(endpoint, null, null);

                if (iar.AsyncWaitHandle.WaitOne(timeout)) {
                    socket.EndConnect(iar);

                    callback?.Invoke(SocketError.Success);
                    return socket;
                } else {
                    socket.Close();
                    callback?.Invoke(SocketError.HostUnreachable);
                    return null;
                }
            } catch (AbandonedMutexException) {
                callback?.Invoke(SocketError.TryAgain);
                return null;
            } catch (ArgumentNullException){
                callback?.Invoke(SocketError.InvalidArgument);
                return null;
            } catch (SocketException) {
                callback?.Invoke(SocketError.HostDown);
                return null;
            } catch (ObjectDisposedException) {
                callback?.Invoke(SocketError.NotSocket);
                return null;
            } catch (SecurityException) {
                callback?.Invoke(SocketError.AccessDenied);
                return null;
            } catch (InvalidOperationException) {
                callback?.Invoke(SocketError.OperationNotSupported);
                return null;
            }
        }
    }
}
