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

namespace EasySSA.Services {
    public sealed class ServiceProviderLocator<ServiceProviderType> {
        public static readonly ServiceProviderLocator<ServiceProviderType> Instance = new ServiceProviderLocator<ServiceProviderType>();

        private readonly Dictionary<ServiceProviderType, IServiceProvider> m_providers;

        public bool IsBooted { get; private set; }

        private ServiceProviderLocator() {
            this.IsBooted = false;
            this.m_providers = new Dictionary<ServiceProviderType, IServiceProvider>();
        }

        public static bool Boot(Dictionary<ServiceProviderType, IServiceProvider> providers, Action onDone = null) {
            if (ServiceProviderLocator<ServiceProviderType>.Instance.IsBooted) {
                throw new Exception("[ServiceProviderLocator::Boot()] -> Providers already booted!");
            }

            ServiceProviderLocator<ServiceProviderType>.Instance.m_providers.Clear();

            try {
                ServiceProviderLocator<ServiceProviderType>.Instance.SetupProviders(providers);
                ServiceProviderLocator<ServiceProviderType>.Instance.StartProviders();
                ServiceProviderLocator<ServiceProviderType>.Instance.IsBooted = true;

                if (onDone != null) onDone();

                return true;
            } catch {
                return false;
            }
        }

        public T GetProvider<T>(ServiceProviderType providerType) {
            if (this.m_providers.ContainsKey(providerType)) {
                return (T)((object)this.m_providers[providerType]);
            }
            throw new Exception("[ServiceProviderLocator::GetProvider()] -> Provider(" + providerType.ToString() + ") not ready.");
        }

        private void SetupProviders(Dictionary<ServiceProviderType, IServiceProvider> providers) {
            foreach (KeyValuePair<ServiceProviderType, IServiceProvider> current in providers) {
                if (ServiceProviderLocator<ServiceProviderType>.Instance.m_providers.ContainsKey(current.Key)) {
                    throw new Exception("[ServiceProviderLocator::SetupProviders()] -> Provider already loaded for: " + current.Key);
                }
                ServiceProviderLocator<ServiceProviderType>.Instance.m_providers.Add(current.Key, current.Value);
            }
        }

        private void StartProviders() {
            foreach (KeyValuePair<ServiceProviderType, IServiceProvider> current in this.m_providers) {
                current.Value.Start();
            }
        }

        public void StopProviders() {
            foreach (KeyValuePair<ServiceProviderType, IServiceProvider> current in this.m_providers) {
                current.Value.Stop();
            }
        }
    }
}
