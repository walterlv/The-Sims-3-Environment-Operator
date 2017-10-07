using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo
{
    public class Sky_1 : SkyColor, ILanguage
    {
        public Sky_1()
        {
            ColorName = Languages.Sky_1.DayColorName;
            ColorDescription = Seo.Languages.Sky_1.DayColorDescription;
            DayColors.Add(new DayColor("ColorWRTSunDark"));
            DayColors.Add(new DayColor("ColorWRTSunLight"));
            DayColors.Add(new DayColor("ColorWRTHorizonDark"));
            DayColors.Add(new DayColor("ColorWRTHorizonLight"));
            DayColors.Add(new DayColor("ColorWRTShadow"));
            Language.Register(this, Priority.Highest);
        }

        ~Sky_1()
        {
            Language.UnRegister(this);
        }

        public void LoadLanguage()
        {
            DayColors[0].ColorName = Seo.Languages.Sky_1.ColorWRTSunDarkName;
            DayColors[0].ColorDescription = Seo.Languages.Sky_1.ColorWRTSunDarkDescription;
            DayColors[1].ColorName = Seo.Languages.Sky_1.ColorWRTSunLightName;
            DayColors[1].ColorDescription = Seo.Languages.Sky_1.ColorWRTSunLightDescription;
            DayColors[2].ColorName = Seo.Languages.Sky_1.ColorWRTHorizonDarkName;
            DayColors[2].ColorDescription = Seo.Languages.Sky_1.ColorWRTHorizonDarkDescription;
            DayColors[3].ColorName = Seo.Languages.Sky_1.ColorWRTHorizonLightName;
            DayColors[3].ColorDescription = Seo.Languages.Sky_1.ColorWRTHorizonLightDescription;
            DayColors[4].ColorName = Seo.Languages.Sky_1.ColorWRTShadowName;
            DayColors[4].ColorDescription = Seo.Languages.Sky_1.ColorWRTShadowDescription;
        }
    }
}
