using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Sky_CustomSky
    {
        public static string DayColorName = "天空";
        public static string DayColorDescription = "改变天空的颜色：太阳、天空、地平线。";
        public static string SunColorName = "太阳 - 基础颜色";
        public static string SunColorDescription = "太阳光包括两个部分，一个是太阳自己，一个是它的光晕。";
        public static string HaloColorName = "太阳 - 光晕颜色";
        public static string HaloColorDescription = "这里是设置太阳光光晕的颜色。";
        public static string SkyDarkName = "天空 - 暗色";
        public static string SkyDarkDescription = "未被太阳照射的半个天空颜色。";
        public static string SkyLightName = "天空 - 亮色";
        public static string SkyLightDescription = "被太阳照射的半个天空颜色。";
        public static string HorizonLightName = "地平线 - 亮色";
        public static string HorizonLightDescription = "有阳光照射的地平线部分颜色。";
        public static string HorizonDarkName = "地平线 - 暗色";
        public static string HorizonDarkDescription = "无阳光照射的地平线部分颜色。";

        private const string Section = "Sky_CustomSky";
        public static void Initialize(LanguageReader lr)
        {
            DayColorName = lr.Read(Section, "Name", DayColorName);
            DayColorDescription = lr.Read(Section, "Description", DayColorDescription);
            SunColorName = lr.Read(Section, "SunColorName", SunColorName);
            SunColorDescription = lr.Read(Section, "SunColorDescription", SunColorDescription);
            HaloColorName = lr.Read(Section, "HaloColorName", HaloColorName);
            HaloColorDescription = lr.Read(Section, "HaloColorDescription", HaloColorDescription);
            SkyDarkName = lr.Read(Section, "SkyDarkName", SkyDarkName);
            SkyDarkDescription = lr.Read(Section, "SkyDarkDescription", SkyDarkDescription);
            SkyLightName = lr.Read(Section, "SkyLightName", SkyLightName);
            SkyLightDescription = lr.Read(Section, "SkyLightDescription", SkyLightDescription);
            HorizonLightName = lr.Read(Section, "HorizonLightName", HorizonLightName);
            HorizonLightDescription = lr.Read(Section, "HorizonLightDescription", HorizonLightDescription);
            HorizonDarkName = lr.Read(Section, "HorizonDarkName", HorizonDarkName);
            HorizonDarkDescription = lr.Read(Section, "HorizonDarkDescription", HorizonDarkDescription);
        }
    }
}
