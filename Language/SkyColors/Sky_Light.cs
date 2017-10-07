namespace Seo.Languages
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
        public static void ReadFile(IniFiles reader)
        {
            DayColorName = reader.ReadString(Section, "Name", DayColorName);
            DayColorDescription = reader.ReadString(Section, "Description", DayColorDescription);
            SunMoonLightName = reader.ReadString(Section, "SunMoonLightName", SunMoonLightName);
            SunMoonLightDescription = reader.ReadString(Section, "SunMoonLightDescription", SunMoonLightDescription);
            AmbientSkyTopName = reader.ReadString(Section, "AmbientSkyTopName", AmbientSkyTopName);
            AmbientSkyTopDescription = reader.ReadString(Section, "AmbientSkyTopDescription", AmbientSkyTopDescription);
            AmbientSkyBottomName = reader.ReadString(Section, "AmbientSkyBottomName", AmbientSkyBottomName);
            AmbientSkyBottomDescription = reader.ReadString(Section, "AmbientSkyBottomDescription", AmbientSkyBottomDescription);
        }
    }
}
