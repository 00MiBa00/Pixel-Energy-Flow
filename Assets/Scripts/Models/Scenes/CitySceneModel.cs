using System;
using System.Collections.Generic;
using Localization.City;
using Models.Energy;
using Types;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Models.Scenes
{
    public class CitySceneModel
    {
        private int _moodIndex;
        private List<int> _lightBuilds;

        public int MoodIndex => _moodIndex;
        
        public CitySceneModel(int buildCount)
        {
            SetLightBuilds(buildCount);
            SetMoodIndex();
        }

        public BuildType GetTypeBuIndex(int index)
        {
            bool isNight = DateTime.Now.Hour < 6 || DateTime.Now.Hour >= 18;
            bool isLight = _lightBuilds != null && _lightBuilds.Contains(index);

            if (isNight)
            {
                return isLight ? BuildType.LightNight : BuildType.DefaultNight;
            }
            else
            {
                return isLight ? BuildType.LightDay : BuildType.DefaultDay;
            }
        }

        public string GetNotificationText()
        {
            int percent = Mathf.RoundToInt(EnergyStatsModel.GetTodayData().Energy * 100);

            string status = NotificationTexts.GetEnergyText(percent);

            return $"Today: {percent}%\n{status}";
        }

        private void SetLightBuilds(int buildCount)
        {
            _lightBuilds ??= new List<int>();
            _lightBuilds.Clear();

            float energy = EnergyStatsModel.GetTodayData().Energy; 
            int countLight = Mathf.FloorToInt(energy * buildCount);
            
            if (countLight <= 0) return;
            
            List<int> allIndexes = new List<int>();
            for (int i = 0; i < buildCount; i++)
                allIndexes.Add(i);
            
            for (int i = 0; i < allIndexes.Count; i++)
            {
                int swapIndex = Random.Range(i, allIndexes.Count);
                (allIndexes[i], allIndexes[swapIndex]) = (allIndexes[swapIndex], allIndexes[i]);
            }
            
            for (int i = 0; i < countLight; i++)
            {
                _lightBuilds.Add(allIndexes[i]);
            }
        }

        private void SetMoodIndex()
        {
            _moodIndex = Mathf.RoundToInt(EnergyStatsModel.GetTodayData().Energy * 4f);
        }
    }
}