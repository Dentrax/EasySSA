﻿#region License
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
using System.Diagnostics;
using System.Security.Principal;

namespace Example2 {
    class Program {

        public static bool HaveAdminRights => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

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
                           .SetVersionID(189)
                           .SetLocaleID(22)
                           .SetClientless(false)
                           .SetClientPath("D:\\_Coding-Corner_\\vSRO\\vSRO Client")
                           .SetLocalAgentEndPoint(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 25880))
                           .SetLocalGatewayEndPoint(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 25779))
                           .SetServiceEndPoint(new IPEndPoint(IPAddress.Parse("145.239.106.209"), 15779))
                           .SetBindTimeout(100)
                           .SetDebugMode(false);

            clientComponent.OnClientStatusChanged += new Action<SROClient, ClientStatusType>(delegate (SROClient client, ClientStatusType status) {
                Console.WriteLine("OnClientStatusChanged : " + status);
            });

            clientComponent.OnAccountStatusChanged += new Action<SROClient, AccountStatusType>(delegate (SROClient client, AccountStatusType status) {
                Console.WriteLine("OnAccountStatusChanged : " + status);
            });

            clientComponent.OnCaptchaStatusChanged += new Action<SROClient, CaptchaStatusType>(delegate (SROClient client, CaptchaStatusType status) {
                Console.WriteLine("OnCaptchaStatusChanged : " + status);
            });

            clientComponent.OnCharacterStatusChanged += new Action<SROClient, CharacterStatusType>(delegate (SROClient client, CharacterStatusType status) {
                Console.WriteLine("OnCharacterStatusChanged : " + status);
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
                Console.WriteLine("Client connected : " + client.Socket.RemoteEndPoint);
            });

            clientComponent.OnSocketDisconnected += new Action<SROClient, ClientDisconnectType>(delegate (SROClient client, ClientDisconnectType disconnectType) {
                Console.WriteLine("Client disconnected : " + client.IPAddress + " -- Reason : " + disconnectType);
            });

            clientComponent.OnPacketReceived += new Func<SROClient, SROPacket, PacketSocketType, PacketResult>(delegate (SROClient client, SROPacket packet, PacketSocketType socketType) {
                DumpPacket(packet);
                return new PacketResult(PacketOperationType.NOTHING);
            });

            clientComponent.DOConnect(delegate (bool success, BindErrorType error) {
                if (success) {
                    Console.WriteLine("PROGRAM.DOConnect() Gateway SUCCESS");
                } else {
                    Console.WriteLine("PROGRAM.DOConnect() Gateway FAILED -- Reason : " + error);
                }
            });
        }

        private static void InitConsole() {
            Console.Clear();
            Console.WindowWidth = 170;
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

        static int tableWidth = 160;
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
    }
}
