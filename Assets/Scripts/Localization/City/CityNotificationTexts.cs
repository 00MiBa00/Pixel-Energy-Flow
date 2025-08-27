namespace Localization.City
{
    public static class NotificationTexts
    {
        public static string GetEnergyText(float energyPercent)
        {
            if (energyPercent < 0.2f)
                return "The city is quiet… citizens feel tired and gloomy.";
            if (energyPercent < 0.5f)
                return "Lights flicker, and people are a bit unsatisfied — not enough energy today.";
            if (energyPercent < 0.8f)
                return "The city brightens, citizens smile more, but they still need a little more spark.";
            if (energyPercent < 1.0f)
                return "Almost perfect! The city shines and the citizens are happy.";

            return "The city is glowing with life — everyone is full of joy and energy!";
        }
    }
}