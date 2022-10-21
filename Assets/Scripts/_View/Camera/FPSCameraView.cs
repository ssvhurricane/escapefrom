using Services.Essence;
using Signals;
using UnityEngine;
using Zenject;

namespace View.Camera
{
    public class FPSCameraView : NetworkEssence
    {
        [SerializeField] protected EssenceType Layer;

        [SerializeField] protected UnityEngine.Camera Camera;

        private SignalBus _signalBus;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            EssenceType = Layer; 
            
            _signalBus.Fire(new EssenceServiceSignals.Register(this));
        }

        public UnityEngine.Camera GetMainCamera() 
        {
            return Camera;
        }
    }
}