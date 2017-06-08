#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using EasySSA.SSA;

namespace EasySSA.Core.Tweening {
    public sealed class TweenParams {

        public static readonly TweenParams Params = new TweenParams();

        internal object id;
        internal object target;

        internal TweenCallback<bool> onHostBind;
        internal TweenCallback<bool> onHostStop;
        internal TweenCallback<Packet> onReceiveFromClient;
        internal TweenCallback<Packet> onReceiveFromModule;
        internal TweenCallback onClientDisconnect;
        internal TweenCallback onModuleDisconnect;

        internal float delay;
        internal bool isRelative;

        public TweenParams() {
            Clear();
        }

        public TweenParams Clear() {
            id = null;
            target = null;

            onHostBind = null;
            onHostStop = null;
            onReceiveFromClient = null;
            onReceiveFromModule = null;
            onClientDisconnect = null;
            onModuleDisconnect = null;

            delay = 0;
            isRelative = false;

            return this;
        }

        public TweenParams SetID(object id) {
            this.id = id;
            return this;
        }

        public TweenParams SetTarget(object target) {
            this.target = target;
            return this;
        }

        public TweenParams OnHostBind(TweenCallback<bool> action) {
            this.onHostBind = action;
            return this;
        }

        public TweenParams OnHostStop(TweenCallback<bool> action) {
            this.onHostStop = action;
            return this;
        }

        public TweenParams OnReceiveFromClient(TweenCallback<Packet> action) {
            this.onReceiveFromClient = action;
            return this;
        }

        public TweenParams OnReceiveFromModule(TweenCallback<Packet> action) {
            this.onReceiveFromModule = action;
            return this;
        }

        public TweenParams OnClientDisconnect(TweenCallback action) {
            this.onClientDisconnect = action;
            return this;
        }

        public TweenParams OnModuleDisconnect(TweenCallback action) {
            this.onModuleDisconnect = action;
            return this;
        }

        public TweenParams SetDelay(float delay) {
            this.delay = delay;
            return this;
        }

        public TweenParams SetRelative(bool isRelative = true) {
            this.isRelative = isRelative;
            return this;
        }


    }
}
