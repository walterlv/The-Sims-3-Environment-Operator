using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Sky_Light
    {
        public static string DayColorName = "Light";
        public static string DayColorDescription = "The light's color of both sun and moon.";
        public static string SunMoonLightName = "Sun and Moon Light";
        public static string SunMoonLightDescription = "The color of sunlght and moonlight";
        public static string AmbientSkyTopName = "Ambient Sky Top";
        public static string AmbientSkyTopDescription = "The color of reflection on the top in night";
        public static string AmbientSkyBottomName = "Ambient Sky Bottom";
        public static string AmbientSkyBottomDescription = "The color of reflection at the bottom in night";

        private const string Section = "Sky_Light";
        public static void Initialize(LanguageReader lr)
        {
            DayColorName = lr.Read(Section, "Name", DayColorName);
            DayColorDescription = lr.Read(Section, "Description", DayColorDescription);
            SunMoonLightName = lr.Read(Section, "SunMoonLightName", SunMoonLightName);
            SunMoonLightDescription = lr.Read(Section, "SunMoonLightDescription", SunMoonLightDescription);
            AmbientSkyTopName = lr.Read(Section, "AmbientSkyTopName", AmbientSkyTopName);
            AmbientSkyTopDescription = lr.Read(Section, "AmbientSkyTopDescription", AmbientSkyTopDescription);
            AmbientSkyBottomName = lr.Read(Section, "AmbientSkyBottomName", AmbientSkyBottomName);
            AmbientSkyBottomDescription = lr.Read(Section, "AmbientSkyBottomDescription", AmbientSkyBottomDescription);
        }
    }
}
