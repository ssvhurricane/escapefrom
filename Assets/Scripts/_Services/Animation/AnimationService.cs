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
        public void SetBool(Animator animator, string id, bool value)
        {
            animator.SetBool(id, value);
        }
        public void SetBool(Animator animator, int id, bool value)
        {
            animator.SetBool(id, value);
        }

        public bool GetBool(Animator animator, string nameAnim) 
        {
            return animator.GetBool(nameAnim);
        }

        public void SetFloat(Animator animator, string name, float value) 
        {
            animator.SetFloat(name, value);
        }

        public void SetFloat(Animator animator, int id, float value)
        {
            animator.SetFloat(id, value);
        }

        public void SetTrigger(Animator animator, int id) 
        {
            animator.SetTrigger(id);
        }

        public AnimatorControllerParameter GetTrigger(Animator animator, int id)
        {
          return animator.GetParameter(id);
        }

        public void SetTrigger(Animator animator, string id)
        {
            animator.SetTrigger(id);
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
