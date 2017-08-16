#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using EasySSA.SSA;
using EasySSA.Server;

namespace EasySSA.Packets {
    public static class PacketDatabase {

        public static ushort GetOPCodeFrom(SROPacket packet) {
            return packet.Opcode;
        }

        public static SROPacket GetPacketFrom(Packet rhs) {
            return PacketDatabase.GetPacketFrom(rhs.Opcode);
        }

        public static SROPacket GetPacketFrom(ushort opcode) {
            switch (opcode) {

                #region GLOBAL

                #region GLOBAL-REQUEST

                #endregion

                #region GLOBAL-RESPONSE

                #endregion

                #endregion

                #region DOWNLOAD

                #region DOWNLOAD-REQUEST
                case (ushort)OPCode.Download.Request.FILE_COMPLETE:
                    return new SROPacket("DOWNLOAD_FILE_COMPLETE_PACKET", opcode, false, PacketSendType.REQUEST, ServerServiceType.DOWNLOAD, PacketSocketType.CLIENT);
                case (ushort)OPCode.Download.Request.FILE_REQUEST:
                    return new SROPacket("DOWNLOAD_FILE_REQUEST_PACKET", opcode, false, PacketSendType.REQUEST, ServerServiceType.DOWNLOAD, PacketSocketType.CLIENT);
                #endregion

                #region DOWNLOAD-RESPONSE
                case (ushort)OPCode.Download.Response.FILE_CHUNK:
                    return new SROPacket("DOWNLOAD_FILE_CHUNK_RESPONSE_PACKET", opcode, false, PacketSendType.RESPONSE, ServerServiceType.DOWNLOAD, PacketSocketType.SERVER);
                #endregion

                #endregion
                 
                default: return new SROPacket(opcode);
            }
        }


    }
}
