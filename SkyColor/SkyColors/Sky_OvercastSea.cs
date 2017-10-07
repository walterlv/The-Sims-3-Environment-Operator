using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky
{
    public class Sky_OvercastSea : SkyColor
    {
        public Sky_OvercastSea()
        {
            DayColors.Add(new DayColor("WaterColor", TS3Sky.Language.Sky_OvercastSea.WaterColorName, TS3Sky.Language.Sky_OvercastSea.WaterColorDescription));
            DayColors.Add(new DayColor("SunMoonColor", TS3Sky.Language.Sky_OvercastSea.SunMoonColorName, TS3Sky.Language.Sky_OvercastSea.SunMoonColorDescription));
        }
    }
}
