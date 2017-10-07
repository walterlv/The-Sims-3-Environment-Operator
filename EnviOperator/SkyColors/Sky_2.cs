using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo
{
    public class Sky_2 : SkyColor
    {
        public Sky_2()
        {
            ColorName = Seo.Language.Sky_2.DayColorName;
            ColorDescription = Seo.Language.Sky_2.DayColorDescription;
            DayColors.Add(new DayColor("ColorWRTSunDark", Seo.Language.Sky_2.ColorWRTSunDarkName, Seo.Language.Sky_2.ColorWRTSunDarkDescription));
            DayColors.Add(new DayColor("ColorWRTSunLight", Seo.Language.Sky_2.ColorWRTSunLightName, Seo.Language.Sky_2.ColorWRTSunLightDescription));
            DayColors.Add(new DayColor("ColorWRTHorizonDark", Seo.Language.Sky_2.ColorWRTHorizonDarkName, Seo.Language.Sky_2.ColorWRTHorizonDarkDescription));
            DayColors.Add(new DayColor("ColorWRTHorizonLight", Seo.Language.Sky_2.ColorWRTHorizonLightName, Seo.Language.Sky_2.ColorWRTHorizonLightDescription));
            DayColors.Add(new DayColor("ColorWRTShadow", Seo.Language.Sky_2.ColorWRTShadowName, Seo.Language.Sky_2.ColorWRTShadowDescription));
        }
    }
}
