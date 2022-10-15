using Zenject;

namespace Services.Detection
{
    public class DetectionService 
    {
        private SignalBus _signalBus;
       public DetectionService(SignalBus signalBus) 
        {
            _signalBus = signalBus;
        }
    }
}
