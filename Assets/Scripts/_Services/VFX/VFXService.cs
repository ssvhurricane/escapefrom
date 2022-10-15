using Zenject;

namespace Services.VFX
{
    public class VFXService
    {
        private SignalBus _signalBus;
        public VFXService(SignalBus signalBus) 
        {
            _signalBus = signalBus;
        }
    }
}