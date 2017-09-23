#region License
// ====================================================
// EasySSA Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using EasySSA.SSA;
using EasySSA.Common;

namespace EasySSA.Packets {
    public static class PacketDatabase {

        public static ushort GetOPCodeFrom(SROPacket packet) {
            return packet.Opcode;
        }

        public static SROPacket GetPacketFrom(Packet packet, PacketSocketType socket) {

            if(socket == PacketSocketType.SERVER) {

                switch (packet.Opcode) {

                    #region RESPONSE

                    #region GLOBAL-RESPONSE

                    case (ushort)OPCode.Global.Response.HANDSHAKE_SETUP_CHALLENGE:
                        return new SROPacket("HANDSHAKE_SETUP_CHALLENGE", packet, PacketSendType.RESPONSE, ServerServiceType.GLOBAL, PacketSocketType.SERVER);
                    case (ushort)OPCode.Global.Response.MODULE_IDENTIFICATION:
                        return new SROPacket("MODULE_IDENTIFICATION", packet, PacketSendType.RESPONSE, ServerServiceType.GLOBAL, PacketSocketType.SERVER);
                    case (ushort)OPCode.Global.Response.NODE_STATUS1: //massive
                        return new SROPacket("NODE_STATUS1", packet, PacketSendType.RESPONSE, ServerServiceType.GLOBAL, PacketSocketType.SERVER);
                    case (ushort)OPCode.Global.Response.NODE_STATUS2: //massive
                        return new SROPacket("NODE_STATUS2", packet, PacketSendType.RESPONSE, ServerServiceType.GLOBAL, PacketSocketType.SERVER);
                    case (ushort)OPCode.Global.Response.MASSIVE_MESSAGE:
                        return new SROPacket("MASSIVE_MESSAGE", packet, PacketSendType.RESPONSE, ServerServiceType.GLOBAL, PacketSocketType.SERVER);

                    #endregion

                    #region DOWNLOAD-RESPONSE

                    case (ushort)OPCode.Download.Response.FILE_CHUNK:
                        return new SROPacket("FILE_CHUNK", packet, PacketSendType.RESPONSE, ServerServiceType.DOWNLOAD, PacketSocketType.SERVER);

                    #endregion

                    #region GATEWAY-RESPONSE

                    case (ushort)OPCode.Gateway.Response.CHECKVERSION:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.RESPONSE, ServerServiceType.GATEWAY, PacketSocketType.SERVER);
                    case (ushort)OPCode.Gateway.Response.PATCH: //massive
                        return new SROPacket("PATCH", packet, PacketSendType.RESPONSE, ServerServiceType.GATEWAY, PacketSocketType.SERVER);
                    case (ushort)OPCode.Gateway.Response.SERVERLIST:
                        return new SROPacket("SERVERLIST", packet, PacketSendType.RESPONSE, ServerServiceType.GATEWAY, PacketSocketType.SERVER);
                    case (ushort)OPCode.Gateway.Response.SERVERLIST_PING:
                        return new SROPacket("SERVERLIST_PING", packet, PacketSendType.RESPONSE, ServerServiceType.GATEWAY, PacketSocketType.SERVER);
                    case (ushort)OPCode.Gateway.Response.LOGIN:
                        return new SROPacket("LOGIN", packet, PacketSendType.RESPONSE, ServerServiceType.GATEWAY, PacketSocketType.SERVER);
                    case (ushort)OPCode.Gateway.Response.LOGIN_IBUV_CONFIRM:
                        return new SROPacket("LOGIN_IBUV_CONFIRM", packet, PacketSendType.RESPONSE, ServerServiceType.GATEWAY, PacketSocketType.SERVER);
                    case (ushort)OPCode.Gateway.Response.LOGIN_IBUV_CHALLENGE:
                        return new SROPacket("LOGIN_IBUV_CHALLENGE", packet, PacketSendType.RESPONSE, ServerServiceType.GATEWAY, PacketSocketType.SERVER);
                    case (ushort)OPCode.Gateway.Response.NEWS:
                        return new SROPacket("NEWS", packet, PacketSendType.RESPONSE, ServerServiceType.GATEWAY, PacketSocketType.SERVER);

                    #endregion

                    #region AGENT-RESPONSE

                    #region ACADEMY

                    case (ushort)OPCode.Agent.Response.ACADEMY_CREATE:
                        return new SROPacket("ACADEMY_CREATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACADEMY_DISBAND:
                        return new SROPacket("ACADEMY_DISBAND", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACADEMY_KICK:
                        return new SROPacket("ACADEMY_KICK", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACADEMY_LEAVE:
                        return new SROPacket("ACADEMY_LEAVE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACADEMY_GRADE:
                        return new SROPacket("ACADEMY_GRADE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACADEMY_UPDATE_COMMENT:
                        return new SROPacket("ACADEMY_UPDATE_COMMENT", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACADEMY_HONOR_RANK:
                        return new SROPacket("ACADEMY_HONOR_RANK", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACADEMY_MATCHING_REGISTER:
                        return new SROPacket("ACADEMY_MATCHING_REGISTER", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACCADEMY_MATCHING_CHANGE:
                        return new SROPacket("ACCADEMY_MATCHING_CHANGE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACCADEMY_MATCHING_DELETE:
                        return new SROPacket("ACCADEMY_MATCHING_DELETE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACCADEMY_MATCHING_LIST:
                        return new SROPacket("ACCADEMY_MATCHING_LIST", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACCADEMY_MATCHING_JOIN:
                        return new SROPacket("ACCADEMY_MATCHING_JOIN", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACCADEMY_MATCHING_REQUEST:
                        return new SROPacket("ACCADEMY_MATCHING_REQUEST", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACADEMY_UPDATE:
                        return new SROPacket("ACADEMY_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACADEMY_INFO:
                        return new SROPacket("ACADEMY_INFO", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACADEMY_UPDATE_BUFF:
                        return new SROPacket("ACADEMY_UPDATE_BUFF", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACADEMY_UNKNOWN1:
                        return new SROPacket("ACADEMY_UNKNOWN1", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACADEMY_UNKNOWN2:
                        return new SROPacket("ACADEMY_UNKNOWN2", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACADEMY_UNKNOWN3:
                        return new SROPacket("ACADEMY_UNKNOWN3", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACADEMY_UNKNOWN4:
                        return new SROPacket("ACADEMY_UNKNOWN4", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ACADEMY_UNKNOWN5:
                        return new SROPacket("ACADEMY_UNKNOWN5", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region ALCHEMY

                    case (ushort)OPCode.Agent.Response.ALCHEMY_REINFORCE:
                        return new SROPacket("ALCHEMY_REINFORCE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ALCHEMY_ENCHANT:
                        return new SROPacket("ALCHEMY_ENCHANT", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ALCHEMY_MANUFACTURE:
                        return new SROPacket("ALCHEMY_MANUFACTURE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ALCHEMY_CANCELED:
                        return new SROPacket("ALCHEMY_CANCELED", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ALCHEMY_DISMANTLE:
                        return new SROPacket("ALCHEMY_DISMANTLE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ALCHEMY_SOCKET:
                        return new SROPacket("ALCHEMY_SOCKET", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region AUTHENTICATION

                    case (ushort)OPCode.Agent.Response.AUTH:
                        return new SROPacket("AUTH", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region BATTLE_ARENA

                    case (ushort)OPCode.Agent.Response.BATTLEARENA_OPERATION:
                        return new SROPacket("BATTLEARENA_OPERATION", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region CAS

                    case (ushort)OPCode.Agent.Response.CAS_CLIENT:
                        return new SROPacket("CAS_CLIENT", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.CAS_SERVER_REQUEST:
                        return new SROPacket("CAS_SERVER_REQUEST", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region CHARACTER

                    case (ushort)OPCode.Agent.Response.CHARACTER_SELECTION_JOIN:
                        return new SROPacket("CHARACTER_SELECTION_JOIN", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.CHARACTER_SELECTION_ACTION:
                        return new SROPacket("CHARACTER_SELECTION_ACTION", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.CHARACTER_SELECTION_RENAME:
                        return new SROPacket("CHARACTER_SELECTION_RENAME", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region CHAT

                    case (ushort)OPCode.Agent.Response.CHAT:
                        return new SROPacket("CHAT", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.CHAT_UPDATE:
                        return new SROPacket("CHAT_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.CHAT_RESTRICT:
                        return new SROPacket("CHAT_RESTRICT", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region COMMUNITY

                    case (ushort)OPCode.Agent.Response.COMMUNITY_FRIEND_ADD:
                        return new SROPacket("COMMUNITY_FRIEND_ADD", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COMMUNITY_FRIEND_REQUEST:
                        return new SROPacket("COMMUNITY_FRIEND_REQUEST", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COMMUNITY_FRIEND_DELETE:
                        return new SROPacket("COMMUNITY_FRIEND_DELETE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COMMUNITY_FRIEND_INFO:
                        return new SROPacket("COMMUNITY_FRIEND_INFO", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COMMUNITY_MEMO_OPEN:
                        return new SROPacket("COMMUNITY_MEMO_OPEN", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COMMUNITY_MEMO_SEND:
                        return new SROPacket("COMMUNITY_MEMO_SEND", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COMMUNITY_MEMO_DELETE:
                        return new SROPacket("COMMUNITY_MEMO_DELETE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COMMUNITY_MEMO_LIST:
                        return new SROPacket("COMMUNITY_MEMO_LIST", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COMMUNITY_MEMO_SEND_GROUP:
                        return new SROPacket("COMMUNITY_MEMO_SEND_GROUP", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COMMUNITY_BLOCK:
                        return new SROPacket("COMMUNITY_BLOCK", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region CONSIGNMENT

                    case (ushort)OPCode.Agent.Response.CONSIGNMENT_DETAIL:
                        return new SROPacket("CONSIGNMENT_DETAIL", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.CONSIGNMENT_CLOSE:
                        return new SROPacket("CONSIGNMENT_CLOSE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.CONSIGNMENT_REGISTER:
                        return new SROPacket("CONSIGNMENT_REGISTER", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.CONSIGNMENT_UNREGISTER:
                        return new SROPacket("CONSIGNMENT_UNREGISTER", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.CONSIGNMENT_BUY:
                        return new SROPacket("CONSIGNMENT_BUY", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.CONSIGNMENT_SETTLE:
                        return new SROPacket("CONSIGNMENT_SETTLE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.CONSIGNMENT_SEARCH:
                        return new SROPacket("CONSIGNMENT_SEARCH", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.CONSIGNMENT_UPDATE:
                        return new SROPacket("CONSIGNMENT_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.CONSIGNMENT_LIST:
                        return new SROPacket("CONSIGNMENT_LIST", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.CONSIGNMENT_BUFF_ADD:
                        return new SROPacket("CONSIGNMENT_BUFF_ADD", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.CONSIGNMENT_BUFF_REMOVE:
                        return new SROPacket("CONSIGNMENT_BUFF_REMOVE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.CONSIGNMENT_BUFF_UPDATE:
                        return new SROPacket("CONSIGNMENT_BUFF_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region COS

                    case (ushort)OPCode.Agent.Response.COS_COMMAND:
                        return new SROPacket("COS_COMMAND", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COS_TERMINATE:
                        return new SROPacket("COS_TERMINATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COS_INFO:
                        return new SROPacket("COS_INFO", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COS_UPDATE:
                        return new SROPacket("COS_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COS_UPDATE_STATE:
                        return new SROPacket("COS_UPDATE_STATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COS_UPDATE_RIDESTATE:
                        return new SROPacket("COS_UPDATE_RIDESTATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COS_UNSUMMON:
                        return new SROPacket("COS_UNSUMMON", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COS_NAME:
                        return new SROPacket("COS_NAME", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COS_UPDATE_SETTINGS:
                        return new SROPacket("COS_UPDATE_SETTINGS", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.COS_UNKNOWN1:
                        return new SROPacket("COS_UNKNOWN1", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region ENVIRONMENT

                    //Obsolote - Overridden from COS

                    /*case (ushort)OPCode.Agent.Response.ENVIRONMENT_CELESTIAL_POSITION:
                        return new SROPacket("ENVIRONMENT_CELESTIAL_POSITION", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ENVIRONMENT_CELESTIAL_UPDATE:
                        return new SROPacket("ENVIRONMENT_CELESTIAL_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.ENVIRONMENT_WEATHER_UPDATE:
                        return new SROPacket("ENVIRONMENT_WEATHER_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    */

                    #endregion

                    #region EXCHANGE

                    case (ushort)OPCode.Agent.Response.EXCHANGE_START:
                        return new SROPacket("EXCHANGE_START", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.EXCHANGE_CONFIRM:
                        return new SROPacket("EXCHANGE_CONFIRM", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.EXCHANGE_APPROVE:
                        return new SROPacket("EXCHANGE_APPROVE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.EXCHANGE_CANCEL:
                        return new SROPacket("EXCHANGE_CANCEL", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.EXCHANGE_STARTED:
                        return new SROPacket("EXCHANGE_STARTED", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.EXCHANGE_CONFIRMED:
                        return new SROPacket("EXCHANGE_CONFIRMED", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.EXCHANGE_APPROVED:
                        return new SROPacket("EXCHANGE_APPROVED", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    //case (ushort)OPCode.Agent.Response.EXCHANGE_CANCELED:
                        //return new SROPacket("EXCHANGE_CANCELED", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    //case (ushort)OPCode.Agent.Response.EXCHANGE_UPDATE:
                        //return new SROPacket("EXCHANGE_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.EXCHANGE_UPDATE_ITEMS:
                        return new SROPacket("EXCHANGE_UPDATE_ITEMS", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region FGW

                    case (ushort)OPCode.Agent.Response.FGW_RECALL_LIST:
                        return new SROPacket("FGW_RECALL_LIST", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.FGW_RECALL_MEMBER:
                        return new SROPacket("FGW_RECALL_MEMBER", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.FGW_RECALL_REQUEST:
                        return new SROPacket("FGW_RECALL_REQUEST", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.FGW_RECALL_RESPONSE:
                        return new SROPacket("FGW_RECALL_RESPONSE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.FGW_EXIT:
                        return new SROPacket("FGW_EXIT", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.FGW_UPDATE:
                        return new SROPacket("FGW_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region CTF

                    case (ushort)OPCode.Agent.Response.FLAGWAR_UPDATE:
                        return new SROPacket("FLAGWAR_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region FRPVP

                    case (ushort)OPCode.Agent.Response.FRPVP_UPDATE:
                        return new SROPacket("FRPVP_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region GAME

                    case (ushort)OPCode.Agent.Response.GAME_NOTIFY:
                        return new SROPacket("GAME_NOTIFY", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GAME_INVITE:
                        return new SROPacket("GAME_INVITE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GAME_RESET:
                        return new SROPacket("GAME_RESET", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GAME_SERVERTIME:
                        return new SROPacket("GAME_SERVERTIME", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region GUIDE

                    case (ushort)OPCode.Agent.Response.GUIDE:
                        return new SROPacket("GUIDE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region GUILD

                    case (ushort)OPCode.Agent.Response.GUILD_ENTITY_UPDATE_HOSTILITY:
                        return new SROPacket("GUILD_ENTITY_UPDATE_HOSTILITY", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_CREATE:
                        return new SROPacket("GUILD_CREATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_DISBAND:
                        return new SROPacket("GUILD_DISBAND", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_LEAVE:
                        return new SROPacket("GUILD_LEAVE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_INVITE:
                        return new SROPacket("GUILD_INVITE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_KICK:
                        return new SROPacket("GUILD_KICK", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_UPDATE:
                        return new SROPacket("GUILD_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_DONATE_OBSOLETE:
                        return new SROPacket("GUILD_DONATE_OBSOLETE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_CLIENT_UNKNOWN1:
                        return new SROPacket("GUILD_CLIENT_UNKNOWN1", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_UPDATE_NOTICE:
                        return new SROPacket("GUILD_UPDATE_NOTICE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_PROMOTE:
                        return new SROPacket("GUILD_PROMOTE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_UNION_INVITE:
                        return new SROPacket("GUILD_UNION_INVITE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_UNION_LEAVE:
                        return new SROPacket("GUILD_UNION_LEAVE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_UNION_KICK:
                        return new SROPacket("GUILD_UNION_KICK", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_UPDATE_SIEGEAUTH:
                        return new SROPacket("GUILD_UPDATE_SIEGEAUTH", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_ENTITY_UPDATE:
                        return new SROPacket("GUILD_ENTITY_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_ENTITY_REMOVE:
                        return new SROPacket("GUILD_ENTITY_REMOVE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_INFO_BEGIN:
                        return new SROPacket("GUILD_INFO_BEGIN", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_INFO_DATA:
                        return new SROPacket("GUILD_INFO_DATA", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_INFO_END:
                        return new SROPacket("GUILD_INFO_END", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_UNION_INFO:
                        return new SROPacket("GUILD_UNION_INFO", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_ENTITY_UPDATE_SIEGEAUTH:
                        return new SROPacket("GUILD_ENTITY_UPDATE_SIEGEAUTH", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_TRANSFER:
                        return new SROPacket("GUILD_TRANSFER", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_UPDATE_PERMISSION:
                        return new SROPacket("GUILD_UPDATE_PERMISSION", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_ELECTION_START:
                        return new SROPacket("GUILD_ELECTION_START", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_ELECTION_PARTICIPATE:
                        return new SROPacket("GUILD_ELECTION_PARTICIPATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_ELECTION_VOTE:
                        return new SROPacket("GUILD_ELECTION_VOTE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_ELECTION_UPDATE:
                        return new SROPacket("GUILD_ELECTION_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_WAR_INFO:
                        return new SROPacket("GUILD_WAR_INFO", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_WAR_START:
                        return new SROPacket("GUILD_WAR_START", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_WAR_REQUEST:
                        return new SROPacket("GUILD_WAR_REQUEST", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_WAR_END:
                        return new SROPacket("GUILD_WAR_END", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_CLIENT_UNKNOWN2:
                        return new SROPacket("GUILD_CLIENT_UNKNOWN2", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_WAR_REWARD:
                        return new SROPacket("GUILD_WAR_REWARD", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_STORAGE_OPEN:
                        return new SROPacket("GUILD_STORAGE_OPEN", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_STORAGE_CLOSE:
                        return new SROPacket("GUILD_STORAGE_CLOSE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_STORAGE_LIST:
                        return new SROPacket("GUILD_STORAGE_LIST", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_STORAGE_BEGIN:
                        return new SROPacket("GUILD_STORAGE_BEGIN", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_STORAGE_END:
                        return new SROPacket("GUILD_STORAGE_END", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_STORAGE_DATA:
                        return new SROPacket("GUILD_STORAGE_DATA", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_UPDATE_NICKNAME:
                        return new SROPacket("GUILD_UPDATE_NICKNAME", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_ENTITY_UPDATE_NICKNAME:
                        return new SROPacket("GUILD_ENTITY_UPDATE_NICKNAME", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_ENTITY_UPDATE_CREST:
                        return new SROPacket("GUILD_ENTITY_UPDATE_CREST", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_DONATE:
                        return new SROPacket("GUILD_DONATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_MERCENARY_ATTR:
                        return new SROPacket("GUILD_MERCENARY_ATTR", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_MERCENARY_TERMINATE:
                        return new SROPacket("GUILD_MERCENARY_TERMINATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.GUILD_GP_HISTORY:
                        return new SROPacket("GUILD_GP_HISTORY", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region INVENTORY

                    case (ushort)OPCode.Agent.Response.INVENTORY_ENTITY_EQUIP:
                        return new SROPacket("INVENTORY_ENTITY_EQUIP", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.INVENTORY_ENTITY_UNEQUIP:
                        return new SROPacket("INVENTORY_ENTITY_UNEQUIP", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.INVENTORY_UPDATE_ITEM_STATS:
                        return new SROPacket("INVENTORY_UPDATE_ITEM_STATS", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.INVENTORY_ENTITY_EQUIP_TIMER_START:
                        return new SROPacket("INVENTORY_ENTITY_EQUIP_TIMER_START", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.INVENTORY_ENTITY_EQUIP_TIMER_STOP:
                        return new SROPacket("INVENTORY_ENTITY_EQUIP_TIMER_STOP", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.INVENTORY_STORAGE_INFO_BEGIN:
                        return new SROPacket("INVENTORY_STORAGE_INFO_BEGIN", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.INVENTORY_STORAGE_INFO_END:
                        return new SROPacket("INVENTORY_STORAGE_INFO_END", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.INVENTORY_STORAGE_INFO_DATA:
                        return new SROPacket("INVENTORY_STORAGE_INFO_DATA", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.INVENTORY_UPDATE_ITEM_DURABILITY:
                        return new SROPacket("INVENTORY_UPDATE_ITEM_DURABILITY", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.INVENTORY_UPDATE_SIZE:
                        return new SROPacket("INVENTORY_UPDATE_SIZE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.INVENTORY_UPDATE_AMMO:
                        return new SROPacket("INVENTORY_UPDATE_AMMO", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.INVENTORY_OPERATION:
                        return new SROPacket("INVENTORY_OPERATION", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.INVENTORY_STORAGE_OPEN:
                        return new SROPacket("INVENTORY_STORAGE_OPEN", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.INVENTORY_ITEM_REPAIR:
                        return new SROPacket("INVENTORY_ITEM_REPAIR", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.INVENTORY_CLIENT_UNKNOWN:
                        return new SROPacket("INVENTORY_CLIENT_UNKNOWN", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.INVENTORY_ITEM_USE:
                        return new SROPacket("INVENTORY_ITEM_USE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region JOB

                    case (ushort)OPCode.Agent.Response.JOB_UPDATE_PRICE:
                        return new SROPacket("JOB_UPDATE_PRICE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.JOB_JOIN:
                        return new SROPacket("JOB_JOIN", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.JOB_LEAVE:
                        return new SROPacket("JOB_LEAVE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.JOB_ALIAS:
                        return new SROPacket("JOB_ALIAS", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.JOB_RANKING:
                        return new SROPacket("JOB_RANKING", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.JOB_OUTCOME:
                        return new SROPacket("JOB_OUTCOME", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.JOB_PREV_INFO:
                        return new SROPacket("JOB_PREV_INFO", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.JOB_UPDATE_EXP:
                        return new SROPacket("JOB_UPDATE_EXP", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.JOB_COS_DISTANCE:
                        return new SROPacket("JOB_COS_DISTANCE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.JOB_UPDATE_SCALE:
                        return new SROPacket("JOB_UPDATE_SCALE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.JOB_EXPORT_DETAIL:
                        return new SROPacket("JOB_EXPORT_DETAIL", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.JOB_UPDATE_SAFETRADE:
                        return new SROPacket("JOB_UPDATE_SAFETRADE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region LOGOUT

                    case (ushort)OPCode.Agent.Response.LOGOUT:
                        return new SROPacket("LOGOUT", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.LOGOUT_CANCEL:
                        return new SROPacket("LOGOUT_CANCEL", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.LOGUT_SUCCESS:
                        return new SROPacket("LOGUT_SUCCESS", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region MAGICOPTION

                    case (ushort)OPCode.Agent.Response.MAGICOPTION_GRANT:
                        return new SROPacket("MAGICOPTION_GRANT", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region OPERATOR

                    case (ushort)OPCode.Agent.Response.OPERATOR_PUNISHMENT:
                        return new SROPacket("OPERATOR_PUNISHMENT", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.OPERATOR_COMMAND:
                        return new SROPacket("OPERATOR_COMMAND", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region PARTY

                    case (ushort)OPCode.Agent.Response.PARTY_CREATE:
                        return new SROPacket("PARTY_CREATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.PARTY_LEAVE:
                        return new SROPacket("PARTY_LEAVE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.PARTY_INVITE:
                        return new SROPacket("PARTY_INVITE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.PARTY_KICK:
                        return new SROPacket("PARTY_KICK", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.PARTY_UPDATE:
                        return new SROPacket("PARTY_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.PARTY_CREATED:
                        return new SROPacket("PARTY_CREATED", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.PARTY_CREATED_FROM_MATCHING:
                        return new SROPacket("PARTY_CREATED_FROM_MATCHING", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.PARTY_CLIENT_OnJoinPartyAck:
                        return new SROPacket("PARTY_CLIENT_OnJoinPartyAck", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.PARTY_DISTRIBUTION:
                        return new SROPacket("PARTY_DISTRIBUTION", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.PARTY_MATCHING_FORM:
                        return new SROPacket("PARTY_MATCHING_FORM", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.PARTY_MATCHING_CHANGE:
                        return new SROPacket("PARTY_MATCHING_CHANGE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.PARTY_MATCHING_DELETE:
                        return new SROPacket("PARTY_MATCHING_DELETE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.PARTY_MATCHING_LIST:
                        return new SROPacket("PARTY_MATCHING_LIST", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.PARTY_MATCHING_JOIN:
                        return new SROPacket("PARTY_MATCHING_JOIN", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.PARTY_MATCHING_REQUEST:
                        return new SROPacket("PARTY_MATCHING_REQUEST", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region PK

                    case (ushort)OPCode.Agent.Response.PK_UPDATE_PENALTY:
                        return new SROPacket("PK_UPDATE_PENALTY", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.PK_UPDATE_DAILY:
                        return new SROPacket("PK_UPDATE_DAILY", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.PK_UPDATE_LEVEL:
                        return new SROPacket("PK_UPDATE_LEVEL", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region QUEST

                    case (ushort)OPCode.Agent.Response.QUEST_TALK:
                        return new SROPacket("QUEST_TALK", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.QUEST_UPDATE:
                        return new SROPacket("QUEST_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.QUEST_MARK_ADD:
                        return new SROPacket("QUEST_MARK_ADD", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.QUEST_MARK_REMOVE:
                        return new SROPacket("QUEST_MARK_REMOVE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.QUEST_DINGDONG:
                        return new SROPacket("QUEST_DINGDONG", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.QUEST_ABANDON:
                        return new SROPacket("QUEST_ABANDON", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.QUEST_GATHER:
                        return new SROPacket("QUEST_GATHER", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.QUEST_GATHER_CANCEL:
                        return new SROPacket("QUEST_GATHER_CANCEL", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.QUEST_CAPTURE_RESULT:
                        return new SROPacket("QUEST_CAPTURE_RESULT", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.QUEST_NOTIFY:
                        return new SROPacket("QUEST_NOTIFY", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.QUEST_REWARD_TALK:
                        return new SROPacket("QUEST_REWARD_TALK", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.QUEST_REWAD_SELECT:
                        return new SROPacket("QUEST_REWAD_SELECT", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.QUEST_SCRIPT:
                        return new SROPacket("QUEST_SCRIPT", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region SIEGE

                    case (ushort)OPCode.Agent.Response.SIEGE_RETURN:
                        return new SROPacket("SIEGE_RETURN", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.SIEGE_ACTION:
                        return new SROPacket("SIEGE_ACTION", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.SIEGE_UPDATE:
                        return new SROPacket("SIEGE_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region SILK

                    case (ushort)OPCode.Agent.Response.SILK_GACHA_PLAY:
                        return new SROPacket("SILK_GACHA_PLAY", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.SILK_GACHA_EXCHANGE:
                        return new SROPacket("SILK_GACHA_EXCHANGE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.SILK_HISTORY:
                        return new SROPacket("SILK_HISTORY", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.SILK_GACHA_ANNOUNCE:
                        return new SROPacket("SILK_GACHA_ANNOUNCE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.SILK_CLIENT_UNKNOWN:
                        return new SROPacket("SILK_CLIENT_UNKNOWN", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.SILK_UPDATE:
                        return new SROPacket("SILK_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.SILK_NOTIFY:
                        return new SROPacket("SILK_NOTIFY", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region SKILL

                    case (ushort)OPCode.Agent.Response.SKILL_LEARN:
                        return new SROPacket("SKILL_LEARN", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.SKILL_MASTERY_LEARN:
                        return new SROPacket("SKILL_MASTERY_LEARN", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.SKILL_WITHDRAW:
                        return new SROPacket("SKILL_WITHDRAW", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.SKILL_MASTERY_WITHDRAW:
                        return new SROPacket("SKILL_MASTERY_WITHDRAW", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.SKILL_WITHDRAW_INFO_WND:
                        return new SROPacket("SKILL_WITHDRAW_INFO_WND", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region STALL

                    case (ushort)OPCode.Agent.Response.STALL_CREATE:
                        return new SROPacket("STALL_CREATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.STALL_DESTROY:
                        return new SROPacket("STALL_DESTROY", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.STALL_TALK:
                        return new SROPacket("STALL_TALK", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.STALL_BUY:
                        return new SROPacket("STALL_BUY", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.STALL_LEAVE:
                        return new SROPacket("STALL_LEAVE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.STALL_ENTITY_ACTION:
                        return new SROPacket("STALL_ENTITY_ACTION", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.STALL_ENTITY_CREATE:
                        return new SROPacket("STALL_ENTITY_CREATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.STALL_ENTITY_DESTROY:
                        return new SROPacket("STALL_ENTITY_DESTROY", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.STALL_UPDATE:
                        return new SROPacket("STALL_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.STALL_ENTITY_NAME:
                        return new SROPacket("STALL_ENTITY_NAME", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region TAP

                    case (ushort)OPCode.Agent.Response.TAP_INFO:
                        return new SROPacket("TAP_INFO", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.TAP_UPDATE:
                        return new SROPacket("TAP_UPDATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.TAP_ICON:
                        return new SROPacket("TAP_ICON", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #region TELEPORT

                    case (ushort)OPCode.Agent.Response.TELEPORT_DESIGNATE:
                        return new SROPacket("TELEPORT_DESIGNATE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.TELEPORT_USE:
                        return new SROPacket("TELEPORT_USE", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);
                    case (ushort)OPCode.Agent.Response.TELEPORT_CANCEL:
                        return new SROPacket("TELEPORT_CANCEL", packet, PacketSendType.RESPONSE, ServerServiceType.AGENT, PacketSocketType.SERVER);

                    #endregion

                    #endregion

                    #endregion

                    default: return new SROPacket("___UNKNOWN___", packet, PacketSendType.RESPONSE, ServerServiceType.UNKNOWN, PacketSocketType.SERVER);
                }

            } else if (socket == PacketSocketType.CLIENT) {

                switch (packet.Opcode) {

                    #region REQUEST

                    #region GLOBAL-REQUEST

                    case (ushort)OPCode.Global.Request.HANDSHAKE_RESPONSE:
                        return new SROPacket("HANDSHAKE_RESPONSE", packet, PacketSendType.REQUEST, ServerServiceType.GLOBAL, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Global.Request.HANDSHAKE_ACCEPT:
                        return new SROPacket("HANDSHAKE_ACCEPT", packet, PacketSendType.REQUEST, ServerServiceType.GLOBAL, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Global.Request.MODULE_IDENTIFICATION: //encrypted
                        return new SROPacket("MODULE_IDENTIFICATION", packet, PacketSendType.REQUEST, ServerServiceType.GLOBAL, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Global.Request.MODULE_KEEP_ALIVE:
                        return new SROPacket("MODULE_KEEP_ALIVE", packet, PacketSendType.REQUEST, ServerServiceType.GLOBAL, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Global.Request.MODULE_CERTIFICATION_REQUEST:
                        return new SROPacket("MODULE_CERTIFICATION_REQUEST", packet, PacketSendType.REQUEST, ServerServiceType.GLOBAL, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Global.Request.MODULE_CERTIFICATION_RESPONSE:
                        return new SROPacket("MODULE_CERTIFICATION_RESPONSE", packet, PacketSendType.REQUEST, ServerServiceType.GLOBAL, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Global.Request.MODULE_RELAY_REQUEST:
                        return new SROPacket("MODULE_RELAY_REQUEST", packet, PacketSendType.REQUEST, ServerServiceType.GLOBAL, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Global.Request.MODULE_RELAY_RESPONSE:
                        return new SROPacket("MODULE_RELAY_RESPONSE", packet, PacketSendType.REQUEST, ServerServiceType.GLOBAL, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Global.Request.MASSIVE_MESSAGE:
                        return new SROPacket("MASSIVE_MESSAGE", packet, PacketSendType.REQUEST, ServerServiceType.GLOBAL, PacketSocketType.CLIENT);

                    #endregion

                    #region DOWNLOAD-REQUEST
                    case (ushort)OPCode.Download.Request.FILE_COMPLETE:
                        return new SROPacket("FILE_COMPLETE", packet, PacketSendType.REQUEST, ServerServiceType.DOWNLOAD, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Download.Request.FILE_REQUEST:
                        return new SROPacket("FILE_REQUEST", packet, PacketSendType.REQUEST, ServerServiceType.DOWNLOAD, PacketSocketType.CLIENT);

                    #endregion

                    #region GATEWAY-REQUEST

                    case (ushort)OPCode.Gateway.Request.CHECKVERSION:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.GATEWAY, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Gateway.Request.PATCH: //encrypted
                        return new SROPacket("PATCH", packet, PacketSendType.REQUEST, ServerServiceType.GATEWAY, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Gateway.Request.NEWS:
                        return new SROPacket("NEWS", packet, PacketSendType.REQUEST, ServerServiceType.GATEWAY, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Gateway.Request.SERVERLIST: //encrypted
                        return new SROPacket("SERVERLIST", packet, PacketSendType.REQUEST, ServerServiceType.GATEWAY, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Gateway.Request.SERVERLIST_PING:
                        return new SROPacket("SERVERLIST_PING", packet, PacketSendType.REQUEST, ServerServiceType.GATEWAY, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Gateway.Request.LOGIN: //encrypted
                        return new SROPacket("LOGIN", packet, PacketSendType.REQUEST, ServerServiceType.GATEWAY, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Gateway.Request.LOGIN_IBUV_CHALLENGE:
                        return new SROPacket("LOGIN_IBUV_CHALLENGE", packet, PacketSendType.REQUEST, ServerServiceType.GATEWAY, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Gateway.Request.LOGIN_IBUV:
                        return new SROPacket("LOGIN_IBUV", packet, PacketSendType.REQUEST, ServerServiceType.GATEWAY, PacketSocketType.CLIENT);

                    #endregion

                    #region AGENT-REQUEST
                   
                    #region ACADEMY

                    case (ushort)OPCode.Agent.Request.ACADEMY_CREATE:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ACADEMY_DISBAND:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ACADEMY_KICK:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ACADEMY_LEAVE:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ACADEMY_GRADE:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ACADEMY_UPDATE_COMMENT:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ACADEMY_HONOR_RANK:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ACADEMY_MATCHING_REGISTER:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ACCADEMY_MATCHING_CHANGE:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ACCADEMY_MATCHING_DELETE:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ACCADEMY_MATCHING_LIST:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ACCADEMY_MATCHING_JOIN:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ACCADEMY_MATCHING_RESPONSE:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ACADEMY_UNKNOWN1:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ACADEMY_UNKNOWN2:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ACADEMY_UNKNOWN3:
                        return new SROPacket("CHECKVERSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region ALCHEMY

                    case (ushort)OPCode.Agent.Request.ALCHEMY_REINFORCE:
                        return new SROPacket("ALCHEMY_REINFORCE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ALCHEMY_ENCHANT:
                        return new SROPacket("ALCHEMY_ENCHANT", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ALCHEMY_MANUFACTURE:
                        return new SROPacket("ALCHEMY_MANUFACTURE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ALCHEMY_DISMANTLE:
                        return new SROPacket("ALCHEMY_DISMANTLE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.ALCHEMY_SOCKET:
                        return new SROPacket("ALCHEMY_SOCKET", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region AUTHENTICATION

                    case (ushort)OPCode.Agent.Request.AUTH:
                        return new SROPacket("AUTH", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region BATTLE_ARENA

                    case (ushort)OPCode.Agent.Request.BATTLEARENA_REQUEST:
                        return new SROPacket("BATTLEARENA_REQUEST", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                   
                    #endregion

                    #region CAS

                    case (ushort)OPCode.Agent.Request.CAS_CLIENT:
                        return new SROPacket("CAS_CLIENT", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.CAS_SERVER_RESPONSE:
                        return new SROPacket("CAS_SERVER_RESPONSE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region CHARACTER

                    case (ushort)OPCode.Agent.Request.CHARACTER_SELECTION_JOIN:
                        return new SROPacket("CHARACTER_SELECTION_JOIN", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.CHARACTER_SELECTION_ACTION:
                        return new SROPacket("CHARACTER_SELECTION_ACTION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.CHARACTER_SELECTION_RENAME:
                        return new SROPacket("CHARACTER_SELECTION_RENAME", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region CHAT

                    case (ushort)OPCode.Agent.Request.CHAT:
                        return new SROPacket("CHAT", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region COMMUNITY

                    case (ushort)OPCode.Agent.Request.COMMUNITY_FRIEND_ADD:
                        return new SROPacket("COMMUNITY_FRIEND_ADD", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.COMMUNITY_FRIEND_RESPONSE:
                        return new SROPacket("COMMUNITY_FRIEND_RESPONSE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.COMMUNITY_FRIEND_DELETE:
                        return new SROPacket("COMMUNITY_FRIEND_DELETE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.COMMUNITY_MEMO_OPEN:
                        return new SROPacket("COMMUNITY_MEMO_OPEN", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.COMMUNITY_MEMO_SEND:
                        return new SROPacket("COMMUNITY_MEMO_SEND", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.COMMUNITY_MEMO_DELETE:
                        return new SROPacket("COMMUNITY_MEMO_DELETE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.COMMUNITY_MEMO_LIST:
                        return new SROPacket("COMMUNITY_MEMO_LIST", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.COMMUNITY_MEMO_SEND_GROUP:
                        return new SROPacket("COMMUNITY_MEMO_SEND_GROUP", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.COMMUNITY_BLOCK:
                        return new SROPacket("COMMUNITY_BLOCK", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region CONFIG

                    case (ushort)OPCode.Agent.Request.CONFIG_UPDATE:
                        return new SROPacket("CONFIG_UPDATE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region CONSIGNMENT

                    case (ushort)OPCode.Agent.Request.CONSIGNMENT_DETAIL:
                        return new SROPacket("CONSIGNMENT_DETAIL", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.CONSIGNMENT_CLOSE:
                        return new SROPacket("CONSIGNMENT_CLOSE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.CONSIGNMENT_REGISTER:
                        return new SROPacket("CONSIGNMENT_REGISTER", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.CONSIGNMENT_UNREGISTER:
                        return new SROPacket("CONSIGNMENT_UNREGISTER", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.CONSIGNMENT_BUY:
                        return new SROPacket("CONSIGNMENT_BUY", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.CONSIGNMENT_SETTLE:
                        return new SROPacket("CONSIGNMENT_SETTLE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.CONSIGNMENT_SEARCH:
                        return new SROPacket("CONSIGNMENT_SEARCH", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.CONSIGNMENT_LIST:
                        return new SROPacket("CONSIGNMENT_LIST", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region COS

                    case (ushort)OPCode.Agent.Request.COS_COMMAND:
                        return new SROPacket("COS_COMMAND", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.COS_TERMINATE:
                        return new SROPacket("COS_TERMINATE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.COS_UPDATE_RIDESTATE:
                        return new SROPacket("COS_UPDATE_RIDESTATE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.COS_UNSUMMON:
                        return new SROPacket("COS_UNSUMMON", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.COS_NAME:
                        return new SROPacket("COS_NAME", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.COS_UPDATE_SETTINGS:
                        return new SROPacket("COS_UPDATE_SETTINGS", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.COS_UNKNOWN1:
                        return new SROPacket("COS_UNKNOWN1", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region EXCHANGE

                    case (ushort)OPCode.Agent.Request.EXCHANGE_START:
                        return new SROPacket("EXCHANGE_START", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.EXCHANGE_CONFIRM:
                        return new SROPacket("EXCHANGE_CONFIRM", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.EXCHANGE_APPROVE:
                        return new SROPacket("EXCHANGE_APPROVE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.EXCHANGE_CANCEL:
                        return new SROPacket("EXCHANGE_CANCEL", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region FGW

                    case (ushort)OPCode.Agent.Request.FGW_RECALL_LIST:
                        return new SROPacket("FGW_RECALL_LIST", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.FGW_RECALL_MEMBER:
                        return new SROPacket("FGW_RECALL_MEMBER", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.FGW_RECALL_RESPONSE:
                        return new SROPacket("FGW_RECALL_RESPONSE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.FGW_EXIT:
                        return new SROPacket("FGW_EXIT", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region CTF

                    case (ushort)OPCode.Agent.Request.FLAGWAR_REGISTER:
                        return new SROPacket("FLAGWAR_REGISTER", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region FRPVP

                    case (ushort)OPCode.Agent.Request.FRPVP_UPDATE:
                        return new SROPacket("FRPVP_UPDATE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region GAME

                    case (ushort)OPCode.Agent.Request.GAME_READY:
                        return new SROPacket("GAME_READY", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GAME_INVITE:
                        return new SROPacket("GAME_INVITE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GAME_RESET_COMPLETE:
                        return new SROPacket("GAME_RESET_COMPLETE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region GUIDE

                    case (ushort)OPCode.Agent.Request.GUIDE:
                        return new SROPacket("GUIDE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region GUILD

                    case (ushort)OPCode.Agent.Request.GUILD_CREATE:
                        return new SROPacket("GUILD_CREATE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_DISBAND:
                        return new SROPacket("GUILD_DISBAND", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_LEAVE:
                        return new SROPacket("GUILD_LEAVE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_INVITE:
                        return new SROPacket("GUILD_INVITE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_KICK:
                        return new SROPacket("GUILD_KICK", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_DONATE_OBSOLETE:
                        return new SROPacket("GUILD_DONATE_OBSOLETE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_UPDATE_NOTICE:
                        return new SROPacket("GUILD_UPDATE_NOTICE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_PROMOTE:
                        return new SROPacket("GUILD_PROMOTE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_UNION_INVITE:
                        return new SROPacket("GUILD_UNION_INVITE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_UNION_LEAVE:
                        return new SROPacket("GUILD_UNION_LEAVE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_UNION_KICK:
                        return new SROPacket("GUILD_UNION_KICK", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_UPDATE_SIEGEAUTH:
                        return new SROPacket("GUILD_UPDATE_SIEGEAUTH", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_TRANSFER:
                        return new SROPacket("GUILD_TRANSFER", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_UPDATE_PERMISSION:
                        return new SROPacket("GUILD_UPDATE_PERMISSION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_ELECTION_START:
                        return new SROPacket("GUILD_ELECTION_START", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_ELECTION_PARTICIPATE:
                        return new SROPacket("GUILD_ELECTION_PARTICIPATE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_ELECTION_VOTE:
                        return new SROPacket("GUILD_ELECTION_VOTE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_WAR_START:
                        return new SROPacket("GUILD_WAR_START", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_WAR_END:
                        return new SROPacket("GUILD_WAR_END", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_WAR_REWARD:
                        return new SROPacket("GUILD_WAR_REWARD", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_STORAGE_OPEN:
                        return new SROPacket("GUILD_STORAGE_OPEN", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_STORAGE_CLOSE:
                        return new SROPacket("GUILD_STORAGE_CLOSE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_STORAGE_LIST:
                        return new SROPacket("GUILD_STORAGE_LIST", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_UPDATE_NICKNAME:
                        return new SROPacket("GUILD_UPDATE_NICKNAME", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_DONATE:
                        return new SROPacket("GUILD_DONATE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_MERCENARY_ATTR:
                        return new SROPacket("GUILD_MERCENARY_ATTR", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_MERCENARY_TERMINATE:
                        return new SROPacket("GUILD_MERCENARY_TERMINATE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.GUILD_GP_HISTORY:
                        return new SROPacket("GUILD_GP_HISTORY", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region INVENTORY

                    case (ushort)OPCode.Agent.Request.INVENTORY_OPERATION:
                        return new SROPacket("INVENTORY_OPERATION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.INVENTORY_STORAGE_OPEN:
                        return new SROPacket("INVENTORY_STORAGE_OPEN", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.INVENTORY_ITEM_REPAIR:
                        return new SROPacket("INVENTORY_ITEM_REPAIR", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.INVENTORY_ITEM_USE:
                        return new SROPacket("INVENTORY_ITEM_USE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region JOB

                    case (ushort)OPCode.Agent.Request.JOB_JOIN:
                        return new SROPacket("JOB_JOIN", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.JOB_LEAVE:
                        return new SROPacket("JOB_LEAVE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.JOB_ALIAS:
                        return new SROPacket("JOB_ALIAS", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.JOB_RANKING:
                        return new SROPacket("JOB_RANKING", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.JOB_OUTCOME:
                        return new SROPacket("JOB_OUTCOME", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.JOB_PREV_INFO:
                        return new SROPacket("JOB_PREV_INFO", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.JOB_EXPORT_DETAIL:
                        return new SROPacket("JOB_EXPORT_DETAIL", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region LOGOUT

                    case (ushort)OPCode.Agent.Request.LOGOUT:
                        return new SROPacket("LOGOUT", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.LOGOUT_CANCEL:
                        return new SROPacket("LOGOUT_CANCEL", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region MAGICOPTION

                    case (ushort)OPCode.Agent.Request.MAGICOPTION_GRANT:
                        return new SROPacket("MAGICOPTION_GRANT", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                   
                    #endregion

                    #region OPERATOR

                    case (ushort)OPCode.Agent.Request.OPERATOR_COMMAND:
                        return new SROPacket("OPERATOR_COMMAND", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region PARTY

                    case (ushort)OPCode.Agent.Request.PARTY_CREATE:
                        return new SROPacket("PARTY_CREATE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.PARTY_LEAVE:
                        return new SROPacket("PARTY_LEAVE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.PARTY_INVITE:
                        return new SROPacket("PARTY_INVITE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.PARTY_KICK:
                        return new SROPacket("PARTY_KICK", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.PARTY_MATCHING_FORM:
                        return new SROPacket("PARTY_MATCHING_FORM", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.PARTY_MATCHING_CHANGE:
                        return new SROPacket("PARTY_MATCHING_CHANGE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.PARTY_MATCHING_DELETE:
                        return new SROPacket("PARTY_MATCHING_DELETE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.PARTY_MATCHING_LIST:
                        return new SROPacket("PARTY_MATCHING_LIST", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.PARTY_MATCHING_JOIN:
                        return new SROPacket("PARTY_MATCHING_JOIN", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region QUEST

                    case (ushort)OPCode.Agent.Request.QUEST_TALK:
                        return new SROPacket("QUEST_TALK", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.QUEST_DINGDONG:
                        return new SROPacket("QUEST_DINGDONG", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.QUEST_ABANDON:
                        return new SROPacket("QUEST_ABANDON", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.QUEST_GATHER_CANCEL:
                        return new SROPacket("QUEST_GATHER_CANCEL", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.QUEST_REWAD_SELECT:
                        return new SROPacket("QUEST_REWAD_SELECT", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region SIEGE

                    case (ushort)OPCode.Agent.Request.SIEGE_RETURN:
                        return new SROPacket("SIEGE_RETURN", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.SIEGE_ACTION:
                        return new SROPacket("SIEGE_ACTION", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    #endregion

                    #region SILK

                    case (ushort)OPCode.Agent.Request.SILK_GACHA_PLAY:
                        return new SROPacket("SILK_GACHA_PLAY", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.SILK_GACHA_EXCHANGE:
                        return new SROPacket("SILK_GACHA_EXCHANGE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.SILK_HISTORY:
                        return new SROPacket("SILK_HISTORY", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.SILK_CLIENT_UNKNOWN_1:
                        return new SROPacket("SILK_CLIENT_UNKNOWN_1", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    #endregion

                    #region SKILL

                    case (ushort)OPCode.Agent.Request.SKILL_LEARN:
                        return new SROPacket("SKILL_LEARN", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.SKILL_MASTERY_LEARN:
                        return new SROPacket("SKILL_MASTERY_LEARN", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.SKILL_WITHDRAW:
                        return new SROPacket("SKILL_WITHDRAW", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.SKILL_MASTERY_WITHDRAW:
                        return new SROPacket("SKILL_MASTERY_WITHDRAW", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    #endregion

                    #region STALL

                    case (ushort)OPCode.Agent.Request.STALL_CREATE:
                        return new SROPacket("STALL_CREATE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.STALL_DESTROY:
                        return new SROPacket("STALL_DESTROY", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.STALL_TALK:
                        return new SROPacket("STALL_TALK", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.STALL_BUY:
                        return new SROPacket("STALL_BUY", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.STALL_LEAVE:
                        return new SROPacket("STALL_LEAVE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.STALL_UPDATE:
                        return new SROPacket("STALL_UPDATE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #region TAP

                    case (ushort)OPCode.Agent.Request.TAP_INFO:
                        return new SROPacket("TAP_INFO", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.TAP_UPDATE:
                        return new SROPacket("TAP_UPDATE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    #endregion

                    #region TELEPORT

                    case (ushort)OPCode.Agent.Request.TELEPORT_DESIGNATE:
                        return new SROPacket("TELEPORT_DESIGNATE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.TELEPORT_USE:
                        return new SROPacket("TELEPORT_USE", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);
                    case (ushort)OPCode.Agent.Request.TELEPORT_CANCEL:
                        return new SROPacket("TELEPORT_CANCEL", packet, PacketSendType.REQUEST, ServerServiceType.AGENT, PacketSocketType.CLIENT);

                    #endregion

                    #endregion

                    #endregion

                    default: return new SROPacket("___UNKNOWN___", packet, PacketSendType.REQUEST, ServerServiceType.UNKNOWN, PacketSocketType.CLIENT);
                }
            } else {
                return new SROPacket("___NOTHING___", packet);
            }
        }
    }
}
