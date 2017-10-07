namespace Seo.Languages
{
    public class Sky_Light
    {
        public static string DayColorName = null;
        public static string DayColorDescription = null;
        public static string SunMoonLightName = null;
        public static string SunMoonLightDescription = null;
        public static string AmbientSkyTopName = null;
        public static string AmbientSkyTopDescription = null;
        public static string AmbientSkyBottomName = null;
        public static string AmbientSkyBottomDescription = null;

        private const string Node = "Sky_Light";
        public static void ReadFile(XmlFiles reader)
        {
            DayColorName = reader.TryRead(DayColorName, Node, "Name");
            DayColorDescription = reader.TryRead(DayColorDescription, Node, "Description");
            SunMoonLightName = reader.TryRead(SunMoonLightName, Node, "SunMoonLight/Name");
            SunMoonLightDescription = reader.TryRead(SunMoonLightDescription, Node, "SunMoonLight/Description");
            AmbientSkyTopName = reader.TryRead(AmbientSkyTopName, Node, "AmbientSkyTop/Name");
            AmbientSkyTopDescription = reader.TryRead(AmbientSkyTopDescription, Node, "AmbientSkyTop/Description");
            AmbientSkyBottomName = reader.TryRead(AmbientSkyBottomName, Node, "AmbientSkyBottom/Name");
            AmbientSkyBottomDescription = reader.TryRead(AmbientSkyBottomDescription, Node, "AmbientSkyBottom/Description");
        }
    }
}
