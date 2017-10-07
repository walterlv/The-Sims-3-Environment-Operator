using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo
{
    public class Sky_Light : SkyColor,ILanguage, IDisposable
    {
        public Sky_Light()
        {
            DayColors.Add(new DayColor("SunMoonLight"));
            DayColors.Add(new DayColor("AmbientSkyTop"));
            DayColors.Add(new DayColor("AmbientSkyBottom"));
            Language.Register(this, Priority.Highest);
        }

        public void Dispose()
        {
            Seo.Language.UnRegister(this);
        }

        public void LoadLanguage()
        {
            ColorName = Seo.Languages.Sky_Light.DayColorName;
            ColorDescription = Seo.Languages.Sky_Light.DayColorDescription;
            DayColors[0].ColorName = Seo.Languages.Sky_Light.SunMoonLightName;
            DayColors[0].ColorDescription = Seo.Languages.Sky_Light.SunMoonLightDescription;
            DayColors[1].ColorName = Seo.Languages.Sky_Light.AmbientSkyTopName;
            DayColors[1].ColorDescription = Seo.Languages.Sky_Light.AmbientSkyTopDescription;
            DayColors[2].ColorName = Seo.Languages.Sky_Light.AmbientSkyBottomName;
            DayColors[2].ColorDescription = Seo.Languages.Sky_Light.AmbientSkyBottomDescription;
        }
    }
}
