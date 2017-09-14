#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace EasySSA.Server {
    public enum BindErrorType {
        SUCCESS,

        COMPONENT_DISPOSED,

        COMPONENT_FINGERPRINT_NULL,

        COMPONENT_LOCAL_ENDPOINT_NULL,
        COMPONENT_SERVICE_ENDPOINT_NULL,

        COMPONENT_LOCAL_BIND_TIMEOUT_NULL_OR_ZERO,
        COMPONENT_SERVICE_BIND_TIMEOUT_NULL_OR_ZERO,

        COMPONENT_SERVICE_INDEX_NULL_OR_ZERO,

        COMPONENT_SERVICE_CLIENT_COUNT_NULL_OR_ZERO,

        SERVER_BIND_SOCKET_NOT_NULL,
        SERVER_BIND_SOCKET_ALREADY_ACTIVE,

        SERVER_BIND_SOCKET_NON_AVAILABLE,

        SERVER_BIND_ARGUMENT_NULL_EXCEPTION,
        SERVER_BIND_SOCKET_EXCEPTION,
        SERVER_BIND_SECURITY_EXCEPTION,
        SERVER_BIND_OBJECT_DISPOSED_EXCEPTION,

        UNKNOWN
    }
}
