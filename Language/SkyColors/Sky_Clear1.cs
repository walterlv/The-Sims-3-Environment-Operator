using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Sky_Clear1
    {
        public static string DayColorName = "云1";
        public static string DayColorDescription = "改变云的颜色";
        public static string ColorWRTSunDarkName = "云-未照射";
        public static string ColorWRTSunDarkDescription = "未被太阳照射的云的颜色。";
        public static string ColorWRTSunLightName = "云-照射";
        public static string ColorWRTSunLightDescription = "被太阳照射的云的颜色。";
        public static string ColorWRTHorizonDarkName = "云-地平线-暗色";
        public static string ColorWRTHorizonDarkDescription = "无阳光照射的地平线云部分颜色。";
        public static string ColorWRTHorizonLightName = "云-地平线-亮色";
        public static string ColorWRTHorizonLightDescription = "阳光照射的地平线云部分颜色。";
        public static string ColorWRTShadowName = "云影";
        public static string ColorWRTShadowDescription = "云的阴影颜色";

        private const string Section = "Sky_Clear1";
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
