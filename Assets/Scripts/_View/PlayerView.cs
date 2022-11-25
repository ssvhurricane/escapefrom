using Services.Essence;
using Signals;
using UnityEngine;
using Zenject;

namespace View
{
    public class PlayerView : NetworkEssence
    {
        [SerializeField] protected EssenceType Layer;

        [SerializeField] protected GameObject Armature;
        [SerializeField] protected GameObject Head;
        [SerializeField] protected GameObject LeftArm, RightArm, LeftArmRoot, RightArmRoot;
        [SerializeField] protected GameObject CameraRoot;

        [SerializeField] protected Animator Animator;

        private SignalBus _signalBus;
      
        [Inject]
        public void Constrcut(SignalBus signalBus)
            
        {
            _signalBus = signalBus;
           
             EssenceType = Layer;

            _signalBus.Fire(new EssenceServiceSignals.Register(this));
        }

        public GameObject GetArmature() 
        {
            return Armature;
        }

        public GameObject GetHead()
        {
            return Head;
        }

        public GameObject GetLeftArm()
        {
            return LeftArm;
        }

        public GameObject GetRightArm()
        {
            return RightArm;
        }

        public GameObject GetLeftArmRoot()
        {
            return LeftArmRoot;
        }

        public GameObject GetRightArmRoot()
        {
            return RightArmRoot;
        }

        public GameObject GetCameraRoot() 
        {
            return CameraRoot;
        }

        public Animator GetAnimator()
        {
            return Animator;
        }
    }
}
