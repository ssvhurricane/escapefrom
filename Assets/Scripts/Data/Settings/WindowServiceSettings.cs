using Config;
using Services.Window;
using System;

namespace Data.Settings
{
    public enum ScreenType
    {
        None,
        Window,
        Hint, 
        Widget
    }

    [Serializable]
    public class WindowServiceSettings : IRegistryData
    {
        public string Id;
        public string Name;

        public CanvasData[] CanvasDatas;
        public ScreensData[] ScreensDatas;
        string IRegistryData.Id => Id;
    }

    [Serializable]
    public class CanvasData
    {
        public WindowType WindowType;
        public int SortingOrder;
    }

    [Serializable]
    public class ScreensData
    {
        public ScreenType ScreenType;
    }
}
