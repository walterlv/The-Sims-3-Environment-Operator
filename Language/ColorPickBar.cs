using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class ColorPickBar
    {
        public static string TheNextDay = "次日凌晨";
        public static string TimeInterval = "时间段";
        public static string ColorValue = "颜色值";

        private const string Section = "ColorBar";
        public static void Initialize(LanguageReader lr)
        {
            TheNextDay = lr.Read(Section, "TheNextDay", TheNextDay);
            TimeInterval = lr.Read(Section, "TimeInterval", TimeInterval);
            ColorValue = lr.Read(Section, "ColorValue", ColorValue);
        }
    }
}
