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
    public abstract class SROPacket : Packet, ISROPacket {

        private PacketSendType m_sendType;

        private PacketServerType m_serverType;

        private PacketSocketType m_socketType;

        private PacketServerType m_incomingFrom;

        private PacketServerType m_outgoingTo;

        public PacketSendType SendType {
            get { return this.m_sendType; }
        }

        public PacketServerType ServerType {
            get { return this.m_serverType; }
        }

        public PacketSocketType SocketType {
            get { return this.m_socketType; }
        }

        public PacketServerType IncomingFrom => throw new NotImplementedException();
        public PacketServerType OutgoingTo => throw new NotImplementedException();


        public SROPacket(Packet rhs) : base(rhs) { }
        public SROPacket(ushort opcode) : base(opcode) { }
        public SROPacket(ushort opcode, bool encrypted) : base(opcode, encrypted) { }
        public SROPacket(ushort opcode, bool encrypted, bool massive) : base(opcode, encrypted, massive) { }
        public SROPacket(ushort opcode, bool encrypted, bool massive, byte[] bytes) : base(opcode, encrypted, massive, bytes) { }
        public SROPacket(ushort opcode, bool encrypted, bool massive, byte[] bytes, int offset, int length) : base(opcode, encrypted, massive, bytes, offset, length) { }

        public SROPacket(ushort opcode, bool encrypted, PacketSendType sendType, PacketServerType serverType, PacketSocketType socketType) : base(opcode, encrypted) {
            this.m_sendType = sendType;
            this.m_serverType = serverType;
            this.m_socketType = socketType;
        }


     
    }
}
