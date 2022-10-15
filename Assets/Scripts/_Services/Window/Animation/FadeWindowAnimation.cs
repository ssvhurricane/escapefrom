using DG.Tweening;
using System;
using UnityEngine;

namespace Services.Window
{
    public class FadeWindowAnimation : IWindowAnimation
    {
        private readonly CanvasGroup _canvasGroup;
        private Tween _tween;

        private const float ShowFade = 1f;
        private const float HideFade = 0f;
        private const float ShowingTime = 0.5f;
        private const float HidingTime = 0.5f;

        public FadeWindowAnimation(CanvasGroup canvasGroup)
        {
            _canvasGroup = canvasGroup;
        } 
        
        public void AnimateShow(Action callback)
        {
            Clear();

            _tween = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(HideFade, HidingTime).SetEase(Ease.OutCubic))
                .AppendCallback(() => callback?.Invoke());
        }

        public void AnimateHide(Action callback)
        {
            Clear();

            _tween = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(HideFade, HidingTime).SetEase(Ease.OutCubic))
                .AppendCallback(() => callback?.Invoke());
        }

        public void Clear()
        {
            _tween?.Kill();
        }
    }
}