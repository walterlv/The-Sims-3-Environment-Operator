using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky
{
    public class Sky_ClearSky : SkyColor
    {
        public Sky_ClearSky()
        {
            ColorFileName = "Sky_ClearSky";
            ColorName = TS3Sky.Language.Sky_ClearSky.DayColorName;
            ColorDescription = TS3Sky.Language.Sky_ClearSky.DayColorDescription;
            DayColors.Add(new DayColor("SunColor", TS3Sky.Language.Sky_ClearSky.SunColorName, TS3Sky.Language.Sky_ClearSky.SunColorDescription));
            DayColors.Add(new DayColor("HaloColor", TS3Sky.Language.Sky_ClearSky.HaloColorName, TS3Sky.Language.Sky_ClearSky.HaloColorDescription));
            DayColors.Add(new DayColor("SkyDark", TS3Sky.Language.Sky_ClearSky.SkyDarkName, TS3Sky.Language.Sky_ClearSky.SkyDarkDescription));
            DayColors.Add(new DayColor("SkyLight", TS3Sky.Language.Sky_ClearSky.SkyLightName, TS3Sky.Language.Sky_ClearSky.SkyLightDescription));
            DayColors.Add(new DayColor("HorizonLight", TS3Sky.Language.Sky_ClearSky.HorizonLightName, TS3Sky.Language.Sky_ClearSky.HorizonLightDescription));
            DayColors.Add(new DayColor("HorizonDark", TS3Sky.Language.Sky_ClearSky.HorizonDarkName, TS3Sky.Language.Sky_ClearSky.HorizonDarkDescription));
        }
    }
}
