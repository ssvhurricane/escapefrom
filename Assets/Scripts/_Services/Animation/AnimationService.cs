using UnityEngine;
using Zenject;

namespace Services.Animation
{
    public class AnimationService
    {
        private readonly SignalBus _signalBus;
       
        public AnimationService(SignalBus signalBus) 
        {
            _signalBus = signalBus;
        }

        public void PlayAnimation(Animator animator, string name) 
        {
            if (animator != null)
            {
                animator.Play(name);
            }
        }

        public void SetBool(Animator animator, string name, bool value)
        {
            animator.SetBool(name, value);
        }

        public bool GetBool(Animator animator, string nameAnim) 
        {
            return animator.GetBool(nameAnim);
        }

        public void SetFloat(Animator animator, string name, float value) 
        {
            animator.SetFloat(name, value);
        }

        public float GetFloat(Animator animator, string nameAnim) 
        {
            return animator.GetFloat(nameAnim);
        }
        public int GetRandomAnimation(int min, int max)
        {
            //UnityEngine.Random.InitState(DateTime.Now.Millisecond);
            return UnityEngine.Random.Range(min, max);
        }

        public void ResetAnimation() { }

        public void StopAnimation() { }

        public void PauseSnimation() { }
    }
}
