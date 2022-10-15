using Data.Settings;
using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "MovementServiceSettingsRegistry", menuName = "Registry/Movement Service Settings Registry")]
    public class MovementServiceSettingsRegistry : RegistryListBase<MovementServiceSettings>
    {
      
    }
}