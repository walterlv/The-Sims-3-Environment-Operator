using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo
{
    public class Configs
    {
        private static string ConfigFile = "";
        private const string customColorSection = "CustomColors";

        public static string SimsFolder;

        public static bool GlassWindow = false;

        private static int[] customColors;
        public static int[] CustomColors
        {
            get { if (customColors == null) ReadCustomColors(); return customColors; }
            set { customColors = value; }
        }
        public static void ReadCustomColors()
        {
            IniFiles ccf = new IniFiles(ConfigFile);
            List<int> cca = new List<int>();
            int temp = 0;
            for (int i = 0; ; i++)
            {
                if (ccf.ValueExists(customColorSection, i.ToString()))
                {
                    temp = ccf.ReadInteger(customColorSection, i.ToString(), 16777215);
                    cca.Add(temp);
                }
                else break;
            }
            ccf.UpdateFile();
            customColors = cca.ToArray();
        }
        public static void SaveCustomColors()
        {
            if (customColors == null) return;
            IniFiles ccf = new IniFiles(ConfigFile);
            ccf.EraseSection(customColorSection);
            for (int i = 0; i < customColors.Length; i++)
            {
                ccf.WriteInteger(customColorSection, i.ToString(), customColors[i]);
            }
            ccf.UpdateFile();
        }
    }
}
