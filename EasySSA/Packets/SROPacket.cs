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
using EasySSA.Server;

namespace EasySSA.Packets {
    public sealed class SROPacket : Packet, ISROPacket {

        private string m_packetID;

        private PacketSendType m_sendType = PacketSendType.UNKNOWN;

        private ServerServiceType m_serverType = ServerServiceType.UNKNOWN;

        private PacketSocketType m_socketType = PacketSocketType.UNKNOWN;

        private ServerServiceType m_incomingFrom = ServerServiceType.UNKNOWN;

        private ServerServiceType m_outgoingTo = ServerServiceType.UNKNOWN;

        public string PacketID {
            get { return this.m_packetID; }
        }

        public PacketSendType SendType {
            get { return this.m_sendType; }
        }

        public ServerServiceType ServerServiceType {
            get { return this.m_serverType; }
        }

        public PacketSocketType SocketType {
            get { return this.m_socketType; }
        }

        public ServerServiceType IncomingFrom {
            get { return this.m_incomingFrom; }
        }
        public ServerServiceType OutgoingTo {
            get { return this.m_outgoingTo; }
        }

        public SROPacket(Packet rhs) : base(rhs) { }
        public SROPacket(ushort opcode) : base(opcode) { }
        public SROPacket(ushort opcode, bool encrypted) : base(opcode, encrypted) { }
        public SROPacket(ushort opcode, bool encrypted, bool massive) : base(opcode, encrypted, massive) { }
        public SROPacket(ushort opcode, bool encrypted, bool massive, byte[] bytes) : base(opcode, encrypted, massive, bytes) { }
        public SROPacket(ushort opcode, bool encrypted, bool massive, byte[] bytes, int offset, int length) : base(opcode, encrypted, massive, bytes, offset, length) { }

        public SROPacket(string id, ushort opcode, bool encrypted, PacketSendType sendType, ServerServiceType serverType, PacketSocketType socketType) : base(opcode, encrypted) {
            this.m_packetID = id;
            this.m_sendType = sendType;
            this.m_serverType = serverType;
            this.m_socketType = socketType;
        }

        public void Create(PacketSendType sendType, ServerServiceType serverType, PacketSocketType socketType){
            this.m_sendType = sendType;
            this.m_serverType = serverType;
            this.m_socketType = socketType;
        }

    }
}
