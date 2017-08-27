#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using EasySSA.Common;
using EasySSA.Services;
using System;
using System.Net;

namespace EasySSA.Server.Services {
    public sealed class AgentServer : SROModuleServer {

        public AgentServer(Client client, SROServiceComponent serviceComponent) : base(client, serviceComponent) {
        }

    }
}
