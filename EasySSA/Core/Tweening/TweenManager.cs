#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using EasySilkroadSecurityApi.Core.Tweening.Options;

namespace EasySilkroadSecurityApi.Core.Tweening {

    internal static class TweenManager {
        internal static TweenerCore<T1, T2, TPlugOptions> GetTweener<T1, T2, TPlugOptions>() where TPlugOptions : struct, IPlugOptions {

            TweenerCore<T1, T2, TPlugOptions> tween;

            Type typeofT1 = typeof(T1);
            Type typeofT2 = typeof(T2);
            Type typeofTPlugOptions = typeof(TPlugOptions);


            throw new NotImplementedException();
        }

    }
}
