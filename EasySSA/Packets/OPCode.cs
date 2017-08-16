#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

//Reference -> http://www.elitepvpers.com/forum/sro-coding-corner/3034938-release-silkroad-packet-documentation.html
//Reference -> https://github.com/tanisman/SilkroadProject/blob/master/SCommon/Opcode.cs
//Reference -> https://github.com/DummkopfOfHachtenduden/SilkroadDoc/wiki/Packets


namespace EasySSA.Packets {
    public static class OPCode {

        public struct Global {
            public enum Request : ushort {
                HANDSHAKE_RESPONSE = 0x5000,
                HANDSHAKE_ACCEPT = 0x9000,
                MODULE_IDENTIFICATION = 0x2001,
                MODULE_KEEP_ALIVE = 0x2002,
                MODULE_CERTIFICATION_REQUEST = 0x6003,
                MODULE_CERTIFICATION_RESPONSE = 0xA003,
                MODULE_RELAY_REQUEST = 0x6008,
                MODULE_RELAY_RESPONSE = 0xA008,
                MASSIVE_MESSAGE = 0x600D,
            }

            public enum Response : ushort {
                HANDSHAKE_SETUP_CHALLENGE = 0x5000,
                MODULE_IDENTIFICATION = 0x2001,
                NODE_STATUS1 = 0x2005,
                NODE_STATUS2 = 0x6005,
                MASSIVE_MESSAGE = 0x600D,
            }
        }

        public struct Download {
            public enum Request : ushort {
                FILE_REQUEST = 0x6004,
                FILE_COMPLETE = 0xA004,
            }

            public enum Response : ushort {
                FILE_CHUNK = 0x1001,
            }
        }

        public struct Gateway {
            public enum Request : ushort {
                CHECKVERSION = 0x6000,
                PATCH = 0x6100,
                NEWS = 0x6104,
                SERVERLIST = 0x6101,
                SERVERLIST_PING = 0x6106,
                LOGIN = 0x6102,
                LOGIN_IBUV_CHALLENGE = 0x2322,
                LOGIN_IBUV_CONFIRM = 0x6323,
            }

            public enum Response : ushort {
                CHECKVERSION = 0x6000,
                PATCH = 0xA100,
                SERVERLIST = 0xA101,
                SERVERLIST_PING = 0xA106,
                LOGIN = 0xA102,
                LOGIN_IBUV_CONFIRM = 0xA323,
                NEWS = 0xA104,
            }
        }

        public struct Agent {
            public enum Request : ushort {
                //ACADEMY
                ACADEMY_CREATE = 0x7470,
                ACADEMY_DISBAND = 0x7471,
                ACADEMY_KICK = 0x7473,
                ACADEMY_LEAVE = 0x7474,
                ACADEMY_GRADE = 0x7475,
                ACADEMY_UPDATE_COMMENT = 0x7477,
                ACADEMY_HONOR_RANK = 0x7478,
                ACADEMY_MATCHING_REGISTER = 0x747A,
                ACCADEMY_MATCHING_CHANGE = 0x747B,
                ACCADEMY_MATCHING_DELETE = 0x747C,
                ACCADEMY_MATCHING_LIST = 0x747D,
                ACCADEMY_MATCHING_JOIN = 0x747E,
                ACCADEMY_MATCHING_REQUEST = 0x0000,
                ACCADEMY_MATCHING_RESPONSE = 0x347F,
                ACADEMY_UPDATE = 0x0000,
                ACADEMY_INFO = 0x0000,
                ACADEMY_UPDATE_BUFF = 0x0000,
                ACADEMY_UNKNOWN1 = 0x7472,
                ACADEMY_UNKNOWN2 = 0x7476,
                ACADEMY_UNKNOWN3 = 0x7483,
                ACADEMY_UNKNOWN4 = 0x0000,
                ACADEMY_UNKNOWN5 = 0x0000,

                //ALCHEMY
                ALCHEMY_REINFORCE = 0x7150,
                ALCHEMY_ENCHANT = 0x7151,
                ALCHEMY_MANUFACTURE = 0x7155,
                ALCHEMY_CANCELED = 0x0000,
                ALCHEMY_DISMANTLE = 0x7157,
                ALCHEMY_SOCKET = 0x716A,

