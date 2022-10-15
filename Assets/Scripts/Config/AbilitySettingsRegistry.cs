using Data.Settings;
using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "AbilitySettingsRegistry", menuName = "Registry/Ability Settings Registry")]
    public class AbilitySettingsRegistry : RegistryListBase<AbilitySettings>
    {

    }
}
