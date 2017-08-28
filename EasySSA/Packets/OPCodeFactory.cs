#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Collections.Generic;

namespace EasySSA.Packets {
    public sealed class OPCodeFactory {

        public static readonly Dictionary<uint, Type> PACKET_LIST;

        static OPCodeFactory() {

            //OPCodeFactory.PACKET_LIST = new Dictionary<uint, Type>() {

            //    { 0x0000, typeof(ExchangePacket) },
            //    { 0x0001, typeof(GuildLimitPacket) },

            //};


        }
    }
}
