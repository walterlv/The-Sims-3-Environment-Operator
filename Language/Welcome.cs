using System;
using System.Text;

namespace Seo.Language
{
    public class Welcome
    {
        public static string ChoosePackage = "Choose an Environment Configuration:";
        public static string ExportPackage = "Share my Environment Configuration:";

        private const string Section = "Welcome";
        public static void Initialize(LanguageReader lr)
        {
            ChoosePackage = lr.Read(Section, "ChoosePackage", ChoosePackage);
            ExportPackage = lr.Read(Section, "ExportPackage", ExportPackage);
        }
    }
}
