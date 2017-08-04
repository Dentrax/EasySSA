﻿#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System.Net;

namespace EasySilkroadSecurityApi.Core.Tweening.Options {
    public struct EndpointOptions : IPlugOptions {

        public IPEndPoint IPEndPoint;

        public void Reset() {
            IPEndPoint = default(IPEndPoint);
        }
    }
}