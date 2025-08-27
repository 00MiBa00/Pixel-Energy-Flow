using System.Collections.Generic;
using System.Linq;
using Models.Scenes;
using UnityEngine;
using UnityEngine.UI;
using Views.General;
using Views.Stats;

namespace Controllers.Scenes
{
    public class StatsSceneController : AbstractSceneController
    {
        [SerializeField] private List<StatisticView> _statisticViews;
        [SerializeField] private AnimationPanel _animationPanel;
        [SerializeField] private Button _backBtn;
        
        private StatsSceneModel _model;
        
        protected override void OnSceneEnable()
        {
            
        }

        protected override void OnSceneStart()
        {
            SetStatistics();
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
            _backBtn.onClick.AddListener(OnPressBackBtn);
        }

        protected override void Unsubscribe()
        {
            _backBtn.onClick.RemoveAllListeners();
        }

        private void SetStatistics()
        {
            List<float> stats = new(_model.GetStats(_statisticViews.Count));

            int zeroCount = stats.Count(e => e > 0);

            if (zeroCount == 0)
            {
                _animationPanel.PlayAnimNotification();
                return;
            }

            for (int i = 0; i < _statisticViews.Count; i++)
            {
                _statisticViews[i].AnimStat(i, stats[i]);
            }
        }

        private void OnPressBackBtn()
        {
            
        }
    }
}