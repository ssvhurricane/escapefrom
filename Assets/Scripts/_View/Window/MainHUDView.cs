using Services.Window;
using Signals;
using UnityEngine;
using Zenject;

namespace View.Window
{
    public class MainHUDView : BaseWindow
    {
        [SerializeField] protected WindowType Type;

        [SerializeField] protected RectTransform PlayerAbilityContainer;

        [SerializeField] protected RectTransform VerticalAbilityPanel;

        private SignalBus _signalBus;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            WindowType = Type;

            _signalBus.Fire(new WindowServiceSignals.Register(this));
        }

        public RectTransform GetPlayerAbilityContainer() 
        {
            return PlayerAbilityContainer;
        }

        public RectTransform GetVerticalAbilityPanel() 
        {
            return VerticalAbilityPanel;
        }
    }
}