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

namespace EasySSA.Services {
    public sealed class SROServiceComponent {

        public SROModuleServer ServiceServer { get; private set; }

        public ServerServiceType ServiceType { get; private set; }

        public int ServiceIndex { get; private set; }
       

        public Func<Client, bool> OnClientConnected;

        public Action<Client, ClientDisconnectType> OnClientDisconnected;

        public Func<Client, SROPacket, PacketOperationType> OnPacketReceived;

        public Fingerprint Fingerprint { get; private set; }

        public IPEndPoint EndPoint { get; private set; }

        public IPEndPoint RedirectPoint { get; private set; }

        private int m_maxClientCount;

        public SROServiceComponent(ServerServiceType serviceType, int serviceIndex) {
            this.ServiceType = serviceType;
            this.ServiceIndex = serviceIndex;
        }


        #region Functions

        public SROServiceComponent SetFingerprint(Fingerprint fingerprint) {
            this.Fingerprint = fingerprint;
            return this;
        }

        public SROServiceComponent SetEndPoint(IPEndPoint endpoint) {
            this.EndPoint = endpoint;
            return this;
        }

        public SROServiceComponent SetRedirectPoint(IPEndPoint redirectPoint) {
            this.RedirectPoint = redirectPoint;
            return this;
        }

        public SROServiceComponent SetMaxClientCount(int count) {
            this.m_maxClientCount = count;
            return this;
        }

        public void DOBind(Action<bool> callback = null) {
            new TCPServer(this).DOBind(callback);
        }

        #endregion

    }
}
