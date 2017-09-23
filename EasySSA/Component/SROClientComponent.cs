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

namespace EasySSA.Component {
    public sealed class SROClientComponent : IDisposable {

        public int ClientIndex { get; private set; }

        public Account Account { get; private set; }

        public string ClientPath { get; private set; }

        public string Captcha { get; private set; }

        public Fingerprint Fingerprint { get; private set; }

        public IPEndPoint LocalEndPoint { get; private set; }
        public IPEndPoint GatewayEndPoint { get; private set; }
        public IPEndPoint DownloadEndPoint { get; private set; }
        public IPEndPoint AgentEndPoint { get; private set; }

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

        public SROClientComponent SetCaptcha(string captcha) {
            this.Captcha = captcha;
            return this;
        }

        public SROClientComponent SetLocalEndPoint(IPEndPoint endpoint) {
            this.LocalEndPoint = endpoint;
            return this;
        }

        public SROClientComponent SetGatewayEndPoint(IPEndPoint endpoint) {
            this.GatewayEndPoint = endpoint;
            return this;
        }

        public SROClientComponent SetDownloadEndPoint(IPEndPoint endpoint) {
            this.DownloadEndPoint = endpoint;
            return this;
        }

        public SROClientComponent SetAgentEndPoint(IPEndPoint endpoint) {
            this.AgentEndPoint = endpoint;
            return this;
        }

        public SROClientComponent SetAccount(Account account) {
            this.Account = account;
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

    }
}
