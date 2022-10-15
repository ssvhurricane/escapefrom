using Signals;
using UnityEngine;
using Zenject;

namespace Services.Window
{
//#pragma warning disable 0649
    public class WindowHolder : MonoBehaviour
    {
        [SerializeField] private WindowType _windowType;

        [Inject]
        public void Constrcut(SignalBus signalBus) 
        {
            signalBus.Fire(new WindowServiceSignals.AddHolder(transform, _windowType));
        }
    }
}