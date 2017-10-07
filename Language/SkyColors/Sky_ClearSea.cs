using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Sky_ClearSea
    {
        public static string DayColorName = "Sea";
        public static string DayColorDescription = "You can change the color of water (sea, river,lake and pool) here.";
        public static string WaterColorName = "Water Color";
        public static string WaterColorDescription = "The basic color of water";
        public static string SunMoonColorName = "Water Reflection Color";
        public static string SunMoonColorDescription = "Water color reflected from the light from the sun and moon";

        private const string Section = "Sky_ClearSea";
        public static void Initialize(LanguageReader lr)
        {
            DayColorName = lr.Read(Section, "Name", DayColorName);
            DayColorDescription = lr.Read(Section, "Description", DayColorDescription);
            WaterColorName = lr.Read(Section, "WaterColorName", WaterColorName);
            WaterColorDescription = lr.Read(Section, "WaterColorDescription", WaterColorDescription);
            SunMoonColorName = lr.Read(Section, "SunMoonColorName", SunMoonColorName);
            SunMoonColorDescription = lr.Read(Section, "SunMoonColorDescription", SunMoonColorDescription);
        }
    }
}
