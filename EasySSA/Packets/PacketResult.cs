#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Collections.Generic;

using EasySSA.SSA;

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

        public sealed class PacketResponseResultInfo : PacketResultInfo {
            public List<Packet> Packets { get; private set; }
            public PacketResponseResultInfo(Packet packet) {
                this.Packets = new List<Packet>();
                this.Packets.Add(packet);
            }
            public PacketResponseResultInfo(List<Packet> packets) {
                Packets = packets;
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
