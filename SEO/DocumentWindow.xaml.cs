using System;
using System.Collections.Generic;
using System.IO;
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
    /// DocumentWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DocumentWindow : Window
    {
        public DocumentWindow()
        {
            InitializeComponent();
        }

        List<RadioButton> radioList = new List<RadioButton>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Electronic Arts");
            DirectoryInfo[] dirs = dir.GetDirectories();
            bool select = false;
            foreach (DirectoryInfo di in dirs)
            {
                RadioButton rb = new RadioButton();
                if (!select) { rb.IsChecked = true; select = true; }
                rb.Content = di.Name;
                DocRadioPanel.Children.Add(rb);
                radioList.Add(rb);
            }
            this.Title = Seo.Languages.Window.MultiDocTitle;
            this.ContentText.Text = Seo.Languages.Window.MultiDocContent;
            this.OkButton.Text = Seo.Languages.Window.OK;
            this.CancelButton.Text = Seo.Languages.Window.Cancel;
        }

        /// <summary>
        /// 获取文档名
        /// </summary>
        public string DocName { get; set; }

        private void OkButton_Click(object sender, WindowParts.SimpleButtonArgs e)
        {
            foreach (RadioButton rb in radioList)
            {
                if (rb.IsChecked == true) { DocName = rb.Content.ToString(); break; }
            }
            this.Close();
        }

        private void CancelButton_Click(object sender, WindowParts.SimpleButtonArgs e)
        {
            this.Close();
        }
    }
}
