using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo
{
    public class Sky_Sea : SkyColor
    {
        public Sky_Sea()
        {
            ColorName = Seo.Language.Sky_Sea.DayColorName;
            ColorDescription = Seo.Language.Sky_Sea.DayColorDescription;
            DayColors.Add(new DayColor("WaterColor", Seo.Language.Sky_Sea.WaterColorName, Seo.Language.Sky_Sea.WaterColorDescription));
            DayColors.Add(new DayColor("SunMoonColor", Seo.Language.Sky_Sea.SunMoonColorName, Seo.Language.Sky_Sea.SunMoonColorDescription));
        }
    }
}
