using Signals;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Services.Anchor
{
    public class AnchorService : IAnchorService
    {
       private readonly SignalBus _signalBus;
       public List<Anchor> _anchors { get; private set; }

       public AnchorService(SignalBus signalBus) 
        {
            _signalBus = signalBus;
        
            _anchors = new List<Anchor>();

            _signalBus.Subscribe<AnchorServiceSignals.Add>(signal =>
            {
                _anchors.Add(new Anchor(signal.Name, true, signal.Transform, signal.AnchorType, AnchorGroup.None));
            });
        }

        public List<Anchor> GetActorByName(string name) 
        {
            if (_anchors?.Count != 0)
                return _anchors.Where(anchor=>anchor.Name == name).ToList();

            return null;
        }

        public void Activate(Anchor anchor)
        {
            anchor.IsActive = true;
        }

        public void Deactivate(Anchor anchor) 
        {
            anchor.IsActive = false;
        }
    }
}
