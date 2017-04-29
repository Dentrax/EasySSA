#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using EasySSA.SSA;

namespace EasySSA.Packets.Messages.Client {
    public sealed class LoginPacket : Packet {

        public LoginPacket(ushort opcode) : base(opcode) {

        }

    }
}
