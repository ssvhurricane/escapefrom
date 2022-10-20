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

        [SerializeField] protected Slider LookSensitivitySlider;

        [SerializeField] protected Toggle AudioEnabledToggle;

        [SerializeField] protected Toggle FramerateCounterEnabledToggle;

        [SerializeField] protected Toggle ShadowsEnabledToggle;

        [SerializeField] protected Dropdown LanguageSelectFropDown;

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