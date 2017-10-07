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
using Seo.WindowParts;

namespace Seo.WindowPages
{
    /// <summary>
    /// AboutPage.xaml 的交互逻辑
    /// </summary>
    public partial class AboutPage : Page, PageNavigation
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

        private void ForumSiteClick(object sender, MouseButtonEventArgs e)
        {
            CommonOperation.VisitSite("http://bbs.3dmgame.com/forum-402-1.html");
        }

        bool IsUpdating = false;
        private void UpdateButton_Click(object sender, WindowParts.SimpleButtonArgs e)
        {
            if (IsUpdating) return;
            IsUpdating = true;
            StatusBar.Show(Status.Progress, "Checking for Update...");
            CommonOperation update = new CommonOperation();
            update.UpdateChecked += update_UpdateChecked;
            update.GetUpdateAsync();
        }
        private void update_UpdateChecked(object sender, UpdateArgs e)
        {
            IsUpdating = false;
            if (e.IsSuccess)
            {
                if (e.NewVersion.UpdateExisted)
                {
                    StatusBar.Show(Status.Success, "New version is available: " + e.NewVersion.Version, 5000);
                    if (MessageBox.Show(e.NewVersion.ToString() + Environment.NewLine + Environment.NewLine + "Would you like to download now?",
                        "New Version is Available", MessageBoxButton.YesNo, MessageBoxImage.Information)
                        == MessageBoxResult.Yes)
                        CommonOperation.VisitSite(Seo.Language.PublishSite);
                }
                else
                {
                    StatusBar.Show(Status.Success, "This version is the latest.", 5000);
                }
            }
            else
            {
                StatusBar.Show(Status.Error, "We could not check for updates for the moment, please try it later.", 10000);
            }
        }

        private void ContactButton_Click(object sender, WindowParts.SimpleButtonArgs e)
        {
            CommonOperation.VisitSite(Seo.Language.PublishSite);
        }
    }
}
