using Data.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "ProjectServiceSettingsRegistry", menuName = "Registry/Project Service Settings Registry")]
    public class ProjectServiceSettingsRegistry : RegistryBase<ProjectServiceSettings>
    {
       
    }
}