                //AUTHENTICATION
                AUTH = 0x6103,

                //BATTLE_ARENA
                BATTLEARENA_OPERATION = 0x0000,
                BATTLEARENA_REQUEST = 0x74D3,

                //CAS
                CAS_CLIENT = 0x6314,
                CAS_SERVER_REQUEST = 0x0000,
                CAS_SERVER_RESPONSE = 0x6316,

                //CHARACTER
                CHARACTER_SELECTION_JOIN = 0x7001,
                CHARACTER_SELECTION_ACTION = 0x7007,
                CHARACTER_SELECTION_RENAME = 0x7450,

                //CHAT
                CHAT = 0x7025,
                CHAT_UPDATE = 0x0000,
                CHAT_RESTRICT = 0x0000,

                //COMMUNITY
                COMMUNITY_FRIEND_ADD = 0x7302,
                COMMUNITY_FRIEND_REQUEST = 0x0000,
                COMMUNITY_FRIEND_RESPONSE = 0x3303,
                COMMUNITY_FRIEND_DELETE = 0x7304,
                COMMUNITY_FRIEND_INFO = 0x0000,
                COMMUNITY_MEMO_OPEN = 0x7308,
                COMMUNITY_MEMO_SEND = 0x7309,
                COMMUNITY_MEMO_DELETE = 0x730A,
                COMMUNITY_MEMO_LIST = 0x730B,
                COMMUNITY_MEMO_SEND_GROUP = 0x730C,
                COMMUNITY_BLOCK = 0x730D,

                //CONFIG
                CONFIG_UPDATE = 0x7158,

                //CONSIGNMENT
	            CONSIGNMENT_DETAIL = 0x7506,
 	            CONSIGNMENT_CLOSE = 0x7507,
 	            CONSIGNMENT_REGISTER = 0x7508,
 	            CONSIGNMENT_UNREGISTER = 0x7509,
 	            CONSIGNMENT_BUY = 0x750A,
 	            CONSIGNMENT_SETTLE = 0x750B,
 	            CONSIGNMENT_SEARCH = 0x750C,
 	            CONSIGNMENT_UPDATE = 0x0000,
 	            CONSIGNMENT_LIST = 0x750E,
 	            CONSIGNMENT_BUFF_ADD = 0x0000,
 	            CONSIGNMENT_BUFF_REMOVE = 0x0000,
 	            CONSIGNMENT_BUFF_UPDATE = 0x0000,

                //COS
                COS_COMMAND = 0x70C5,
                COS_TERMINATE = 0x70C6,
                COS_INFO = 0x0000,
                COS_UPDATE = 0x0000,
                COS_UPDATE_STATE = 0x0000,
                COS_UPDATE_RIDESTATE = 0x70CB,
                COS_UNSUMMON = 0x7116,
                COS_NAME = 0x7117,
                COS_UPDATE_SETTINGS = 0x7420,
                COS_UNKNOWN1 = 0x70C7,

                //ENTITY - IN [W.I.P] ?

                //ENVIRONMENT
                ENVIRONMENT_CELESTIAL_POSITION = 0x0000,
                ENVIRONMENT_CELESTIAL_UPDATE = 0x0000,
                ENVIRONMENT_WEATHER_UPDATE = 0x0000,

                //EXCHANGE
                EXCHANGE_START = 0x7081,
                EXCHANGE_CONFIRM = 0x7082,
                EXCHANGE_APPROVE = 0x7083,
                EXCHANGE_CANCEL = 0x7084,
                EXCHANGE_STARTED = 0x0000,
                EXCHANGE_CONFIRMED = 0x0000,
                EXCHANGE_APPROVED = 0x0000,
                EXCHANGE_CANCELED = 0x0000,
                EXCHANGE_UPDATE = 0x0000,
                EXCHANGE_UPDATE_ITEMS = 0x0000,

