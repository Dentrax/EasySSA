using System;
using System.Net;
using System.Net.Sockets;

using EasySSA.Common;
using EasySSA.Packets;
using EasySSA.Server;
using EasySSA.Services;
using EasySSA.SSA;

namespace Example1 {
    class Program {
        static void Main(string[] args) {

            SROServiceComponent gateway = new SROServiceComponent(ServerServiceType.GATEWAY, 1)
                                        .SetFingerprint(new Fingerprint("SRO_Client", 1, ""))
                                        .SetLocalEndPoint(new IPEndPoint(IPAddress.Parse("192.168.1.1"), 5050))
                                        .SetServiceEndPoint(new IPEndPoint(IPAddress.Parse("192.168.1.1"), 6060))
                                        .SetMaxClientCount(500);

            gateway.OnLocalSocketStatusChanged += new Action<SocketError>(delegate (SocketError error) {

                if(error == SocketError.Success) {
                    Console.WriteLine("Local socket bind success");
                }

            });

            gateway.OnServiceSocketStatusChanged += new Action<SocketError>(delegate (SocketError error) {

                if (error == SocketError.Success) {
                    Console.WriteLine("Service socket connect success");
                }

            });

            gateway.OnClientConnected += new Func<Client, bool> (delegate (Client client) {

                Console.WriteLine("New client connected : " + client.IPAddress);

                return true;
            });

            gateway.OnClientDisconnected += new Action<Client, ClientDisconnectType>(delegate (Client client, ClientDisconnectType disconnectType) {

                Console.WriteLine("Client disconnected : " + client.IPAddress);

            });

            gateway.OnPacketReceived += new Func<Client, SROPacket, PacketSocketType, PacketResult>(delegate (Client client, SROPacket packet, PacketSocketType socketType) {

                Console.WriteLine("Packet received : " + packet.Opcode);

                return new PacketResult(PacketOperationType.NOTHING);
            });

            gateway.DOBind(delegate (bool success) {
                if (success) {
                    Console.WriteLine("BIND SUCCESS");
                } else {
                    Console.WriteLine("BIND ERROR");
                }
            });

            Console.ReadLine();

        }
    }
}
