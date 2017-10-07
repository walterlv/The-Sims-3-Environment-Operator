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

namespace Seo
{
    /// <summary>
    /// SimsDirWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SimsDirWindow : Window, ILanguage, IDisposable
    {
        public SimsDirWindow()
        {
            InitializeComponent();
            Seo.Language.Register(this, Priority.Normal);
        }

        public void Dispose()
        {
            Seo.Language.UnRegister(this);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FolderText.Focus();
        }

        public void LoadLanguage()
        {
            this.Title = Seo.Languages.Window.SimsDirTitle;
            DescriptionText.Text = Seo.Languages.Window.SimsDirContent;
            OkButton.Text = Seo.Languages.Window.OK;
            CancelButton.Text = Seo.Languages.Window.Cancel;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) this.Close();
            else if (e.Key == Key.Enter) OkButton_Click(this, null);
        }

        public string SimsDir { get; private set; }

        private void FolderButton_Click(object sender, WindowParts.SimpleButtonArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folder = new System.Windows.Forms.FolderBrowserDialog();
            folder.ShowNewFolderButton = false;
            folder.SelectedPath = FolderText.Text;
            if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderText.Text = folder.SelectedPath;
            }
            FolderText.Focus();
            FolderText.SelectAll();
        }

        private void FolderText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.IO.Directory.Exists(FolderText.Text + EnvironmentOperator.SimsDirectoryTail))
            {
                OkButton.IsCustomEnabled = true;
                ErrorMsg.Text = String.Empty;
            }
            else
            {
                OkButton.IsCustomEnabled = false;
                OkButton.DisableTip = Seo.Languages.Window.SimsDirInvalid;
                ErrorMsg.Text = Seo.Languages.Window.SimsDirInvalid;
            }
        }

        private void OkButton_Click(object sender, WindowParts.SimpleButtonArgs e)
        {
            if (System.IO.Directory.Exists(FolderText.Text + EnvironmentOperator.SimsDirectoryTail))
            {
                SimsDir = FolderText.Text;
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, WindowParts.SimpleButtonArgs e)
        {
            this.Close();
        }
    }
}
