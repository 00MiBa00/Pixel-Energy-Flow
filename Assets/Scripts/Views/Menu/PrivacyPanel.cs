using DG.Tweening;
using UnityEngine;
using Views.General;

namespace Views.Menu
{
    public class PrivacyPanel : PanelView
    {
        private Tween _tween;
        
        public void ShowPanel()
        {
            _tween?.Kill();
            
            transform.localScale = Vector3.zero;
            
            base.Open();

            _tween = transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
        }

        public void ClosePanel()
        {
            _tween?.Kill();

            _tween = transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
            
            base.Close();
        }
    }
}