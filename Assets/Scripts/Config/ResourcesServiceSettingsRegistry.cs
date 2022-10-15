using Data.Settings;
using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "ResourcesServiceSettingsRegistry", menuName = "Registry/Resources Service Settings Registry")]
    public class ResourcesServiceSettingsRegistry : RegistryBase<ResourcesServiceSettings>
    {
        
        
    }
}