using Services.Window;
using Signals;
using UnityEngine;
using Zenject;

namespace View.Window
{
    [Window("Resources/Prefabs/Windows/CheatSettingsView", WindowType.BaseWindow)]
    public class CheatSettingsView : BaseWindow
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected CreationMethod Method;

        private SignalBus _signalBus;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            WindowType = Type;

            CreationMethod = Method;

            _signalBus.Fire(new WindowServiceSignals.Register(this));
        }
    }
}
