using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Sky_ClearLight
    {
        public static string DayColorName = "亮光";
        public static string DayColorDescription = "改变游戏里太阳光和月光（同时改变）";
        public static string SunMoonLightName = "光色";
        public static string SunMoonLightDescription = "月光和太阳光的颜色";
        public static string AmbientSkyTopName = "物体顶部反光颜色";
        public static string AmbientSkyTopDescription = "物体顶部反光的颜色";
        public static string AmbientSkyBottomName = "物体底部反光颜色";
        public static string AmbientSkyBottomDescription = "物体底部反光的颜色";

        private const string Section = "Sky_ClearLight";
        public static void Initialize(LanguageReader lr)
        {
            DayColorName = lr.Read(Section, "Name", DayColorName);
            DayColorDescription = lr.Read(Section, "Description", DayColorDescription);
            SunMoonLightName = lr.Read(Section, "SunMoonLightName", SunMoonLightName);
            SunMoonLightDescription = lr.Read(Section, "SunMoonLightDescription", SunMoonLightDescription);
            AmbientSkyTopName = lr.Read(Section, "AmbientSkyTopName", AmbientSkyTopName);
            AmbientSkyTopDescription = lr.Read(Section, "AmbientSkyTopDescription", AmbientSkyTopDescription);
            AmbientSkyBottomName = lr.Read(Section, "AmbientSkyBottomName", AmbientSkyBottomName);
            AmbientSkyBottomDescription = lr.Read(Section, "AmbientSkyBottomDescription", AmbientSkyBottomDescription);
        }
    }
}
