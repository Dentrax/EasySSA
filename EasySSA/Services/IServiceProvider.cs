#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using EasySilkroadSecurityApi.Common;

namespace EasySilkroadSecurityApi.Services {
    public interface IServiceProvider : IProvider {
        bool IsStarted { get; }
        void Initialize(ServiceProvider.InitializeContextBase context);
        void Terminate();
        void Start();
        void Stop();
    }
}
