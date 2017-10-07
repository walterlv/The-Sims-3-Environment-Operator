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
using System.Text.RegularExpressions;

namespace TS3Sky
{
    /// <summary>
    /// ExportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExportWindow : Window
    {
        public ExportWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NameText.Focus();
            CreatorText.Text = Environment.UserName;
        }

        private void ImagePathButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "选择预览图";
            open.Filter = "所有图片|*.png;*.bmp;*.jpg|PNG|*.png|BMP|*.bmp|JPG|*.jpg|所有文件|*.*";
            open.AddExtension = true;
            open.AutoUpgradeEnabled = true;
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    Uri uri = new Uri(open.FileName);
                    BitmapImage bitmap = new BitmapImage(uri);
                    previewImage.Source = bitmap;
                    ImagePathText.Text = open.FileName;
                }
                catch
                {
                    System.Windows.MessageBox.Show(String.Format("无法识别的图片：{0}", open.FileName), "预览图", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = NameText.Text.Trim();
            string creator = CreatorText.Text.Trim();
            string savePath = SaveToText.Text.Trim();

            ExportButton.IsEnabled = false;
            if (name.Contains("\\") || name.Contains("/") || name.Contains(":") || name.Contains("*")
                || name.Contains("?") || name.Contains("\"") || name.Contains("<") || name.Contains(">") || name.Contains("|"))
            {
                ErrorText.Content = "方案名不能包含这些字符：“\\/:*?\"<>|”";
            }
            else if (creator.Contains(@"\n"))
            {
                ErrorText.Content = @"作者名字不能包含“\n”";
            }
            else if (((System.Windows.Controls.TextBox)sender) == SaveToText && !Regex.IsMatch(savePath, @"^[a-zA-Z]:(((\\(?! )[^/:*?<>\""|\\]+)+\\?)|(\\)?)\s*$"))
            {
                ErrorText.Content = "保存路径无效";
            }
            else
            {
                ErrorText.Content = String.Empty;
                if (name.Length > 0 && creator.Length > 0)
                    ExportButton.IsEnabled = true;
            }
        }

        private void SaveToButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "导出环境配置包";
            save.Filter = String.Format("{1}|*.{0}", Package.Extension, Package.ExtensionDescription);
            save.AddExtension = true;
            save.AutoUpgradeEnabled = true;
            string name = NameText.Text;
            if (name.Length <= 0) name = "*";
            save.FileName = name + "." + Package.Extension;
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveToText.Text = save.FileName;
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameText.Text.Trim();
            string creator = CreatorText.Text.Trim();
            string description = DescriptionText.Text.Trim();
            string imagePath = ImagePathText.Text.Trim();
            string savePath = SaveToText.Text.Trim();

            try
            {
                Package.Create(savePath, name, creator, description, imagePath);
                this.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "导出失败", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
