using Data.Settings;
using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "ItemServiceSettingsRegistry", menuName = "Registry/Item Service Settings Registry")]
    public class ItemServiceSettingsRegistry : RegistryListBase<ItemServiceSettings>
    {

    }
}
