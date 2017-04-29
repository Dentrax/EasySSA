#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace EasySSA.Server {
    public enum ServerServiceType {
        GATEWAY,
        AGENT,
        SR_GAME,
        DOWNLOAD_MANAGER,
        SR_SHARD_MANAGER,
        GLOBAL_MANAGER,
        FARM_MANAGER,
        MACHINE_MANAGER,
        NONE
    }
}
