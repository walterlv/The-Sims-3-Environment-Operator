﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Sky_OvercastSea
    {
        public static string DayColorName = "海洋 - 阴";
        public static string DayColorDescription = "阴天时，改变水（包括海洋、河流、湖泊、池塘）的颜色。";
        public static string WaterColorName = "基础颜色";
        public static string WaterColorDescription = String.Empty;
        public static string SunMoonColorName = "倒影颜色";
        public static string SunMoonColorDescription = "（以下建议全是黑色）因为阴天天空并没有太阳月亮，所以请不要让水反射它的光。";

        private const string Section = "Sky_OvercastSea";
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
