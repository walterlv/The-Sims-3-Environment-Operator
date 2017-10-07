using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky
{
    public class Sky_Clear1 : SkyColor
    {
        public Sky_Clear1()
        {
            ColorFileName = "Sky_Clear1";
            ColorName = TS3Sky.Language.Sky_Clear1.DayColorName;
            ColorDescription = TS3Sky.Language.Sky_Clear1.DayColorDescription;
            DayColors.Add(new DayColor("ColorWRTSunDark", TS3Sky.Language.Sky_Clear1.ColorWRTSunDarkName, TS3Sky.Language.Sky_Clear1.ColorWRTSunDarkDescription));
            DayColors.Add(new DayColor("ColorWRTSunLight", TS3Sky.Language.Sky_Clear1.ColorWRTSunLightName, TS3Sky.Language.Sky_Clear1.ColorWRTSunLightDescription));
            DayColors.Add(new DayColor("ColorWRTHorizonDark", TS3Sky.Language.Sky_Clear1.ColorWRTHorizonDarkName, TS3Sky.Language.Sky_Clear1.ColorWRTHorizonDarkDescription));
            DayColors.Add(new DayColor("ColorWRTHorizonLight", TS3Sky.Language.Sky_Clear1.ColorWRTHorizonLightName, TS3Sky.Language.Sky_Clear1.ColorWRTHorizonLightDescription));
            DayColors.Add(new DayColor("ColorWRTShadow", TS3Sky.Language.Sky_Clear1.ColorWRTShadowName, TS3Sky.Language.Sky_Clear1.ColorWRTShadowDescription));
        }
    }
}
