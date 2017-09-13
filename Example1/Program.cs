using System;
using System.Net;
using System.Net.Sockets;

using EasySSA.Common;
using EasySSA.Packets;
using EasySSA.Server;
using EasySSA.Services;
using EasySSA.SSA;
using EasySSA.Core.Network.Securities;

namespace Example1 {
    class Program {
        static void Main(string[] args) {

            SROServiceComponent gateway = new SROServiceComponent(ServerServiceType.GATEWAY, 1)
                                        .SetFingerprint(new Fingerprint("SR_Client", 0, SecurityFlags.Handshake & SecurityFlags.Blowfish & SecurityFlags.SecurityBytes, "")) 
                                        .SetLocalEndPoint(new IPEndPoint(IPAddress.Parse("192.168.1.111"), 25800))
                                        .SetLocalBindTimeout(10)
                                        .SetServiceEndPoint(new IPEndPoint(IPAddress.Parse("192.168.1.111"), 20002))
                                        .SetServiceBindTimeout(10)
                                        .SetMaxClientCount(500);

            gateway.OnLocalSocketStatusChanged += new Action<SocketError>(delegate (SocketError error) {
                if(error == SocketError.Success) {
                    Console.WriteLine("EasySSA local socket bind success on : " + gateway.LocalEndPoint.ToString());
                } else {
                    Console.WriteLine("EasySSA local socket bind FAILED!!! : " + error);
                }
            });

            gateway.OnServiceSocketStatusChanged += new Action<Client, SocketError>(delegate (Client client, SocketError error) {
                if (error == SocketError.Success) {
                    Console.WriteLine("Module service socket connect success on : " + gateway.ServiceEndPoint.ToString());
                } else { 
                    Console.WriteLine("Module service socket failed : " + error);
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
