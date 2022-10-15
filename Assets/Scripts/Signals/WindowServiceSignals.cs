using Services.Window;
using UnityEngine;

namespace Signals
{
    public class WindowServiceSignals
    {
        public class Register 
        {
            public IWindow Window { get; set; }

            public Register(IWindow window) 
            {
                Window = window;
            }
        }

        public class AddHolder 
        {
            public Transform Transform { get; }

            public WindowType WindowType { get; }

            public AddHolder(Transform transform, WindowType windowType)
            {
                Transform = transform;
                WindowType = windowType;
            }
        }

        public class Shown 
        {
            public IWindow Window { get; }
        
            public Shown(IWindow window) 
            {
                Window = window;
            }
        }

        public class Hidden 
        {
            public IWindow Window { get; }
          
            public Hidden(IWindow window)
            {
                Window = window;
            }
        }
    }
}
