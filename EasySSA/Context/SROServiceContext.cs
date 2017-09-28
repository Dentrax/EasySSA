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
using EasySSA.Packets;
using EasySSA.Component;
using EasySSA.Core.Network;

namespace EasySSA.Context {
    public sealed class SROServiceContext : IDisposable {

        public bool IsRunning { get; private set; }

        public DateTime StartTime { get; private set; }

        private SROServiceComponent ServiceComponent;

        private SROClient Client;

        private Socket m_clientSocket;
        private Socket m_serviceSocket;

        private Security m_clientSecurity;
        private Security m_serviceSecurity;

        private int TotalPacketCount = 0;

        byte[] m_clientBuffer = new byte[4096];
        byte[] m_serviceBuffer = new byte[4096];

        private bool m_wasDisposed = false;
        private bool m_isDisconnecting = false;
        private bool m_canPacketProcess = false;

        private readonly object LOCK = new object();

        public Security GetClientSecurity { get { return this.m_clientSecurity; } }

        public SROServiceContext(SROClient client, SROServiceComponent serviceComponent) {
            if(client == null || serviceComponent == null) {
                Logger.SERVICE.Print(LogLevel.Allocation, "SROServiceContext or client null");
                this.IsRunning = false;
                return;
            }

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

            if (ServiceComponent.IsDebugMode) {
                Logger.SERVICE.Print(LogLevel.Allocation, "SROServiceContext created from Client");
            }
        }

        ~SROServiceContext() {
            Dispose(false);
        }

        public void DOBind(Action<SROClient, SocketError> callback = null) {
            this.m_serviceSocket = NetworkHelper.TryConnect(this.ServiceComponent.ServiceEndPoint, this.ServiceComponent.ServiceBindTimeout, delegate(SocketError error) {
                callback?.Invoke(this.Client, error);
            });

            this.Start();
        }

        public bool IsServiceSocketConnected() {
            try {
                return !((m_serviceSocket.Poll(1000, SelectMode.SelectRead) && (m_serviceSocket.Available == 0)) || !m_serviceSocket.Connected);
            } catch {
                return false;
            }
        }

        public bool Start() {
            if (m_serviceSocket == null || !IsServiceSocketConnected()) {
                Stop();
                return false;
            }

            if (this.m_serviceSocket.Connected) {
                this.m_canPacketProcess = true;
                this.IsRunning = true;

                this.DoRecvFromService();
                this.DoRecvFromClient();

                if (ServiceComponent.IsDebugMode) {
                    Logger.SERVICE.Print(LogLevel.Info, "SROServiceContext started...");
                }

                return true;
            }

            return false;
        }

        public bool Stop() {
            if (!IsRunning) return true;

            bool flag1 = false;
            bool flag2 = false;

            this.m_canPacketProcess = false;
            this.m_isDisconnecting = true;

            try {
                this.Client.Disconnect();
                if (!this.Client.IsConnected()) {
                    this.Client.Dispose();
                }
                flag1 = true;
            } catch { }

            try {
                if (this.m_serviceSocket != null) {
                    if (this.m_serviceSocket.Connected) {
                        this.m_serviceSocket.Shutdown(SocketShutdown.Both);
                    }
                    this.m_serviceSocket.Close();
                    this.m_serviceSocket = null;
                    flag2 = true;
                }
            } catch { }

            if (flag1 && flag2) {
                this.IsRunning = false;
                if (ServiceComponent.IsDebugMode) {
                    Logger.SERVICE.Print(LogLevel.Info, "SROServiceContext stopped..!");
                }
                this.m_isDisconnecting = false;
                return true;
            }

            if (ServiceComponent.IsDebugMode) {
                Logger.SERVICE.Print(LogLevel.Info, "SROServiceContext stop error");
            }

            return false;
        }

        public void Disconnect(SROServiceContext server, ClientDisconnectType disconnectType) {
            if (this.IsRunning) {
                this.ServiceComponent.OnClientDisconnected?.Invoke(this.Client, disconnectType);
            }
        }

