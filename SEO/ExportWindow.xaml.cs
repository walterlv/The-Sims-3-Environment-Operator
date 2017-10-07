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
    /// ExportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExportWindow : Window
    {
        public ExportWindow()
        {
            InitializeComponent();
        }

        private string exportPath = String.Empty;
        public string ExportPath
        {
            get
            {
                return exportPath;
            }
            set
            {
                exportPath = value;
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 初始化语言
            this.Title = Seo.Language.Operation.ExportTitle;
            NameLabel.Content = Seo.Language.Operation.ExportName;
            CreatorLabel.Content = Seo.Language.Operation.ExportCreator;
            ImageLabel.Content = Seo.Language.Operation.ExportPreview;
            DescriptionLabel.Content = Seo.Language.Operation.ExportDescription;
            SaveToLabel.Content = Seo.Language.Operation.ExportTo;
            ExportButton.Content = Seo.Language.Operation.ExportOK;
            CancelButton.Content = Seo.Language.Operation.ExportCancel;
            // 初始化界面
            NameText.Focus();
            CreatorText.Text = Environment.UserName;
        }

        private void ImagePathButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = Seo.Language.Operation.ExportPreviewTitle;
            open.Filter = String.Format("{0}|*.png;*.bmp;*.jpg|PNG|*.png|BMP|*.bmp|JPG|*.jpg|{1}|*.*", Seo.Language.Dialog.AllImageFiles, Seo.Language.Dialog.AllFiles);
            open.AddExtension = true;
            open.AutoUpgradeEnabled = true;
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    Uri uri = new Uri(open.FileName);
                    BitmapImage bitmap = new BitmapImage(uri);
                    PreviewImage.Source = bitmap;
                    ImagePathText.Text = open.FileName;
                }
                catch
                {
                    System.Windows.MessageBox.Show(String.Format(Seo.Language.Dialog.OpenPreviewImageFailedDescription, open.FileName), Seo.Language.Dialog.OpenPreviewImageFailedTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = NameText.Text.Trim();
            string creator = CreatorText.Text.Trim();
            string description = DescriptionText.Text.Trim();
            string savePath = SaveToText.Text.Trim();

            ExportButton.IsEnabled = false;
            if (name.Contains("\\") || name.Contains("/") || name.Contains(":") || name.Contains("*")
                || name.Contains("?") || name.Contains("\"") || name.Contains("<") || name.Contains(">") || name.Contains("|"))
            {
                ErrorText.Content = String.Format(Seo.Language.Dialog.InvalidCharInName, "\\/:*?\"<>|");
            }
            else if (creator.Contains(@"\n"))
            {
                ErrorText.Content = String.Format(Seo.Language.Dialog.InvalidCharInCreator, "\\n");
            }
            else if (DescriptionText.LineCount > 3)
            {
                ErrorText.Content = Seo.Language.Dialog.TooManyLinesInDescription;
            }
            else
            {
                ErrorText.Content = String.Empty;
                if (name.Length > 0 && creator.Length > 0 && savePath.Length > 0)
                    ExportButton.IsEnabled = true;
            }
        }

        private void SaveToButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = Seo.Language.Operation.ExportToTitle;
            save.Filter = String.Format("{1}|*.{0}", Package.Extension, Package.ExtensionDescription);
            save.AddExtension = true;
            save.AutoUpgradeEnabled = true;
            string name = NameText.Text;
            if (name.Length <= 0) name = "*";
            save.FileName = name + "." + Package.Extension;
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveToText.Text = save.FileName;
                if (save.FileName.StartsWith(Package.CustomPath))
                {
                    ErrorText.Content = Seo.Language.Dialog.ExportToCustomForbidden;
                    ExportButton.IsEnabled = false;
                }
                else
                {
                    TextBox_TextChanged(null, null);
                }
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
                ExportPath = savePath;
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                Log.ErrorLog.WriteErrorLog(ex, -102);
                System.Windows.MessageBox.Show(Seo.Language.Dialog.ExportPackageFailedContent, Seo.Language.Dialog.ExportPackageFailedTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}