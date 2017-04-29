#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using EasySSA.SSA;

namespace EasySSA.Packets {
    public abstract class FixedPacket : Packet, IExploitProcessor, IExploitFixer {
        public FixedPacket(ushort opcode) : base(opcode) {

        }

        public abstract void FoundExploits();

        public abstract void ProcessExploits();

        public abstract void FixExploits();
        
    }
}
