#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

//Reference -> https://github.com/tanisman/SilkroadProject/blob/master/SCommon/Networking/SocketContext.cs

using System;
using System.Net.Sockets;
using EasySilkroadSecurityApi.SSA;
using EasySilkroadSecurityApi.Core.Network.Securities;
using System.Threading;
using System.Collections.Generic;

namespace EasySilkroadSecurityApi.Core.Network {
    public abstract class SocketContext : IDisposable {

        private bool m_wasDisposed;

        private TCPServer m_owner;

        private Socket m_socket;

        private Security m_security;

        private byte[] m_buffer;

        private object m_context;

        private volatile int m_firedConnectionLost;

        private bool m_sendingInProgress;

        private bool m_disconnectAfterTransfer;

        public Socket Socket {
            get { return this.m_socket; }
        }

        public object Context {
            get { return this.m_context; }
        }


        public static EventHandler<Packet> OnPacketReceived;

        public static EventHandler OnLostConnection;

        public static EventHandler<Packet> OnPacketSent;

        public SocketContext() {
            m_buffer = new byte[4096];
        }

        public SocketContext(Socket client, SecurityFlags flags) : this() {
            SetSocket(client);
            SetSecurity(flags);
        }

        ~SocketContext() {
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (!m_wasDisposed) {
                if (disposing) {
                    //
                }
                if (m_security != null) {
                    m_security.Dispose();
                    m_security = null;
                }
                m_buffer = null;
                m_wasDisposed = true;
            }
        }

        private void Clean() {
            m_security.Dispose();
            m_security = null;
            m_socket = null;
        }


        public void SetOwner(TCPServer server) {
            m_owner = server;
        }

        public void SetSocket(Socket client) {
            m_firedConnectionLost = 0;
            m_disconnectAfterTransfer = false;
            m_sendingInProgress = false;
            m_socket = client;
        }

        public void SetSecurity(SecurityFlags flags) {
            m_security = new Security();
            m_security.GenerateSecurity(flags.HasFlag(SecurityFlags.Blowfish), flags.HasFlag(SecurityFlags.SecurityBytes), flags.HasFlag(SecurityFlags.Handshake));
            m_security.ChangeIdentity("GatewayServer", 0);
        }

        public void SetContext(object context) {
            m_context = context;
        }

        public void Begin() {
            if (m_socket == null || m_security == null) {
                throw new InvalidOperationException("[EasySSA.Core.Network::SocketContext::Begin()] You must set socket & security before calling Begin()");
            }

            SocketError ec;
            m_socket.BeginReceive(this.m_buffer, 0, this.m_buffer.Length, SocketFlags.None, out ec, AsyncReceive, null);

            if (ec != SocketError.Success && ec != SocketError.IOPending) {
                Disconnect();
                FireOnLostConnection();
                return;
            }

            ProcessSendQueue();
        }

        public void Send(Packet p) {
            m_security.Send(p);
            ProcessSendQueue();
        }

        public void EnqueuePacket(Packet p) {
            m_security.Send(p);
        }

        public void ProcessSendQueue() {
            try {
                lock (this) {
                    if (m_sendingInProgress) 
                        return;

                    if (m_security.HasPacketToSend()) {
                        m_sendingInProgress = true;

                        var packet = m_security.GetPacketToSend();

                        SocketError ec;
                        m_socket.BeginSend(packet.Key.Buffer, 0, packet.Key.Size, SocketFlags.None, out ec, AsyncSend, packet);

                        if (ec != SocketError.Success && ec != SocketError.IOPending) {
                            Disconnect();
                            FireOnLostConnection();
                            return;
                        }
                    }
                }
            } catch (Exception) {
                Disconnect();
                FireOnLostConnection();
            }
        }

        public void Disconnect() {
            try {
                lock (this) {
                    if (m_socket != null) {
                        if (m_socket.Connected) {
                            if (m_sendingInProgress) {
                                m_disconnectAfterTransfer = true;
                                return;
                            } else {
                                m_socket.Disconnect(true);
                                m_socket.Close();
                            }
                        }
                        FireOnLostConnection();
                    }
                }
            } catch (Exception) {

            }
        }

        private void AsyncReceive(IAsyncResult res) {
            try {
                if (!m_socket.Connected)
                {
                    FireOnLostConnection();
                    return;
                }

                SocketError ec;

                int size = m_socket.EndReceive(res, out ec);

                if (ec != SocketError.Success) {
                    FireOnLostConnection();
                    return;
                }

                if (size <= 0)
                {
                    FireOnLostConnection();
                    return;
                }


                m_security.Recv(m_buffer, 0, size);
                List<Packet> packets = m_security.TransferIncoming();
                foreach (var p in packets)
                    FireOnPacketReceived(p);

                m_socket.BeginReceive(m_buffer, 0, m_buffer.Length, SocketFlags.None, out ec, AsyncReceive, null);

                if (ec != SocketError.Success && ec != SocketError.IOPending) {
                    Disconnect();
                    FireOnLostConnection();
                    return;
                }
            } catch (Exception ex) {
                Console.WriteLine(ex);
                Disconnect();
                FireOnLostConnection();
            }
        }

        private void AsyncSend(IAsyncResult res) {
            try {

                lock (this) {

                    if (!m_socket.Connected) {
                        FireOnLostConnection();
                        return;
                    }

                    SocketError ec;
                    int size = m_socket.EndSend(res, out ec);

                    if (ec != SocketError.Success) {
                        FireOnLostConnection();
                        return;
                    }

                    if (size <= 0)
                    {
                        FireOnLostConnection();
                        return;
                    }

                    KeyValuePair<TransferBuffer, Packet> transferedPacket = (KeyValuePair<TransferBuffer, Packet>)res.AsyncState;
                    FireOnPacketSent(transferedPacket.Value);

                    if (!m_security.HasPacketToSend()) 
                    {
                        m_sendingInProgress = false; 
                        if (m_disconnectAfterTransfer) {
                            Disconnect();
                        }

                        return;
                    }

                    var packet = m_security.GetPacketToSend();
                    m_socket.BeginSend(packet.Key.Buffer, 0, packet.Key.Size, SocketFlags.None, out ec, AsyncSend, packet);

                    if (ec != SocketError.Success && ec != SocketError.IOPending) {
                        Disconnect();
                        FireOnLostConnection();
                        return;
                    }
                }
            } catch (Exception) {
                Disconnect();
                FireOnLostConnection();
            }
        }

        private void FireOnLostConnection() {
            if (Interlocked.CompareExchange(ref m_firedConnectionLost, 1, 0) == 0) {
                if (OnLostConnection != null) {
                    OnLostConnection(this, null);
                }
                this.Clean();
                m_owner.SocketContextPool.PutObject(this);
                Interlocked.Decrement(ref m_owner.ConnectionCount);
            }
        }

        private void FireOnPacketReceived(Packet p) {
            if (p.Opcode == 0x5000 || p.Opcode == 0x9000) {
                ProcessSendQueue();
            } else {
                if (OnPacketReceived != null) {
                    OnPacketReceived(this, p);
                }
            }
        }

        private void FireOnPacketSent(Packet p) {
            if (OnPacketSent != null) {
                OnPacketSent(this, p);
            }
        }
    }
}
