using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky
{
    public class Sky_ClearSea : SkyColor
    {
        public Sky_ClearSea()
        {
            DayColors.Add(new DayColor("WaterColor", TS3Sky.Language.Sky_ClearSea.WaterColorName, TS3Sky.Language.Sky_ClearSea.WaterColorDescription));
            DayColors.Add(new DayColor("SunMoonColor", TS3Sky.Language.Sky_ClearSea.SunMoonColorName, TS3Sky.Language.Sky_ClearSea.SunMoonColorDescription));
        }
    }
}
