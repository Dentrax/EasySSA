using EasySSA;
using EasySSA.Common;
using EasySSA.Extensions;
using EasySSA.Packets;
using EasySSA.Server;
using EasySSA.Services;
using EasySSA.SSA;
using System;
using System.Net;

namespace Example1 {
    class Program {
        static void Main(string[] args) {

            SROServiceComponent gateway = new SROServiceComponent(ServerServiceType.GATEWAY, 1)
                                        .SetFingerprint(new Fingerprint("SRO_Client", 1, ""))
                                        .SetEndPoint(new IPEndPoint(IPAddress.Parse("192.168.1.1"), 5050))
                                        .SetRedirectPoint(new IPEndPoint(IPAddress.Parse("192.168.1.1"), 6060))
                                        .SetMaxClientCount(500);


            gateway.OnClientConnected += new Func<Client, bool> (delegate (Client client) {


                return true;
            });

            gateway.OnClientDisconnected += new Action<Client, ClientDisconnectType>(delegate (Client client, ClientDisconnectType disconnectType) {


            });

            gateway.OnPacketReceived += new Func<Client, SROPacket, PacketOperationType>(delegate (Client client, SROPacket packet) {


                return PacketOperationType.DISCONNECT;
            });

            gateway.DOBind(delegate (bool success) {
                if (success) {
                    //Bind success
                }
            });

        }
    }
}
