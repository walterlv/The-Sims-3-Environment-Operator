using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo
{
    public class Sky_Sky : SkyColor,ILanguage
    {
        public Sky_Sky()
        {
            ColorName = Seo.Languages.Sky_Sky.DayColorName;
            ColorDescription = Seo.Languages.Sky_Sky.DayColorDescription;
            DayColors.Add(new DayColor("SunColor"));
            DayColors.Add(new DayColor("HaloColor"));
            DayColors.Add(new DayColor("SkyDark"));
            DayColors.Add(new DayColor("SkyLight"));
            DayColors.Add(new DayColor("HorizonLight"));
            DayColors.Add(new DayColor("HorizonDark"));
            Language.Register(this, Priority.Highest);
        }

        ~Sky_Sky()
        {
            Language.UnRegister(this);
        }

        public void LoadLanguage()
        {
            DayColors[0].ColorName = Seo.Languages.Sky_Sky.SunColorName;
            DayColors[0].ColorDescription = Seo.Languages.Sky_Sky.SunColorDescription;
            DayColors[1].ColorName = Seo.Languages.Sky_Sky.HaloColorName;
            DayColors[1].ColorDescription = Seo.Languages.Sky_Sky.HaloColorDescription;
            DayColors[2].ColorName = Seo.Languages.Sky_Sky.SkyDarkName;
            DayColors[2].ColorDescription = Seo.Languages.Sky_Sky.SkyDarkDescription;
            DayColors[3].ColorName = Seo.Languages.Sky_Sky.SkyLightName;
            DayColors[3].ColorDescription = Seo.Languages.Sky_Sky.SkyLightDescription;
            DayColors[4].ColorName = Seo.Languages.Sky_Sky.HorizonLightName;
            DayColors[4].ColorDescription = Seo.Languages.Sky_Sky.HorizonLightDescription;
            DayColors[5].ColorName = Seo.Languages.Sky_Sky.HorizonDarkName;
            DayColors[5].ColorDescription = Seo.Languages.Sky_Sky.HorizonDarkDescription;
        }
    }
}
