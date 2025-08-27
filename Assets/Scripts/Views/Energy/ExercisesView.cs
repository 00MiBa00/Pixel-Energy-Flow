using System;
using System.Collections.Generic;
using Datas.Energy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Views.Energy
{
    public class ExercisesView : MonoBehaviour
    {
        [SerializeField] private GameObject _exercisesItemPrefab;
        [SerializeField] private RectTransform _container;
        [SerializeField] private Button _addBtn;
        [SerializeField] private TextMeshProUGUI _countText;

        private List<ExerciseItemView> _activeExercises;

        public event Action OnPressBtnAction;
        public event Action<int> OnExerciseToggleSelectedAction;

        private void OnEnable()
        {
            _addBtn.onClick.AddListener(OnPressBtn);
        }

        private void OnDisable()
        {
            _addBtn.onClick.RemoveListener(OnPressBtn);
        }

        public void SetExercises(List<ExerciseData> datas)
        {
            if (datas == null || datas.Count == 0)
            {
                return;
            }

            if (_activeExercises is { Count: > 0 })
            {
                foreach (var item in _activeExercises)
                {
                    item.OnToggleSelectedAction -= OnCompletedExercise;
                    Destroy(item.gameObject);
                }
                
                _activeExercises.Clear();
            }

            _activeExercises ??= new List<ExerciseItemView>();

            foreach (var data in datas)
            {
                GameObject go = Instantiate(_exercisesItemPrefab, _container);
                ExerciseItemView view = go.GetComponent<ExerciseItemView>();
                
                view.OnToggleSelectedAction += OnCompletedExercise;
                view.SetInfo(data);
                
                _activeExercises.Add(view);
            }

            int doneCount = datas.Count(e => e.IsDone);

            _countText.text = $"{doneCount}/{datas.Count}";
        }

        private void OnPressBtn()
        {
            Notification();
        }

        private void Notification()
        {
            OnPressBtnAction?.Invoke();
        }

        private void OnCompletedExercise(ExerciseItemView view)
        {
            int index = _activeExercises.IndexOf(view);
            
            OnExerciseToggleSelectedAction?.Invoke(index);
        }
    }
}