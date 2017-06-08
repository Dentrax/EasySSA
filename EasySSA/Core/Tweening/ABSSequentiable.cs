#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace EasySSA.Core.Tweening {
    public abstract class ABSSequentiable {
        //internal TweenType tweenType;
        //internal float sequencedPosition; // position in Sequence
        //internal float sequencedEndPosition; // end position in Sequence

        /// <summary>Called the first time the tween is set in a playing state, after any eventual delay</summary>
        internal TweenCallback onStart; // Used also by SequenceCallback as main callback
    }
}
