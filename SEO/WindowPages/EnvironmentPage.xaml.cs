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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Seo.WindowParts;

namespace Seo.WindowPages
{
    /// <summary>
    /// PackagePage.xaml 的交互逻辑
    /// </summary>
    public partial class EnvironmentPage : Page, PageNavigation, ILanguage, IDisposable
    {
        public EnvironmentPage()
        {
            InitializeComponent();
            // 注册语言
            Seo.Language.Register(this, Priority.Low);
            // 添加环境列表
            EnviFile ef = new EnviFile();
            ef.EnviFileOpen += ef_EnviFileOpen;
            ef.EnviFileOpenCompleted += ef_EnviFileOpenCompleted;
            ef.OpenAllAsync();
            StatusBar.Show(Status.Progress, Seo.Languages.Information.ReadingEnvironment);
            // 设置导出
            CreatorText.Text = Configs.CustomCreator;
        }

        public void Dispose()
        {
            Seo.Language.UnRegister(this);
        }

        List<string> ImportErrorMsgList;
        private void ef_EnviFileOpen(object sender, EventArgs e)
        {
            EnviFile envi = (EnviFile)sender;
            if (envi.IsValid)
            {
                EnviFileItem item = new EnviFileItem();
                item.EnviFileShow = envi;
                item.Click += EnviFileItem_Click;
                EnvironmentList.Children.Add(item);
            }
            else
            {
                if (ImportErrorMsgList == null) ImportErrorMsgList = new List<string>();
                ImportErrorMsgList.Add(envi.ErrorMessage);
            }
        }

        bool hasLoaded = false;
        int oldCount = 0;
        private void ef_EnviFileOpenCompleted(object sender, EventArgs e)
        {
            if (!hasLoaded)
            {
                hasLoaded = true;
                StatusBar.Show(Status.Success, String.Format(Seo.Languages.Information.ReadEnvironmentCompleted, EnviFile.Packages.Count), 2000);
            }
            else
            {
                StatusBar.Show(Status.Success, String.Format(Seo.Languages.Information.ImportEnvironmentCompleted, EnviFile.Packages.Count - oldCount), 5000);
            }
            if (ImportErrorMsgList != null && ImportErrorMsgList.Count > 0)
            {
                StringBuilder errMsg = new StringBuilder();
                string numSep = ": ";
                errMsg.AppendLine(Seo.Languages.Information.ReadEnvironmentErrorContent);
                errMsg.AppendLine();
                int i = 1;
                foreach (string err in ImportErrorMsgList)
                {
                    errMsg.Append(i++);
                    errMsg.Append(numSep);
                    errMsg.AppendLine(err);
                }
                ImportErrorMsgList.Clear();
                MessageBox.Show(errMsg.ToString(), Seo.Languages.Information.ReadEnvironmentErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public bool NavigationIn()
        {
            if (originallocal != null && originallocal != Seo.Language.Local)
            {
                if (newLocal != originallocal) StatusBar.Show(Status.Warning, Seo.Languages.Window.EnvironmentRestartForLanguage, 15000);
                newLocal = originallocal;
            }
            if (IsExportExpanded) IsExportExpanded = false;
            SelectedIndex = -1;
            EnvironmentScroll.ScrollToHome();
            return true;
        }

        public bool NavigationOut()
        {
            return true;
        }

        private string originallocal;
        private string newLocal;
        public void LoadLanguage()
        {
            if (originallocal == null) originallocal = Seo.Language.Local;
            newLocal = Seo.Language.Local;
            this.Title = Seo.Languages.Window.EnvironmentPage;
            SelectText.Text = Seo.Languages.Window.SelectEnvironment;
            ApplyButton.Text = Seo.Languages.Window.ApplyEnvironment;
            DeleteButton.Text = Seo.Languages.Window.DeleteEnvironment;
            ImportButton.Text = Seo.Languages.Window.ImportEnvironment;
            DownloadButton.Text = Seo.Languages.Window.DownloadEnvironment;
            ExportExpandButton.Text = TextToExpand(Seo.Languages.Window.ExportEnvironment);
            ExportTitle.Text = Seo.Languages.Window.ExportTitle;
            NameTitleText.Text = Seo.Languages.Window.ExportName;
            CreatorTitleText.Text = Seo.Languages.Window.ExportCreator;
            ImageTitleText.Text = Seo.Languages.Window.ExportImage;
            DescriptionTitleText.Text = Seo.Languages.Window.ExportDescription;
            SaveToTitleText.Text = Seo.Languages.Window.ExportSaveTo;
            ExportButton.Text = Seo.Languages.Window.ExportEnvironment;
        }

        private string TextToExpand(string text)
        {
            if (IsExportExpanded) return text + ">>";
            else return "<<" + text;
        }

        #region 列表
        private int selectedIndex = -1;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                if (value < 0) SelectedItem = null;
                else SelectedItem = (EnviFileItem)EnvironmentList.Children[value];
            }
        }
        private EnviFileItem selectedItem;
        public EnviFileItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                // 设置选择的项
                selectedItem = value;
                selectedIndex = EnvironmentList.Children.IndexOf(value);
                foreach (EnviFileItem item in EnvironmentList.Children) item.IsSelected = false;
                // 设置列表操作按钮
                if (value == null)
                {
                    ApplyButton.IsCustomEnabled = false;
                    ApplyButton.DisableTip = Seo.Languages.Window.EnvironmentNotSelectedTip;
                    DeleteButton.IsCustomEnabled = false;
                    DeleteButton.DisableTip = Seo.Languages.Window.EnvironmentNotSelectedTip;
                }
                else
                {
                    value.IsSelected = true;
                    ApplyButton.IsCustomEnabled = true;
                    if (value.EnviFileShow.IsProtected) { DeleteButton.IsCustomEnabled = false; DeleteButton.DisableTip = Seo.Languages.Window.EnvironmentProtectedTip; }
                    else DeleteButton.IsCustomEnabled = true;
                }
            }
        }
        private void EnviFileItem_Click(object sender, EventArgs e)
        {
            SelectedItem = (EnviFileItem)sender;
        }
        #endregion


