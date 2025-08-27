using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views.City
{
    public class CityMoodView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private List<Sprite> _sprites;

        public void SetSprite(int index)
        {
            Debug.Log(index);
            
            _image.sprite = _sprites[index];
        }
    }
}