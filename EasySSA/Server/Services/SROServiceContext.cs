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
using System.Collections.Generic;

using EasySSA.SSA;
using EasySSA.Common;
using EasySSA.Services;
using EasySSA.Packets;
using EasySSA.Core.Network;
using EasySSA.Core.Network.Securities;

namespace EasySSA.Server.Services {
    public sealed class SROServiceContext : IDisposable {

        public bool IsRunning { get; private set; }

        public DateTime StartTime { get; private set; }

        private SROServiceComponent ServiceComponent;

        private Client Client;

        private Socket m_clientSocket;
        private Socket m_serviceSocket;

        private Security m_clientSecurity;
        private Security m_serviceSecurity;

        private int TotalPacketCount = 0;

        byte[] m_clientBuffer = new byte[4096];
        byte[] m_serviceBuffer = new byte[4096];

        private bool m_wasDisposed = false;

        private readonly object LOCK = new object();

        public Security GetClientSecurity { get { return this.m_clientSecurity; } }

        public SROServiceContext(Client client, SROServiceComponent serviceComponent) {
            this.ServiceComponent = serviceComponent;
            this.Client = client;
            this.Client.SetFingerprint(serviceComponent.Fingerprint);

            this.m_clientSocket = this.Client.Socket;

            this.m_serviceSecurity = new Security();
            this.m_clientSecurity = new Security();
            //this.m_clientSecurity = this.Client.Security;
            this.m_clientSecurity.GenerateSecurity(true, true, true);

            //this.m_clientTransferBuffer = this.Client.TransferBuffer;
            //this.m_serviceTransferBuffer = new TransferBuffer(0x10000, 0, 0);

            this.IsRunning = false;
        }

        ~SROServiceContext() {
            Dispose(false);
        }

        public void DOBind(Action<Client, SocketError> callback = null) {
            this.m_serviceSocket = NetworkHelper.TryConnect(this.ServiceComponent.ServiceEndPoint, this.ServiceComponent.ServiceBindTimeout, delegate(SocketError error) {
                callback?.Invoke(this.Client, error);
            });

            this.Start();
        }

        public bool Start() {
            if (m_serviceSocket == null) {
                Stop();
                return false;
            }

            this.IsRunning = true;

            this.DoRecvFromService();
            this.DoRecvFromClient();

            return true;
        }

        public bool Stop() {
            if (!IsRunning) return true;

            bool flag1 = false;
            bool flag2 = false;

            Console.WriteLine("Stop starting...");

            try {
                this.Client.Disconnect();
                if (!this.Client.IsConnected()) {
                    this.Client.Dispose();
                }
                flag1 = true;
            } catch { }

            try {
                this.m_serviceSocket.Close();
                if (!this.m_serviceSocket.Connected) {
                    flag2 = true;
                }
            } catch { }

            if (flag1 && flag2) {
                this.IsRunning = false;
                Console.WriteLine("Stopped..!");
                return true;
            }

            Console.WriteLine("Non-Stopped..!");

            return false;
        }

        public void Disconnect(SROServiceContext server, ClientDisconnectType disconnectType) {
            if (this.Client.IsConnected()) {
                this.ServiceComponent.OnClientDisconnected?.Invoke(this.Client, disconnectType);
            }
        }

        private void HandleAndTransferResult(Packet packet, PacketSocketType direction, PacketResult result) {

            Security security = (direction == PacketSocketType.CLIENT) ? this.m_clientSecurity : this.m_serviceSecurity;

            PacketOperationType operation = result.ResultType;
            PacketResult.PacketResultInfo resultInfo = result.ResultInfo;

            switch (operation) {
                case PacketOperationType.DISCONNECT:
                    if (resultInfo != null) {
                        if (resultInfo is PacketResult.PacketDisconnectResultInfo disconnect) {
                            if (!string.IsNullOrEmpty(disconnect.Notice)) {
                                this.SendMessage(MessageType.NOTICE, disconnect.Notice.Trim());
                            }
                            if (disconnect.DisconnectReason != null) {
                                int id = Convert.ToInt32(disconnect.DisconnectReason);
                                //TODO: Complete
                            }

                            this.Disconnect(this, ClientDisconnectType.PACKET_OPERATION_DISCONNECT);
                            this.Stop();
                        }
                    }
                    break;
                case PacketOperationType.REPLACE:
                    if (resultInfo != null) {
                        if (resultInfo is PacketResult.PacketReplaceResultInfo replace) {
                            if (replace.Packet == packet) {
                                replace.ReplaceWith.ForEach(packets => {
                                    security.Send(packets);
                                });
                            }
                        }
                    }
                    break;
                case PacketOperationType.INJECT:
                    if (resultInfo != null) {
                        if (resultInfo is PacketResult.PacketInjectResultInfo inject) {
                            if (!inject.AfterPacket) {
                                security.Send(packet);
                            }

                            if (inject.Packet == packet) {
                                inject.InjectWith.ForEach(packets => {
                                    security.Send(packets);
                                });
                            }

                            if (inject.AfterPacket) {
                                security.Send(packet);
                            }
                        }
                    }
                    break;
                case PacketOperationType.IGNORE:
                    break;
                case PacketOperationType.BLOCK_IP:
                    this.Disconnect(this, ClientDisconnectType.PACKET_OPERATION_DISCONNECT);
                    this.Stop();
                    break;
                case PacketOperationType.NOTHING:
                    security.Send(packet);
                    break;
                default:
                    security.Send(packet);
                    break;
            }

            if (direction == PacketSocketType.CLIENT) {
                TransferToClient();
            } else if (direction == PacketSocketType.SERVER) {
                TransferToService();
            }

        }

