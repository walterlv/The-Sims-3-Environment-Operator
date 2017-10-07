using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Seo.WindowPages
{
    public class PageManager
    {
        public const int Count = 5;
        public static string[] PageNames = new string[] { "首页", "环境方案", "编辑环境", "设置", "关于" };
        public static Page[] Pages = new Page[Count];
        public static Page GetPageByIndex(int index)
        {
            if (index == 0) return FrontPage;
            else if (index == 1) return PackagePage;
            else if (index == 2) return OperatorPage;
            else if (index == 3) return SettingPage;
            else if (index == 4) return AboutPage;
            else return null;
        }

        private static Page frontPage;
        public static Page FrontPage
        {
            get
            {
                if (frontPage == null) { frontPage = new FrontPage(); Pages[0] = frontPage; }
                return frontPage;
            }
        }

        private static Page packagePage;
        public static Page PackagePage
        {
            get
            {
                if (packagePage == null) { packagePage = new PackagePage(); Pages[1] = packagePage; }
                return packagePage;
            }
        }

        private static Page operatorPage;
        public static Page OperatorPage
        {
            get
            {
                if (operatorPage == null) { operatorPage = new OperatorPage(); Pages[2] = operatorPage; }
                return operatorPage;
            }
        }

        private static Page settingPage;
        public static Page SettingPage
        {
            get
            {
                if (settingPage == null) { settingPage = new SettingPage(); Pages[3] = settingPage; }
                return settingPage;
            }
        }

        private static Page aboutPage;
        public static Page AboutPage
        {
            get
            {
                if (aboutPage == null) { aboutPage = new AboutPage(); Pages[4] = aboutPage; }
                return aboutPage;
            }
        }
    }
}
