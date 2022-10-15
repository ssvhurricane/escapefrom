using System;
using Config;
using Data.Settings;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NetworkServiceSettingsRegistry", menuName = "Registry/Network Service Settings Registry")]
public class NetworkServiceSettingsRegistry : RegistryBase<NetworkServiceSettings>
{
    
}
