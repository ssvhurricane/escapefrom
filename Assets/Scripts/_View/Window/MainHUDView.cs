using Services.Window;
using Signals;
using UnityEngine;
using Zenject;

namespace View.Window
{
    public class MainHUDView : BaseWindow
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] public RectTransform PlayerAbilityContainer;

        [SerializeField] public RectTransform WolfAbilityContainer;

        [SerializeField] public RectTransform VerticalAbilityPanel;

        [SerializeField] public RectTransform HorizontalAbilityPanel;

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