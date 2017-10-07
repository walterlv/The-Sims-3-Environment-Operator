using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky
{
    public class Sky_2 : SkyColor
    {
        public Sky_2()
        {
            ColorName = TS3Sky.Language.Sky_2.DayColorName;
            ColorDescription = TS3Sky.Language.Sky_2.DayColorDescription;
            DayColors.Add(new DayColor("ColorWRTSunDark", TS3Sky.Language.Sky_2.ColorWRTSunDarkName, TS3Sky.Language.Sky_2.ColorWRTSunDarkDescription));
            DayColors.Add(new DayColor("ColorWRTSunLight", TS3Sky.Language.Sky_2.ColorWRTSunLightName, TS3Sky.Language.Sky_2.ColorWRTSunLightDescription));
            DayColors.Add(new DayColor("ColorWRTHorizonDark", TS3Sky.Language.Sky_2.ColorWRTHorizonDarkName, TS3Sky.Language.Sky_2.ColorWRTHorizonDarkDescription));
            DayColors.Add(new DayColor("ColorWRTHorizonLight", TS3Sky.Language.Sky_2.ColorWRTHorizonLightName, TS3Sky.Language.Sky_2.ColorWRTHorizonLightDescription));
            DayColors.Add(new DayColor("ColorWRTShadow", TS3Sky.Language.Sky_2.ColorWRTShadowName, TS3Sky.Language.Sky_2.ColorWRTShadowDescription));
        }
    }
}
