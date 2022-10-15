using Data.Settings;
using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "WindowServiceSettingsRegistry", menuName = "Registry/Window Service Settings Registry")]
    public class WindowServiceSettingsRegistry : RegistryListBase<WindowServiceSettings>
    {
    }
}
