using Data.Settings;
using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = " CameraServiceSettingsRegistry", menuName = "Registry/ Camera Service Settings Registry")]
    public class CameraServiceSettingsRegistry : RegistryListBase<CameraServiceSettings>
    {

    }
}
