using Types;
using UnityEngine;
using UnityEngine.UI;
using Views.Menu;

namespace Controllers.Scenes
{
    public class MenuSceneController : AbstractSceneController
    {
        [SerializeField] private Button _cityBtn;
        [SerializeField] private Button _statsBtn;
        [SerializeField] private Button _energyBtn;
        [SerializeField] private Button _privacyBtn;
        [SerializeField] private PrivacyPanel _privacyPanel;
        
        protected override void OnSceneEnable()
        {
            
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            
        }

        protected override void Subscribe()
        {
            _energyBtn.onClick.AddListener(OnPressEnergyBtn);
            _cityBtn.onClick.AddListener(OnPressCityBtn);
            _statsBtn.onClick.AddListener(OnPressStatsBtn);
            _privacyBtn.onClick.AddListener(OnPressPrivacyBtn);
        }

        protected override void Unsubscribe()
        {
           _energyBtn.onClick.RemoveAllListeners();
           _cityBtn.onClick.RemoveAllListeners();
           _statsBtn.onClick.RemoveAllListeners();
           _privacyBtn.onClick.RemoveAllListeners();
        }

        private void OnPressEnergyBtn()
        {
            base.LoadScene(SceneType.EnergyScene);
        }

        private void OnPressStatsBtn()
        {
            base.LoadScene(SceneType.StatsScene);
        }

        private void OnPressCityBtn()
        {
            base.LoadScene(SceneType.CityScene);
        }

        private void OnPressPrivacyBtn()
        {
            _privacyPanel.PressBtnAction += OnReceiveAnswerPrivacyPanel;
            _privacyPanel.ShowPanel();
        }

        private void OnReceiveAnswerPrivacyPanel(int answer)
        {
            _privacyPanel.PressBtnAction -= OnReceiveAnswerPrivacyPanel;
            _privacyPanel.ClosePanel();
        }
    }
}