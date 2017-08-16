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
using System.Linq;
using EasySSA.Services.PacketService;
using EasySSA.Services.ServerService;

namespace EasySSA.Services {
    public sealed class CoreServices {
        private Dictionary<ServiceProviderType, IServiceProvider> m_services = new Dictionary<ServiceProviderType, IServiceProvider>();

        public bool WasInitialized { get; private set; }

        public bool IsBooted { get; private set; }

        public CoreServices() {
            this.WasInitialized = false;
            this.IsBooted = false;

            this.m_services.Add(ServiceProviderType.Server, new ServerProvider());
            this.m_services.Add(ServiceProviderType.Packet, new PacketProvider());
        }

        public void Initialize(Dictionary<ServiceProviderType, ServiceProvider.InitializeContextBase> initDict) {
            if (this.WasInitialized) {
                throw new Exception("[CoreServices::Initialize()] -> CoreServices already initialized");
            }

            if (m_services.Count != initDict.Count) {
                throw new Exception("[CoreServices::Initialize()] -> m_service count != initDict count");
            }

            if (!m_services.Keys.SequenceEqual(initDict.Keys)) {
                throw new Exception("[CoreServices::Initialize()] -> Keys are not sequential equal");
            }

            foreach (KeyValuePair<ServiceProviderType, ServiceProvider.InitializeContextBase> init in initDict) {
                if (this.m_services.Count <= 0) {
                    throw new Exception("[CoreServices::Initialize()] -> Service list is empty");
                }
                if (!this.IsServiceListContains(init.Key)) {
                    throw new Exception("[CoreServices::Initialize()] -> Service list not contains : " + init.Key);
                }
                if (init.Value == null) {
                    throw new Exception("[CoreServices::Initialize()] -> Null init value received : " + init.Key);
                }

                this.m_services[init.Key].Initialize(init.Value);
            }

            this.WasInitialized = true;
        }

        public void Boot() {
            if (this.IsBooted) {
                throw new Exception("[CoreServices::Boot()] -> CoreServices already booted!");
            }

            if (!this.WasInitialized) {
                throw new Exception("[CoreServices::Boot()] -> You should initialize first!");
            }

            ServiceProviderLocator<ServiceProviderType>.Boot(m_services, delegate {
                this.IsBooted = true;
            });
        }

        public bool IsAllServicesStarted() {
            foreach (KeyValuePair<ServiceProviderType, IServiceProvider> service in m_services) {
                if (!service.Value.IsStarted) {
                    return false;
                }
            }
            return true;
        }

        private bool IsServiceListContains(ServiceProviderType provider) {
            return this.m_services.ContainsKey(provider);
        }
    }
}
