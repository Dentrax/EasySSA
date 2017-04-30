#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using EasySSA.Packets.Messages;
using EasySSA.Packets.Messages.Client;

namespace EasySSA.Packets {
    public static class PacketDatabase {

        public static ushort GetOPCodeFrom(SROPacket packet) {
            return packet.Opcode;
        }

        public static SROPacket GetOPCodeTypeFrom(ushort opcode) {
            switch (opcode) {

                case (ushort)OPCode.Gateway.Request.LAUNCHER:
                    return new LoginPacket(opcode);

                case (ushort)OPCode.Gateway.Request.CHECKVERSION:
                    return new LoginPacket(opcode);


                default: return new UnknownPacket(opcode);
            }
        }


    }
}