        private void DOPacketTransfer(Packet packet, PacketSocketType direction, PacketResult result) {

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

                            if (ServiceComponent.IsDebugMode) {
                                Logger.PACKET.Print(LogLevel.Warning, "Packet Operation (DISCONNECT) received : " + packet.HexOpcode);
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

                                if (ServiceComponent.IsDebugMode) {
                                    Logger.PACKET.Print(LogLevel.Warning, "Packet Operation (REPLACE) received : " + packet.HexOpcode);
                                }

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

                            if (ServiceComponent.IsDebugMode) {
                                Logger.PACKET.Print(LogLevel.Warning, "Packet Operation (INJECT) received : " + packet.HexOpcode);
                            }

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
                    if (ServiceComponent.IsDebugMode) {
                        Logger.PACKET.Print(LogLevel.Warning, "Packet Operation (IGNORE) received : " + packet.HexOpcode);
                    }
                    break;
                case PacketOperationType.RESPONSE:
                    if (resultInfo != null) {
                        if (resultInfo is PacketResult.PacketResponseResultInfo response) {
                            if (ServiceComponent.IsDebugMode) {
                                Logger.PACKET.Print(LogLevel.Warning, "Packet Operation (SEND) received : " + packet.HexOpcode);
                            }

                            response.Packets.ForEach(packets => {
                                security.Send(packets);
                            });
                        }
                    }
                    security.Send(packet);
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
                if (!this.m_isDisconnecting && this.m_canPacketProcess) {
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
                                if (this.ServiceComponent.OnPacketReceived != null) {
                                    PacketResult result = this.ServiceComponent.OnPacketReceived(this.Client, PacketDatabase.GetPacketFrom(packet, PacketSocketType.CLIENT), PacketSocketType.CLIENT);
                                    if (packet.Opcode != 0x9000 && packet.Opcode != 0x5000 && packet.Opcode != 0x2001) {
                                        DOPacketTransfer(packet, PacketSocketType.SERVER, result);
                                    }
                                }
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
        }

        private void OnRecvFromService(IAsyncResult iar) {
            lock (this.LOCK) {
                if (!this.m_isDisconnecting && this.m_canPacketProcess) {
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
                                if (this.ServiceComponent.OnPacketReceived != null) {
                                    PacketResult result = this.ServiceComponent.OnPacketReceived(this.Client, PacketDatabase.GetPacketFrom(packet, PacketSocketType.SERVER), PacketSocketType.SERVER);
                                    if (packet.Opcode != 0x9000 && packet.Opcode != 0x5000) {
                                        DOPacketTransfer(packet, PacketSocketType.CLIENT, result);
                                    }
                                }
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
            if (!this.m_isDisconnecting && this.m_canPacketProcess) {
                if (this.m_clientSocket.Connected) {
                    try {
                        SocketError error;
                        this.m_clientSocket.BeginSend(buffer, 0, len, SocketFlags.None, out error, new AsyncCallback(RaiseOnClientSendCompleteCallBack), null);
                    } catch {
                        this.Disconnect(this, ClientDisconnectType.SENDTO_CLIENT_BEGINSEND);
                        this.Stop();
                    }
                }
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
            if (!this.m_isDisconnecting && this.m_canPacketProcess) {
                if (this.m_serviceSocket.Connected) {
                    try {
                        SocketError error;
                        this.m_serviceSocket.BeginSend(buffer, 0, len, SocketFlags.None, out error, new AsyncCallback(RaiseOnServiceSendCompleteCallBack), null);
                    } catch {
                        this.Disconnect(this, ClientDisconnectType.SENDTO_SERVICE_BEGINSEND);
                        this.Stop();
                    }
                }
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

                if (ServiceComponent.IsDebugMode) {
                    Logger.SERVICE.Print(LogLevel.Deallocation, "SROServiceContext disponsed");
                }

                m_wasDisposed = true;
            }
        }


    }
}
