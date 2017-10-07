using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky
{
    public class Sky_Sky : SkyColor
    {
        public Sky_Sky()
        {
            ColorName = TS3Sky.Language.Sky_Sky.DayColorName;
            ColorDescription = TS3Sky.Language.Sky_Sky.DayColorDescription;
            DayColors.Add(new DayColor("SunColor", TS3Sky.Language.Sky_Sky.SunColorName, TS3Sky.Language.Sky_Sky.SunColorDescription));
            DayColors.Add(new DayColor("HaloColor", TS3Sky.Language.Sky_Sky.HaloColorName, TS3Sky.Language.Sky_Sky.HaloColorDescription));
            DayColors.Add(new DayColor("SkyDark", TS3Sky.Language.Sky_Sky.SkyDarkName, TS3Sky.Language.Sky_Sky.SkyDarkDescription));
            DayColors.Add(new DayColor("SkyLight", TS3Sky.Language.Sky_Sky.SkyLightName, TS3Sky.Language.Sky_Sky.SkyLightDescription));
            DayColors.Add(new DayColor("HorizonLight", TS3Sky.Language.Sky_Sky.HorizonLightName, TS3Sky.Language.Sky_Sky.HorizonLightDescription));
            DayColors.Add(new DayColor("HorizonDark", TS3Sky.Language.Sky_Sky.HorizonDarkName, TS3Sky.Language.Sky_Sky.HorizonDarkDescription));
        }
    }
}
