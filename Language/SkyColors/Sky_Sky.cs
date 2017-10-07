namespace Seo.Languages
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
        public static void ReadFile(IniFiles reader)
        {
            DayColorName = reader.ReadString(Section, "Name", DayColorName);
            DayColorDescription = reader.ReadString(Section, "Description", DayColorDescription);
            SunColorName = reader.ReadString(Section, "SunColorName", SunColorName);
            SunColorDescription = reader.ReadString(Section, "SunColorDescription", SunColorDescription);
            HaloColorName = reader.ReadString(Section, "HaloColorName", HaloColorName);
            HaloColorDescription = reader.ReadString(Section, "HaloColorDescription", HaloColorDescription);
            SkyDarkName = reader.ReadString(Section, "SkyDarkName", SkyDarkName);
            SkyDarkDescription = reader.ReadString(Section, "SkyDarkDescription", SkyDarkDescription);
            SkyLightName = reader.ReadString(Section, "SkyLightName", SkyLightName);
            SkyLightDescription = reader.ReadString(Section, "SkyLightDescription", SkyLightDescription);
            HorizonLightName = reader.ReadString(Section, "HorizonLightName", HorizonLightName);
            HorizonLightDescription = reader.ReadString(Section, "HorizonLightDescription", HorizonLightDescription);
            HorizonDarkName = reader.ReadString(Section, "HorizonDarkName", HorizonDarkName);
            HorizonDarkDescription = reader.ReadString(Section, "HorizonDarkDescription", HorizonDarkDescription);
        }
    }
}
