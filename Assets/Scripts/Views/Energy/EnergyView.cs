using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Energy
{
    public class EnergyView : MonoBehaviour
    {
        [SerializeField] private Image energyImage;

        public void AnimateEnergy(float target)
        {
            Debug.Log(target);
            
            energyImage.fillAmount = 0f;
            energyImage.DOFillAmount(target, 1.5f)
                .SetEase(Ease.OutQuad);
        }
    }
}