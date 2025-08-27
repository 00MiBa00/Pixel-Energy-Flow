using Localization.Energy;
using Models.Scenes;
using UnityEngine;
using Views.Energy;
using TMPro;
using Types;
using Views.General;

namespace Controllers.Scenes
{
    public class EnergySceneController : AbstractSceneController
    {
        [SerializeField] private TMP_InputField _sleepinputField;
        [SerializeField] private MoodView _moodView;
        [SerializeField] private ExercisesView _exercisesView;
        [SerializeField] private PanelView _mainPanel;
        [SerializeField] private AddExercisePanel _addExercisePanel;
        [SerializeField] private AnimationPanel _animationPanel;
        [SerializeField] private EnergyView _energyView;
        
        private EnergySceneModel _model;
        
        protected override void OnSceneEnable()
        {
            OpenMainPanel();
            TryUpdateSleepInputField();
            TryUpdateMoodBody();
            TryUpdateExercises();
            TryGenerateEnergy(true);
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new();
        }

        protected override void Subscribe()
        {
            _sleepinputField.onEndEdit.AddListener(OnEndEditSleepCount);
            _moodView.OnPressBtnAction += OnSelectedMood;
            _exercisesView.OnPressBtnAction += OnPressAddBtn;
            _exercisesView.OnExerciseToggleSelectedAction += OnCompletedExercise;
        }

        protected override void Unsubscribe()
        {
            _sleepinputField.onEndEdit.RemoveAllListeners();
            _moodView.OnPressBtnAction -= OnSelectedMood;
            _exercisesView.OnPressBtnAction -= OnPressAddBtn;
            _exercisesView.OnExerciseToggleSelectedAction -= OnCompletedExercise;
        }

        private void TryUpdateExercises()
        {
            _exercisesView.SetExercises(_model.Data.ExerciseDatas);
        }

        private void TryUpdateSleepInputField()
        {
            if (!_model.CanSetSleep)
            {
                return;
            }

            _sleepinputField.text = $"{_model.SleepToday} H";
        }

        private void TryUpdateMoodBody()
        {
            if (!_model.CanSetMood)
            {
                return;
            }
            
            _moodView.SetActiveMood(_model.MoodToday);
        }

        private void OnEndEditSleepCount(string input)
        {
            if (!int.TryParse(input, out int count))
            {
                return;
            }
            
            _model.SleepToday = count;
            TryUpdateSleepInputField();
        }

        private void OnSelectedMood(int index)
        {
            _model.MoodToday = index;
            TryUpdateMoodBody();
        }

        private void OnPressAddBtn()
        {
            if (!_model.CanAddExercise)
            {
                _animationPanel.SetText(NotificationTexts.AddExerciseNotification);
                _animationPanel.PlayAnimNotification();
                return;
            }

            SubscribeAddExercisePanel();
            CloseMainPanel();
            OpenAddExercisePanel();
        }

        private void OnReceiveAnswerAddExercisePanel(int answer)
        {
            UnsubscribeAddExercisePanel();
            ClosePanel(_addExercisePanel);
            OpenMainPanel();
        }

        private void OnReceiveAnswerMainPanel(int answer)
        {
            switch (answer)
            {
                case 0:
                    base.LoadScene(SceneType.MenuScene);
                    break;
                case 1:
                    TryGenerateEnergy(false);
                    break;
            }
        }

        private void OnPressSaveBtn(string title)
        {
            _model.AddExercise(title);
            ClosePanel(_addExercisePanel);
            UnsubscribeAddExercisePanel();
            OpenMainPanel();
            
            TryUpdateExercises();
        }

        private void OnCompletedExercise(int index)
        {
            _model.MarkDone(index);
            
            TryUpdateExercises();
        }

        private void SubscribeAddExercisePanel()
        {
            _addExercisePanel.PressBtnAction += OnReceiveAnswerAddExercisePanel;
            _addExercisePanel.OnPressSaveBtnAction += OnPressSaveBtn;
        }

        private void UnsubscribeAddExercisePanel()
        {
            _addExercisePanel.PressBtnAction -= OnReceiveAnswerAddExercisePanel;
            _addExercisePanel.OnPressSaveBtnAction -= OnPressSaveBtn;
        }

        private void OpenMainPanel()
        {
            _mainPanel.PressBtnAction += OnReceiveAnswerMainPanel;
            
            OpenPanel(_mainPanel);
        }

        private void OpenAddExercisePanel()
        {
            SubscribeAddExercisePanel();
            OpenPanel(_addExercisePanel);
        }

        private void CloseMainPanel()
        {
            _mainPanel.PressBtnAction -= OnReceiveAnswerMainPanel;
            
            ClosePanel(_mainPanel);
        }

        private void OpenPanel(PanelView view)
        {
            view.Open();
        }

        private void ClosePanel(PanelView view)
        {
            view.Close();
        }

        private void TryGenerateEnergy(bool isFirst)
        {
            if (!_model.CanGenerateEnergy && !isFirst)
            {
                _animationPanel.SetText(NotificationTexts.GenerateNotification);
                _animationPanel.PlayAnimNotification();
                return;
            }

            if (!isFirst)
            {
                _model.SaveDatas();
            }

            _energyView.AnimateEnergy(_model.GetEnergyPercent());
        }
    }
}