using Config;
using System;
using UnityEngine;

namespace Data.Settings
{
    [Serializable]
    public class PlayerSettings : IRegistryData
    {
        public string Id;
        public string Name;
        string IRegistryData.Id => Id;
    }
}
