using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class ColorPickBar
    {
        public static string TheNextDay = "0:00 a.m.";
        public static string TimeInterval = "Time";
        public static string ColorValue = "Color";

        private const string Section = "ColorBar";
        public static void Initialize(LanguageReader lr)
        {
            TheNextDay = lr.Read(Section, "TheNextDay", TheNextDay);
            TimeInterval = lr.Read(Section, "TimeInterval", TimeInterval);
            ColorValue = lr.Read(Section, "ColorValue", ColorValue);
        }
    }
}
