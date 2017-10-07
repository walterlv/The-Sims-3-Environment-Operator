using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Navigation
    {
        public static string Welcome = "欢迎";
        public static string About = "关于";
        public static string Label = "作者：walterlv、Kuree\n发布：3DMGAME M3小组\n模拟人生3全版本通用";

        private const string Section = "Navigation";
        public static void Initialize(LanguageReader lr)
        {
            Welcome = lr.Read(Section, "Welcome", Welcome);
            About = lr.Read(Section, "About", About);
            Label = lr.Read(Section, "Label", Label);
        }
    }
}
