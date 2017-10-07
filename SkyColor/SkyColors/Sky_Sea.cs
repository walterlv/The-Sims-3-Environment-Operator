using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky
{
    public class Sky_Sea : SkyColor
    {
        public Sky_Sea()
        {
            ColorName = TS3Sky.Language.Sky_Sea.DayColorName;
            ColorDescription = TS3Sky.Language.Sky_Sea.DayColorDescription;
            DayColors.Add(new DayColor("WaterColor", TS3Sky.Language.Sky_Sea.WaterColorName, TS3Sky.Language.Sky_Sea.WaterColorDescription));
            DayColors.Add(new DayColor("SunMoonColor", TS3Sky.Language.Sky_Sea.SunMoonColorName, TS3Sky.Language.Sky_Sea.SunMoonColorDescription));
        }
    }
}
