using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class LanguageManager
    {
        private static string LocalPath;
        private static string LanguagePath;

        public static void Initialize()
        {
            LocalPath = Environment.CurrentDirectory + @"\Language\local.ini";
            LanguageReader llr = new LanguageReader(LocalPath);
            string local = llr.Read("Language", "Local", "zh-cn");
            LanguagePath = Environment.CurrentDirectory + @"\Language\" + local + ".ini";
            LanguageReader lr = new LanguageReader(LanguagePath);

            Application.Initialize(local);
            Navigation.Initialize(lr);
            About.Initialize(lr);
            Operation.Initialize(lr);
            Dialog.Initialize(lr);
            ColorPickBar.Initialize(lr);
            Sky_CustomSky.Initialize(lr);
            Sky_ClearSea.Initialize(lr);
            Sky_OvercastSea.Initialize(lr);

            lr.UpdateFile();
        }

        public static void Initialize(bool externLanguage)
        {
            if (externLanguage) { Initialize(); return; }
        }
    }
}
