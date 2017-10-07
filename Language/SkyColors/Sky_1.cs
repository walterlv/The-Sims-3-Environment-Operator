﻿namespace Seo.Languages
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
        public static void ReadFile(IniFiles reader)
        {
            DayColorName = reader.ReadString(Section, "Name", DayColorName);
            DayColorDescription = reader.ReadString(Section, "Description", DayColorDescription);
            ColorWRTSunDarkName = reader.ReadString(Section, "ColorWRTSunDarkName", ColorWRTSunDarkName);
            ColorWRTSunDarkDescription = reader.ReadString(Section, "ColorWRTSunDarkDescription", ColorWRTSunDarkDescription);
            ColorWRTSunLightName = reader.ReadString(Section, "ColorWRTSunLightName", ColorWRTSunLightName);
            ColorWRTSunLightDescription = reader.ReadString(Section, "ColorWRTSunLightDescription", ColorWRTSunLightDescription);
            ColorWRTHorizonDarkName = reader.ReadString(Section, "ColorWRTHorizonDarkName", ColorWRTHorizonDarkName);
            ColorWRTHorizonDarkDescription = reader.ReadString(Section, "ColorWRTHorizonDarkDescription", ColorWRTHorizonDarkDescription);
            ColorWRTHorizonLightName = reader.ReadString(Section, "ColorWRTHorizonLightName", ColorWRTHorizonLightName);
            ColorWRTHorizonLightDescription = reader.ReadString(Section, "ColorWRTHorizonLightDescription", ColorWRTHorizonLightDescription);
            ColorWRTShadowName = reader.ReadString(Section, "ColorWRTShadowName", ColorWRTShadowName);
            ColorWRTShadowDescription = reader.ReadString(Section, "ColorWRTShadowDescription", ColorWRTShadowDescription);
        }
    }
}
