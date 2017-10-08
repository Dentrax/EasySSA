#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using EasySSA.Common;
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
            //socket.Blocking = false;
            //socket.NoDelay = true;

            try {

                socket.Connect(endpoint);

                if (socket.Connected) {
                    callback?.Invoke(SocketError.Success);
                    return socket;
                } else {
                    callback?.Invoke(SocketError.HostUnreachable);
                    return null;
                }

                //TODO: Async connect ?
                /*IAsyncResult iar = socket.BeginConnect(endpoint, null, null);
                if (iar.AsyncWaitHandle.WaitOne(timeout)) {
                    socket.EndConnect(iar);
                    callback?.Invoke(SocketError.Success);
                    return socket;
                } else {
                    socket.Close();
                    callback?.Invoke(SocketError.HostUnreachable);
                    return null;
                }*/

            } catch (AbandonedMutexException e) {
                Logger.NETWORK.Print(LogLevel.Error, e.ToString());
                callback?.Invoke(SocketError.TryAgain);
                return null;
            } catch (ArgumentNullException e){
                Logger.NETWORK.Print(LogLevel.Error, e.ToString());
                callback?.Invoke(SocketError.InvalidArgument);
                return null;
            } catch (SocketException e) {
                Logger.NETWORK.Print(LogLevel.Error, e.ToString());
                callback?.Invoke(SocketError.SocketNotSupported);
                return null;
            } catch (ObjectDisposedException e) {
                Logger.NETWORK.Print(LogLevel.Error, e.ToString());
                callback?.Invoke(SocketError.NotSocket);
                return null;
            } catch (SecurityException e) {
                Logger.NETWORK.Print(LogLevel.Error, e.ToString());
                callback?.Invoke(SocketError.AccessDenied);
                return null;
            } catch (InvalidOperationException e) {
                Logger.NETWORK.Print(LogLevel.Error, e.ToString());
                callback?.Invoke(SocketError.OperationNotSupported);
                return null;
            }
        }

        public static ushort TryGetRandomValidPort() {
            ushort port = 25000;
            ushort currentPort = port;
            bool isValidPort = false;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            do {
                try {
                    socket.Bind(new IPEndPoint(IPAddress.Loopback, currentPort));
                    port = currentPort;
                    isValidPort = true;
                } catch { currentPort++; }

            } while (!isValidPort);
            socket.Close();
            socket = null;
            return port;
        }
    }
}
