using Config;
using Services.Scene;
using System;
using UnityEngine;

namespace Data.Settings
{
    [Serializable]
    public class SceneServiceSettings : IRegistryData
    {
        [SerializeField]
        public Level Level;
        public string Id 
         {
                get { return Level.Name; }
         }

        public override string ToString()
        {
            return string.Format("(LevelData, Id{0}, Path{1})", Id, Level.ScenePath);
        }
    }
}