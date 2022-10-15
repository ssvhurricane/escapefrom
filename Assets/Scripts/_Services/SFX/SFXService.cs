using Zenject;

namespace Services.SFX
{
    public class SFXService 
    {
        private SignalBus _signalBus;
       public SFXService(SignalBus signalBus) 
        {
            _signalBus = signalBus;
        }
    }
}
