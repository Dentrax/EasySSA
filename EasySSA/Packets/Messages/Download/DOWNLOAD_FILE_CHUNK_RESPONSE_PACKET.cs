using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySilkroadSecurityApi.Packets.Messages.Download {

    public sealed class DOWNLOAD_FILE_CHUNK_RESPONSE_PACKET : SROPacket {
        public DOWNLOAD_FILE_CHUNK_RESPONSE_PACKET(ushort opcode, bool encrypted, PacketSendType sendType, PacketServerType serverType, PacketSocketType socketType) : base(opcode, encrypted, sendType, serverType, socketType) {
        }
    }
}
