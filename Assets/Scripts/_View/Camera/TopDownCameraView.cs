using Services.Essence;
using Signals;
using UnityEngine;
using Zenject;

namespace View.Camera
{
    public class TopDownCameraView : NetworkEssence
    {
        [SerializeField] protected EssenceType Layer;

        private SignalBus _signalBus;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            EssenceType = Layer;

            _signalBus.Fire(new EssenceServiceSignals.Register(this));
        }

    }
}