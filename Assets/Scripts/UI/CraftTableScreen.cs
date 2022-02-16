using DG.Tweening;
using UnityEngine;

namespace LittleFroggyHat
{
    public class CraftTableScreen : Screen
    {
        public CanvasGroup group;
        public float showTime;
        public float hideTime;
        public AnimationCurve showCurve;
        public AnimationCurve hideCurve;
        public float showSizeValue;
        public float hideSizeValue;
        public override void Show()
        {
            base.Show();
            Sequence sequence = DOTween.Sequence();
            sequence.Append(@group.DOFade(1, showTime).SetEase(showCurve));
            sequence.Insert(0, @group.transform.DOScale(showSizeValue, showTime).SetEase(showCurve));
            
        }

        public override void Hide()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(group.DOFade(0, hideTime).SetEase(hideCurve));
            sequence.Insert(0,@group.transform.DOScale(hideSizeValue, hideTime).SetEase(hideCurve)).OnComplete(() => gameObject.SetActive(false));
        }
    }
}