                //FGW
 	            FGW_RECALL_LIST = 0x7519,
 	            FGW_RECALL_MEMBER = 0x751A,
 	            FGW_RECALL_REQUEST = 0x0000,
 	            FGW_RECALL_RESPONSE = 0x751C,
 	            FGW_EXIT = 0x751D,
 	            FGW_UPDATE = 0x0000,

                //CTF
                FLAGWAR_UPDATE = 0x0000,
                FLAGWAR_REGISTER = 0x74B2,

                //FRPVP
                FRPVP_UPDATE = 0x7516,

                //GAME
                GAME_NOTIFY = 0x0000,
                GAME_READY = 0x3012,
                GAME_INVITE = 0x3080,
                GAME_RESET = 0x0000,
                GAME_RESET_COMPLETE = 0x35B6,
                GAME_SERVERTIME = 0x0000,

                //GUIDE
                GUIDE = 0x70EA,

                //GUILD

                //INVENTORY
                //JOB
                //LOGOUT
                //MAGICOPTION
                //OPERATOR
                //PARTY
                //PK
                //QUEST
                //SIEGE
                //SILK
                //SKILL
                //STALL
                //TAP
                //TELEPORT




                TEST = 0x6666,
            }

            public enum Response : ushort {
                //ACADEMY
                ACADEMY_CREATE = 0xB470,
                ACADEMY_DISBAND = 0xB471,
                ACADEMY_KICK = 0xB473,
                ACADEMY_LEAVE = 0xB474,
                ACADEMY_GRADE = 0xB475,
                ACADEMY_UPDATE_COMMENT = 0xB477,
                ACADEMY_HONOR_RANK = 0xB478,
                ACADEMY_MATCHING_REGISTER = 0xB47A,
                ACCADEMY_MATCHING_CHANGE = 0xB47B,
                ACCADEMY_MATCHING_DELETE = 0xB47C,
                ACCADEMY_MATCHING_LIST = 0xB47D,
                ACCADEMY_MATCHING_JOIN = 0xB47E,
                ACCADEMY_MATCHING_REQUEST = 0x747E,
                ACCADEMY_MATCHING_RESPONSE = 0x0000,
                ACADEMY_UPDATE = 0x3C80,
                ACADEMY_INFO = 0x3C81,
                ACADEMY_UPDATE_BUFF = 0x3C82,
                ACADEMY_UNKNOWN1 = 0xB472,
                ACADEMY_UNKNOWN2 = 0xB476,
                ACADEMY_UNKNOWN3 = 0xB483,
                ACADEMY_UNKNOWN4 = 0x3C86,
                ACADEMY_UNKNOWN5 = 0x3C87,

                //ALCHEMY
                ALCHEMY_REINFORCE = 0xB150,
                ALCHEMY_ENCHANT = 0xB151,
                ALCHEMY_MANUFACTURE = 0xB155,
                ALCHEMY_CANCELED = 0x3156,
                ALCHEMY_DISMANTLE = 0xB157,
                ALCHEMY_SOCKET = 0xB16A,

                //AUTHENTICATION
                AUTH = 0xA103,

                //BATTLEARENA
                BATTLEARENA_OPERATION = 0x34D2,
                BATTLEARENA_REQUEST = 0x0000,

                //CAS
                CAS_CLIENT = 0xA314,
                CAS_SERVER_REQUEST = 0x6315,
                CAS_SERVER_RESPONSE = 0x0000,

                //CHARACTER
                CHARACTER_SELECTION_JOIN = 0xB001,
                CHARACTER_SELECTION_ACTION = 0xB007,
                CHARACTER_SELECTION_RENAME = 0xB450,

                //CHAT
                CHAT = 0xB025,
                CHAT_UPDATE = 0x3026,
                CHAT_RESTRICT = 0x302D,

