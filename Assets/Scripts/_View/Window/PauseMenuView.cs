using Services.Window;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    public class PauseMenuView : PopUpWindow
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] public Button _backToGameButton;
        [SerializeField] public Button _settingsButton;
        [SerializeField] public Button _quitMainMenuButton;

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