using Services.Essence;
using Signals;
using UnityEngine;
using Zenject;

namespace View
{
    public class WolfView : NetworkEssence
    {
        [SerializeField] public Animator Animator;
        [SerializeField] protected EssenceType Layer;

        private SignalBus _signalBus;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            EssenceType = Layer;

            signalBus.Fire(new EssenceServiceSignals.Register(this));
        }

    }
}