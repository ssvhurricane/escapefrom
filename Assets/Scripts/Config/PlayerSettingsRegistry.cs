using Data.Settings;
using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "PlayerSettingsRegistry", menuName = "Registry/Player Settings Registry")]
    public class PlayerSettingsRegistry : RegistryBase<PlayerSettings>
    {

    }
}