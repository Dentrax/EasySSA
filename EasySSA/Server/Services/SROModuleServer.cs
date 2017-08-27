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


namespace EasySSA.Server.Services {
    public abstract class SROModuleServer {

        internal protected SROServiceComponent ServiceComponent;

        internal protected Client Client;

        protected Socket m_clientSocket;
        protected Socket m_serviceSocket;

        protected Security m_localSecurity;
        protected Security m_serviceSecurity;

        protected List<Packet> m_localRecevivePackets;
        protected List<Packet> m_serviceRecevivePackets;

        private List<KeyValuePair<TransferBuffer, Packet>> m_localSendPackets;
        private List<KeyValuePair<TransferBuffer, Packet>> m_serviceSendPackets;

        internal protected readonly object LOCK = new object();

        public SROModuleServer(Client client, SROServiceComponent serviceComponent) {
            this.ServiceComponent = serviceComponent;
            this.Client = client;
        }

        public void DOBind(Action<bool> callback) {

        }

    }
}
