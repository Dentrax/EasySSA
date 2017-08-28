#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using EasySSA.Server.Services;
using EasySSA.Server;

namespace EasySSA.Services.ServerService {
    public sealed class ServerProvider : ServiceProvider, IServerProvider {

        public sealed new class InitializeContext : ServiceProvider.InitializeContextBase {

            public InitializeContext() : base() {

            }
        }


        public ServerProvider() {
        }

        ~ServerProvider() { }


        public override void Initialize(InitializeContextBase context) {
            if (!this.WasInitialized) {

                this.WasInitialized = true;
            }
        }

        public override void Terminate() {
            if (this.WasInitialized) {

                GC.SuppressFinalize(this);
                this.WasInitialized = false;
            }
        }

        public override void Start() {
            throw new NotImplementedException();
        }

        public override void Stop() {
            throw new NotImplementedException();
        }


        public void AddModuleServer(SROServiceContext moduleServer) {

        }

    }
}
