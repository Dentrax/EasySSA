#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using EasySSA.Server;
using EasySSA.Server.Services;
using EasySSA.Common;

namespace EasySSA.Core.Tweening {
    public static class SSAExtensions {


        public static T SetServerType<T>(this T t, ServerType serverType) where T : SROModuleServer {
            if (t == null || !t.IsActive) return t;

            t.ServerType = serverType;
            return t;
        }

        public static T SetMaxClientCount<T>(this T t, int count) where T : SROModuleServer {
            if (t == null || !t.IsActive) return t;

            t.MaxClientCount = count;
            return t;
        }


        public static T OnClientConnected<T>(this T t, Func<Client, bool> confirmCallback, Action action) where T : SROModuleServer {
            if (t == null || !t.IsActive) return t;

            t.OnClientConnected = action;
            return t;
        }

        public static T OnClientDisconnected<T>(this T t, Func<Client, bool> clientCallback, Action<ClientDisconnectType> disconnectCallback) where T : SROModuleServer {
            if (t == null || !t.IsActive) return t;

            t.OnClientDisconnected = disconnectCallback;
            return t;
        }
    }
}
