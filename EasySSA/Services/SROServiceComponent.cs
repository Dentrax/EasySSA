#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Net;

using EasySSA.Server;
using EasySSA.SSA;
using EasySSA.Common;
using EasySSA.Packets;
using EasySSA.Server.Services;
using EasySSA.Core.Network;
using System.Net.Sockets;

namespace EasySSA.Services {
    public sealed class SROServiceComponent {

        public SROServiceContext ServiceServer { get; private set; }

        public ServerServiceType ServiceType { get; private set; }

        public int ServiceIndex { get; private set; }

        public Action<SocketError> OnLocalSocketStatusChanged;

        public Action<SocketError> OnServiceSocketStatusChanged;


        public Func<Client, bool> OnClientConnected;

        public Action<Client, ClientDisconnectType> OnClientDisconnected;

        public Func<Client, SROPacket, PacketSocketType, PacketResult> OnPacketReceived;

        public Fingerprint Fingerprint { get; private set; }

        public IPEndPoint LocalEndPoint { get; private set; }

        public IPEndPoint ServiceEndPoint { get; private set; }

        public int MaxClientCount { get; private set; }

        public int LocalBindTimeout { get; private set; }

        public int ServiceBindTimeout { get; private set; }

        public SROServiceComponent(ServerServiceType serviceType, int serviceIndex) {
            this.ServiceType = serviceType;
            this.ServiceIndex = serviceIndex;
        }


        #region Functions

        public SROServiceComponent SetFingerprint(Fingerprint fingerprint) {
            this.Fingerprint = fingerprint;
            return this;
        }

        public SROServiceComponent SetLocalEndPoint(IPEndPoint endpoint) {
            this.LocalEndPoint = endpoint;
            return this;
        }

        public SROServiceComponent SetServiceEndPoint(IPEndPoint redirectPoint) {
            this.ServiceEndPoint = redirectPoint;
            return this;
        }

        public SROServiceComponent SetMaxClientCount(int count) {
            this.MaxClientCount = count;
            return this;
        }

        public SROServiceComponent SetLocalBindTimeout(int timeout) {
            this.LocalBindTimeout = timeout;
            return this;
        }

        public SROServiceComponent SetServiceBindTimeout(int timeout) {
            this.ServiceBindTimeout = timeout;
            return this;
        }

        public void DOBind(Action<bool> callback = null) {
            new TCPServer(this).DOBind(callback);
        }

        #endregion

    }
}
