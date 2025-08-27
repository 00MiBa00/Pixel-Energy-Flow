using System;
using Datas.Energy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Energy
{
    public class ExerciseItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private Toggle _toggle;

        public event Action<ExerciseItemView> OnToggleSelectedAction;

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(OnPressToggle);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveAllListeners();
        }

        public void SetInfo(ExerciseData data)
        {
            _title.text = data.Title;
            _toggle.isOn = data.IsDone;
            _toggle.interactable = !data.IsDone;
        }

        private void OnPressToggle(bool isOn)
        {
            if (!isOn)
            {
                return;
            }

            Notification();
        }

        private void Notification()
        {
            OnToggleSelectedAction?.Invoke(this);
        }
    }
}