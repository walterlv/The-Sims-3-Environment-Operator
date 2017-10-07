namespace Seo.Languages
{
    public class Sky_Sea
    {
        public static string DayColorName = null;
        public static string DayColorDescription = null;
        public static string WaterColorName = null;
        public static string WaterColorDescription = null;
        public static string SunMoonColorName = null;
        public static string SunMoonColorDescription = null;

        private const string Node = "Sky_Sea";
        public static void ReadFile(XmlFiles reader)
        {
            DayColorName = reader.TryRead(DayColorName, Node, "Name");
            DayColorDescription = reader.TryRead(DayColorDescription, Node, "Description");
            WaterColorName = reader.TryRead(WaterColorName, Node, "WaterColor/Name");
            WaterColorDescription = reader.TryRead(WaterColorDescription, Node, "WaterColor/Description");
            SunMoonColorName = reader.TryRead(SunMoonColorName, Node, "SunMoonColor/Name");
            SunMoonColorDescription = reader.TryRead(SunMoonColorDescription, Node, "SunMoonColor/Description");
        }
    }
}
