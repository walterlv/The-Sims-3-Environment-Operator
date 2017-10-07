using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo
{
    public class Sky_2 : SkyColor, ILanguage
    {
        public Sky_2()
        {
            ColorName = Seo.Languages.Sky_2.DayColorName;
            ColorDescription = Seo.Languages.Sky_2.DayColorDescription;
            DayColors.Add(new DayColor("ColorWRTSunDark"));
            DayColors.Add(new DayColor("ColorWRTSunLight"));
            DayColors.Add(new DayColor("ColorWRTHorizonDark"));
            DayColors.Add(new DayColor("ColorWRTHorizonLight"));
            DayColors.Add(new DayColor("ColorWRTShadow"));
            Language.Register(this, Priority.Highest);
        }

        ~Sky_2()
        {
            Language.UnRegister(this);
        }

        public void LoadLanguage()
        {
            DayColors[0].ColorName = Seo.Languages.Sky_2.ColorWRTSunDarkName;
            DayColors[0].ColorDescription = Seo.Languages.Sky_2.ColorWRTSunDarkDescription;
            DayColors[1].ColorName = Seo.Languages.Sky_2.ColorWRTSunLightName;
            DayColors[1].ColorDescription = Seo.Languages.Sky_2.ColorWRTSunLightDescription;
            DayColors[2].ColorName = Seo.Languages.Sky_2.ColorWRTHorizonDarkName;
            DayColors[2].ColorDescription = Seo.Languages.Sky_2.ColorWRTHorizonDarkDescription;
            DayColors[3].ColorName = Seo.Languages.Sky_2.ColorWRTHorizonLightName;
            DayColors[3].ColorDescription = Seo.Languages.Sky_2.ColorWRTHorizonLightDescription;
            DayColors[4].ColorName = Seo.Languages.Sky_2.ColorWRTShadowName;
            DayColors[4].ColorDescription = Seo.Languages.Sky_2.ColorWRTShadowDescription;
        }
    }
}
