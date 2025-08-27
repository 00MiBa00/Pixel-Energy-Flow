using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

namespace Views.Stats
{
    public class StatisticView : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            if (!TryGetComponent(out Image image))
            {
                Debug.Log("No Image(((");
                return;
            }

            _image = image;
        }

        public void AnimStat(int index, float endValue)
        {
            _image.fillAmount = 0f;
            
            _image.DOFillAmount(endValue, 0.8f)
                .SetEase(Ease.OutCubic)
                .SetDelay(index * 0.1f);
        }
    }
}