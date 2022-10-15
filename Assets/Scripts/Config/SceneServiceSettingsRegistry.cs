using Data.Settings;
using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "SceneServiceSettingsRegistry", menuName = "Registry/Scene Service Settings Registry")]
    public class SceneServiceSettingsRegistry : RegistryListBase<SceneServiceSettings>
    {
        
    }
}
