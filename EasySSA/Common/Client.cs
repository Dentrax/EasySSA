using EasySSA.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EasySSA.Common {
    public sealed class Client {

        public Socket Socket { get; private set; }

        public ServerServiceType ServerType { get; private set; }

        public string IPAddress { get; private set; }

        private readonly long m_socketHandle;

        public Client(Socket socket) {
            this.Socket = socket;
            this.m_socketHandle = socket.Handle.ToInt64();
        }

        public long GetSocketHandle() {
            return m_socketHandle;
        }

        public bool IsConnected() {
            try {
                return !((Socket.Poll(1000, SelectMode.SelectRead) && (Socket.Available == 0)) || !Socket.Connected);
            } catch {
                return false;
            }
        }

    }
}
