using EasySSA.SSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySSA.Packets {
    public sealed class PacketResult {
     
        public abstract class PacketResultInfo {

        }

        public sealed class PacketDisconnectResultInfo : PacketResultInfo {
            public string Notice { get; private set; }
            public Enum DisconnectReason { get; private set; }
            public PacketDisconnectResultInfo(string notice, Enum disconnectReason = null) {
                this.Notice = notice;
                this.DisconnectReason = disconnectReason;
            }
        }

        public sealed class PacketReplaceResultInfo : PacketResultInfo {
            public Packet Packet { get; private set; }
            public List<Packet> ReplaceWith { get; private set; }
            public PacketReplaceResultInfo(Packet packet, List<Packet> replaceWith) {
                Packet = packet;
                ReplaceWith = replaceWith;
            }
        }

        public sealed class PacketInjectResultInfo : PacketResultInfo {
            public Packet Packet { get; private set; }
            public List<Packet> InjectWith { get; private set; }
            public bool AfterPacket { get; private set; }
            public PacketInjectResultInfo(Packet packet, List<Packet> replaceWith, bool afterPacket) {
                Packet = packet;
                InjectWith = replaceWith;
                this.AfterPacket = afterPacket;
            }
        }

        public PacketResultInfo ResultInfo { get; private set; }

        public PacketOperationType ResultType { get; private set; }

        public PacketResult(PacketOperationType resultType) {
            ResultInfo = null;
            ResultType = resultType;
        }

        public PacketResult(PacketOperationType resultType, PacketResultInfo resultInfo) {
            ResultInfo = resultInfo;
            ResultType = resultType;
        }


    }
}
