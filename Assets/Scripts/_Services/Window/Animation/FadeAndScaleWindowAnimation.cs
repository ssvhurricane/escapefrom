using DG.Tweening;
using System;
using UnityEngine;

namespace Services.Window
{
    public class FadeAndScaleWindowAnimation : IWindowAnimation, IWindowInput
    {
        private readonly CanvasGroup _canvasGroup;
        private readonly RectTransform _panelRectTransform; 
        private Tween _tween;

        private const float Scale = 0.95f;
        private const float ShowFade = 1f;
        private const float HideFade = 0f;
        private const float TagetScale = 1f;
        private const float ShowingTime = 0.5f;
        private const float HidingTime = 0.5f;
        private const float Overshoot = 2f;

        public FadeAndScaleWindowAnimation(CanvasGroup canvasGroup, RectTransform panelRectTransform)
        {
            _canvasGroup = canvasGroup;
            _panelRectTransform = panelRectTransform;
        }
        public void AnimateShow(Action callback)
        {
            Clear();

            _panelRectTransform.localScale = Vector3.one * Scale;
            _tween = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(ShowFade, ShowingTime).SetEase(Ease.OutCirc))
                .Join(_panelRectTransform.DOScale(TagetScale, ShowingTime).SetEase(Ease.InOutBack))
                .AppendCallback(() => callback?.Invoke());
        }
        
        public void AnimateHide(Action callback)
        {
            Clear();

            _tween = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(HideFade, HidingTime).SetEase(Ease.InQuart))
                .Join(_panelRectTransform.DOScale(Scale, HidingTime).SetEase(Ease.InBack, Overshoot))
                .AppendCallback(() => callback?.Invoke());
        }

        public void Clear()
        {
            _tween?.Kill();
        }
    }
}