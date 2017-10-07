namespace Seo.Languages
{
    public class Sky_Sky
    {
        public static string DayColorName = null;
        public static string DayColorDescription = null;
        public static string SunColorName = null;
        public static string SunColorDescription = null;
        public static string HaloColorName = null;
        public static string HaloColorDescription = null;
        public static string SkyDarkName = null;
        public static string SkyDarkDescription = null;
        public static string SkyLightName = null;
        public static string SkyLightDescription = null;
        public static string HorizonLightName = null;
        public static string HorizonLightDescription = null;
        public static string HorizonDarkName = null;
        public static string HorizonDarkDescription = null;

        private const string Node = "Sky_Sky";
        public static void ReadFile(XmlFiles reader)
        {
            DayColorName = reader.TryRead(DayColorName, Node, "Name");
            DayColorDescription = reader.TryRead(DayColorDescription, Node, "Description");
            SunColorName = reader.TryRead(SunColorName, Node, "SunColor/Name");
            SunColorDescription = reader.TryRead(SunColorDescription, Node, "SunColor/Description");
            HaloColorName = reader.TryRead(HaloColorName, Node, "HaloColor/Name");
            HaloColorDescription = reader.TryRead(HaloColorDescription, Node, "HaloColor/Description");
            SkyDarkName = reader.TryRead(SkyDarkName, Node, "SkyDark/Name");
            SkyDarkDescription = reader.TryRead(SkyDarkDescription, Node, "SkyDark/Description");
            SkyLightName = reader.TryRead(SkyLightName, Node, "SkyLight/Name");
            SkyLightDescription = reader.TryRead(SkyLightDescription, Node, "SkyLight/Description");
            HorizonLightName = reader.TryRead(HorizonLightName, Node, "HorizonLight/Name");
            HorizonLightDescription = reader.TryRead(HorizonLightDescription, Node, "HorizonLight/Description");
            HorizonDarkName = reader.TryRead(HorizonDarkName, Node, "HorizonDark/Name");
            HorizonDarkDescription = reader.TryRead(HorizonDarkDescription, Node, "HorizonDark/Description");
        }
    }
}
