using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo
{
    public class Sky_Sky : SkyColor
    {
        public Sky_Sky()
        {
            ColorName = Seo.Language.Sky_Sky.DayColorName;
            ColorDescription = Seo.Language.Sky_Sky.DayColorDescription;
            DayColors.Add(new DayColor("SunColor", Seo.Language.Sky_Sky.SunColorName, Seo.Language.Sky_Sky.SunColorDescription));
            DayColors.Add(new DayColor("HaloColor", Seo.Language.Sky_Sky.HaloColorName, Seo.Language.Sky_Sky.HaloColorDescription));
            DayColors.Add(new DayColor("SkyDark", Seo.Language.Sky_Sky.SkyDarkName, Seo.Language.Sky_Sky.SkyDarkDescription));
            DayColors.Add(new DayColor("SkyLight", Seo.Language.Sky_Sky.SkyLightName, Seo.Language.Sky_Sky.SkyLightDescription));
            DayColors.Add(new DayColor("HorizonLight", Seo.Language.Sky_Sky.HorizonLightName, Seo.Language.Sky_Sky.HorizonLightDescription));
            DayColors.Add(new DayColor("HorizonDark", Seo.Language.Sky_Sky.HorizonDarkName, Seo.Language.Sky_Sky.HorizonDarkDescription));
        }
    }
}
