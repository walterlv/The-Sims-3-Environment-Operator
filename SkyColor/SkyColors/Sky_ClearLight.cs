using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky
{
    public class Sky_ClearLight : SkyColor
    {
        public Sky_ClearLight()
        {
            ColorFileName = "Sky_ClearLight";
            ColorName = TS3Sky.Language.Sky_ClearLight.DayColorName;
            ColorDescription = TS3Sky.Language.Sky_ClearLight.DayColorDescription;
            DayColors.Add(new DayColor("SunMoonLight", TS3Sky.Language.Sky_ClearLight.SunMoonLightName, TS3Sky.Language.Sky_ClearLight.SunMoonLightDescription));
            DayColors.Add(new DayColor("AmbientSkyTop", TS3Sky.Language.Sky_ClearLight.AmbientSkyTopName, TS3Sky.Language.Sky_ClearLight.AmbientSkyTopDescription));
            DayColors.Add(new DayColor("AmbientSkyBottom", TS3Sky.Language.Sky_ClearLight.AmbientSkyBottomName, TS3Sky.Language.Sky_ClearLight.AmbientSkyBottomDescription));
        }
    }
}
