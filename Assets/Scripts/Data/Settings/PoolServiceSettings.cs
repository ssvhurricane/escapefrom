using Config;
using System;

namespace Data.Settings
{
    [Serializable]
    public class PoolServiceSettings : IRegistryData
    {
        public string Id;

        public string Name;

        public int InitialSize;

        public int MaxPoolSize;
        string IRegistryData.Id => Id;
    }
}
