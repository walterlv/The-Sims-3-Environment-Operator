using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo
{
    public class Sky_Sea : SkyColor,ILanguage, IDisposable
    {
        public Sky_Sea()
        {
            DayColors.Add(new DayColor("WaterColor"));
            DayColors.Add(new DayColor("SunMoonColor"));
            Language.Register(this, Priority.Highest);
        }

        public void Dispose()
        {
            Seo.Language.UnRegister(this);
        }

        public void LoadLanguage()
        {
            ColorName = Seo.Languages.Sky_Sea.DayColorName;
            ColorDescription = Seo.Languages.Sky_Sea.DayColorDescription;
            DayColors[0].ColorName = Seo.Languages.Sky_Sea.WaterColorName;
            DayColors[0].ColorDescription = Seo.Languages.Sky_Sea.WaterColorDescription;
            DayColors[1].ColorName = Seo.Languages.Sky_Sea.SunMoonColorName;
            DayColors[1].ColorDescription = Seo.Languages.Sky_Sea.SunMoonColorDescription;
        }
    }
}
