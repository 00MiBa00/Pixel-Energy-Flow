using System.Collections.Generic;
using Datas.Energy;
using Models.Energy;

namespace Models.Scenes
{
    public class StatsSceneModel
    {
        public List<float> GetStats(int statsCount)
        {
            List<float> stats = new();

            List<DayEnergyData> dayStats = new(EnergyStatsModel.GetWeekData()) ;

            for (int i = 0; i < statsCount; i++)
            {
                if (i < dayStats.Count)
                {
                    float energy = dayStats[i].Energy;
                    stats.Add(energy);
                }
                else
                {
                    stats.Add(0);
                }
            }

            return stats;
        }
    }
}