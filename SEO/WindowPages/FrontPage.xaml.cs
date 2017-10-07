using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security;
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
    /// FrontPage.xaml 的交互逻辑
    /// </summary>
    public partial class FrontPage : Page, PageNavigation, ILanguage
    {
        public FrontPage()
        {
            InitializeComponent();
            Seo.Language.Register(this, Priority.Low);
        }

        ~FrontPage()
        {
            Seo.Language.UnRegister(this);
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
            this.Title = Seo.Languages.Window.FrontPage;
        }

        // 初始化
        private void StartButton_Click(object sender, WindowParts.SimpleButtonArgs e)
        {
            if (Directory.Exists(CustomFolderText.Text))
            {
                EnvironmentOperator.Instance.WorkDirectory = CustomFolderText.Text;
                if (!EnvironmentOperator.Instance.Prepare()) StatusBar.Show(Status.Error, "读取失败", 8000);
                else { StatusBar.Show(Status.Success, "读取成功", 5000); ((SimpleButton)sender).IsCustomEnabled = false; }
            }
            else
            {
                if (GetWorkDirectory())
                {
                    if (!EnvironmentOperator.Instance.Prepare()) StatusBar.Show(Status.Error, "读取失败", 8000);
                    else { StatusBar.Show(Status.Success, "读取成功", 5000); ((SimpleButton)sender).IsCustomEnabled = false; }
                }
            }
        }

        private bool GetWorkDirectory()
        {
            try
            {
                string directory = EnvironmentOperator.Instance.WorkDirectory;
            }
            catch
            {
                SimsDirWindow sdf = new SimsDirWindow();
                sdf.ShowDialog();
                if (sdf.SimsDir != null)
                {
                    Configs.SimsFolder = sdf.SimsDir;
                    EnvironmentOperator.Instance.WorkDirectory = sdf.SimsDir + EnvironmentOperator.SimsDirectoryTail;
                }
                else return false;
            }
            return true;
        }
    }
}
