using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo
{
    public class Sky_Light : SkyColor
    {
        public Sky_Light()
        {
            ColorName = Seo.Language.Sky_Light.DayColorName;
            ColorDescription = Seo.Language.Sky_Light.DayColorDescription;
            DayColors.Add(new DayColor("SunMoonLight", Seo.Language.Sky_Light.SunMoonLightName, Seo.Language.Sky_Light.SunMoonLightDescription));
            DayColors.Add(new DayColor("AmbientSkyTop", Seo.Language.Sky_Light.AmbientSkyTopName, Seo.Language.Sky_Light.AmbientSkyTopDescription));
            DayColors.Add(new DayColor("AmbientSkyBottom", Seo.Language.Sky_Light.AmbientSkyBottomName, Seo.Language.Sky_Light.AmbientSkyBottomDescription));
        }
    }
}
