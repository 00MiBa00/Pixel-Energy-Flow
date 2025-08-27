using System;
using DG.Tweening;
using UnityEngine;

namespace Views.Menu
{
    public class BallAnimation : MonoBehaviour
    {
        private Sequence _seq;

        private void Start()
        {
            AnimateBall();
        }

        private void OnDisable()
        {
            _seq?.Kill();
        }

        private void AnimateBall()
        {
            _seq = DOTween.Sequence()
            .Append(transform.DOScale(1.2f, 0.75f).SetEase(Ease.OutQuad)) 
            .Append(transform.DOScale(1f, 0.75f).SetEase(Ease.InQuad))
            .Append(transform.DORotate(new Vector3(0, 0, 360), 1.5f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear));
            
            _seq.SetLoops(-1);
        }
    }
}