        #region 环境列表
        private bool isBackupExisted = false;
        private void ApplyButton_Click(object sender, SimpleButtonArgs e)
        {
            int index = SelectedIndex;
            if (MessageBox.Show(String.Format(Seo.Languages.Information.ApplyEnvironmentContent, EnviFile.Packages[index].Name),Seo.Languages.Information.ApplyEnvironmentTitle, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    // 尝试备份用户方案
                    try
                    {
                        bool isModified = EnvironmentOperator.Instance.IsModified;
                        if (!isBackupExisted || isModified)
                        {
                            if (isModified) EnvironmentOperator.Instance.Save();
                            string backupPackageFile = EnviFile.TempPath + "\\Backup." + EnviFile.Extension;
                            EnviFile.Create(backupPackageFile, Seo.Languages.Operator.BackupEnvironmentName,
                                Seo.Languages.Operator.BackupEnvironmentCreator,
                                String.Format(Seo.Languages.Operator.BackupEnvironmentDescription, DateTime.Now.ToLocalTime()),
                                null, true);
                            ImportPackage(backupPackageFile);
                            isBackupExisted = true;
                        }
                    }
                    catch { }
                    // 开始应用方案
                    EnviFile.Apply(EnviFile.Packages[index]);
                    EnvironmentOperator.Instance.Read();
                    // 调整此方案的天气参数
                    // 清空撤消步骤
                    StatusBar.Show(Status.Success, String.Format(Seo.Languages.Information.ApplyEnvironmentOkay, EnviFile.Packages[index].Name), 3000);
                }
                catch { }
            }
        }

        private void DeleteButton_Click(object sender, SimpleButtonArgs e)
        {
            int index = SelectedIndex;
            if (MessageBox.Show(String.Format(Seo.Languages.Information.DeleteEnvironmentContent, EnviFile.Packages[index].Name), Seo.Languages.Information.DeleteEnvironmentTitle, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DeletePackage(index, true);
            }
        }

        private void ImportButton_Click(object sender, SimpleButtonArgs e)
        {
            System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();
            open.Title = Seo.Languages.Window.SelectEnvironment;
            open.Multiselect = true;
            open.Filter = String.Format("{1}|*.{0}", EnviFile.Extension, EnviFile.ExtensionDescription);
            open.AddExtension = true;
            open.AutoUpgradeEnabled = true;
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                oldCount = EnviFile.Packages.Count;
                ImportPackage(open.FileNames);
            }
        }