        #region Receivers

        private void OnRecvFromClient(IAsyncResult iar) {
            lock (this.LOCK) {
                try {
                    int recvCount = this.m_clientSocket.EndReceive(iar);

                    if (recvCount == 0) {
                        this.Disconnect(this, ClientDisconnectType.ONRECV_FROM_CLIENT_SIZE_ZERO);
                        this.Stop();
                        return;
                    }

                    this.m_clientSecurity.Recv(m_clientBuffer, 0, recvCount);

                    List<Packet> clientRecevivePackets = this.m_clientSecurity.TransferIncoming();

                    if (clientRecevivePackets != null) {

                        TotalPacketCount += clientRecevivePackets.Count;

                        for (int i = 0; i < clientRecevivePackets.Count; i++) {
                            Packet packet = clientRecevivePackets[i];

                            if (packet.Opcode == 0x9000 || packet.Opcode == 0x5000 || packet.Opcode == 0x2001) {
                                Console.WriteLine("[Client] HANDSHAKE : " + packet.Opcode);
                                continue;
                            }

                            /*if(this.ServiceComponent.OnPacketReceived != null) {
                                PacketResult result = this.ServiceComponent.OnPacketReceived(this.Client, new SROPacket(packet), PacketSocketType.CLIENT);
                                HandleAndTransferResult(packet, PacketSocketType.SERVER, result);
                                   
                            }*/

                            m_serviceSecurity.Send(packet);
                        }

                    }

                    this.TransferToService();
                    this.DoRecvFromClient();
                } catch {
                    this.Disconnect(this, ClientDisconnectType.ONRECV_FROM_CLIENT);
                    this.Stop();
                }
            }
        }

        private void OnRecvFromService(IAsyncResult iar) {
            lock (this.LOCK) {
                try {
                    int recvCount = this.m_serviceSocket.EndReceive(iar);

                    if (recvCount == 0) {
                        this.Disconnect(this, ClientDisconnectType.ONRECV_FROM_SERVICE_SIZE_ZERO);
                        this.Stop();
                        return;
                    }

                    this.m_serviceSecurity.Recv(m_serviceBuffer, 0, recvCount);

                    List<Packet> serviceRecevivePackets = this.m_serviceSecurity.TransferIncoming();

                    if (serviceRecevivePackets != null) {

                        for (int i = 0; i < serviceRecevivePackets.Count; i++) {
                            Packet packet = serviceRecevivePackets[i];

                            if (packet.Opcode == 0x9000 || packet.Opcode == 0x5000) {
                                Console.WriteLine("[Service] HANDSHAKE : " + packet.Opcode);
                                continue;
                            }

                            /*if (this.ServiceComponent.OnPacketReceived != null) {
                                PacketResult result = this.ServiceComponent.OnPacketReceived(this.Client, new SROPacket(packet), PacketSocketType.SERVER);
                                HandleAndTransferResult(packet, PacketSocketType.CLIENT, result);
                            }*/

                            m_clientSecurity.Send(packet);
                        }
                           
                    }
                    this.TransferToClient();
                    this.DoRecvFromService();
                } catch {
                    this.Disconnect(this, ClientDisconnectType.ONRECV_FROM_SERVICE);
                    this.Stop();
                }
            }
        }

