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
    public sealed class SROServiceComponent : IDisposable {

        public SROServiceContext ServiceServer { get; private set; }

        public ServerServiceType ServiceType { get; private set; }

        public int ServiceIndex { get; private set; }

        public Action<SocketError> OnLocalSocketStatusChanged;

        public Action<Client, SocketError> OnServiceSocketStatusChanged;


        public Func<Client, bool> OnClientConnected;

        public Action<Client, ClientDisconnectType> OnClientDisconnected;

        public Func<Client, SROPacket, PacketSocketType, PacketResult> OnPacketReceived;
       

        public Fingerprint Fingerprint { get; private set; }

        public IPEndPoint LocalEndPoint { get; private set; }

        public IPEndPoint ServiceEndPoint { get; private set; }

        public int MaxClientCount { get; private set; }

        public int LocalBindTimeout { get; private set; }

        public int ServiceBindTimeout { get; private set; }

        public bool IsDebugMode { get; private set; }

        private bool m_wasDisposed;

        public SROServiceComponent(ServerServiceType serviceType, int serviceIndex) {
            this.ServiceType = serviceType;
            this.ServiceIndex = serviceIndex;
            this.MaxClientCount = 10;
            this.LocalBindTimeout = 10;
            this.ServiceBindTimeout = 10;
            this.IsDebugMode = false;
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

        public SROServiceComponent SetDebugMode(bool debug) {
            this.IsDebugMode = debug;
            return this;
        }

        public void DOBind(Action<bool, BindErrorType> callback = null) {
            bool flag = true;

            if (this.m_wasDisposed) {
                callback(false, BindErrorType.COMPONENT_DISPOSED);
                flag = false;
                return;
            }

            if(this.LocalEndPoint == null) {
                callback(false, BindErrorType.COMPONENT_LOCAL_ENDPOINT_NULL);
                flag = false;
            }

            if (this.ServiceEndPoint == null) {
                callback(false, BindErrorType.COMPONENT_SERVICE_ENDPOINT_NULL);
                flag = false;
            }

            if(this.ServiceIndex <= 0) {
                callback(false, BindErrorType.COMPONENT_SERVICE_INDEX_NULL_OR_ZERO);
                flag = false;
            }

            if (this.LocalBindTimeout <= 0) {
                callback(false, BindErrorType.COMPONENT_LOCAL_BIND_TIMEOUT_NULL_OR_ZERO);
                flag = false;
            }

            if (this.ServiceBindTimeout <= 0) {
                callback(false, BindErrorType.COMPONENT_SERVICE_BIND_TIMEOUT_NULL_OR_ZERO);
                flag = false;
            }

            if (this.MaxClientCount <= 0) {
                callback(false, BindErrorType.COMPONENT_SERVICE_CLIENT_COUNT_NULL_OR_ZERO);
                flag = false;
            }

            if (this.Fingerprint == null) {
                callback(false, BindErrorType.COMPONENT_FINGERPRINT_NULL);
                flag = false;
            }

            if (flag) {
                new TCPServer(this).DOBind(callback);
            }
           
        }

        public void Dispose() {
            this.Dispose();
        }

        private void Dispose(bool disposing) {
            if (!m_wasDisposed) {
                if (disposing) {
                    //
                }
                this.ServiceServer.Dispose();

                m_wasDisposed = true;
            }
            
        }

        #endregion

    }
}
