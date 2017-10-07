using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Sky_1
    {
        public static string DayColorName = "Cloud 1";
        public static string DayColorDescription = "You can change the color of Cloud1 here.";
        public static string ColorWRTSunDarkName = "Cloud1 without Sunlight";
        public static string ColorWRTSunDarkDescription = "The the color of cloud1 without sunlight";
        public static string ColorWRTSunLightName = "Cloud1 with Sunlight";
        public static string ColorWRTSunLightDescription = "The the color of cloud1 with sunlight";
        public static string ColorWRTHorizonDarkName = "Cloud1 at Horizon without Sunlight";
        public static string ColorWRTHorizonDarkDescription = "The color of cloud1 at horizon without sunlight";
        public static string ColorWRTHorizonLightName = "Cloud1 at Horizon with Sunlight";
        public static string ColorWRTHorizonLightDescription = "The color of cloud1 at horizon with sunlight";
        public static string ColorWRTShadowName = "Cloud1 Shadow";
        public static string ColorWRTShadowDescription = "The color of the cloud1's shadow";

        private const string Section = "Sky_1";
        public static void Initialize(LanguageReader lr)
        {
            DayColorName = lr.Read(Section, "Name", DayColorName);
            DayColorDescription = lr.Read(Section, "Description", DayColorDescription);
            ColorWRTSunDarkName = lr.Read(Section, "ColorWRTSunDarkName", ColorWRTSunDarkName);
            ColorWRTSunDarkDescription = lr.Read(Section, "ColorWRTSunDarkDescription", ColorWRTSunDarkDescription);
            ColorWRTSunLightName = lr.Read(Section, "ColorWRTSunLightName", ColorWRTSunLightName);
            ColorWRTSunLightDescription = lr.Read(Section, "ColorWRTSunLightDescription", ColorWRTSunLightDescription);
            ColorWRTHorizonDarkName = lr.Read(Section, "ColorWRTHorizonDarkName", ColorWRTHorizonDarkName);
            ColorWRTHorizonDarkDescription = lr.Read(Section, "ColorWRTHorizonDarkDescription", ColorWRTHorizonDarkDescription);
            ColorWRTHorizonLightName = lr.Read(Section, "ColorWRTHorizonLightName", ColorWRTHorizonLightName);
            ColorWRTHorizonLightDescription = lr.Read(Section, "ColorWRTHorizonLightDescription", ColorWRTHorizonLightDescription);
            ColorWRTShadowName = lr.Read(Section, "ColorWRTShadowName", ColorWRTShadowName);
            ColorWRTShadowDescription = lr.Read(Section, "ColorWRTShadowDescription", ColorWRTShadowDescription);
        }
    }
}
