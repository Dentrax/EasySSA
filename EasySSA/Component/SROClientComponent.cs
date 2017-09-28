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

using EasySSA.SSA;
using EasySSA.Common;
using EasySSA.Context;
using EasySSA.Packets;
using System.Net.Sockets;

namespace EasySSA.Component {
    public sealed class SROClientComponent : IDisposable {

        public Action<SROClient, ClientStatusType> OnClientStatusChanged;

        public Action<SROClient, AccountStatusType> OnAccountStatusChanged;

        public Action<SROClient, bool> OnCaptchaStatusChanged;

        public Action<SROClient, bool> OnCharacterLogin;

        public Action<SROClient, bool> OnSocketConnected;

        public Action<SROClient, ClientDisconnectType> OnSocketDisconnected;

        public Action<SocketError> OnLocalSocketStatusChanged;

        public Action<SROClient, SocketError> OnServiceSocketStatusChanged;

        public Func<SROClient, SROPacket, PacketSocketType, PacketResult> OnPacketReceived;

        public int ClientIndex { get; private set; }

        public Account Account { get; private set; }

        public string ClientPath { get; private set; }

        public string Captcha { get; private set; }

        public byte LocaleID { get; private set; }

        public byte VersionID { get; private set; }

        public ushort ServerID { get; private set; }

        public bool IsClientless { get; private set; }
        public string CharacterName { get; private set; }

        public Fingerprint Fingerprint { get; private set; }

        public IPEndPoint LocalAgentEndPoint { get; private set; }
        public IPEndPoint LocalGatewayEndPoint { get; private set; }

        public IPEndPoint ServiceEndPoint { get; private set; }

        public int BindTimeout { get; private set; }

        public bool IsDebugMode { get; private set; }

        private bool m_wasDisposed;

        public SROClientComponent(int clientIndex) {
            this.ClientIndex = clientIndex;
            this.BindTimeout = 10;
            this.IsDebugMode = false;
        }

        #region Functions

        public SROClientComponent SetFingerprint(Fingerprint fingerprint) {
            this.Fingerprint = fingerprint;
            return this;
        }

        public SROClientComponent SetClientless(bool clientless) {
            this.IsClientless = clientless;
            return this;
        }

        public SROClientComponent SetCaptcha(string captcha) {
            this.Captcha = captcha;
            return this;
        }

        public SROClientComponent SetLocaleID(byte locale) {
            this.LocaleID = locale;
            return this;
        }

        public SROClientComponent SetVersionID(byte version) {
            this.VersionID = version;
            return this;
        }

        public SROClientComponent SetServerID(ushort server) {
            this.ServerID = server;
            return this;
        }

        public SROClientComponent SetLocalAgentEndPoint(IPEndPoint endpoint) {
            this.LocalAgentEndPoint = endpoint;
            return this;
        }

        public SROClientComponent SetLocalGatewayEndPoint(IPEndPoint endpoint) {
            this.LocalGatewayEndPoint = endpoint;
            return this;
        }

        public SROClientComponent SetServiceEndPoint(IPEndPoint endpoint) {
            this.ServiceEndPoint = endpoint;
            return this;
        }

        public SROClientComponent SetAccount(Account account, string characterName) {
            this.Account = account;
            this.CharacterName = characterName;
            return this;
        }

        public SROClientComponent SetClientPath(string path) {
            this.ClientPath = path;
            return this;
        }

        public SROClientComponent SetBindTimeout(int timeout) {
            this.BindTimeout = timeout;
            return this;
        }

        public SROClientComponent SetDebugMode(bool debug) {
            this.IsDebugMode = debug;
            return this;
        }

        public void DOBind(Action<bool, BindErrorType> callback = null) {


        }

        public void Dispose() {
            this.Dispose(true);
        }

        private void Dispose(bool disposing) {
            if (!m_wasDisposed) {
                if (disposing) {
                    //
                }
                //this.ServiceServer.Dispose();

                m_wasDisposed = true;
            }

        }

        #endregion

        #region Packets

        public Packet GetCaptchaPacket() {
            Packet packet = new Packet(0x6323);
            packet.WriteAscii(Captcha);
            packet.Lock();
            return packet;
        }

        public Packet GetLoginPacket() {
            Packet packet = new Packet(0x6102, true);
            packet.WriteByte(this.LocaleID);
            packet.WriteAscii(this.Account.Username);
            packet.WriteAscii(this.Account.Password);
            packet.WriteUShort(this.ServerID);
            packet.Lock();
            return packet;
        }

        public Packet GetCharacterSelectionPacket() {
            Packet packet = new Packet(0x7001);
            packet.WriteAscii(this.CharacterName);
            packet.Lock();
            return packet;
        }

        #endregion

    }
}
