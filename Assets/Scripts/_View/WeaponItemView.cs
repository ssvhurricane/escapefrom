using Services.Essence;
using Signals;
using UnityEngine;
using Zenject;

namespace View
{
    public class WeaponItemView : BaseEssence
    {
        [SerializeField] protected EssenceType Layer;
        
        private SignalBus _signalBus;

        private GameObject _sourcePrefab;

        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            EssenceType = Layer;

            _signalBus.Fire(new EssenceServiceSignals.Register(this));
        }

        public void InitializeView()
        {
            if(_sourcePrefab != null) 
            {
                gameObject.AddComponent<MeshFilter>();
                gameObject.GetComponent<MeshFilter>().sharedMesh = _sourcePrefab.GetComponent<MeshFilter>().sharedMesh;

                gameObject.AddComponent<MeshRenderer>();
                gameObject.GetComponent<MeshRenderer>().sharedMaterial = _sourcePrefab.GetComponent<MeshRenderer>().sharedMaterial;

                
            }
        }
        

        public GameObject GetSourcePrefab() 
        {
            return _sourcePrefab;
        }

        public void SetSourcePrefab(GameObject gameObject) 
        {
            _sourcePrefab = gameObject;
        }

    }
}
