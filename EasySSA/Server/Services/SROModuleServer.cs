#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using EasySSA.Common;
using EasySSA.Core.Network;
using EasySSA.Packets;
using System;
using System.Net;

namespace EasySSA.Server.Services {
    public abstract class SROModuleServer {

        internal Action OnClientConnected;
        internal Func<Client, ClientDisconnectType> OnClientDisconnected;

        internal ServerServiceType ServerServiceType;
        internal int ServerIndex;

        internal int MaxClientCount;


        internal int CurrentClientCount;

        public SROModuleServer() {

        }

       



    }
}
