using System;

namespace Services.Window
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WindowAttribute : Attribute
    {
        public readonly string PrefabPath;
        public readonly WindowType WindowType;
        public readonly int Layer;

        public WindowAttribute(string prefabPath, WindowType windowType = WindowType.BaseWindow, int layer = 0)
        {
            PrefabPath = prefabPath;

            WindowType = windowType;

            Layer = layer;
        }
    }
}