using Config;
using Services.Log;
using System;

namespace Data.Settings
{
    [Serializable]
    public class LogServiceSettings : IRegistryData
    {
        public string Id;

        public bool EnableAll;

        public LogItemData[] LogItemDatas;
        string IRegistryData.Id => Id;
    }
}
