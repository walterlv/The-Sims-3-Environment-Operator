using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky
{
    public class Sky_1 : SkyColor
    {
        public Sky_1()
        {
            ColorName = TS3Sky.Language.Sky_1.DayColorName;
            ColorDescription = TS3Sky.Language.Sky_1.DayColorDescription;
            DayColors.Add(new DayColor("ColorWRTSunDark", TS3Sky.Language.Sky_1.ColorWRTSunDarkName, TS3Sky.Language.Sky_1.ColorWRTSunDarkDescription));
            DayColors.Add(new DayColor("ColorWRTSunLight", TS3Sky.Language.Sky_1.ColorWRTSunLightName, TS3Sky.Language.Sky_1.ColorWRTSunLightDescription));
            DayColors.Add(new DayColor("ColorWRTHorizonDark", TS3Sky.Language.Sky_1.ColorWRTHorizonDarkName, TS3Sky.Language.Sky_1.ColorWRTHorizonDarkDescription));
            DayColors.Add(new DayColor("ColorWRTHorizonLight", TS3Sky.Language.Sky_1.ColorWRTHorizonLightName, TS3Sky.Language.Sky_1.ColorWRTHorizonLightDescription));
            DayColors.Add(new DayColor("ColorWRTShadow", TS3Sky.Language.Sky_1.ColorWRTShadowName, TS3Sky.Language.Sky_1.ColorWRTShadowDescription));
        }
    }
}
