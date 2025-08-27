using System.Collections.Generic;
using Types;
using UnityEngine;

namespace Views.City
{
    public class BuildView : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _daySpites;
        [SerializeField] private List<Sprite> _nightSprites;

        private SpriteRenderer _spriteRenderer;

        public void SetType(BuildType type)
        {
            switch (type)
            {
                case BuildType.DefaultDay:
                    SetDayState(false);
                    break;
                case BuildType.LightDay:
                    SetDayState(true);
                    break;
                case BuildType.DefaultNight:
                    SetNightState(false);
                    break;
                case BuildType.LightNight:
                    SetNightState(true);
                    break;
            }
        }

        private void SetDayState(bool isLight)
        {
            if (!TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                return;
            }

            _spriteRenderer = spriteRenderer;
            
            int index = isLight ? 0 : 1;

            _spriteRenderer.sprite = _daySpites[index];
        }
        
        private void SetNightState(bool isLight)
        {
            if (!TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                return;
            }

            _spriteRenderer = spriteRenderer;
            
            int index = isLight ? 0 : 1;

            _spriteRenderer.sprite = _nightSprites[index];
        }
    }
}