using System.Collections.Generic;
using Models.Scenes;
using Types;
using UnityEngine;
using UnityEngine.UI;
using Views.City;
using Views.General;
using Views.Menu;

namespace Controllers.Scenes
{
    public class CitySceneController : AbstractSceneController
    {
        [SerializeField] private List<BuildView> _buildViews;
        [SerializeField] private CityMoodView _cityMoodView;
        [SerializeField] private PrivacyPanel _infoPanel;
        [SerializeField] private AnimationPanel _notificationPanel;
        [SerializeField] private Button _infoBtn;
        [SerializeField] private Button _backBtn;
        
        private CitySceneModel _model;
        
        protected override void OnSceneEnable()
        {
            OpenNotificationPanel();
            SetTypeBuilds();
            SetCityMood();
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new CitySceneModel(_buildViews.Count);
        }

        protected override void Subscribe()
        {
            _infoBtn.onClick.AddListener(OnPressInfoBtn);
            _backBtn.onClick.AddListener(OnPressBackBtn);
        }

        protected override void Unsubscribe()
        {
            _infoBtn.onClick.RemoveAllListeners();
            _backBtn.onClick.RemoveAllListeners();
        }

        private void SetTypeBuilds()
        {
            for (int i = 0; i < _buildViews.Count; i++)
            {
                BuildType type = _model.GetTypeBuIndex(i);
                
                _buildViews[i].SetType(type);
            }
        }

        private void SetCityMood()
        {
            int index = _model.MoodIndex;
            
            _cityMoodView.SetSprite(index);
        }

        private void OnPressInfoBtn()
        {
            _infoPanel.PressBtnAction += OnReceiveAnswerInfoPanel;
            _infoPanel.ShowPanel();
        }

        private void OnPressBackBtn()
        {
            base.LoadScene(SceneType.MenuScene);
        }

        private void OnReceiveAnswerInfoPanel(int answer)
        {
            _infoPanel.PressBtnAction -= OnReceiveAnswerInfoPanel;
            _infoPanel.ClosePanel();
        }

        private void OpenNotificationPanel()
        {
            _notificationPanel.SetText(_model.GetNotificationText());
            _notificationPanel.PlayAnimNotification();
        }
    }
}