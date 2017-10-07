using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo.Language
{
    public class Weather
    {
        public static string Clear = "Sunshine";
        public static string PartlyCloudy = "Cloudy";
        public static string Overcast = "Overcast";
        public static string Storm = "Storm";
        public static string Custom = "Custom";

        public static string ChooseWeather = "Please choose the weather you want to edit:";
        public static string SetWeatherWeight = "set the possibility at which every weather will occur:";
        public static string SetWeatherWeightTip = "If you are editing a whole series seo, we strongly recommand you to set the the possibility of edited weather as 100%. However, please note that it won't work until tomorrow 8:00 am in the game.";
        public static string LockWeight = "Lock the weight of each weather";
        public static string SaveWeight = "Save the weight";

        private const string Section = "Weather";
        public static void Initialize(LanguageReader lr)
        {
            Clear = lr.Read(Section, "Clear", Clear);
            PartlyCloudy = lr.Read(Section, "PartlyCloudy", PartlyCloudy);
            Overcast = lr.Read(Section, "Overcast", Overcast);
            Storm = lr.Read(Section, "Storm", Storm);
            Custom = lr.Read(Section, "Custom", Custom);

            ChooseWeather = lr.Read(Section, "ChooseWeather", ChooseWeather);
            SetWeatherWeight = lr.Read(Section, "SetWeatherWeight", SetWeatherWeight);
            SetWeatherWeightTip = lr.Read(Section, "SetWeatherWeightTip", SetWeatherWeightTip);
            LockWeight = lr.Read(Section, "LockWeight", LockWeight);
            SaveWeight = lr.Read(Section, "SaveWeight", SaveWeight);
        }
    }
}