        private void DoRecvFromClient() {
            try {
                this.m_clientSocket.BeginReceive(m_clientBuffer, 0, m_clientBuffer.Length, SocketFlags.None, new AsyncCallback(OnRecvFromClient), null);
            } catch {
                this.Disconnect(this, ClientDisconnectType.DORECV_FROM_CLIENT);
                this.Stop();
            }
        }

        private void DoRecvFromService() {
            try {
                this.m_serviceSocket.BeginReceive(m_serviceBuffer, 0, m_serviceBuffer.Length, SocketFlags.None, new AsyncCallback(OnRecvFromService), null);
            } catch {
                this.Disconnect(this, ClientDisconnectType.DORECV_FROM_SERVICE);
                this.Stop();
            }
        }


        public void TransferToClient() {
            try {
                List<KeyValuePair<TransferBuffer, Packet>> clientSendPackets = this.m_clientSecurity.TransferOutgoing();
                if (clientSendPackets != null) {
                    clientSendPackets.ForEach(item => {
                        DoSendToClient(item.Key.Buffer, item.Key.Buffer.Length);
                    });
                }
            } catch {
                this.Disconnect(this, ClientDisconnectType.TRANSFERTO_CLIENT_OUTGOING);
                this.Stop();
            }
        }

        private void TransferToService() {
            try {
                List<KeyValuePair<TransferBuffer, Packet>> serviceSendPackets = this.m_serviceSecurity.TransferOutgoing();
                if (serviceSendPackets != null) {
                    serviceSendPackets.ForEach(item => {
                        DoSendToService(item.Key.Buffer, item.Key.Buffer.Length);
                    });
                }
            } catch {
                this.Disconnect(this, ClientDisconnectType.TRANSFERTO_SERVICE_OUTGOING);
                this.Stop();
            }
        }

        private void DoSendToClient(byte[] buffer, int len) {
            try {
                SocketError error;
                this.m_clientSocket.BeginSend(buffer, 0, len, SocketFlags.None, out error, new AsyncCallback(RaiseOnClientSendCompleteCallBack), null);
            } catch {
                this.Disconnect(this, ClientDisconnectType.SENDTO_CLIENT_BEGINSEND);
                this.Stop();
            }
        }

        private void RaiseOnClientSendCompleteCallBack(IAsyncResult iar) {
            try {
                SocketError error;
                this.m_clientSocket.EndSend(iar, out error);
            } catch {
                this.Disconnect(this, ClientDisconnectType.SENDTO_CLIENT_ENDSEND);
                this.Stop();
            }
        }

        private void DoSendToService(byte[] buffer, int len) {
            try {
                SocketError error;
                this.m_serviceSocket.BeginSend(buffer, 0, len, SocketFlags.None, out error, new AsyncCallback(RaiseOnServiceSendCompleteCallBack), null);
            } catch {
                this.Disconnect(this, ClientDisconnectType.SENDTO_SERVICE_BEGINSEND);
                this.Stop();
            }
        }

        private void RaiseOnServiceSendCompleteCallBack(IAsyncResult iar) {
            try {
                SocketError error;
                this.m_serviceSocket.EndSend(iar, out error);
            } catch {
                this.Disconnect(this, ClientDisconnectType.SENDTO_SERVICE_ENDSEND);
                this.Stop();
            }
        }

        #endregion

        #region FUNCTIONS_PACKET


        public void SendPacket(Packet packet) {
            this.SendPacket(packet, PacketSocketType.CLIENT, null);
        }

        public void SendPacket(Packet packet, PacketSocketType direction) {
            this.SendPacket(packet, direction, null);
        }

        public void SendPacket(Packet packet, PacketSocketType direction, Action<bool> callback) {
            try {
                if (direction == PacketSocketType.CLIENT) {
                    this.m_clientSecurity.Send(packet);
                    this.TransferToClient();
                } else if (direction == PacketSocketType.SERVER) {
                    this.m_serviceSecurity.Send(packet);
                    this.TransferToService();
                }
                callback?.Invoke(true);
            } catch {
                callback?.Invoke(false);
            }
        }

        public void SendMessage(MessageType type, string message, string sender = "EasySSA") {
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
            this.SendPacket(packet, PacketSocketType.CLIENT);
        }

        #endregion

        public void Dispose() {
            this.Dispose(true);
        }

        private void Dispose(bool disposing) {
            if (!m_wasDisposed) {
                if (disposing) {
                    //
                }
                if (this.m_clientSocket != null) {
                    this.m_clientSocket.Dispose();
                    this.m_clientSocket = null;
                }

                m_wasDisposed = true;
            }
        }


    }
}
