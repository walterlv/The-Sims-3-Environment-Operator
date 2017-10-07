namespace Seo.Languages
{
    public class Sky_2
    {
        public static string DayColorName = null;
        public static string DayColorDescription = null;
        public static string ColorWRTSunDarkName = null;
        public static string ColorWRTSunDarkDescription = null;
        public static string ColorWRTSunLightName = null;
        public static string ColorWRTSunLightDescription = null;
        public static string ColorWRTHorizonDarkName = null;
        public static string ColorWRTHorizonDarkDescription = null;
        public static string ColorWRTHorizonLightName = null;
        public static string ColorWRTHorizonLightDescription = null;
        public static string ColorWRTShadowName = null;
        public static string ColorWRTShadowDescription = null;

        private const string Node = "Sky_2";
        public static void ReadFile(XmlFiles reader)
        {
            DayColorName = reader.TryRead(DayColorName, Node, "Name");
            DayColorDescription = reader.TryRead(DayColorDescription, Node, "Description");
            ColorWRTSunDarkName = reader.TryRead(ColorWRTSunDarkName, Node, "ColorWRTSunDark/Name");
            ColorWRTSunDarkDescription = reader.TryRead(ColorWRTSunDarkDescription, Node, "ColorWRTSunDark/Description");
            ColorWRTSunLightName = reader.TryRead(ColorWRTSunLightName, Node, "ColorWRTSunLight/Name");
            ColorWRTSunLightDescription = reader.TryRead(ColorWRTSunLightDescription, Node, "ColorWRTSunLight/Description");
            ColorWRTHorizonDarkName = reader.TryRead(ColorWRTHorizonDarkName, Node, "ColorWRTHorizonDark/Name");
            ColorWRTHorizonDarkDescription = reader.TryRead(ColorWRTHorizonDarkDescription, Node, "ColorWRTHorizonDark/Description");
            ColorWRTHorizonLightName = reader.TryRead(ColorWRTHorizonLightName, Node, "ColorWRTHorizonLight/Name");
            ColorWRTHorizonLightDescription = reader.TryRead(ColorWRTHorizonLightDescription, Node, "ColorWRTHorizonLight/Description");
            ColorWRTShadowName = reader.TryRead(ColorWRTShadowName, Node, "ColorWRTShadow/Name");
            ColorWRTShadowDescription = reader.TryRead(ColorWRTShadowDescription, Node, "ColorWRTShadow/Description");
        }
    }
}
