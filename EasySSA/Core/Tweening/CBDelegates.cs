#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace EasySSA.Core.Tweening {
    public delegate void TweenCallback();
    public delegate void TweenCallback<in T>(T value);

    public delegate T DOGetter<out T>();
    public delegate void DOSetter<in T>(T newValue);
}
