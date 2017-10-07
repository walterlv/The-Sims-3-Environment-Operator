﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo.Language
{
    public class Sky_2
    {
        public static string DayColorName = "Cloud 2";
        public static string DayColorDescription = "You can change the color of Cloud2 here.";
        public static string ColorWRTSunDarkName = "Cloud2 without Sunlight";
        public static string ColorWRTSunDarkDescription = "The the color of cloud2 without sunlight";
        public static string ColorWRTSunLightName = "Cloud2 with Sunlight";
        public static string ColorWRTSunLightDescription = "The the color of cloud1 with sunlight";
        public static string ColorWRTHorizonDarkName = "Cloud2 at Horizon without Sunlight";
        public static string ColorWRTHorizonDarkDescription = "The color of cloud1 at horizon without sunlight";
        public static string ColorWRTHorizonLightName = "Cloud2 at Horizon with Sunlight";
        public static string ColorWRTHorizonLightDescription = "The color of cloud1 at horizon with sunlight";
        public static string ColorWRTShadowName = "Cloud2 Shadow";
        public static string ColorWRTShadowDescription = "The color of the cloud1's shadow";

        private const string Section = "Sky_2";
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