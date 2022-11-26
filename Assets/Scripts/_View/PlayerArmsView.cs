using Services.Essence;
using Signals;
using UnityEngine;
using Zenject;

namespace View
{
    public class PlayerArmsView : NetworkEssence
    {
        [SerializeField] protected EssenceType Layer;
      
        [SerializeField] protected GameObject LeftArmRoot, RightArmRoot;
       
        private SignalBus _signalBus;
      
        [Inject]
        public void Constrcut(SignalBus signalBus)
            
        {
            _signalBus = signalBus;
           
             EssenceType = Layer;

            _signalBus.Fire(new EssenceServiceSignals.Register(this));
        }

        public GameObject GetLeftArmRoot()
        {
            return LeftArmRoot;
        }

        public GameObject GetRightArmRoot()
        {
            return RightArmRoot;
        }

       
        private void OnAnimatorIK(int layerIndex)
        {
           // TODO:
        }
    }
}
