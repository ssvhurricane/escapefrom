using Config;
using System;

namespace Data.Settings
{
    [Serializable]
    public class ResourcesServiceSettings : IRegistryData
    {
        public string Id;
        public string Name;
        string IRegistryData.Id => Id;
    }
}