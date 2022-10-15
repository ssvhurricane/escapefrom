using UnityEngine;
using View;

namespace Services.Window
{
    public interface IWindow : IView
    { 
        IWindowArgs Arguments { get; set; }
        WindowType WindowType { get; set; }
    }

    public interface IWindowInput{ }

    public interface IAnimatedWindow : IWindow { }

    public interface IWindowWithInput<in TWindowInput> : IWindow 
    {
        void Show(TWindowInput data);
    }

    public interface IAnimatedWindowWithInput<in TWindowInput>:IAnimatedWindow, IWindowWithInput<TWindowInput>
        where TWindowInput : IWindowInput
    {

    }
}
