using Services.Input;
using UnityEngine;

namespace Signals
{
    public class InputServiceSignals 
    {
        public class Action
        {
            public ActionType ActionType { get; }

            public KeyCode PCKeyCode { get; }

            public KeyCode JoystickKeyCode { get; }

            public ActionModifier ActionModifier { get; }

            public Action(ActionType actionType, KeyCode pCKeyCode, KeyCode joystickKeyCode, ActionModifier actionModifier)
            {
                ActionType = actionType;
                PCKeyCode = pCKeyCode;
                JoystickKeyCode = joystickKeyCode;
                ActionModifier = actionModifier;
            }
        }
    }
}
