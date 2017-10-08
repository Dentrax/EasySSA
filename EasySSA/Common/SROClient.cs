#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Net.Sockets;

using EasySSA.SSA;
using EasySSA.Context;
using EasySSA.Core.Network.Securities;
using System.Collections.Generic;

namespace EasySSA.Common {
    public sealed class SROClient : IDisposable {

        private SROServiceContext m_owner;

        private readonly Object LOCK = new Object();

        private bool m_disposed;

        #region Variables-Network

        public Socket Socket { get; private set; }

        public Security Security { get; private set; }

        public TransferBuffer TransferBuffer { get; private set; }

        public ServerServiceType ServerServiceType { get; private set; }

        public bool HasSendingPackets { get; set; }

        public string IPAddress { get; private set; }

        private readonly long m_socketHandle;

        #endregion

        public Account Account { get; private set; }

        public List<Character> Characters { get; private set; }

        public bool IsWaitingForData { get; set; }
        public bool IsWatingForFinish { get; set; }

        public bool IsClientless { get; set; }
        public bool CanSwitchClient { get; set; }

        public bool CanAccountLogin { get; set; }
        public bool CanCaptchaCheck { get; set; }
        public bool CanCharacterSelection { get; set; }
        public bool CanClientlessSwitchToClient { get; set; }

        public uint SessionID { get; set; }
        public ushort ServerID { get; set; }
        public byte LocaleID { get; set; }
        public byte VersionID { get; set; }

        public ushort LocalPort { get; set; }

        public bool IsConnectedToAgent { get; set; }
        public string AgentIP { get; set; }
        public ushort AgentPort { get; set; }

        public byte AgentLoginFixCounter { get; set; }


        public bool ClientConnected { get; set; }


        public SROClient(Socket socket) {
            this.Socket = socket;
            this.m_socketHandle = socket.Handle.ToInt64();
            Socket.Blocking = false;
            Socket.NoDelay = true;

            this.Security = new Security();
            this.TransferBuffer = new TransferBuffer(0x10000, 0, 0);
            this.Characters = new List<Character>();
            this.HasSendingPackets = false;
        }

        ~SROClient() {
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
                lock (LOCK) {
                    if (Socket != null) {
                        if (Socket.Connected) {
                            Socket.Shutdown(SocketShutdown.Both);
                        }
                        Socket.Close();
                        Socket = null;
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

        #region MAIN_RECEIVE_LOOP



        #endregion

        public void SendPacket(Packet packet, Action<bool> callback = null) {
            //TODO: Fix callback
            try {
                this.m_owner.GetClientSecurity.Send(packet);
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
