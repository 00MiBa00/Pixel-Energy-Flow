using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Views.General
{
    public class AnimationPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _notText;
        
        private Sequence _seq;

        public void SetText(string text)
        {
            _notText.text = text;
        }

        public void PlayAnimNotification()
        {
            _seq?.Kill();
            
            transform.localScale = Vector3.zero;
            _notText.gameObject.transform.localScale = Vector3.zero;

            _seq = DOTween.Sequence()
                .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack))
                .Append(_notText.gameObject.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutBack))
                .AppendInterval(2)
                .Append(_notText.gameObject.transform.DOScale(Vector3.zero, 1).SetEase(Ease.InBack))
                .Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack));
        }
    }
}