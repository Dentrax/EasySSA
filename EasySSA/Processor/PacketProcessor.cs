#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using EasySilkroadSecurityApi.Packets;
using EasySilkroadSecurityApi.SSA;

namespace EasySilkroadSecurityApi.Processor {
    public abstract class PacketProcessor {

        void test() {
            Packet p = new Packet(0x5000, false);

            SROPacket sro = PacketDatabase.GetPacketFrom(0x5000);


        }



    }
}
