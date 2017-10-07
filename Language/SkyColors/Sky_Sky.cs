using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Sky_Sky
    {
        public static string DayColorName = "Sky";
        public static string DayColorDescription = "You can change the color of the sun, sky, and horizon here.";
        public static string SunColorName = "Sun Color";
        public static string SunColorDescription = "The color of sun contains two parts: the sun itself, and the halo.";
        public static string HaloColorName = "Halo Color";
        public static string HaloColorDescription = "You can cahnge the color of halo here.";
        public static string SkyDarkName = "Sky - Dark";
        public static string SkyDarkDescription = "Semisphere without Light";
        public static string SkyLightName = "Sky - Light";
        public static string SkyLightDescription = "Semisphere with Light";
        public static string HorizonLightName = "Horizon - Light";
        public static string HorizonLightDescription = "Horizon with Light";
        public static string HorizonDarkName = "Horizon - Dark";
        public static string HorizonDarkDescription = "Horizon without Light";

        private const string Section = "Sky_Sky";
        public static void Initialize(LanguageReader lr)
        {
            DayColorName = lr.Read(Section, "Name", DayColorName);
            DayColorDescription = lr.Read(Section, "Description", DayColorDescription);
            SunColorName = lr.Read(Section, "SunColorName", SunColorName);
            SunColorDescription = lr.Read(Section, "SunColorDescription", SunColorDescription);
            HaloColorName = lr.Read(Section, "HaloColorName", HaloColorName);
            HaloColorDescription = lr.Read(Section, "HaloColorDescription", HaloColorDescription);
            SkyDarkName = lr.Read(Section, "SkyDarkName", SkyDarkName);
            SkyDarkDescription = lr.Read(Section, "SkyDarkDescription", SkyDarkDescription);
            SkyLightName = lr.Read(Section, "SkyLightName", SkyLightName);
            SkyLightDescription = lr.Read(Section, "SkyLightDescription", SkyLightDescription);
            HorizonLightName = lr.Read(Section, "HorizonLightName", HorizonLightName);
            HorizonLightDescription = lr.Read(Section, "HorizonLightDescription", HorizonLightDescription);
            HorizonDarkName = lr.Read(Section, "HorizonDarkName", HorizonDarkName);
            HorizonDarkDescription = lr.Read(Section, "HorizonDarkDescription", HorizonDarkDescription);
        }
    }
}
