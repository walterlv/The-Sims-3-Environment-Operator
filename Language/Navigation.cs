using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Navigation
    {
        public static string WelcomeLabel = "欢迎使用";
        public static string Welcome = "快速设置";
        public static string SettingsLabel = "高级设置";
        public static string OtherInfoLabel = "其它信息";
        public static string About = "关于";

        private const string Section = "Navigation";
        public static void Initialize(LanguageReader lr)
        {
            WelcomeLabel = lr.Read(Section, "WelcomeLabel", WelcomeLabel);
            Welcome = lr.Read(Section, "Welcome", Welcome);
            SettingsLabel = lr.Read(Section, "SettingsLabel", SettingsLabel);
            OtherInfoLabel = lr.Read(Section, "OtherInfoLabel", OtherInfoLabel);
            About = lr.Read(Section, "About", About);
        }
    }
}
