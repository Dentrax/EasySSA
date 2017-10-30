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
using System.Threading;
using System.Reflection;
using System.Net.Sockets;

using EasySSA.SSA;
using EasySSA.Common;
using EasySSA.Packets;
using EasySSA.Component;
using EasySSA.Core.Network.Securities;
using System.Collections.Generic;
using System.Security.Principal;
using System.Diagnostics;

namespace Example1 {
    class Program {

        public static bool HaveAdminRights => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        static void Main(string[] args) {
            InitConsole();
            new Thread(new ThreadStart(StartGateway)).Start();
            Console.ReadLine();
        }

        private static void StartGateway() {
            //Burak 145.239.106.209

            IPEndPoint service = new IPEndPoint(IPAddress.Parse("145.239.106.209"), 15779);

            SROServiceComponent gateway = new SROServiceComponent(ServerServiceType.GATEWAY, 1)
                            .SetFingerprint(new Fingerprint("SR_Client", 0, SecurityFlags.Handshake & SecurityFlags.Blowfish & SecurityFlags.SecurityBytes, ""))
                            .SetLocalEndPoint(new IPEndPoint(IPAddress.Parse("145.239.106.209"), 25779))
                            .SetLocalBindTimeout(10)
                            .SetServiceEndPoint(service)
                            .SetServiceBindTimeout(100)
                            .SetMaxClientCount(500)
                            .SetDebugMode(true);

            gateway.OnLocalSocketStatusChanged += new Action<SocketError>(delegate (SocketError error) {

                if (error == SocketError.Success) {
                    Console.WriteLine("LOCAL socket bind SUCCESS! : " + gateway.LocalEndPoint.ToString());
                } else {
                    Console.WriteLine("LOCAL socket bind FAILED!  : " + error);
                }

            });

            gateway.OnServiceSocketStatusChanged += new Action<SROClient, SocketError>(delegate (SROClient client, SocketError error) {

                if (error == SocketError.Success) {
                    Console.WriteLine("SERVICE socket connect SUCCESS! : " + gateway.ServiceEndPoint.ToString());
                } else {
                    Console.WriteLine("SERVICE socket connect FAILED!  : " + error);
                }

            });

            gateway.OnClientConnected += new Func<SROClient, bool>(delegate (SROClient client) {

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Client connection established : " + client.Socket.RemoteEndPoint);
                Console.ResetColor();

                //if (string.IsNullOrEmpty(client.IPAddress)) { //Example
                //    return false; //Decline client
                //} else {
                //    //Accept client
                //}

                return true;
            });

            gateway.OnClientDisconnected += new Action<SROClient, ClientDisconnectType>(delegate (SROClient client, ClientDisconnectType disconnectType) {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Client disconnected : " + client.IPAddress + " -- Reason : " + disconnectType);
                Console.ResetColor();

            });

            gateway.OnPacketReceived += new Func<SROClient, SROPacket, PacketSocketType, PacketResult>(delegate (SROClient client, SROPacket packet, PacketSocketType socketType) {

                DumpPacket(packet);

                //switch (packet.Opcode) {
                //    case 0x1111:
                //        return new PacketResult(PacketOperationType.DISCONNECT, new PacketResult.PacketDisconnectResultInfo("DC Reason : 0x5555 received"));

                //    case 0x2222:
                //        return new PacketResult(PacketOperationType.IGNORE);

                //    case 0x3333:
                //        return new PacketResult(PacketOperationType.INJECT, new PacketResult.PacketInjectResultInfo(packet, new List<Packet> { new Packet(0x3334), new Packet(0x3335) }, true));

                //    case 0x4444:
                //        return new PacketResult(PacketOperationType.REPLACE, new PacketResult.PacketReplaceResultInfo(packet, new List<Packet> { new Packet(0x4445) }));

                //    case 0x5555:
                //        return new PacketResult(PacketOperationType.RESPONSE, new PacketResult.PacketResponseResultInfo(new Packet(0x5556)));

                //    default:
                //        return new PacketResult(PacketOperationType.NOTHING);
                //}

                return new PacketResult(PacketOperationType.NOTHING);
            });

            gateway.DOBind(delegate (bool success, BindErrorType error) {

                if (success) {
                    Console.WriteLine("EasySSA bind SUCCESS");
                    Console.WriteLine("Waiting client connection on : " + service.ToString());
                } else {
                    Console.WriteLine("EasySSA bind FAILED -- Reason : " + error);
                }

            });
        }

        static int tableWidth = 140;
        static bool first = true;
        private static void FirstPacketGrid() {
            Console.WriteLine(new string('-', tableWidth));
            PrintRow(new string[] { "DIRECTION", "NAME", "OPCODE", "ENCRYPTED", "MASSIVE" });
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns) {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";
            foreach (string column in columns) {
                row += AlignCentre(column, width) + "|";
            }
            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width) {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;
            if (string.IsNullOrEmpty(text)) {
                return new string(' ', width);
            } else {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        private static void DumpPacket(SROPacket packet) {
            if (first) {
                FirstPacketGrid();
                first = false;
            }
            string direction;
            if (packet.SendType == PacketSendType.REQUEST) {
                direction = string.Format("[CLIENT->{0}]", packet.ServerServiceType);
            } else if (packet.SendType == PacketSendType.RESPONSE) {
                direction = string.Format("[{0}->CLIENT]", packet.ServerServiceType);
            } else {
                direction = "[?->?]";
            }
            string id = string.Format("[{0}]", packet.PacketID);
            string opcode = string.Format("[{0:X4}]", packet.Opcode);
            PrintRow(new string[] { direction, id, opcode, packet.Encrypted.ToString(), packet.Massive.ToString() });
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

            if (HaveAdminRights) {
                Console.WriteLine("Running as Administrator privileges");
                using (Process p = Process.GetCurrentProcess()) {
                    p.PriorityClass = ProcessPriorityClass.High;
                    Console.WriteLine($"Process Priority = {p.PriorityClass}");
                }
            }

            Console.ResetColor();
            Console.Beep();
        }
    }
}
