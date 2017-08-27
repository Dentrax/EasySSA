#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using EasySSA.Server;
using EasySSA.SSA;
using System;
using System.Net;

namespace EasySSA
{
    public sealed class EasySSA {

        public static readonly Version Version = new Version(1, 0, 0, 0);

        internal static bool WasInitialized { get; private set; }

        internal static bool IsQuitting { get; private set; }

        internal static bool IsDebugBuild;

        
        internal static EasySSAComponent s_instance;

        static EasySSA() {
#if DEBUG
            IsDebugBuild = true;
#endif
        }

        //EasySSA ssa = new EasySSA();
        //ssa.Init();

        //ssa.AddServer(ServerType.Gateway, 1)
        //   .SetFingerprint("f")
        //   .SetEndPoint("ip", "port")
        //   .SetRedirect("ip", "port")
        //   .SetMaxClientCount(500)
        //   .OnClientConnect()
        //   .OnClientDisconnect()
        //   .OnPacketReceived()
        //   ...

        //ssa.AddServer(ServerType.Agent, 1)
        //   .SetFingerprint("f")
        //   .SetEndPoint("ip", "port")
        //   .SetRedirect("ip", "port")
        //   .SetMaxClientCount(500)
        //   .OnClientConnect()
        //   .OnClientDisconnect()
        //   .OnPacketReceived()
        //   ...

        //ssa.DOBind()

        #region Initializers

        static void InitCheck() {
            if (WasInitialized || IsQuitting) return;

            AutoInit();
        }

        static void AutoInit() {
            EasySSASettings settings = new EasySSASettings();
            Init(settings, false);
        }

        public static IEasySSAInit Init(bool useSafeMode) {
            if (WasInitialized) return s_instance;
            if (!IsQuitting) return null;

            EasySSASettings settings = new EasySSASettings();
            return Init(settings, useSafeMode);
        }

        static IEasySSAInit Init(EasySSASettings settings, bool useSafeMode) {
            WasInitialized = true;


            return s_instance;
        }

        #endregion

        //new TestServer().DOBind("ip", "port").SetMaxBindTries(3).SetMaxConnections(300).ONBind(delegate {...}).ONReceiveBytes(delegate {...}).ONSocketStatusChanged(delegate {...}).ONBlaBlaBla...

        public static void Clear(bool destroy = false) {
            if (!destroy) return;


        }


    }
}
