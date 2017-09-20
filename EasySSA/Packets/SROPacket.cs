#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System.Text;
using System.Linq;

using EasySSA.SSA;
using EasySSA.Server;
using System;

namespace EasySSA.Packets {
    public sealed class SROPacket : Packet, ISROPacket {

        private string m_packetID;

        private PacketSendType m_sendType = PacketSendType.UNKNOWN;

        private ServerServiceType m_serverType = ServerServiceType.UNKNOWN;

        private PacketSocketType m_socketType = PacketSocketType.UNKNOWN;

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

        public SROPacket(Packet rhs) : base(rhs) { }
        public SROPacket(string id, Packet packet) : base(packet) { this.m_packetID = id; }
        public SROPacket(ushort opcode) : base(opcode) { }
        public SROPacket(ushort opcode, bool encrypted) : base(opcode, encrypted) { }
        public SROPacket(ushort opcode, bool encrypted, bool massive) : base(opcode, encrypted, massive) { }
        public SROPacket(ushort opcode, bool encrypted, bool massive, byte[] bytes) : base(opcode, encrypted, massive, bytes) { }
        public SROPacket(ushort opcode, bool encrypted, bool massive, byte[] bytes, int offset, int length) : base(opcode, encrypted, massive, bytes, offset, length) { }

        public SROPacket(string id, Packet packet, PacketSendType sendType, ServerServiceType serverType, PacketSocketType socketType) : base(packet) {
            this.m_packetID = id;
            this.m_sendType = sendType;
            this.m_serverType = serverType;
            this.m_socketType = socketType;
        }

        public SROPacket(string id, ushort opcode, bool encrypted, bool massive, PacketSendType sendType, ServerServiceType serverType, PacketSocketType socketType) : base(opcode, encrypted, massive) {
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

        public string Dump() {
            StringBuilder sb = new StringBuilder();

            if(this.m_sendType == PacketSendType.REQUEST) {
                sb.Append(string.Format("[CLIENT->{0}]", this.m_serverType));
            } else if (this.m_sendType == PacketSendType.RESPONSE) {
                sb.Append(string.Format("[{0}->CLIENT]", this.m_serverType));
            } else {
                sb.Append("[?->?]");
            }
            
            sb.Append(string.Format("[{0}]", this.m_packetID));
            sb.Append(string.Format("[{0:X4}]", this.Opcode));
            //sb.Append(string.Format("[{0} bytes]", this.Length));

            if (this.Encrypted) sb.Append("[Encrypted]");
            if (this.Massive) sb.Append("[Massive]");


            return sb.ToString();
        }

        public override bool Equals(object obj) {
            if (obj == null || this.GetType() != obj.GetType()) {
                return false;
            }

            SROPacket other = obj as SROPacket;

            bool flag1 = this.Opcode == other.Opcode && this.Encrypted == other.Encrypted && this.Massive == other.Massive && this.GetBytes().SequenceEqual(other.GetBytes());
            bool flag2 = this.m_sendType == other.SendType && this.m_serverType == other.ServerServiceType && this.m_socketType == other.SocketType;

            return flag1 && flag2;
        }

        public override int GetHashCode() {
            unchecked {
                var result = 13;
                result = (result * 7) ^ m_sendType.GetHashCode();
                result = (result * 7) ^ m_serverType.GetHashCode();
                result = (result * 7) ^ m_socketType.GetHashCode();
                return result;
            }
        }

    }
}
