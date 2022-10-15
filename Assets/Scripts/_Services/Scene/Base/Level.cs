using System;

namespace Services.Scene
{
    [Serializable]
    public class Level
    {
        public string Name;
        public string ScenePath;
        public bool IsSingleScene;
        public bool Additive;
    }
}