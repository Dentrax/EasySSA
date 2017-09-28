#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Text;
using System.Linq;

using EasySSA.SSA;
using EasySSA.Common;

namespace EasySSA.Packets {
    public sealed class SROPacket : Packet, ISROPacket, IEquatable<Packet>, IEquatable<SROPacket> {

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

        public string Dump(bool setLock = false) {
            StringBuilder sb = new StringBuilder();

            if (this.m_sendType == PacketSendType.REQUEST) {
                sb.Append(string.Format("[CLIENT->{0}]", this.m_serverType));
            } else if (this.m_sendType == PacketSendType.RESPONSE) {
                sb.Append(string.Format("[{0}->CLIENT]", this.m_serverType));
            } else {
                sb.Append("[?->?]");
            }

            sb.Append("\t\t");

            sb.Append(string.Format("[{0}]", this.m_packetID));

            sb.Append("\t\t");

            sb.Append(string.Format("[{0:X4}]", this.Opcode));

            if (setLock) {
                this.Lock();
                sb.Append("\t\t");
                sb.Append(string.Format("[{0} bytes]", this.Length));
            }


            if (this.Encrypted) {
                sb.Append("\t\t");
                sb.Append("[Encrypted]");
            }

            if (this.Massive) {
                sb.Append("\t\t");
                sb.Append("[Massive]");
            }

            //sb.AppendLine(Environment.NewLine);

            //sb.Append(HexDump(this.GetBytes()));

            return sb.ToString();
        }

        private string HexDump(byte[] buffer) {
            return HexDump(buffer, 0, buffer.Length);
        }

        private string HexDump(byte[] buffer, int offset, int count) {
            const int bytesPerLine = 16;
            StringBuilder output = new StringBuilder();
            StringBuilder ascii_output = new StringBuilder();
            int length = count;
            if (length % bytesPerLine != 0) {
                length += bytesPerLine - length % bytesPerLine;
            }
            for (int x = 0; x <= length; ++x) {
                if (x % bytesPerLine == 0) {
                    if (x > 0) {
                        output.AppendFormat("  {0}{1}", ascii_output.ToString(), Environment.NewLine);
                        ascii_output.Clear();
                    }
                    if (x != length) {
                        output.AppendFormat("{0:d10}   ", x);
                    }
                }
                if (x < count) {
                    output.AppendFormat("{0:X2} ", buffer[offset + x]);
                    char ch = (char)buffer[offset + x];
                    if (!Char.IsControl(ch)) {
                        ascii_output.AppendFormat("{0}", ch);
                    } else {
                        ascii_output.Append(".");
                    }
                } else {
                    output.Append("   ");
                    ascii_output.Append(".");
                }
            }
            return output.ToString();
        }

        public bool Equals(SROPacket obj) {
            if (obj == null || this.GetType() != obj.GetType()) {
                return false;
            }

            SROPacket other = obj as SROPacket;

            bool flag1 = this.Opcode == other.Opcode && this.Encrypted == other.Encrypted && this.Massive == other.Massive && this.GetBytes().SequenceEqual(other.GetBytes());
            bool flag2 = this.m_sendType == other.SendType && this.m_serverType == other.ServerServiceType && this.m_socketType == other.SocketType;

            return flag1 && flag2;
        }

        public bool Equals(Packet obj) {
            if (obj == null || this.GetType() != obj.GetType()) {
                return false;
            }

            Packet other = obj as Packet;

            bool flag1 = this.Opcode == other.Opcode && this.Encrypted == other.Encrypted && this.Massive == other.Massive && this.GetBytes().SequenceEqual(other.GetBytes());

            return flag1;
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
