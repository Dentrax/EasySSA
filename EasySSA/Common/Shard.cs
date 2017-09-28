#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using EasySSA.SSA;

namespace EasySSA.Common {
    public struct Shard {
        public enum StatusType : byte {
            Online = 0,
            Check = 1
        }

        public ushort ID { get; private set; }

        public string Name { get; private set; }

        public ushort Players { get; private set; }

        public ushort Capacity { get; private set; }

        public byte GlobalOperationID { get; private set; }

        public StatusType Status { get; private set; }

        public Shard(Packet packet) {
            this.ID = packet.ReadUShort();
            this.Name = packet.ReadAscii();
            this.Players = packet.ReadUShort();
            this.Capacity = packet.ReadUShort();
            this.Status = (StatusType)packet.ReadByte();
            this.GlobalOperationID = packet.ReadByte();
        }
        
    }
}
