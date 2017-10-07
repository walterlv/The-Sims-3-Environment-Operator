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
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;

namespace Seo
{
    /// <summary>
    /// SetInstallDirWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SetInstallDirWindow : Window
    {
        public SetInstallDirWindow()
        {
            InitializeComponent();
            this.Title = Seo.Language.Dialog.CannotFindInstallDirTitle;
            SetDirLabel.Text = Seo.Language.Dialog.CannotFindInstallDirLabel;
            OKButton.Content = Seo.Language.Application.OK;
        }

        public string InstallDir;

        private void SetDirButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.ShowNewFolderButton = false;
            folder.SelectedPath = SetDirText.Text;
            if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SetDirText.Text = folder.SelectedPath;
                InstallDir = folder.SelectedPath;
                if (Directory.Exists(folder.SelectedPath + WeatherSky.SimsDirectoryTail))
                {
                    OKButton.IsEnabled = true;
                    ErrorMsg.Text = String.Empty;
                }
                else
                {
                    ErrorMsg.Text = Seo.Language.Dialog.NotTheRightInstallDirMsg;
                }
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            InstallDir = @"D:\The Sims 3\The Sims 3";
            DialogResult = true;
            this.Close();
        }
    }
}
