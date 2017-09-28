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
    public struct Character {
        public string Name { get; private set; }

        public byte Level { get; private set; }

        public Character(Packet packet) {
            packet.ReadUInt(); //Model
            this.Name = packet.ReadAscii();
            packet.ReadByte(); //Volume
            this.Level = packet.ReadByte(); //Level
            packet.ReadULong(); //EXP
            packet.ReadUShort(); //STR
            packet.ReadUShort(); //INT
            packet.ReadUShort(); //STAT
            packet.ReadUInt(); //HP
            packet.ReadUInt(); //MP
            var restoreFlag = packet.ReadBool();
            if (restoreFlag)
                packet.ReadUInt(); //Delete Time

            packet.ReadByte(); //Something
            packet.ReadByte(); //with
            packet.ReadByte(); //Guild,Trader etc...

            var itemCount = packet.ReadByte();
            for (int i = 0; i < itemCount; i++) {
                packet.ReadUInt();
                packet.ReadByte();
            }

            var avatarCount = packet.ReadByte();
            for (int i = 0; i < avatarCount; i++) {
                packet.ReadUInt();
                packet.ReadByte();
            }
        }
    }
}
