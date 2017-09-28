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
using EasySSA.Packets;
using System.Net.Sockets;

namespace Example2 {
    class Program {
        static void Main(string[] args) {
            InitConsole();
            new Thread(new ThreadStart(StartClient)).Start();
            Console.ReadLine();
        }

        private static void StartClient() {
            SROClientComponent clientComponent = new SROClientComponent(1)
                           .SetFingerprint(new Fingerprint("SR_Client", 0, SecurityFlags.Handshake & SecurityFlags.Blowfish & SecurityFlags.SecurityBytes, string.Empty))
                           .SetAccount(new Account("furkan", "1"), "Dentrax")
                           .SetCaptcha(string.Empty)
                           .SetVersionID(191)
                           .SetLocaleID(22)
                           .SetClientless(false)
                           .SetClientPath("D:\\_Coding-Corner_\\vSRO\\vSRO Client")
                           .SetLocalAgentEndPoint(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 25880))
                           .SetLocalGatewayEndPoint(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 25779))
                           .SetServiceEndPoint(new IPEndPoint(IPAddress.Parse("145.239.106.209"), 15779))
                           .SetBindTimeout(100)
                           .SetDebugMode(false);

            clientComponent.OnClientStatusChanged += new Action<SROClient, ClientStatusType>(delegate (SROClient client, ClientStatusType status) {

            });

            clientComponent.OnAccountStatusChanged += new Action<SROClient, AccountStatusType>(delegate (SROClient client, AccountStatusType status) {

            });

            clientComponent.OnCaptchaStatusChanged += new Action<SROClient, bool>(delegate (SROClient client, bool status) {

            });

            clientComponent.OnCharacterLogin += new Action<SROClient, bool>(delegate (SROClient client, bool started) {

            });

            clientComponent.OnLocalSocketStatusChanged += new Action<SocketError>(delegate (SocketError error) {
                if (error == SocketError.Success) {
                    Console.WriteLine("LOCAL socket bind SUCCESS! : " + clientComponent.LocalGatewayEndPoint.ToString());
                } else {
                    Console.WriteLine("LOCAL socket bind FAILED!  : " + error);
                }
            });

            clientComponent.OnServiceSocketStatusChanged += new Action<SROClient, SocketError>(delegate (SROClient client, SocketError error) {
                if (error == SocketError.Success) {
                    Console.WriteLine("SERVICE socket connect SUCCESS! : " + clientComponent.ServiceEndPoint.ToString());
                } else {
                    Console.WriteLine("SERVICE socket connect FAILED!  : " + error);
                }
            });

            clientComponent.OnSocketConnected += new Action<SROClient, bool>(delegate (SROClient client, bool connected) {
                Console.WriteLine("New client connected : " + client.Socket.RemoteEndPoint);
            });

            clientComponent.OnSocketDisconnected += new Action<SROClient, ClientDisconnectType>(delegate (SROClient client, ClientDisconnectType disconnectType) {
                Console.WriteLine("Client disconnected : " + client.IPAddress + " -- Reason : " + disconnectType);
            });

            clientComponent.OnPacketReceived += new Func<SROClient, SROPacket, PacketSocketType, PacketResult>(delegate (SROClient client, SROPacket packet, PacketSocketType socketType) {
                return new PacketResult(PacketOperationType.NOTHING);
            });

            clientComponent.DOBind(delegate (bool success, BindErrorType error) {
                if (success) {
                    Console.WriteLine("EasySSA bind SUCCESS");
                } else {
                    Console.WriteLine("EasySSA bind FAILED -- Reason : " + error);
                }
            });
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
