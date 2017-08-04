#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace EasySilkroadSecurityApi {
    public sealed class EasySSASettings {

        public static bool USE_SAFE_MODE = true;

        public static bool USE_DEBUG_MODE = false;

        public const int DEFAULT_MAX_TWEENERS = 200;
        public const int DEFAULT_MAX_SEQUENCES = 50;

        public bool UseSafeMode = true;
        public bool DefaultRecyclable;
    }
}
