#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using EasySSA.Core.Tweening;
using EasySSA.Core.Tweening.Options;
using System.Collections.Generic;
using System.Net;

namespace EasySSA
{
    public sealed class EasySSA {

        public static readonly Version Version = new Version(1, 0, 0, 0);


        public const string TEXT_MAX_TWEENS_REACHED = "Max Tweens reached: capacity has automatically been increased from #0 to #1. Use DOTween.SetTweensCapacity to set it manually at startup";
        public const string TEXT_CANT_CHANGE_SEQUENCED_VALUES = "You cannot change the values of a tween contained inside a Sequence";

        internal static bool WasInitialized { get; private set; }

        internal static bool IsQuitting { get; private set; }

        internal static bool IsDebugBuild;

        internal static int maxActiveTweenersReached, maxActiveSequencesReached; 
        
        internal static EasySSAComponent instance;

        //EasySSA ssa = new EasySSA();
        //ssa.Init();
        //ssa.OnPacketReceived()

        //SROModuleServer agent = new AgentServer();
        //agent.DOBind(ip, port).On

        static void InitCheck() {
            if (WasInitialized || IsQuitting) return;

            AutoInit();
        }

        static void AutoInit() {
            EasySSASettings settings = new EasySSASettings();
            Init(settings, null, null, null);
        }

        public static IEasySSAInit Init(bool? recycleAllByDefault = null, bool? useSafeMode = null, LogBehaviour? logBehaviour = null) {
            if (WasInitialized) return instance;
            if (!Application.isPlaying || isQuitting) return null;

            DOTweenSettings settings = Resources.Load(DOTweenSettings.AssetName) as DOTweenSettings;
            return Init(settings, recycleAllByDefault, useSafeMode, logBehaviour);
        }

        static IEasySSAInit Init(EasySSASettings settings, bool? recycleAllByDefault, bool? useSafeMode, LogBehaviour? logBehaviour) {
            WasInitialized = true;
            // Options
            if (recycleAllByDefault != null) DOTween.defaultRecyclable = (bool)recycleAllByDefault;
            if (useSafeMode != null) DOTween.useSafeMode = (bool)useSafeMode;
            if (logBehaviour != null) DOTween.logBehaviour = (LogBehaviour)logBehaviour;
            // Gameobject - also assign instance
            DOTweenComponent.Create();
            // Assign settings
            if (settings != null) {
                if (useSafeMode == null) DOTween.useSafeMode = settings.useSafeMode;
                if (logBehaviour == null) DOTween.logBehaviour = settings.logBehaviour;
                if (recycleAllByDefault == null) DOTween.defaultRecyclable = settings.defaultRecyclable;
                DOTween.timeScale = settings.timeScale;
                DOTween.useSmoothDeltaTime = settings.useSmoothDeltaTime;
                DOTween.maxSmoothUnscaledTime = settings.maxSmoothUnscaledTime;
                DOTween.defaultRecyclable = recycleAllByDefault == null ? settings.defaultRecyclable : (bool)recycleAllByDefault;
                DOTween.showUnityEditorReport = settings.showUnityEditorReport;
                DOTween.drawGizmos = settings.drawGizmos;
                DOTween.defaultAutoPlay = settings.defaultAutoPlay;
                DOTween.defaultUpdateType = settings.defaultUpdateType;
                DOTween.defaultTimeScaleIndependent = settings.defaultTimeScaleIndependent;
                DOTween.defaultEaseType = settings.defaultEaseType;
                DOTween.defaultEaseOvershootOrAmplitude = settings.defaultEaseOvershootOrAmplitude;
                DOTween.defaultEasePeriod = settings.defaultEasePeriod;
                DOTween.defaultAutoKill = settings.defaultAutoKill;
                DOTween.defaultLoopType = settings.defaultLoopType;
            }
            // Log
            if (Debugger.logPriority >= 2) Debugger.Log("DOTween initialization (useSafeMode: " + DOTween.useSafeMode + ", recycling: " + (DOTween.defaultRecyclable ? "ON" : "OFF") + ", logBehaviour: " + DOTween.logBehaviour + ")");

            return instance;
        }