        private void DownloadButton_Click(object sender, SimpleButtonArgs e)
        {
            CommonOperation.VisitSite(Seo.Language.EnviFileDownloadSite);
        }

        private void DeletePackage(int index, bool showMessage)
        {
            EnvironmentList.Children.RemoveAt(index);
            try { EnviFile.Packages[index].Delete(); }
            catch { StatusBar.Show(Status.Error, Seo.Languages.Information.DeleteEnvironmentError); }
        }

        private void ImportPackage(string file)
        {
            ef_EnviFileOpen(EnviFile.Import(file), null);
            if (ImportErrorMsgList != null && ImportErrorMsgList.Count > 0) ImportErrorMsgList.Clear();
        }
        private void ImportPackage(string[] files)
        {
            EnviFile ef = new EnviFile();
            ef.EnviFileOpen += ef_EnviFileOpen;
            ef.EnviFileOpenCompleted += ef_EnviFileOpenCompleted;
            ef.ImportAsync(files);
            StatusBar.Show(Status.Progress, Seo.Languages.Information.ImportingEnvironment);
        }
        #endregion

        #region 导出
        bool isExportExpanded = false;
        bool IsExportExpanded
        {
            get { return isExportExpanded; }
            set
            {
                if (value)
                {
                    CoverGrid.Visibility = System.Windows.Visibility.Visible;
                    BeginStoryboard(FindResource("ExportExpandStory") as Storyboard);
                }
                else
                {
                    BeginStoryboard(FindResource("ExportCollapseStory") as Storyboard);
                }
                isExportExpanded = value;
                ExportExpandButton.Text = TextToExpand(Seo.Languages.Window.ExportEnvironment);
            }
        }
        private void ExportExpandButton_Click(object sender, SimpleButtonArgs e)
        {
            IsExportExpanded = !IsExportExpanded;
        }

        private void ExportCollapseStory_Completed(object sender, EventArgs e)
        {
            CoverGrid.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void CoverGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsExportExpanded = false;
        }
        #endregion

