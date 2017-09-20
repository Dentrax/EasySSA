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
using System.Net.Sockets;

using EasySSA.Common;
using EasySSA.Packets;
using EasySSA.Server;
using EasySSA.Services;
using EasySSA.SSA;
using EasySSA.Core.Network.Securities;
using System.Reflection;

namespace Example1 {
    class Program {
        static void Main(string[] args) {
            InitConsole();

            SROServiceComponent gateway = new SROServiceComponent(ServerServiceType.GATEWAY, 1)
                                        .SetFingerprint(new Fingerprint("SR_Client", 0, SecurityFlags.Handshake & SecurityFlags.Blowfish & SecurityFlags.SecurityBytes, ""))
                                        .SetLocalEndPoint(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 15779))
                                        .SetLocalBindTimeout(10)
                                        .SetServiceEndPoint(new IPEndPoint(IPAddress.Parse("37.187.151.145"), 15779))
                                        .SetServiceBindTimeout(100)
                                        .SetMaxClientCount(500)
                                        .SetDebugMode(true);

            gateway.OnLocalSocketStatusChanged += new Action<SocketError>(delegate (SocketError error) {
                if(error == SocketError.Success) {
                    Console.WriteLine("LOCAL socket bind SUCCESS! : " + gateway.LocalEndPoint.ToString());
                } else {
                    Console.WriteLine("LOCAL socket bind FAILED!  : " + error);
                }
            });

            gateway.OnServiceSocketStatusChanged += new Action<Client, SocketError>(delegate (Client client, SocketError error) {
                if (error == SocketError.Success) {
                    Console.WriteLine("SERVICE socket connect SUCCESS! : " + gateway.ServiceEndPoint.ToString());
                } else { 
                    Console.WriteLine("SERVICE socket connect FAILED!  : " + error);
                }
            });

            gateway.OnClientConnected += new Func<Client, bool> (delegate (Client client) {

                Console.WriteLine("New client connected : " + client.Socket.RemoteEndPoint);

                return true;
            });

            gateway.OnClientDisconnected += new Action<Client, ClientDisconnectType>(delegate (Client client, ClientDisconnectType disconnectType) {

                Console.WriteLine("Client disconnected : " + client.IPAddress + " -- Reason : " + disconnectType);

            });

            gateway.OnPacketReceived += new Func<Client, SROPacket, PacketSocketType, PacketResult>(delegate (Client client, SROPacket packet, PacketSocketType socketType) {

                //SROPacket p = PacketDatabase.GetPacketFrom(packet, socketType);
                //p.Lock();
                //Console.WriteLine(p.Dump());

                Console.WriteLine(packet.Dump());

                return new PacketResult(PacketOperationType.NOTHING);
            });

            gateway.DOBind(delegate (bool success, BindErrorType error) {
                if (success) {
                    Console.WriteLine("EasySSA bind SUCCESS");
                } else {
                    Console.WriteLine("EasySSA bind FAILED -- Reason : " + error);
                }
            });

            Console.ReadLine();

        }

        private static void InitConsole() {
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
