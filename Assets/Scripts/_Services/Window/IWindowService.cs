using System;

namespace Services.Window
{
    public interface IWindowService
    {
        IWindow GetWindow<TWindow>() where TWindow : class, IWindow; 
        
        bool IsWindowShowing<TWindow>() where TWindow : class, IWindow;

        bool IsWindowShowing(Type type);

        IWindow ShowWindow<TWindow>(IWindowArgs windowArgs = null) where TWindow: class, IWindow; 

        IWindow ShowWindow(Type type, IWindowArgs windowArgs = null);

        void ShowWindowWithInput<TWindow, TInput>(TInput input, IWindowArgs windowArgs = null) where TInput : IWindowInput where TWindow : class, IWindowWithInput<TInput>;

        void HideWindow<TWindow>() where TWindow : class, IWindow; 

        void HideWindow(Type type);

        void HideAllWindows();

        void ClearServiceValues();
    }
}