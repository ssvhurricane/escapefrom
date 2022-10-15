using Services.Window;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    [Window("Resources/Prefabs/Windows/GameSettingsView", WindowType.PopUpWindow)]
    public class GameSettingsView : PopUpWindow
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] public Button _backButton;

        private SignalBus _signalBus;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            WindowType = Type;

            _signalBus.Fire(new WindowServiceSignals.Register(this));
        }
    }
}