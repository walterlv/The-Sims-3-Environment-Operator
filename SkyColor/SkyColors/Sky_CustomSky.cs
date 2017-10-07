using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky
{
    public class Sky_CustomSky : SkyColor
    {
        public Sky_CustomSky()
        {
            DayColors.Add(new DayColor("SunColor", TS3Sky.Language.Sky_CustomSky.SunColorName, TS3Sky.Language.Sky_CustomSky.SunColorDescription));
            DayColors.Add(new DayColor("HaloColor", TS3Sky.Language.Sky_CustomSky.HaloColorName, TS3Sky.Language.Sky_CustomSky.HaloColorDescription));
            DayColors.Add(new DayColor("SkyDark", TS3Sky.Language.Sky_CustomSky.SkyDarkName, TS3Sky.Language.Sky_CustomSky.SkyDarkDescription));
            DayColors.Add(new DayColor("SkyLight", TS3Sky.Language.Sky_CustomSky.SkyLightName, TS3Sky.Language.Sky_CustomSky.SkyLightDescription));
            DayColors.Add(new DayColor("HorizonLight", TS3Sky.Language.Sky_CustomSky.HorizonLightName, TS3Sky.Language.Sky_CustomSky.HorizonLightDescription));
            DayColors.Add(new DayColor("HorizonDark", TS3Sky.Language.Sky_CustomSky.HorizonDarkName, TS3Sky.Language.Sky_CustomSky.HorizonDarkDescription));
        }
    }
}
