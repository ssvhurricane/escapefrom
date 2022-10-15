using Config;
using System;

namespace Data.Settings
{
    [Serializable]
    public class InputServiceSettings : IRegistryData
    {
        public string Id;
        string IRegistryData.Id => Id;
    }

}
