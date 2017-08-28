using EasySSA.Server;
using EasySSA.SSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EasySSA.Common {
    public sealed class Client : IDisposable {

        public Socket Socket { get; private set; }

        public Security Security { get; private set; }

        public TransferBuffer TransferBuffer { get; private set; }

        public ServerServiceType ServerServiceType { get; private set; }

        //TODO: Implement this
        public bool HasSendingPackets { get; set; }

        public string IPAddress { get; private set; }

        private readonly long m_socketHandle;

        private bool m_disposed;

        public Client(Socket socket) {
            this.Socket = socket;
            this.m_socketHandle = socket.Handle.ToInt64();
            this.Security = new Security();
            this.TransferBuffer = new TransferBuffer(0x10000, 0, 0);
            this.HasSendingPackets = false;
        }

        ~Client() {
            Dispose(false);
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

        public void Disconnect() {
            try {
                lock (this) {
                    if (Socket != null) {
                        if (Socket.Connected) {
                            Socket.Disconnect(true);
                            Socket.Close();
                        }
                    }
                }
            } catch { }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing) {
            if (!this.m_disposed) {
                if (disposing) {
                    if (this.Security != null) {
                        this.Security.Dispose();
                        this.Security = null;
                    }
                    if (this.TransferBuffer != null) {
                        this.TransferBuffer = null;
                    }
                }
                this.m_disposed = true;
            }
        }
    }
}
