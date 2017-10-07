using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Seo.WindowPages
{
    /// <summary>
    /// AboutPage.xaml 的交互逻辑
    /// </summary>
    public partial class AboutPage : Page, PageNavigation, ILanguage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        public bool NavigationIn()
        {
            return true;
        }

        public bool NavigationOut()
        {
            return true;
        }

        public void LoadLanguage()
        {
            this.Title = Seo.Languages.Window.AboutPage;
        }

        private void ForumSiteClick(object sender, MouseButtonEventArgs e)
        {
            CommonOperation.VisitSite("http://bbs.3dmgame.com/forum-402-1.html");
        }
    }
}
