using Signals;
using UnityEngine;
using Zenject;

namespace Services.Anchor
{
    public class AnchorComponent : MonoBehaviour
    {
        [SerializeField] protected AnchorType AnchorType;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            signalBus.Fire(new AnchorServiceSignals.Add(gameObject.name, transform, AnchorType));
        }
    }
}
