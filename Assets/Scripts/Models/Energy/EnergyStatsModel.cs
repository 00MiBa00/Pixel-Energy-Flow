using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Datas.Energy;
using UnityEngine;
using Wrappers.Energy;

namespace Models.Energy
{
    public static class EnergyStatsModel
    {
        private const string FileName = "week_energy.json";
        private static string FullPath => Path.Combine(Application.persistentDataPath, FileName);

        private static WeekEnergyWrapper currentWeek;
        
        public static void SaveEnergyForToday(float energy)
        {
            EnsureLoaded();

            var today = DateTime.Now.Date;
            string ymd = today.ToString("yyyy-MM-dd");

            var day = currentWeek.Days.Find(d => d.DateYmd == ymd);
            if (day == null)
            {
                day = new DayEnergyData { DateYmd = ymd, Energy = energy };
                currentWeek.Days.Add(day);
            }
            else
            {
                day.Energy = energy;
            }

            Save();
        }

        public static List<DayEnergyData> GetWeekData()
        {
            EnsureLoaded();
            return currentWeek.Days;
        }

        public static DayEnergyData GetTodayData()
        {
            EnsureLoaded();

            string todayYmd = DateTime.Now.Date.ToString("yyyy-MM-dd");
            var todayData = currentWeek.Days.Find(d => d.DateYmd == todayYmd);
            
            if (todayData == null)
            {
                todayData = new DayEnergyData
                {
                    DateYmd = todayYmd,
                    Energy = 0f
                };
                currentWeek.Days.Add(todayData);
                Save();
            }

            return todayData;
        }

        private static void EnsureLoaded()
        {
            if (currentWeek == null)
                currentWeek = Load();

            var now = DateTime.Now;
            int week = GetIso8601WeekOfYear(now);

            if (currentWeek == null || currentWeek.WeekNumber != week || currentWeek.Year != now.Year)
            {
                currentWeek = new WeekEnergyWrapper()
                {
                    Year = now.Year,
                    WeekNumber = week,
                    Days = new List<DayEnergyData>()
                };
                
                DateTime monday = now.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday);
                for (int i = 0; i < 7; i++)
                {
                    var date = monday.AddDays(i);
                    currentWeek.Days.Add(new DayEnergyData
                    {
                        DateYmd = date.ToString("yyyy-MM-dd"),
                        Energy = 0f
                    });
                }

                Save();
            }
        }

        private static void Save()
        {
            string json = JsonUtility.ToJson(currentWeek, true);
            File.WriteAllText(FullPath, json);
        }

        private static WeekEnergyWrapper Load()
        {
            if (!File.Exists(FullPath)) return null;
            return JsonUtility.FromJson<WeekEnergyWrapper>(File.ReadAllText(FullPath));
        }

        private static int GetIso8601WeekOfYear(DateTime time)
        {
            var cal = CultureInfo.InvariantCulture.Calendar;
            return cal.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
