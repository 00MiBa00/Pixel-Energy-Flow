using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Energy
{
    public class MoodView : MonoBehaviour
    {
        [SerializeField]
        private List<Button> _moodBtns;
        [SerializeField] 
        private List<Sprite> _activeSprites;
        [SerializeField] 
        private List<Sprite> _disableSprites;

        public event Action<int> OnPressBtnAction;

        private void OnEnable()
        {
            _moodBtns.ForEach(btn => btn.onClick.AddListener(() => OnPressBtn(btn)));
        }

        private void OnDisable()
        {
            _moodBtns.ForEach(btn => btn.onClick.RemoveAllListeners());
        }

        public void SetActiveMood(int value)
        {
            for (int i = 0; i < _moodBtns.Count; i++)
            {
                _moodBtns[i].interactable = i != value;

                Sprite sprite = i != value ? _disableSprites[i] : _activeSprites[i];
                
                _moodBtns[i].image.sprite = sprite;
            }
        }

        private void OnPressBtn(Button btn)
        {
            int index = _moodBtns.IndexOf(btn);
            
            Notification(index);
        }

        private void Notification(int index)
        {
            OnPressBtnAction?.Invoke(index);
        }
    }
}