        public static void SetTweensCapacity(int tweenersCapacity, int sequencesCapacity) {
            TweenManager.SetCapacities(tweenersCapacity, sequencesCapacity);
        }

        /// <summary>
        /// Kills all tweens, clears all cached tween pools and plugins and resets the max Tweeners/Sequences capacities to the default values.
        /// </summary>
        /// <param name="destroy">If TRUE also destroys DOTween's gameObject and resets its initializiation, default settings and everything else
        /// (so that next time you use it it will need to be re-initialized)</param>
        public static void Clear(bool destroy = false) {
            TweenManager.PurgeAll();
            PluginsManager.PurgeAll();
            if (!destroy) return;

            initialized = false;
            useSafeMode = false;
            showUnityEditorReport = false;
            drawGizmos = true;
            timeScale = 1;
            useSmoothDeltaTime = false;
            logBehaviour = LogBehaviour.ErrorsOnly;
            defaultEaseType = Ease.OutQuad;
            defaultEaseOvershootOrAmplitude = 1.70158f;
            defaultEasePeriod = 0;
            defaultUpdateType = UpdateType.Normal;
            defaultTimeScaleIndependent = false;
            defaultAutoPlay = AutoPlay.All;
            defaultLoopType = LoopType.Restart;
            defaultAutoKill = true;
            defaultRecyclable = false;
            maxActiveTweenersReached = maxActiveSequencesReached = 0;

            DOTweenComponent.DestroyInstance();
        }

        public static void ClearCachedTweens() {
            TweenManager.PurgePools();
        }

        public static int Validate() {
            return TweenManager.Validate();
        }

        #region Global Info Getters

        public static bool IsTweening(object targetOrId, bool alsoCheckIfIsPlaying = false) {
            return TweenManager.FilteredOperation(OperationType.IsTweening, FilterType.TargetOrId, targetOrId, alsoCheckIfIsPlaying, 0) > 0;
        }

        public static int TotalPlayingTweens() {
            return TweenManager.TotalPlayingTweens();
        }

        public static List<Tween> PlayingTweens() {
            return TweenManager.GetActiveTweens(true);
        }

        public static List<Tween> PausedTweens() {
            return TweenManager.GetActiveTweens(false);
        }

        public static List<Tween> TweensById(object id, bool playingOnly = false) {
            if (id == null) return null;

            return TweenManager.GetTweensById(id, playingOnly);
        }

        public static List<Tween> TweensByTarget(object target, bool playingOnly = false) {
            return TweenManager.GetTweensByTarget(target, playingOnly);
        }

        #endregion

        public static TweenerCore<string, IPEndPoint, EndpointOptions> Bind(DOGetter<string> getter, DOSetter<string> setter, IPEndPoint host) {
            return ApplyTo<string, IPEndPoint, EndpointOptions>(getter, setter, host);
        }

        public static TweenerCore<T1, T2, TPlugOptions> To<T1, T2, TPlugOptions>(ABSTweenPlugin<T1, T2, TPlugOptions> plugin, DOGetter<T1> getter, DOSetter<T1> setter, T2 endValue) where TPlugOptions : struct, IPlugOptions {
            return ApplyTo(getter, setter, endValue, plugin);
        }

        static TweenerCore<T1, T2, TPlugOptions> ApplyTo<T1, T2, TPlugOptions>(DOGetter<T1> getter, DOSetter<T1> setter, T2 endValue, ABSTweenPlugin<T1, T2, TPlugOptions> plugin = null) where TPlugOptions : struct, IPlugOptions {
            InitCheck();
            TweenerCore<T1, T2, TPlugOptions> tweener = TweenManager.GetTweener<T1, T2, TPlugOptions>();
            if (!Tweener.Setup(tweener, getter, setter, endValue, plugin)) {
                TweenManager.Despawn(tweener);
                return null;
            }
            return tweener;
        }

    }
}
