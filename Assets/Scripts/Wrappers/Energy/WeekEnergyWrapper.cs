using System;
using System.Collections.Generic;
using Datas.Energy;

namespace Wrappers.Energy
{
    [Serializable]
    public class WeekEnergyWrapper
    {
        public int Year;
        public int WeekNumber;
        public List<DayEnergyData> Days = new();
    }
}