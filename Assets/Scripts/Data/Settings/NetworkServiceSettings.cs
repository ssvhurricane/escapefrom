using System;
using Config;
using Services.Network;

namespace Data.Settings
{
    [Serializable]
    public class NetworkServiceSettings : IRegistryData
    {
        public string Id;

        public NetworkType NetworkType;

        public NetworkEngine NetworkEngine;

        public NetworkAuthMode NetworkAuthMode;

        public NetworkContextType NetworkContextType;

        string IRegistryData.Id => Id;
    }
}
