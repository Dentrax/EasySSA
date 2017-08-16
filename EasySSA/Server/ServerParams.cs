#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using EasySSA.Server;
using System;

namespace EasySSA.Server {
    public sealed class ServerParams {

        public static readonly ServerParams Params = new ServerParams();

        internal Action m_onClientConnected;
        internal Action m_onClientDisconnected;

        internal ServerType m_serverType;
        internal ServerType m_serverIndex;

        internal int m_maxClientCount;

        public ServerParams OnClientConnected(Action action) {
            this.m_onClientConnected = action;
            return this;
        }

        public ServerParams OnClientDisconnected(Action action) {
            this.m_onClientDisconnected = action;
            return this;
        }


        public ServerParams SetServerType(ServerType serverType) {
            this.m_serverType = serverType;
            return this;
        }

        public ServerParams SetMaxClientCount(int count) {
            this.m_maxClientCount = count;
            return this;
        }

    }
}
