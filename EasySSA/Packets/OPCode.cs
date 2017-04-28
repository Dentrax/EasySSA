#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace EasySSA.Packets {
    public static class OPCode {

        public struct Gateway {
            public const ushort CHECKVERSION = 0x6000;

            public struct Request {
                public const ushort PATCH = 0x6100;
                public const ushort LAUNCHER = 0x6104;
                public const ushort SERVERLIST = 0x6101;
                public const ushort LOGIN = 0x6102;
            }

            public struct Response {
                public const ushort PATCH = 0xA100;
                public const ushort LAUNCHER = 0xA104;
                public const ushort SERVERLIST = 0xA101;
                public const ushort LOGIN = 0xA102;
            }
        }


        public struct Agent {
            public struct Request {
                public const ushort CONNECTION = 0x6103;
                public const ushort INGAME = 0x7001;
                public const ushort CHARACTER_SCREEN = 0x7007;
                public const ushort CHARACTER_SELECT = 0x7001;
                public const ushort CHARACTER_ENTERWORLD = 0x3012;
                public const ushort GAMEOBJECT_MOVEMENT = 0x7021;
                public const ushort GAMEOBJECT_STOP = 0x7023;
                public const ushort GAMEOBJECT_SET_ANGLE = 0x7024;
                public const ushort SELECT_GAMEOBJECT = 0x7045;
                public const ushort TELEPORT = 0x705A;
                public const ushort TELEPORT_LOADING = 0x34B6;
                public const ushort CHAT = 0x7025;
                public const ushort INVENTORY_OPERATION = 0x7034;
                public const ushort SHOP_OPEN = 0x7046;
                public const ushort SHOP_CLOSE = 0x704B;
                public const ushort TELEPORT_APPOINT = 0x7059;
                public const ushort GAMEGUIDE = 0x70EA;
                public const ushort GM_COMMAND = 0x7010;
            }

            public struct Response {
                public const ushort CONNECTION = 0xA103;
                public const ushort INGAME = 0xB001;
                public const ushort CHARACTER_SCREEN = 0xB007;
                public const ushort CHARACTER_SELECT = 0xB001;
                public const ushort GAMEOBJECT_SET_ANGLE = 0xB024;
                public const ushort SELECT_GAMEOBJECT = 0xB045;
                public const ushort CHAT = 0xB025;
                public const ushort INVENTORY_OPERATION = 0xB034;
                public const ushort UPDATE_INVENTORY_SLOT = 0xB04C;
                public const ushort SHOP_OPEN = 0xB046;
                public const ushort SHOP_CLOSE = 0xB04B;
                public const ushort TELEPORT_APPOINT = 0xB059;
                public const ushort GAMEGUIDE = 0xB0EA;
            }
        }
    }
}
