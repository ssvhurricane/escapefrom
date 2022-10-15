using System;

namespace Services.Window
{
    public interface IWindowAnimation
    {
        void AnimateShow(Action callback);

        void AnimateHide(Action callback);
    }
}
