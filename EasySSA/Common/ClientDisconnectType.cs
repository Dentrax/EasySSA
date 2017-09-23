#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace EasySSA.Common {
    public enum ClientDisconnectType {
        MAX_CONNECTION_REACHED,
        TIMEOUT,
        DISPOSED,

        PACKET_OPERATION_DISCONNECT,

        SERVICE_SOCKET_NULL,

        ONRECV_FROM_CLIENT,
        ONRECV_FROM_SERVICE,

        ONRECV_FROM_CLIENT_SIZE_ZERO,
        ONRECV_FROM_SERVICE_SIZE_ZERO,

        DORECV_FROM_CLIENT,
        DORECV_FROM_SERVICE,

        SENDTO_CLIENT_BEGINSEND,
        SENDTO_CLIENT_ENDSEND,

        SENDTO_SERVICE_BEGINSEND,
        SENDTO_SERVICE_ENDSEND,

        TRANSFERTO_CLIENT_OUTGOING,
        TRANSFERTO_SERVICE_OUTGOING,

        MANUALLY
    }
}
