using System;
using System.Text;

namespace TS3Sky.Language
{
    public class Welcome
    {
        public static string ChoosePackage = "选择一个你喜欢的环境配置方案：";
        public static string ExportPackage = "你也可以导出自己的方案分享给朋友：";

        private const string Section = "Welcome";
        public static void Initialize(LanguageReader lr)
        {
            ChoosePackage = lr.Read(Section, "ChoosePackage", ChoosePackage);
            ExportPackage = lr.Read(Section, "ExportPackage", ExportPackage);
        }
    }
}
