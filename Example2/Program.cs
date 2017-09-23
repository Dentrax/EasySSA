#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Net;
using System.Reflection;
using System.Threading;

using EasySSA.SSA;
using EasySSA.Common;
using EasySSA.Component;
using EasySSA.Core.Network.Securities;

namespace Example2 {
    class Program {
        static void Main(string[] args) {
            InitConsole();
            new Thread(new ThreadStart(StartClient)).Start();
            Console.ReadLine();
        }

        private static void StartClient() {
            SROClientComponent gateway = new SROClientComponent(1)
                           .SetFingerprint(new Fingerprint("SR_Client", 0, SecurityFlags.Handshake & SecurityFlags.Blowfish & SecurityFlags.SecurityBytes, ""))
                           .SetAccount(new Account("furkan", "1"))
                           .SetCaptcha(string.Empty)
                           .SetClientPath("")
                           .SetLocalEndPoint(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 15779))
                           .SetGatewayEndPoint(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 15779))
                           .SetAgentEndPoint(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 15779))
                           .SetDownloadEndPoint(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 15779))
                           .SetBindTimeout(100)
                           .SetDebugMode(false);
        }

        private static void InitConsole() {
            Console.Clear();
            Console.WindowWidth = 150;
            Console.BufferHeight = 5000;

            Assembly assembly = Assembly.GetExecutingAssembly();
            string attributeTitle = assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
            string attributeVersion = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
            string attributeCopyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;

            Console.Title = string.Format("{0} {1}", attributeTitle, attributeVersion);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(attributeCopyright);
            Console.WriteLine();
            Console.ResetColor();

            Console.Beep();
        }
    }
}