                //COMMUNITY
                COMMUNITY_FRIEND_ADD = 0xB302,
                COMMUNITY_FRIEND_REQUEST = 0x7302,
                COMMUNITY_FRIEND_RESPONSE = 0x0000,
                COMMUNITY_FRIEND_DELETE = 0xB304,
                COMMUNITY_FRIEND_INFO = 0x3305,
                COMMUNITY_MEMO_OPEN = 0xB308,
                COMMUNITY_MEMO_SEND = 0xB309,
                COMMUNITY_MEMO_DELETE = 0xB30A,
                COMMUNITY_MEMO_LIST = 0xB30B,
                COMMUNITY_MEMO_SEND_GROUP = 0xB30C,
                COMMUNITY_BLOCK = 0xB30D,

                //CONFIG
                CONFIG_UPDATE = 0x0000,

                //CONSIGNMENT
                CONSIGNMENT_DETAIL = 0xB506,
                CONSIGNMENT_CLOSE = 0xB507,
                CONSIGNMENT_REGISTER = 0xB508,
                CONSIGNMENT_UNREGISTER = 0xB509,
                CONSIGNMENT_BUY = 0xB50A,
                CONSIGNMENT_SETTLE = 0xB50B,
                CONSIGNMENT_SEARCH = 0xB50C,
                CONSIGNMENT_UPDATE = 0x350D,
                CONSIGNMENT_LIST = 0xB50E,
                CONSIGNMENT_BUFF_ADD = 0x3530,
                CONSIGNMENT_BUFF_REMOVE = 0x3531,
                CONSIGNMENT_BUFF_UPDATE = 0x3532,

                //COS
                COS_COMMAND = 0xB0C5,
                COS_TERMINATE = 0xB0C6,
                COS_INFO = 0x30C8,
                COS_UPDATE = 0x30C9,
                COS_UPDATE_STATE = 0x30CA,
                COS_UPDATE_RIDESTATE = 0xB0CB,
                COS_UNSUMMON = 0xB116,
                COS_NAME = 0xB117,
                COS_UPDATE_SETTINGS = 0xB420,
                COS_UNKNOWN1 = 0xB0C7,

                //ENTITY - IN [W.I.P] ?

                //ENVIRONMENT
                ENVIRONMENT_CELESTIAL_POSITION = 0x30C8,
                ENVIRONMENT_CELESTIAL_UPDATE = 0x30C9,
                ENVIRONMENT_WEATHER_UPDATE = 0x30CA,

                //EXCHANGE
                EXCHANGE_START = 0xB081,
                EXCHANGE_CONFIRM = 0xB082,
                EXCHANGE_APPROVE = 0xB083,
                EXCHANGE_CANCEL = 0xB084,
                EXCHANGE_STARTED = 0x3085,
                EXCHANGE_CONFIRMED = 0x3086,
                EXCHANGE_APPROVED = 0x3087,
                EXCHANGE_CANCELED = 0x3038,
                EXCHANGE_UPDATE = 0x3039,
                EXCHANGE_UPDATE_ITEMS = 0x308C,

                //FGW
                FGW_RECALL_LIST = 0xB519,
                FGW_RECALL_MEMBER = 0xB51A,
                FGW_RECALL_REQUEST = 0x741A,
                FGW_RECALL_RESPONSE = 0xB51C,
                FGW_EXIT = 0xB51D,
                FGW_UPDATE = 0x351E,

                //CTF
                FLAGWAR_UPDATE = 0x34B1,
                FLAGWAR_REGISTER = 0x0000,

                //FRPVP
                FRPVP_UPDATE = 0xB516,

                //GAME
                GAME_NOTIFY = 0x300C,
                GAME_READY = 0x0000,
                GAME_INVITE = 0x3080,
                GAME_RESET = 0x35B5,
                GAME_RESET_COMPLETE = 0x0000,
                GAME_SERVERTIME = 0x34BE,

                //GUIDE
                GUIDE = 0xB0EA,


                //GUILD
                //https://github.com/DummkopfOfHachtenduden/SilkroadDoc/wiki/Agent-packets#guild


                //INVENTORY
                //JOB
                //LOGOUT
                //MAGICOPTION
                //OPERATOR
                //PARTY
                //PK
                //QUEST
                //SIEGE
                //SILK
                //SKILL
                //STALL
                //TAP
                //TELEPORT



                TEST = 0xB0EA,
            }
        }
    }
}
