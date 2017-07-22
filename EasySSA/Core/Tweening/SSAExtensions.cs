#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System.Net;
using EasySSA.Server.Services;

namespace EasySSA.Core.Tweening {
    public static class SSAExtensions {

        public static Tweener DOBind(this SROModuleServer target, IPEndPoint host, bool snapping = false) {
            return EasySSA.To(() => target.position, x => target.position = x, endValue, duration).SetOptions(snapping).SetTarget(target);

        }

        #region Operation Shortcuts

        public static int DOComplete(this Component target, bool withCallbacks = false) {
            return DOTween.Complete(target, withCallbacks);
        }

        public static int DOComplete(this Material target, bool withCallbacks = false) {
            return DOTween.Complete(target, withCallbacks);
        }

        public static int DOKill(this Component target, bool complete = false) {
            //            int tot = complete ? DOTween.CompleteAndReturnKilledTot(target) : 0;
            //            return tot + DOTween.Kill(target);
            return DOTween.Kill(target, complete);
        }

        public static int DOKill(this Material target, bool complete = false) {
            return DOTween.Kill(target, complete);
        }

        public static int DOGoto(this Component target, float to, bool andPlay = false) {
            return DOTween.Goto(target, to, andPlay);
        }

        public static int DOGoto(this Material target, float to, bool andPlay = false) {
            return DOTween.Goto(target, to, andPlay);
        }

        public static int DOPause(this Component target) {
            return DOTween.Pause(target);
        }

        public static int DOPause(this Material target) {
            return DOTween.Pause(target);
        }

        public static int DOPlay(this Component target) {
            return DOTween.Play(target);
        }

        public static int DOPlay(this Material target) {
            return DOTween.Play(target);
        }

        public static int DOPlayBackwards(this Component target) {
            return DOTween.PlayBackwards(target);
        }

        public static int DOPlayBackwards(this Material target) {
            return DOTween.PlayBackwards(target);
        }

        public static int DOPlayForward(this Component target) {
            return DOTween.PlayForward(target);
        }

        public static int DOPlayForward(this Material target) {
            return DOTween.PlayForward(target);
        }
        public static int DORestart(this Component target, bool includeDelay = true) {
            return DOTween.Restart(target, includeDelay);
        }
        public static int DORestart(this Material target, bool includeDelay = true) {
            return DOTween.Restart(target, includeDelay);
        }
        public static int DORewind(this Component target, bool includeDelay = true) {
            return DOTween.Rewind(target, includeDelay);
        }
        public static int DORewind(this Material target, bool includeDelay = true) {
            return DOTween.Rewind(target, includeDelay);
        }
        public static int DOSmoothRewind(this Component target) {
            return DOTween.SmoothRewind(target);
        }
        public static int DOSmoothRewind(this Material target) {
            return DOTween.SmoothRewind(target);
        }
        public static int DOTogglePause(this Component target) {
            return DOTween.TogglePause(target);
        }
        public static int DOTogglePause(this Material target) {
            return DOTween.TogglePause(target);
        }

        #endregion
    }
}
