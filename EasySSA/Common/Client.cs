using System;
using System.Net.Sockets;

using EasySSA.SSA;
using EasySSA.Server;
using EasySSA.Packets;
using EasySSA.Server.Services;
using EasySSA.Core.Network.Securities;

namespace EasySSA.Common {
    public sealed class Client : IDisposable {

        private SROServiceContext m_owner;

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

        public void SetOwner(SROServiceContext context) {
            if(this.m_owner != null) {
                this.m_owner = context;
            }
        }

        public long GetSocketHandle() {
            return m_socketHandle;
        }

        public void SetFingerprint(Fingerprint fingerprint) {
            Security = new Security();
            Security.GenerateSecurity(fingerprint.SecurityFlag.HasFlag(SecurityFlags.Blowfish), fingerprint.SecurityFlag.HasFlag(SecurityFlags.SecurityBytes), fingerprint.SecurityFlag.HasFlag(SecurityFlags.Handshake));
            Security.ChangeIdentity(fingerprint.IdentityID, fingerprint.IdentityFlag);
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

        public void SendPacket(Packet packet, Action<bool> callback = null) {
            try {
                this.m_owner.GetLocalSecurity.Send(packet);
                this.m_owner.TransferToClient();
                callback?.Invoke(true);
            } catch {
                callback?.Invoke(false);
            }
        }

        public void SendMessage(MessageType type, string sender, string message, Action<bool> callback = null) {
            Packet packet = new Packet(0x3026);
            switch (type) {
                case MessageType.PM:
                    packet.WriteUInt8(2);
                    packet.WriteAscii(sender);
                    break;
                case MessageType.GLOBAL:
                    packet.WriteUInt8(6);
                    packet.WriteAscii(sender);
                    break;
                case MessageType.NOTICE:
                    packet.WriteUInt8(7);
                    break;
                case MessageType.ACADEMY:
                    packet.WriteUInt8(0x10);
                    packet.WriteAscii(sender);
                    break;
                default:
                    break;
            }

            packet.WriteAscii(message);
            this.SendPacket(packet, callback);
        }

    }
}