        #region 判断导出逻辑
        bool isNameValid = false;
        bool IsNameValid { get { return isNameValid; } set { isNameValid = value; CheckExportEnabled(); } }
        bool isCreatorValid = false;
        bool IsCreatorValid { get { return isCreatorValid; } set { isCreatorValid = value; CheckExportEnabled(); } }
        bool isImageValid = true;
        bool IsImageValid { get { return isImageValid; } set { isImageValid = value; CheckExportEnabled(); } }
        bool isDescriptionValid = true;
        bool IsDescriptionValid { get { return isDescriptionValid; } set { isDescriptionValid = value; CheckExportEnabled(); } }
        bool isSaveToValid = false;
        bool IsSaveToValid { get { return isSaveToValid; } set { isSaveToValid = value; CheckExportEnabled(); } }
        string DisableTip { get; set; }
        private void ImageFileText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ImageFileText.Text.Length <= 0) IsImageValid = true;
            else if (ViewImage(ImageFileText.Text)) { ImageErrorText.Text = null; IsImageValid = true; }
        }

        private void ImageBrowseButton_Click(object sender, SimpleButtonArgs e)
        {
            System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();
            open.Title = Seo.Languages.Window.SelectImageTitle;
            open.Filter = String.Format("{0}|*.png;*.bmp;*.jpg|PNG|*.png|BMP|*.bmp|JPG|*.jpg|{1}|*.*", Seo.Languages.Window.AllImage, Seo.Languages.Window.AllFiles);
            open.AddExtension = true;
            open.AutoUpgradeEnabled = true;
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ImageFileText.Text = open.FileName;
            }
        }

        private bool ViewImage(string fileName)
        {
            bool result = false;
            try
            {
                Uri uri = new Uri(fileName);
                BitmapImage bitmap = new BitmapImage(uri);
                PreviewImage.Source = bitmap;
                result = true;
            }
            catch
            {
                PreviewImage.Source = null;
                ImageErrorText.Text = Seo.Languages.Information.ExportEnvironmentInvalidImage;
                DisableTip = ImageErrorText.Text;
                IsImageValid = false;
            }
            return result;
        }

        private void SaveToFileText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SaveToFileText.Text.Length <= 0)
            {
                IsSaveToValid = false;
                DisableTip = Seo.Languages.Information.ExportEnvironmentEmptyPath;
            }
            else if (SaveToFileText.Text.StartsWith(EnviFile.CustomPath))
            {
                ErrorText.Text = Seo.Languages.Information.ExportEnvironmentCustomPathForbidden;
                DisableTip = ErrorText.Text;
                IsSaveToValid = false;
            }
            else if (!IsFileInValidDirectory(SaveToFileText.Text))
            {
                ErrorText.Text = Seo.Languages.Information.ExportEnvironmentInvalidPath;
                DisableTip = ErrorText.Text;
                IsSaveToValid = false;
            }
            else
            {
                ErrorText.Text = null;
                DisableTip = null;
                IsSaveToValid = true;
            }
        }

        private void SaveToBrowseButton_Click(object sender, SimpleButtonArgs e)
        {
            System.Windows.Forms.SaveFileDialog save = new System.Windows.Forms.SaveFileDialog();
            save.Title = Seo.Languages.Window.ExportFileTitle;
            save.Filter = String.Format("{1}|*.{0}", EnviFile.Extension, EnviFile.ExtensionDescription);
            save.AddExtension = true;
            save.AutoUpgradeEnabled = true;
            string name = NameText.Text;
            if (name.Length <= 0) name = "*";
            save.FileName = name + "." + EnviFile.Extension;
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveToFileText.Text = save.FileName;
            }
        }

        private bool IsFileInValidDirectory(string fileName)
        {
            bool result = false;
            try
            {
                string dir = fileName.Substring(0, fileName.LastIndexOf('\\'));
                if (Directory.Exists(dir)) result = true;
            }
            catch { }
            return result;
        }

        private void ExportTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = NameText.Text.Trim();
            string creator = CreatorText.Text.Trim();
            string description = DescriptionText.Text.Trim();
            string savePath = SaveToFileText.Text.Trim();
            if (name.Contains("\\") || name.Contains("/") || name.Contains(":") || name.Contains("*")
                || name.Contains("?") || name.Contains("\"") || name.Contains("<") || name.Contains(">") || name.Contains("|"))
            {
                DisableTip = String.Format(Seo.Languages.Information.ExportEnvironmentInvalidName, "\\/:*?\"<>|");
                IsNameValid = false;
            }
            else if (creator.Contains("\\") && creator.Contains("=") && creator.Contains(";") && creator.Contains("#"))
            {
                DisableTip = String.Format(Seo.Languages.Information.ExportEnvironmentInvalidCreator, "\\=;#");
                IsCreatorValid = false;
            }
            else if (name.Length <= 0)
            {
                DisableTip = Seo.Languages.Information.ExportEnvironmentEmptyName;
                IsNameValid = false;
            }
            else if (creator.Length <= 0)
            {
                DisableTip = Seo.Languages.Information.ExportEnvironmentEmptyCreator;
                IsCreatorValid = false;
            }
            else
            {
                IsNameValid = true;
                IsCreatorValid = true;
                SaveToFileText_TextChanged(null, null);
            }
        }

        private void CheckExportEnabled()
        {
            if (IsNameValid && IsCreatorValid && IsImageValid && IsDescriptionValid && IsSaveToValid) ExportButton.IsCustomEnabled = true;
            else { ExportButton.IsCustomEnabled = false; ExportButton.DisableTip = DisableTip; }
        }

        private void ExportButton_Click(object sender, SimpleButtonArgs e)
        {
            string name = NameText.Text.Trim();
            string creator = CreatorText.Text.Trim();
            string description = DescriptionText.Text.Trim();
            string imagePath = ImageFileText.Text.Trim();
            string savePath = SaveToFileText.Text.Trim();

            try
            {
                EnviFile.Create(savePath, name, creator, description, imagePath);
                ImportPackage(savePath);
                EnvironmentScroll.ScrollToEnd();
                StatusBar.Show(Status.Success, Seo.Languages.Information.ExportEnvironmentOkay, 5000);
                Configs.CustomCreator = CreatorText.Text;
                NameText.Text = null;
                DescriptionText.Text = null;
                ImageFileText.Text = null;
                SaveToFileText.Text = null;
            }
            catch { StatusBar.Show(Status.Error, Seo.Languages.Information.ExportEnvironmentFailed, 5000); }
        }
        #endregion
    }
}
