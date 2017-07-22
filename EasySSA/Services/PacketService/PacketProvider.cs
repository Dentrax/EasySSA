#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;

namespace EasySilkroadSecurityApi.Services.PacketService {
    public sealed class PacketProvider : ServiceProvider, IPacketProvider {
        public sealed class InitializeContext : ServiceProvider.InitializeContextBase {

            public InitializeContext() : base() {

            }
        }

        public PacketProvider() {
        }

        ~PacketProvider() { }


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
    }
}
