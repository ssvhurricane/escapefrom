using Data.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "InputServiceSettingsRegistry", menuName = "Registry/Input Service Settings Registry")]
    public class InputServiceSettingsRegistry : RegistryListBase<InputServiceSettings>
    {

    }
}