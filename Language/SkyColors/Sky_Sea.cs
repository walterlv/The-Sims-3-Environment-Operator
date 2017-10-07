namespace Seo.Languages
{
    public class Sky_Sea
    {
        public static string DayColorName = "Sea";
        public static string DayColorDescription = "You can change the color of water (sea, river,lake and pool) here.";
        public static string WaterColorName = "Water Color";
        public static string WaterColorDescription = "The basic color of water";
        public static string SunMoonColorName = "Water Reflection Color";
        public static string SunMoonColorDescription = "Water color reflected from the light from the sun and moon";

        private const string Section = "Sky_Sea";
        public static void ReadFile(IniFiles reader)
        {
            DayColorName = reader.ReadString(Section, "Name", DayColorName);
            DayColorDescription = reader.ReadString(Section, "Description", DayColorDescription);
            WaterColorName = reader.ReadString(Section, "WaterColorName", WaterColorName);
            WaterColorDescription = reader.ReadString(Section, "WaterColorDescription", WaterColorDescription);
            SunMoonColorName = reader.ReadString(Section, "SunMoonColorName", SunMoonColorName);
            SunMoonColorDescription = reader.ReadString(Section, "SunMoonColorDescription", SunMoonColorDescription);
        }
    }
}
