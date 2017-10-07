using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky
{
    public class Sky_Light : SkyColor
    {
        public Sky_Light()
        {
            ColorName = TS3Sky.Language.Sky_Light.DayColorName;
            ColorDescription = TS3Sky.Language.Sky_Light.DayColorDescription;
            DayColors.Add(new DayColor("SunMoonLight", TS3Sky.Language.Sky_Light.SunMoonLightName, TS3Sky.Language.Sky_Light.SunMoonLightDescription));
            DayColors.Add(new DayColor("AmbientSkyTop", TS3Sky.Language.Sky_Light.AmbientSkyTopName, TS3Sky.Language.Sky_Light.AmbientSkyTopDescription));
            DayColors.Add(new DayColor("AmbientSkyBottom", TS3Sky.Language.Sky_Light.AmbientSkyBottomName, TS3Sky.Language.Sky_Light.AmbientSkyBottomDescription));
        }
    }
}
