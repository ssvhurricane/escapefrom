using Services.Window;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace View.Window
{
    public class MainMenuView : BaseWindow
    {
       [SerializeField] protected WindowType Type;
       [SerializeField] public Button _startButton;
       [SerializeField] public Button _settingsButton;
       [SerializeField] public Button _quitButton;

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