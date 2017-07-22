#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;

namespace EasySilkroadSecurityApi.Core.Tweening {
    public abstract class Tween : ABSSequentiable {

        public object ID;
        public object target; 

        internal TweenCallback onPlay;
        internal TweenCallback onPause;
        internal TweenCallback onRewind;
        internal TweenCallback onUpdate;
        internal TweenCallback onStepComplete;
        internal TweenCallback onComplete;
        internal TweenCallback onKill;
        internal TweenCallback<int> onWaypointChange;

        internal bool isBlendable;
        internal bool isRecyclable;
        internal bool autoKill;
        internal float duration;
        internal int loops;

        internal float delay;
        internal bool isRelative;


        internal Type typeofT1; // Only used by Tweeners
        internal Type typeofT2; // Only used by Tweeners
        internal Type typeofTPlugOptions; // Only used by Tweeners
        internal bool active; // FALSE when tween is (or should be) despawned - set only by TweenManager
        internal bool isSequenced; // Set by Sequence when adding a Tween to it
        internal Sequence sequenceParent;  // Set by Sequence when adding a Tween to it
        internal int activeId = -1; // Index inside its active list (touched only by TweenManager)
        internal SpecialStartupMode specialStartupMode;

        // PLAY DATA /////////////////////////////////////////////////

        /// <summary>Gets and sets the time position (loops included, delays excluded) of the tween</summary>
        public float fullPosition { get { return this.Elapsed(true); } set { this.Goto(value, this.isPlaying); } }

        internal bool creationLocked; // TRUE after the tween was updated the first time (even if it was delayed), or when added to a Sequence
        internal bool startupDone; // TRUE the first time the actual tween starts, AFTER any delay has elapsed (unless it's a FROM tween)
        internal bool playedOnce; // TRUE after the tween was set in a play state at least once, AFTER any delay is elapsed
        internal float position; // Time position within a single loop cycle
        internal float fullDuration; // Total duration loops included
        internal int completedLoops;
        internal bool isPlaying; // Set by TweenManager when getting a new tween
        internal bool isComplete;
        internal float elapsedDelay; // Amount of eventual delay elapsed (shared by Sequences only for compatibility reasons, otherwise not used)
        internal bool delayComplete = true; // TRUE when the delay has elapsed or isn't set, also set by Delay extension method (shared by Sequences only for compatibility reasons, otherwise not used)

        internal int miscInt = -1; // Used by some plugins to store data (currently only by Paths to store current waypoint index)

        internal virtual void Reset() {
            //timeScale = 1;
            isBackwards = false;
            id = null;
            isIndependentUpdate = false;
            onStart = onPlay = onRewind = onUpdate = onComplete = onStepComplete = onKill = null;
            onWaypointChange = null;

            target = null;
            isFrom = false;
            isBlendable = false;
            isSpeedBased = false;
            duration = 0;
            loops = 1;
            delay = 0;
            isRelative = false;
            customEase = null;
            isSequenced = false;
            sequenceParent = null;
            specialStartupMode = SpecialStartupMode.None;
            creationLocked = startupDone = playedOnce = false;
            position = fullDuration = completedLoops = 0;
            isPlaying = isComplete = false;
            elapsedDelay = 0;
            delayComplete = true;

            miscInt = -1;

            // The following are set during a tween's Setup
            //            isRecyclable = DOTween.defaultRecyclable;
            //            autoKill = DOTween.defaultAutoKill;
            //            loopType = DOTween.defaultLoopType;
            //            easeType = DOTween.defaultEaseType;
            //            easeOvershootOrAmplitude = DOTween.defaultEaseOvershootOrAmplitude;
            //            easePeriod = DOTween.defaultEasePeriod

            // The following are set during TweenManager.AddActiveTween
            // (so the previous updateType is still stored while removing tweens)
            //            updateType = UpdateType.Normal;
        }

        internal abstract bool Validate();
        internal virtual float UpdateDelay(float elapsed) { return 0; }
        internal abstract bool Startup();



        internal static bool OnTweenCallback(TweenCallback callback) {
            if (EasySSA.USE_SAFE_MODE) {
                try {
                    callback();
                } catch (Exception e) {
                    Console.WriteLine("[Tween::OnTweenCallback()] -> An error inside a tween callback was silently taken care of > " + e.Message + "\n\n" + e.StackTrace + "\n\n");
                    return false;
                }
            } else callback();
            return true;
        }
        internal static bool OnTweenCallback<T>(TweenCallback<T> callback, T param) {
            if (EasySSA.USE_SAFE_MODE) {
                try {
                    callback(param);
                } catch (Exception e) {
                    Console.WriteLine("[Tween::OnTweenCallback(T)] -> An error inside a tween callback was silently taken care of > " + e.Message);
                    return false;
                }
            } else callback(param);
            return true;
        }

    }
}
