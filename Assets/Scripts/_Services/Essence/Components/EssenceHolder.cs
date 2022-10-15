using Signals;
using UnityEngine;
using Zenject;

namespace Services.Essence
{
    public class EssenceHolder : MonoBehaviour
    {
        [SerializeField] private EssenceType _essenceType;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            signalBus.Fire(new EssenceServiceSignals.AddHolder(transform, _essenceType));
        }
    }
}