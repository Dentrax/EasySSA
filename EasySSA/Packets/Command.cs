#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using EasySSA.SSA;
using System.Collections.Generic;

namespace EasySSA.Packets {
    public abstract class Command {

        private Security m_security;

        private readonly object m_locker = new object();

        public const int MaxEmbeddedDepth = 10;

        public int Depth { get; set; }

        public virtual byte[] Encode() => new List<byte>().ToArray();

        protected Security GetSecurity() {
            return this.m_security;
        }

        public Command(Security security) {
            this.m_security = security;
        }

        public abstract Packet CreatePacket();

        public virtual void Execute(Packet packet) {
            lock (this.m_locker) {
                this.m_security.Send(packet);
            }
        }
    }
}
