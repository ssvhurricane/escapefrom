using UnityEngine;

namespace Commands.Button
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public abstract class AbstractButtonCommand : MonoBehaviour
    {
        private UnityEngine.UI.Button _button;
        protected UnityEngine.UI.Button Button => _button ?? (_button = GetComponent<UnityEngine.UI.Button>());

        protected virtual void Awake() 
        {
            Button?.onClick.AddListener(Activate);
        }
        public abstract void Activate();
    }
}