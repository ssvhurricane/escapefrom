using Data.Settings;
using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "PoolServiceSettingsRegistry", menuName = "Registry/Pool Service Settings Registry")]
    public class PoolServiceSettingsRegistry : RegistryListBase<PoolServiceSettings>
    {
       
    }
}