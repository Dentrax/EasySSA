<h1 align="center">EasySSA Public Source Repository</h1>

**SilkroadSecurityApi improvement and more features by 'Dentrax'**

**Ultra simple Silkroad <sup>Proxy</sup> Library Ever!**

Don't repeat yourself every time!

[What It Is](#what-it-is)

[How To Use](#how-to-use)

[About](#about)  

[Collaborators](#collaborators)  

[Branches](#branches) 

[Copyright & Licensing](#copyright--licensing)  

[Contributing](#contributing)  

[Contact](#contact)


## What It Is

**EasySSA**

EasySSA library for VSRO files is an easy and advanced way to create proxy programs.

**Uses : `.NET Framework v4.6.1 Library`**

Controls and wizards are available for users to:

> * Detect, block, replace, ignore features to incoming-outgoing packets
> * Analyze server-side or client-side packets with details more details
> * Advanced packet database library. **[Click here to see PacketDatabase.cs](https://github.com/Dentrax/EasySSA/blob/master/EasySSA/Packets/PacketDatabase.cs)**
> * **[Packet.cs](https://github.com/Dentrax/EasySSA/blob/master/EasySSA/SSA/Packet.cs)** is obsolete! The new **[SROPacket.cs](https://github.com/Dentrax/EasySSA/blob/master/EasySSA/Packets/SROPacket.cs)** is overriding now.
> * Not complex, not hard, dont repeat yourself.


## How To Use

Example Usage
--------------------------

![Preview Thumbnail](https://raw.githubusercontent.com/Dentrax/EasySSA/master/Example1/Thumbnail.png)

Create Server-Side Proxy
=============
```csharp
	SROServiceComponent gateway = new SROServiceComponent(ServerServiceType, Index);
```

ServiceComponent Functions
--------------------------

| Function                | Parameter                   | Explanation				                                                    |
| ----------------------- |:---------------------------:|:-----------------------------------------------------------------------------:|
| `SetFingerprint`        | Fingerprint					| Sets a fingerprint for Security. Uses IdentityID, IdentityFlag, SecurityFlag. |
| `SetLocalEndPoint`      | IPEndPoint					| Associates a Socket with local endpoint.										|
| `SetServiceEndPoint`    | IPEndPoint					| Associates a Socket with service/module endpoint.								|
| `SetMaxClientCount`     | int							| How many clients can connect the prxoy ?										|
| `SetLocalBindTimeout`   | int							| Sets local bind timeout.														|
| `SetServiceBindTimeout` | int							| Sets service bind timeout.													|
| `SetDebugMode`          | bool                        | Sets debug mode.																|
| `DOBind`                | Action<bool, BindErrorType> | Initialize component and start listening the server.							|


ServiceComponent Listeners
--------------------------

| Listener                       | Type                                                     | Explanation				                                                                                    |
| ------------------------------ |:--------------------------------------------------------:|:-------------------------------------------------------------------------------------------------------------:|
| `OnLocalSocketStatusChanged`   | Action<SocketError>										| Trigger if local socket status changed some reason. **(readonly)**										    |
| `OnServiceSocketStatusChanged` | Action<Client, SocketError>								| Trigger if service socket status changed some reason.	**(readonly)**											|
| `OnClientConnected`			 | Func<Client, bool>										| Trigger if a client socket established. To accept or decline a client use **return true** or **return false**	|
| `OnClientDisconnected`		 | Action<Client, ClientDisconnectType>						| Trigger if a client socket disconnected due to a error or problem. **(readonly)**			             		|
| `OnPacketReceived`			 | Func<Client, SROPacket, PacketSocketType, PacketResult>  | Trigger if a packet received from a client.																    |


OnPacketReceived Operations
--------------------------

| PacketOperationType  | PacketResult Type                         | PacketResult Parameter				                        | Explanation										                                               |
| -------------------- |:-----------------------------------------:|:----------------------------------------------------------:|:------------------------------------------------------------------------------------------------:|
| `RESPONSE`		   | PacketResult.PacketResponseResultInfo	   | List<Packet> packet or Packet packet						| Send packet response list to ServiceContext where the packet comes from.						   |
| `DISCONNECT`		   | PacketResult.PacketDisconnectResultInfo   | string notice, Enum disconnectReason						| Send **notice** to client before disconnect if possible.                                         |
| `REPLACE`			   | PacketResult.PacketReplaceResultInfo	   | Packet packet, List<Packet> replaceWith					| Replace current packet with **replaceWith** array if current packet == packet.				   |
| `INJECT`			   | PacketResult.PacketInjectResultInfo	   | Packet packet, List<Packet> injectWith, bool afterPacket 	| Inject **injectWith** packets if current packet == packet. Inject after if **afterPacket** true. |
| `IGNORE`			   | Independent							   | Parameterless			             						| Ignore current packet. Dont send.																   |
| `NOTHING`			   | Independent							   | Parameterless												| Nothing																						   |


DOBind Error Types
--------------------------

| BindErrorType								    | Explanation				                                                |
| --------------------------------------------- |:-------------------------------------------------------------------------:|
| `SUCCESS`										| Trigger if everything is great and TCP listener socket bind successfull. 	|
| `COMPONENT_DISPOSED`							| Trigger if SROServiceComponent object disposed.							|
| `COMPONENT_FINGERPRINT_NULL`					| Trigger if Fingerprint object is null.									|
| `COMPONENT_LOCAL_ENDPOINT_NULL`				| Trigger if local socket's endpoint is null.			             		|
| `COMPONENT_SERVICE_ENDPOINT_NULL`				| Trigger if service socket's endpoint is null.							    |
| `COMPONENT_LOCAL_BIND_TIMEOUT_NULL_OR_ZERO`	| Trigger if local socket's timeout null or under 1.						|
| `COMPONENT_SERVICE_BIND_TIMEOUT_NULL_OR_ZERO`	| Trigger if service socket's timeout null or under 1.						|
| `COMPONENT_SERVICE_INDEX_NULL_OR_ZERO`		| Trigger if service index null or under 1.									|
| `COMPONENT_SERVICE_CLIENT_COUNT_NULL_OR_ZERO`	| Trigger if max client count null or under 1.							    |
| `SERVER_BIND_SOCKET_NOT_NULL`					| Trigger if TCP listener socket is not null. 							    |
| `SERVER_BIND_SOCKET_ALREADY_ACTIVE`			| Trigger if TCP listener socket is already listening and not null.		    |
| `SERVER_BIND_SOCKET_NON_AVAILABLE`			| Trigger if TCP listener socket bind failed or available == 0.				|
| `SERVER_BIND_ARGUMENT_NULL_EXCEPTION`			| Trigger if TCP listener catch **ArgumentNullException**					|
| `SERVER_BIND_SOCKET_EXCEPTION`				| Trigger if TCP listener catch **SocketException**							|
| `SERVER_BIND_SECURITY_EXCEPTION`				| Trigger if TCP listener catch **SecurityException**					    |
| `SERVER_BIND_OBJECT_DISPOSED_EXCEPTION`		| Trigger if TCP listener catch **ObjectDisposedException**					|
| `UNKNOWN`										| Trigger if a unexcepted error handled.									|


AccountStatusType Types
--------------------------

| AccountStatusType | Explanation				                                        |
| ----------------- |:-----------------------------------------------------------------:|
| `LOGIN_SUCCESS`	| Trigger if account login success. 								|
| `LOGIN_FAILED`	| Trigger if account login failed. Wrong (ID, PW).					|
| `BLOCKED`			| Trigger if account login blocked when over captcha or login try.	|
| `BANNED`			| Trigger if account login banned from GM.			             	|


ClientStatusType Types
--------------------------

| ClientStatusType				 | Explanation																|
| ------------------------------ |:------------------------------------------------------------------------:|
| `STARTED`						 | Trigger if sro_client started successfully. 								|
| `SHUTDOWN`					 | Trigger if sro_client closed or crashed some reason.						|
| `SWITCH_TO_CLIENT_SUCCESS`	 | Trigger if clientless to client switch successfully.						|
| `SWITCH_TO_CLIENT_FAILED`		 | Trigger if clientless to client switch failed.			             	|
| `SWITCH_TO_CLIENTLESS_SUCCESS` | Trigger if client to clientless switch successfully.						|
| `SWITCH_TO_CLIENTLESS_FAILED`  | Trigger if client to clientless switch failed.					        |
| `READY`						 | Trigger if sro_client fully ready up when the character enters the game. |


Example SROServiceComponent Usage
--------------------------
```csharp
   SROServiceComponent gateway = new SROServiceComponent(ServerServiceType.GATEWAY, 1)
			.SetFingerprint(new Fingerprint("SR_Client", 0, SecurityFlags.Handshake & SecurityFlags.Blowfish & SecurityFlags.SecurityBytes, ""))
			.SetLocalEndPoint(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 15779))
			.SetLocalBindTimeout(10)
			.SetServiceEndPoint(new IPEndPoint(IPAddress.Parse("111.111.111.111"), 15779))
			.SetServiceBindTimeout(100)
			.SetMaxClientCount(500)
			.SetDebugMode(false);

    gateway.OnLocalSocketStatusChanged += new Action<SocketError>(delegate (SocketError error) {

        if (error == SocketError.Success) {
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

    gateway.OnClientConnected += new Func<Client, bool>(delegate (Client client) {

        Console.WriteLine("New client connected : " + client.Socket.RemoteEndPoint);

        if (string.IsNullOrEmpty(client.IPAddress)) { //Example
            return false; //Decline client
        } else {
            return true; //Accept client
        }

    });

    gateway.OnClientDisconnected += new Action<Client, ClientDisconnectType>(delegate (Client client, ClientDisconnectType disconnectType) {

        Console.WriteLine("Client disconnected : " + client.IPAddress + " -- Reason : " + disconnectType);

    });

    gateway.OnPacketReceived += new Func<Client, SROPacket, PacketSocketType, PacketResult>(delegate (Client client, SROPacket packet, PacketSocketType socketType) {

        switch (packet.Opcode) {
            case 0x1111:
                return new PacketResult(PacketOperationType.DISCONNECT, new PacketResult.PacketDisconnectResultInfo("DC Reason : 0x5555 received"));

            case 0x2222:
                return new PacketResult(PacketOperationType.IGNORE);

            case 0x3333:
                return new PacketResult(PacketOperationType.INJECT, new PacketResult.PacketInjectResultInfo(packet, new List<Packet> { new Packet(0x3334), new Packet(0x3335) }, true));

            case 0x4444:
                return new PacketResult(PacketOperationType.REPLACE, new PacketResult.PacketReplaceResultInfo(packet, new List<Packet> { new Packet(0x4445) }));

            default:
                return new PacketResult(PacketOperationType.NOTHING);
        }

    });

    gateway.DOBind(delegate (bool success, BindErrorType error) {

        if (success) {
            Console.WriteLine("EasySSA bind SUCCESS");
        } else {
            Console.WriteLine("EasySSA bind FAILED -- Reason : " + error);
        }

    });
```


Example SROClientComponent Usage
--------------------------
```csharp
   SROClientComponent clientComponent = new SROClientComponent(1)
			.SetFingerprint(new Fingerprint("SR_Client", 0, SecurityFlags.Handshake & SecurityFlags.Blowfish & SecurityFlags.SecurityBytes, string.Empty))
            .SetAccount(new Account("furkan", "1"), "Dentrax")
            .SetCaptcha(string.Empty)
            .SetVersionID(191)
            .SetLocaleID(22)
            .SetClientless(false)
            .SetClientPath("D:\\vSRO\\vSRO Client")
            .SetLocalAgentEndPoint(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 25880))
            .SetLocalGatewayEndPoint(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 25779))
            .SetServiceEndPoint(new IPEndPoint(IPAddress.Parse("111.111.111.111"), 15779))
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
```

## About

EasySSA was created to serve three purposes:

**EasySSA is a server-client proxy tool which logs all SilkroadOnline packet traffic between your server and the client with SilkroadSecurityApi**

1. To act as a library to create simplest security for VSRO server files.

2. To provide a simplest way to create any feature, what you think. 

3. Instead of writing long and complex code every time, it provides the easiest and strongest way.


## Collaborators

**Project Manager** - Furkan Türkal (GitHub: dentrax)

## Branches

We publish source for the **[EasySSA]** in three rolling branches:

We publish source for the engine in three rolling branches:

The **[release branch](https://github.com/dentrax/EasySSA/tree/release)** is extensively tested by our QA team and makes a great starting point for learning the EasySSA.

The **[promoted branch](https://github.com/dentrax/EasySSA/tree/promoted)** is updated with builds for our team members to use. It's a good balance between getting the latest cool stuff and knowing most things work.

The **[master branch](https://github.com/dentrax/EasySSA/tree/master)** tracks [live changes](https://github.com/dentrax/EasySSA/commits/master) by our EasySSA team. 
This is the cutting edge and may be buggy - it may not even compile.

Other short-lived branches may pop-up from time to time as we stabilize new releases or hotfixes.

 ## Copyright & Licensing
 
The base project code is copyrighted by Furkan 'Dentrax' Türkal and is covered by single licence.

All program code (i.e. C#, Java) is licensed under GPL v3.0 unless otherwise specified. Please see the **[LICENSE.md](https://github.com/Dentrax/EasySSA/blob/master/LICENSE)** file for more information.

**SilkroadSecurityApi**

It was developed and made by Drew 'pushedx' Benton. For more information, please **[click here](https://goo.gl/w9uJjw)**

## Contributing

Please check the [CONTRIBUTING.md](CONTRIBUTING.md) file for contribution instructions and naming guidelines.

## Contact

EasySQLITE was created by Furkan 'Dentrax' Türkal

 * <https://www.furkanturkal.com>
 
You can contact EasySSA by URL:
    **[CONTACT](https://github.com/dentrax)**
