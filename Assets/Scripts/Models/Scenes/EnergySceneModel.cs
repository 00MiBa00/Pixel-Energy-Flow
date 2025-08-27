using System;
using System.Collections.Generic;
using System.IO;
using Datas.Energy;
using Keys.Energy;
using UnityEngine;
using Wrappers.Energy;
using System.Linq;
using Models.Energy;

namespace Models.Scenes
{
    public class EnergySceneModel
    {
        private const string SleepTodayKey = "EnergySceneModel.SleepToday";
        private const string DateTodayKey = "EnergySceneModel.DateToday";
        private const string MoodTodayKey = "EnergySceneModel.MoodToday";
        private string FilePath => Path.Combine(Application.persistentDataPath, ExerciseKeys.FileName);

        private int _todayDay;
        private int _sleepToday;
        private int _moodToday;

        public bool CanSetSleep => _sleepToday > 0;
        public bool CanSetMood => _moodToday > -1;
        public bool CanAddExercise => Data.ExerciseDatas.Count < 3;
        public bool CanGenerateEnergy => CanSetSleep && CanSetMood;

        public int SleepToday
        {
            get => _sleepToday;
            set => TrySetSleepToday(value);
        }

        public int MoodToday
        {
            get => _moodToday;
            set => TrySetMoodToday(value);

        }
        
        public ExercisesWrapper Data { get; private set; }

        public EnergySceneModel()
        {
            _todayDay = PlayerPrefs.GetInt(DateTodayKey, 0);
            
            if (DateTime.Today.Day != _todayDay)
            {
                _todayDay = DateTime.Today.Day;
                _moodToday = -1;
                
                PlayerPrefs.SetInt(DateTodayKey, _todayDay);
                PlayerPrefs.SetInt(SleepTodayKey, 0);
                DeleteSave();
            }
            else
            {
                _moodToday = PlayerPrefs.GetInt(MoodTodayKey);
            }

            _sleepToday = PlayerPrefs.GetInt(SleepTodayKey, 0);
            
            Load();
        }

        public float GetEnergyPercent()
        {
            float sleepScore = Mathf.Clamp01(SleepToday / 8f);

            int doneExercises = Data.ExerciseDatas.Count(e => e.IsDone);
            
            float exerciseScore = Mathf.Clamp01((float)doneExercises / 3f);  
            float moodScore = Mathf.Clamp01(MoodToday / 5f);

            float energy = (sleepScore + exerciseScore + moodScore) / 3f;

            return energy;
        }

        public void SaveDatas()
        {
            PlayerPrefs.SetInt(SleepTodayKey, _sleepToday);
            PlayerPrefs.SetInt(MoodTodayKey, _moodToday);
            EnergyStatsModel.SaveEnergyForToday(GetEnergyPercent());
        }
        
        public void AddExercise(string title)
        {
            if (Data.ExerciseDatas.Exists(e => e.Title == title))
            {
                Debug.LogWarning($"Exercise '{title}' уже существует.");
                return;
            }

            Data.ExerciseDatas.Add(new ExerciseData
            {
                Title = title,
                IsDone = false
            });
            Save();
        }
        
        public void MarkDone(int index)
        {
            Debug.Log(index);
            
            var ex = Data.ExerciseDatas[index];
            
            if (ex != null)
            {
                ex.IsDone = true;
                Save();
            }
            else
            {
                Debug.LogWarning($"Exercise '{index}' не найден.");
            }
        }

        private void TrySetSleepToday(int value)
        {
            if (value <= 0)
            {
                return;
            }
            
            Debug.Log(value);

            _sleepToday = value;
        }

        private void TrySetMoodToday(int value)
        {
            if (value < 0)
            {
                return;
            }

            _moodToday = value;
        }
        
        private void Load()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                Data = JsonUtility.FromJson<ExercisesWrapper>(json) ?? new ExercisesWrapper { ExerciseDatas = new List<ExerciseData>() };
            }
            else
            {
                Data = new ExercisesWrapper { ExerciseDatas = new List<ExerciseData>() };
            }
        }
        
        private void Save()
        {
            string json = JsonUtility.ToJson(Data, true);
            File.WriteAllText(FilePath, json);
        }
        
        private void DeleteSave()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);

            Data = new ExercisesWrapper { ExerciseDatas = new List<ExerciseData>() };
        }
    }
}