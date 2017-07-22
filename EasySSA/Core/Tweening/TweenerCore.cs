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
    public class TweenerCore<T1, T2, TPlugOptions> : Tweener where TPlugOptions : struct, IPlugOptions {
        public T2 startValue, endValue, changeValue;
        public TPlugOptions plugOptions;
        public DOGetter<T1> getter;
        public DOSetter<T1> setter;
        internal ABSTweenPlugin<T1, T2, TPlugOptions> tweenPlugin;

        internal sealed override void Reset() {
            base.Reset();

            if (tweenPlugin != null) tweenPlugin.Reset(this);
            //            plugOptions = new TPlugOptions(); // Generates GC because converts to an Activator.CreateInstance
            //            plugOptions = Utils.InstanceCreator<TPlugOptions>.Create(); // Fixes GC allocation using workaround (doesn't work with IL2CPP)
            plugOptions.Reset();
            getter = null;
            setter = null;
            hasManuallySetStartValue = false;
            isFromAllowed = true;
        }

        internal override bool Startup() {
            throw new NotImplementedException();
        }

        // Called by TweenManager.Validate.
        // Returns TRUE if the tween is valid
        internal override bool Validate() {
            try {
                getter();
            } catch {
                return false;
            }
            return true;
        }


    }
}
