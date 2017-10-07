using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo
{
    public class Sky_1 : SkyColor
    {
        public Sky_1()
        {
            ColorName = Seo.Language.Sky_1.DayColorName;
            ColorDescription = Seo.Language.Sky_1.DayColorDescription;
            DayColors.Add(new DayColor("ColorWRTSunDark", Seo.Language.Sky_1.ColorWRTSunDarkName, Seo.Language.Sky_1.ColorWRTSunDarkDescription));
            DayColors.Add(new DayColor("ColorWRTSunLight", Seo.Language.Sky_1.ColorWRTSunLightName, Seo.Language.Sky_1.ColorWRTSunLightDescription));
            DayColors.Add(new DayColor("ColorWRTHorizonDark", Seo.Language.Sky_1.ColorWRTHorizonDarkName, Seo.Language.Sky_1.ColorWRTHorizonDarkDescription));
            DayColors.Add(new DayColor("ColorWRTHorizonLight", Seo.Language.Sky_1.ColorWRTHorizonLightName, Seo.Language.Sky_1.ColorWRTHorizonLightDescription));
            DayColors.Add(new DayColor("ColorWRTShadow", Seo.Language.Sky_1.ColorWRTShadowName, Seo.Language.Sky_1.ColorWRTShadowDescription));
        }
    }
}
