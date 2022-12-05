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
        [SerializeField] protected GameObject CameraRoot;

        [SerializeField] protected Animator Animator;

        private GameObject LeftArmRoot, RightArmRoot;

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

        public GameObject GetLeftArmRoot()
        {
            return LeftArmRoot;
        }

        public GameObject GetRightArmRoot()
        {
            return RightArmRoot;
        }


        public void SetLeftArmRoot(GameObject obj)
        {
            LeftArmRoot = obj;
        }

        public void SetRightArmRoot(GameObject obj)
        {
            RightArmRoot = obj;
        }

        public GameObject GetCameraRoot() 
        {
            return CameraRoot;
        }

        public Animator GetAnimator()
        {
            return Animator;
        }

        private void OnAnimatorIK(int layerIndex)
        {
            //GetAnimator().SetLookAtWeight(1);
           // GetAnimator().SetLookAtPosition(GetLeftArmRoot().gameObject.transform.position);
           
            
           GetAnimator().SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
           GetAnimator().SetIKPosition(AvatarIKGoal.LeftHand, GetLeftArmRoot().gameObject.transform.position);

           GetAnimator().SetIKRotation(AvatarIKGoal.LeftHand, GetLeftArmRoot().gameObject.transform.rotation);
       

            Debug.Log("Left Hand Position Arm Root: " + GetLeftArmRoot().gameObject.transform.position);

           GetAnimator().SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
           GetAnimator().SetIKPosition(AvatarIKGoal.RightHand, GetRightArmRoot().gameObject.transform.position);
           GetAnimator().SetIKRotation(AvatarIKGoal.RightHand, GetRightArmRoot().gameObject.transform.rotation);

        }
    }
}
