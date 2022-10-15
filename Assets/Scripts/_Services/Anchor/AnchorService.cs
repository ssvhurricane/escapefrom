using Signals;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Zenject;

namespace Services.Anchor
{
    public class AnchorService : IAnchorService
    {
       private readonly SignalBus _signalBus;
       public ReactiveProperty<List<Anchor>> _anchors { get; private set; }

       public AnchorService(SignalBus signalBus) 
        {
            _signalBus = signalBus;

            _anchors = new ReactiveProperty<List<Anchor>>();
            _anchors.Value = new List<Anchor>();

            _signalBus.Subscribe<AnchorServiceSignals.Add>(signal =>
            {
                _anchors.Value.Add(new Anchor(signal.Name, true, signal.Transform, signal.AnchorType, AnchorGroup.None));
            });
        }

        public List<Anchor> GetActorByName(string name) 
        {
            if (_anchors?.Value.Count != 0)
                return _anchors.Value.Where(anchor=>anchor.Name == name).ToList();

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
