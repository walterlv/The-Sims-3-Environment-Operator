using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Seo.WindowPages
{
    public class PageManager : ILanguage
    {
        public PageManager()
        {
            Seo.Language.Register(this, Priority.High);
        }

        ~PageManager()
        {
            Seo.Language.UnRegister(this);
        }

        public const int Count = 5;
        private string[] pageNames = null;
        public string[] PageNames
        {
            get
            {
                if (pageNames == null)
                {
                    pageNames = new string[5];
                    pageNames[0] = Seo.Languages.Window.FrontPage;
                    pageNames[1] = Seo.Languages.Window.EnvironmentPage;
                    pageNames[2] = Seo.Languages.Window.OperatorPage;
                    pageNames[3] = Seo.Languages.Window.SettingsPage;
                    pageNames[4] = Seo.Languages.Window.AboutPage;
                }
                return pageNames;
            }
        }

        public void LoadLanguage()
        {
            PageNames[0] = Seo.Languages.Window.FrontPage;
            PageNames[1] = Seo.Languages.Window.EnvironmentPage;
            PageNames[2] = Seo.Languages.Window.OperatorPage;
            PageNames[3] = Seo.Languages.Window.SettingsPage;
            PageNames[4] = Seo.Languages.Window.AboutPage;
        }

        private Page[] Pages = new Page[Count];
        public Page GetPageByIndex(int index)
        {
            if (Pages[index] != null) return Pages[index];
            else
            {
                Page result = null;
                if (index == 0) result = FrontPage;
                else if (index == 1) return PackagePage;
                else if (index == 2) return OperatorPage;
                else if (index == 3) return SettingPage;
                else if (index == 4) return AboutPage;
                result.Title = PageNames[index];
                Pages[index] = result;
                return result;
            }
        }

        private Page frontPage;
        public Page FrontPage
        {
            get
            {
                if (frontPage == null) { frontPage = new FrontPage(); }
                return frontPage;
            }
        }

        private Page packagePage;
        public Page PackagePage
        {
            get
            {
                if (packagePage == null) { packagePage = new EnvironmentPage(); }
                return packagePage;
            }
        }

        private Page operatorPage;
        public Page OperatorPage
        {
            get
            {
                if (operatorPage == null) { operatorPage = new OperatorPage(); }
                return operatorPage;
            }
        }

        private Page settingPage;
        public Page SettingPage
        {
            get
            {
                if (settingPage == null) { settingPage = new SettingPage(); }
                return settingPage;
            }
        }

        private Page aboutPage;
        private Page aboutPage_chs;
        private Page aboutPage_cht;
        public Page AboutPage
        {
            get
            {
                if (Language.Local.Equals("zh-CN"))
                {
                    if (aboutPage_chs == null) { aboutPage_chs = new AboutPage_chs(); }
                    return aboutPage_chs;
                }
                else if (Language.Local.Equals("zh-HK") || Language.Local.Equals("zh-MO") || Language.Local.Equals("zh-TW"))
                {
                    if (aboutPage_cht == null) { aboutPage_cht = new AboutPage_cht(); }
                    return aboutPage_cht;
                }
                else
                {
                    if (aboutPage == null) { aboutPage = new AboutPage(); }
                    return aboutPage;
                }
            }
        }
    }
}
