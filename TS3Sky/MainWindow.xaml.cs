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
using System.IO;
using System.Diagnostics;

namespace TS3Sky
{
    #region 枚举类型 - 三种可能的页
    enum TS3SkyPage
    {
        Welcome,
        Weather,
        Content,
        About
    }
    #endregion

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 所有可使用的环境配置包
        /// </summary>
        private List<Package> Packages;

        #region 初始化应用程序
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 初始化应用程序资源路径
            string BackgroundPath = Environment.CurrentDirectory + @"\Images\Background.jpg";
            string WelcomePath = Environment.CurrentDirectory + @"\Images\Welcome.png";
            #endregion

            #region 初始化应用程序界面风格
            string local = TS3Sky.Language.LanguageManager.LocalLanguage;
            if (local.Equals("zh-cn"))
            {
                // Logo
                logoImage.Source = new BitmapImage(new Uri(String.Format("/Images/Logo-zh-cn.png", local), UriKind.Relative));
                // 关于背景
                AboutBackground.Source = new BitmapImage(new Uri(String.Format("/Images/Logo-zh-cn.png", local), UriKind.Relative));
            }
            else if (local.Equals("zh-tw") || local.Equals("zh-hk") || local.Equals("zh-mo"))
            {
                // Logo
                logoImage.Source = new BitmapImage(new Uri(String.Format("/Images/Logo-zh-tw.png", local), UriKind.Relative));
                // 关于背景
                AboutBackground.Source = new BitmapImage(new Uri(String.Format("/Images/Logo-zh-tw.png", local), UriKind.Relative));
            }
            // 背景图片
            try
            {
                if (File.Exists(BackgroundPath))
                {
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri(BackgroundPath));
                    imageBrush.Stretch = Stretch.Fill;
                    this.Background = imageBrush;
                }
            }
            catch { }
            #endregion

            #region 初始化应用程序界面内容
            // 全局窗口
            this.Title = TS3Sky.Language.Application.Name;
            // 初始化导航按钮
            WelcomeLabel.Content = TS3Sky.Language.Navigation.WelcomeLabel;
            SettingsLabel.Content = TS3Sky.Language.Navigation.SettingsLabel;
            OtherInfoLabel.Content = TS3Sky.Language.Navigation.OtherInfoLabel;
            SkyColorWelcome.Content = TS3Sky.Language.Navigation.Welcome;
            SkyColorWeather.Content = TS3Sky.Language.Navigation.Weather;
            SkyColorAbout.Content = TS3Sky.Language.Navigation.About;
            SkyColorButton0.Content = WeatherSky.CurrentWeather.SkyColors[0].ColorName;
            SkyColorButton1.Content = WeatherSky.CurrentWeather.SkyColors[1].ColorName;
            SkyColorButton2.Content = WeatherSky.CurrentWeather.SkyColors[2].ColorName;
            SkyColorButton3.Content = WeatherSky.CurrentWeather.SkyColors[3].ColorName;
            SkyColorButton4.Content = WeatherSky.CurrentWeather.SkyColors[4].ColorName;
            // 初始化欢迎页面
            WelcomeTitle.Text = TS3Sky.Language.Application.Name;
            WelcomeDescription.Text = TS3Sky.Language.Application.Description;
            PackageListBoxTitle.Text = TS3Sky.Language.Welcome.ChoosePackage;
            PackageExportTitle.Text = TS3Sky.Language.Welcome.ExportPackage;
            ApplyPackageButton.Content = TS3Sky.Language.Operation.ApplyPackage;
            DeletePackageButton.Content = TS3Sky.Language.Operation.DeletePackage;
            ImportPackageButton.Content = TS3Sky.Language.Operation.ImportPackage;
            DownloadPackageButton.Content = TS3Sky.Language.Operation.DownloadPackage;
            ExportPackageButton.Content = TS3Sky.Language.Operation.ExportPackage;
            // 初始化天气页面
            ChooseWeatherLabel.Content = TS3Sky.Language.Weather.ChooseWeather;
            SetWeatherWeightLabel.Content = TS3Sky.Language.Weather.SetWeatherWeight;
            SetWeatherWeightTip.Text = TS3Sky.Language.Weather.SetWeatherWeightTip;
            LockWeatherWeight.Content = TS3Sky.Language.Weather.LockWeight;
            SaveWeatherWeightButton.Content = TS3Sky.Language.Weather.SaveWeight;
            WeatherLabel0.Content = TS3Sky.Language.Weather.Clear;
            WeatherLabel1.Content = TS3Sky.Language.Weather.PartlyCloudy;
            WeatherLabel2.Content = TS3Sky.Language.Weather.Overcast;
            WeatherLabel3.Content = TS3Sky.Language.Weather.Storm;
            WeatherLabel4.Content = TS3Sky.Language.Weather.Custom;
            // 初始化操作按钮
            DownloadWorldButton.Content = TS3Sky.Language.Operation.DownloadWorld;
            SaveButton.Content = TS3Sky.Language.Operation.Save;
            UndoButton.Content = TS3Sky.Language.Operation.Undo;
            SetToDefaultButton.Content = TS3Sky.Language.Operation.SetToDefault;
            // 初始化关于画面
            AboutTitle.Text = TS3Sky.Language.Application.Name;
            AboutInfomation.Text = String.Format("{0}:\n{1}\n\n{2}: {3}\n\n{4}: {5}\n{6}",
                                    TS3Sky.Language.About.Author,
                                    TS3Sky.Language.Application.Developer,
                                    TS3Sky.Language.About.Version,
                                    TS3Sky.Language.Application.Version,
                                    TS3Sky.Language.About.Publish,
                                    TS3Sky.Language.About.PublishText,
                                    TS3Sky.Language.About.Contact);
            ContactUsButton.Content = TS3Sky.Language.About.ContactUs;
            WorldDescription.Text = TS3Sky.Language.About.DownloadWorldsDescription;
            DownloadWorldButton.Content = TS3Sky.Language.About.DownloadWorlds;
            CopyrightInfomation.Text = TS3Sky.Language.Application.Copyright;
            #endregion

            #region 初始化预设方案列表
            Packages = Package.Packages;
            foreach (Package p in Packages)
            {
                PackageListItem pli = new PackageListItem();
                pli.ShowPackage = p;
                PackageListBox.Items.Add(pli);
            }
            #endregion

            #region 初始化天气选项
            WeatherPicker.Owner = this;
            InitializeWeatherPage();
            if (!Configs.UnlockAllWeathers) CollapseWeather();
            #endregion
        }
        #endregion

        #region 关闭程序时执行操作
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (WeatherSky.IsWeatherModified)
            {
                MessageBoxResult msgR = MessageBox.Show(TS3Sky.Language.Dialog.SaveClosingContent, TS3Sky.Language.Dialog.SaveClosingTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.None);
                if (msgR == MessageBoxResult.Yes)
                {
                    saveAll();
                }
                else if (msgR == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                }
            }
            // 退出后保存设置
            Configs.Save();
            ColorPickBar.SaveCustomColors();
            // 退出后清除缓存
            if (e.Cancel == false) Package.ClearTempBackupAndCache();
        }
        #endregion

        #region 导航
        #region 点击导航按钮
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button) sender;
            string name = (string) button.Content;
            if (name.Equals(TS3Sky.Language.Navigation.Welcome))
            {
                showPage(TS3SkyPage.Welcome);
            }
            else if (name.StartsWith(TS3Sky.Language.Navigation.Weather))
            {
                showPage(TS3SkyPage.Weather);
            }
            else if (name.Equals(TS3Sky.Language.Navigation.About))
            {
                showPage(TS3SkyPage.About);
            }
            else if (name.StartsWith(TS3Sky.Language.Sky_1.DayColorName))
            {
                showPage(WeatherSky.CurrentWeather.SkyColors[0], button);
                UpdatePageState();
            }
            else if (name.StartsWith(TS3Sky.Language.Sky_2.DayColorName))
            {
                showPage(WeatherSky.CurrentWeather.SkyColors[1], button);
                UpdatePageState();
            }
            else if (name.StartsWith(TS3Sky.Language.Sky_Light.DayColorName))
            {
                showPage(WeatherSky.CurrentWeather.SkyColors[2], button);
                UpdatePageState();
            }
            else if (name.StartsWith(TS3Sky.Language.Sky_Sky.DayColorName))
            {
                showPage(WeatherSky.CurrentWeather.SkyColors[3], button);
                UpdatePageState();
            }
            else if (name.StartsWith(TS3Sky.Language.Sky_Sea.DayColorName))
            {
                showPage(WeatherSky.CurrentWeather.SkyColors[4], button);
                UpdatePageState();
            }
            else
            {
            }
        }
        #endregion

        #region 对应按钮与颜色
        private Button FindButtonBySkyColor(SkyColor color)
        {
            SkyColor newSkyColor = null;
            foreach (SkyColor oldSkyColor in colorPickBarLink.SkyColorListAsIndex)
            {
                if (color.ColorType == oldSkyColor.ColorType)
                {
                    newSkyColor = oldSkyColor;
                    break;
                }
            }
            int index = colorPickBarLink.SkyColorListAsIndex.IndexOf(newSkyColor);
            if (index >= 0)
            {
                return colorPickBarLink.SkyColorButton[index];
            }
            else return null;
        }
        #endregion

        #region 布局页导航
        private void showPage(TS3SkyPage page)
        {
            switch (page)
            {
                case TS3SkyPage.Welcome:
                    PackageListBox.SelectedIndex = -1;
                    if (SkyWelcome.Visibility != Visibility.Visible)
                    {
                        SkyWelcome.Visibility = Visibility.Visible;
                        SkyWeather.Visibility = Visibility.Collapsed;
                        ColorPageScroll.Visibility = Visibility.Collapsed;
                        SkyAbout.Visibility = Visibility.Collapsed;
                    }
                    currentShow = null;
                    break;
                case TS3SkyPage.Weather:
                    if (SkyWeather.Visibility != Visibility.Visible)
                    {
                        SkyWelcome.Visibility = Visibility.Collapsed;
                        SkyWeather.Visibility = Visibility.Visible;
                        ColorPageScroll.Visibility = Visibility.Collapsed;
                        SkyAbout.Visibility = Visibility.Collapsed;
                    }
                    break;
                case TS3SkyPage.Content:
                    break;
                case TS3SkyPage.About:
                    if (SkyAbout.Visibility != Visibility.Visible)
                    {
                        SkyWelcome.Visibility = Visibility.Collapsed;
                        SkyWeather.Visibility = Visibility.Collapsed;
                        ColorPageScroll.Visibility = Visibility.Collapsed;
                        SkyAbout.Visibility = Visibility.Visible;
                    }
                    currentShow = null;
                    break;
                default:
                    currentShow = null;
                    break;
            }
        }
        #endregion

        #region 颜色页导航
        int ColorBarStartIndex = -1;
        ColorPickBarLink colorPickBarLink = new ColorPickBarLink();
        SkyColor currentShow = null;
        private void showPage(SkyColor skyColor)
        {
            showPage(skyColor, null);
        }
        // 添加一个button参数是为了绑定导航按钮的
        private void showPage(SkyColor skyColor, Button button)
        {
            try
            {
                // 移除原来的所有颜色行
                if (ColorBarStartIndex > 0 && SkyContent.Children != null)
                {
                    for (; ColorBarStartIndex < SkyContent.Children.Count; )
                    {
                        SkyContent.Children.RemoveAt(ColorBarStartIndex);
                    }
                }
                // 设置标题
                SkyTitleText.Text = WeatherSky.CurrentWeather.Name + " - " + skyColor.ColorName;
                SkyTitleDescription.Text = skyColor.ColorDescription;
                // 新建颜色行
                int index = colorPickBarLink.SkyColorListAsIndex.IndexOf(skyColor);
                if (index >= 0)
                {
                    for (int i = 0; i < colorPickBarLink.ColorPickBarList[index].Count; i++)
                    {
                        SkyContent.Children.Add(colorPickBarLink.ColorPickBarList[index][i]);
                    }
                }
                else
                {
                    List<ColorPickBar> colorPickBarList = new List<ColorPickBar>();
                    ColorPickBar colorPickBar;
                    for (int i = 0; i < skyColor.DayColors.Count; i++)
                    {
                        colorPickBar = new ColorPickBar();
                        colorPickBar.Owner = this;
                        colorPickBar.Width = 482;
                        colorPickBar.ColorBar = skyColor.DayColors[i];
                        colorPickBar.ColorBarGroup = skyColor;
                        colorPickBar.Title = skyColor.DayColors[i].ColorName;
                        colorPickBar.Description = skyColor.DayColors[i].ColorDescription;
                        SkyContent.Children.Add(colorPickBar);
                        colorPickBarList.Add(colorPickBar);
                        if (ColorBarStartIndex < 0)
                        {
                            ColorBarStartIndex = SkyContent.Children.IndexOf(colorPickBar);
                        }
                    }
                    colorPickBarLink.SkyColorListAsIndex.Add(skyColor);
                    colorPickBarLink.SkyColorButton.Add(button);
                    colorPickBarLink.ColorPickBarList.Add(colorPickBarList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(TS3Sky.Language.Dialog.ReadEnvironmentFileFailedContent, ex.Message), TS3Sky.Language.Dialog.ReadEnvironmentFileFailedTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            // 确保只有颜色页显示
            if (ColorPageScroll.Visibility != Visibility.Visible)
            {
                SkyWelcome.Visibility = Visibility.Collapsed;
                SkyWeather.Visibility = Visibility.Collapsed;
                ColorPageScroll.Visibility = Visibility.Visible;
                SkyAbout.Visibility = Visibility.Collapsed;
            }
            // 确保换页时滚动条滚到顶端
            ColorPageScroll.ScrollToHome();
            currentShow = skyColor;
        }
        #endregion
        #endregion

        #region 欢迎页
        #region 当选择一个方案时
        private void PackageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = PackageListBox.SelectedIndex;
            if (index < 0 || index >= PackageListBox.Items.Count)
            {
                ApplyPackageButton.IsEnabled = false;
                DeletePackageButton.IsEnabled = false;
            }
            else
            {
                ApplyPackageButton.IsEnabled = true;
                if (Packages[index].IsProtected)
                {
                    DeletePackageButton.IsEnabled = false;
                }
                else
                {
                    DeletePackageButton.IsEnabled = true;
                }
            }
        }
        #endregion
        #region 应用方案
        private bool isBackupExisted = false;
        private void ApplyPackageButton_Click(object sender, RoutedEventArgs e)
        {
            int index = PackageListBox.SelectedIndex;
            if (MessageBox.Show(String.Format(TS3Sky.Language.Dialog.ApplyPackageContent, Packages[index].Name), TS3Sky.Language.Dialog.ApplyPackageTitle, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    // 尝试备份用户方案
                    try
                    {
                        bool isModified = WeatherSky.IsWeatherModified;
                        if (!isBackupExisted || isModified)
                        {
                            if (isModified) saveAll();
                            string backupPackageFile = Package.TempPath + "\\Backup." + Package.Extension;
                            Package.Create(backupPackageFile, TS3Sky.Language.Package.BackupPackageName, TS3Sky.Language.Package.BackupPackageCreator,
                                String.Format(TS3Sky.Language.Package.BackupPackageDescription, DateTime.Now.ToLocalTime()), null, true);
                            ImportPackage(backupPackageFile, false);
                            isBackupExisted = true;
                        }
                    }
                    catch { }
                    // 开始应用方案
                    Package.Apply(Packages[index]);
                    foreach (SkyColor color in WeatherSky.CurrentWeather.SkyColors)
                    {
                        color.Reread();
                        RefreshPage(color);
                    }
                    // 调整此方案的天气参数
                    if (Configs.LockWeatherWeight) WeatherSky.SaveWeight();
                    else WeatherSky.RereadWeight();
                    InitializeWeatherPage();
                    // 清空撤消步骤
                    ColorChangeAction.ClearActions();
                    MessageBox.Show(String.Format(TS3Sky.Language.Dialog.ApplyPackageSuccessfullyContent, Packages[index].Name), TS3Sky.Language.Dialog.ApplyPackageSuccessfullyTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format(TS3Sky.Language.Dialog.ApplyPackageFailedContent, ex.Message), TS3Sky.Language.Dialog.ApplyPackageFailedTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }
        #endregion
        #region 删除方案
        private void DeletePackageButton_Click(object sender, RoutedEventArgs e)
        {
            int index = PackageListBox.SelectedIndex;
            if (MessageBox.Show(String.Format(TS3Sky.Language.Dialog.DeletePackageContent, Packages[index].Name), TS3Sky.Language.Dialog.DeletePackageTitle, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DeletePackage(index, true);
            }
        }
        private void DeletePackage(int index, bool showMessage)
        {
            PackageListBox.Items.RemoveAt(index);
            try
            {
                Packages[index].Delete();
            }
            catch (Exception ex)
            {
                if (showMessage) MessageBox.Show(String.Format(TS3Sky.Language.Dialog.DeletePackageFailedContent, ex.Message), TS3Sky.Language.Dialog.DeletePackageFailedTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            Packages.RemoveAt(index);
        }
        #endregion
        #region 导入(添加)方案
        private void ImportPackageButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();
            open.Title = TS3Sky.Language.Dialog.ImportPackageTitle;
            open.Filter = String.Format("{1}|*.{0}", Package.Extension, Package.ExtensionDescription);
            open.AddExtension = true;
            open.AutoUpgradeEnabled = true;
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ImportPackage(open.FileName, true);
            }
        }
        private bool ImportPackage(string file, bool showMessage)
        {
            Package imp = Package.Import(file);
            if (imp.IsValid)
            {
                Packages.Add(imp);
                PackageListItem pli = new PackageListItem();
                pli.ShowPackage = imp;
                PackageListBox.Items.Add(pli);
                return true;
            }
            else
            {
                if (showMessage) MessageBox.Show(String.Format(TS3Sky.Language.Dialog.ImportPackageFailedContent, imp.ErrorMessage), TS3Sky.Language.Dialog.ImportPackageFailedTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }
        #endregion
        #region 下载方案
        private void DownloadPackageButton_Click(object sender, RoutedEventArgs e)
        {
            VisitSite(TS3Sky.Language.Application.PackageDownloadSite);
        }
        #endregion
        #region 导出
        private void ExportPackageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 如果方案被修改, 则提示保存
                if (WeatherSky.IsWeatherModified)
                {
                    if (MessageBox.Show(TS3Sky.Language.Dialog.ExportPackageButNotSavedContent, TS3Sky.Language.Dialog.ExportPackageButNotSavedTitle,
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        saveAll();
                    }
                    else return;
                }
                // 如果已经保存过了, 则修改
                ExportWindow ew = new ExportWindow();
                ew.Owner = this;
                if (ew.ShowDialog() == true) ImportPackage(ew.ExportPath, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(TS3Sky.Language.Dialog.UnknownErrorContent, ex.Message + "\n" + ex.StackTrace), TS3Sky.Language.Dialog.UnknownErrorTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
        #endregion
        #region 保存所有方案
        private void saveAll()
        {
            WeatherSky.CurrentWeather.Save();
            foreach (SkyColor color in WeatherSky.CurrentWeather.SkyColors)
            {
                try { FindButtonBySkyColor(color).Content = color.ColorName; }
                catch { }
            }
            SkyColorWeather.Content = TS3Sky.Language.Navigation.Weather;
        }
        #endregion
        #endregion

        #region 天气页
        // 初始化天气页
        private void InitializeWeatherPage()
        {
            // 当前天气
            WeatherPicker.SelectedWeather = WeatherSky.CurrentWeather.Type;
            // 滑杆
            double total = 0.0;
            foreach (WeatherSky ws in WeatherSky.AllWeathers)
            {
                total += ws.WeatherWeight;
            }
            if (total > 0)
            {
                WeatherSlide0.Value = WeatherSky.AllWeathers[0].WeatherWeight / total;
                WeatherSlide1.Value = WeatherSky.AllWeathers[1].WeatherWeight / total;
                WeatherSlide2.Value = WeatherSky.AllWeathers[2].WeatherWeight / total;
                WeatherSlide3.Value = WeatherSky.AllWeathers[3].WeatherWeight / total;
                WeatherSlide4.Value = WeatherSky.AllWeathers[4].WeatherWeight / total;
            }
            // 锁定天气比例
            LockWeatherWeight.IsChecked = Configs.LockWeatherWeight;
        }
        // 隐藏部分天气
        private void CollapseWeather()
        {
            WeatherLabel2.Visibility = Visibility.Collapsed;
            WeatherLabel3.Visibility = Visibility.Collapsed;
            WeatherLabel4.Visibility = Visibility.Collapsed;
            WeatherSlide2.Visibility = Visibility.Collapsed;
            WeatherSlide3.Visibility = Visibility.Collapsed;
            WeatherSlide4.Visibility = Visibility.Collapsed;
            WeatherPercent2.Visibility = Visibility.Collapsed;
            WeatherPercent3.Visibility = Visibility.Collapsed;
            WeatherPercent4.Visibility = Visibility.Collapsed;
            WeatherPicker.CollapseWeather();
        }
        // 修改概率百分比
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double[] pers = new double[5];
            pers[0] = WeatherSlide0.Value;
            pers[1] = WeatherSlide1.Value;
            pers[2] = WeatherSlide2.Value;
            pers[3] = WeatherSlide3.Value;
            pers[4] = WeatherSlide4.Value;
            double total = pers[0] + pers[1] + pers[2] + pers[3] + pers[4];
            if (total <= 0)
            {
                pers[0] = pers[1] = pers[2] = pers[3] = pers[4] = 0.2;
            }
            else
            {
                pers[0] = pers[0] / total;
                pers[1] = pers[1] / total;
                pers[2] = pers[2] / total;
                pers[3] = pers[3] / total;
                pers[4] = pers[4] / total;
            }
            WeatherPercent0.Content = pers[0].ToString("P");
            WeatherPercent1.Content = pers[1].ToString("P");
            WeatherPercent2.Content = pers[2].ToString("P");
            WeatherPercent3.Content = pers[3].ToString("P");
            WeatherPercent4.Content = pers[4].ToString("P");
            if (!SaveWeatherWeightButton.IsEnabled) SaveWeatherWeightButton.IsEnabled = true;
        }
        // 应用天气设置
        public void WeatherSelectionChanged()
        {
            WeatherSky.SetCurrentWeather(WeatherPicker.SelectedWeather);
            // 更新按钮
            foreach (SkyColor color in WeatherSky.CurrentWeather.SkyColors)
            {
                RefreshPage(color);
            }
            // 处理保存星号
            if (WeatherSky.IsWeatherModified)
            {
                if (!SkyColorWeather.Content.ToString().EndsWith("*")) SkyColorWeather.Content += "*";
            }
            else
            {
                if (SkyColorWeather.Content.ToString().EndsWith("*")) SkyColorWeather.Content = TS3Sky.Language.Navigation.Weather;
            }
            ColorChangeAction.ClearActions();
        }
        // 锁定天气比例
        private void LockWeatherWeight_Checked(object sender, RoutedEventArgs e)
        {
            Configs.LockWeatherWeight = true;
        }
        private void LockWeatherWeight_Unchecked(object sender, RoutedEventArgs e)
        {
            Configs.LockWeatherWeight = false;
        }
        // 保存天气比例
        private void SaveWeatherWeightButton_Click(object sender, RoutedEventArgs e)
        {
            double[] pers = new double[5];
            pers[0] = WeatherSlide0.Value;
            pers[1] = WeatherSlide1.Value;
            pers[2] = WeatherSlide2.Value;
            pers[3] = WeatherSlide3.Value;
            pers[4] = WeatherSlide4.Value;
            double total = pers[0] + pers[1] + pers[2] + pers[3] + pers[4];
            if (total <= 0)
            {
                pers[0] = pers[1] = pers[2] = pers[3] = 0.2;
            }
            else
            {
                pers[0] = pers[0] / total;
                pers[1] = pers[1] / total;
                pers[2] = pers[2] / total;
                pers[3] = pers[3] / total;
                pers[4] = pers[4] / total;
            }
            for (int i = 0; i < 5; i++)
            {
                WeatherSky.AllWeathers[i].WeatherWeight = pers[i];
            }
            WeatherSky.SaveWeight();
            SaveWeatherWeightButton.IsEnabled = false;
        }
















        #endregion

        #region 颜色页
        #region 点击颜色页功能按钮
        // 下面四个方法是为了给SaveButton, UndoButton和RedoButton传递快捷键的
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CommandBinding_SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveButton_Click(this, null);
        }
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (UndoButton.IsEnabled) UndoButton_Click(UndoButton, null);
        }
        private void CommandBinding_ReExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            RedoButton_Click(this, null);
        }
        // 保存
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                try
                {
                    currentShow.Save();
                    // 使保存按钮不可用
                    SaveButton.IsEnabled = false;
                    // 去掉导航按钮后面的星号
                    int index = colorPickBarLink.SkyColorListAsIndex.IndexOf(currentShow);
                    if (index >= 0) colorPickBarLink.SkyColorButton[index].Content = currentShow.ColorName;
                    break;
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(String.Format(TS3Sky.Language.Dialog.SaveRetryContent, ex.Message), TS3Sky.Language.Dialog.SaveRetryTitle,
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) continue;
                    else break;
                }
            }
        }
        // 撤消
        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            ColorChangeAction action = ColorChangeAction.GetUndoAction();
            if (action != null)
            {
                action.ActionBar.Undo();
                if (currentShow != action.ActionBar.ColorBarGroup) showPage(action.ActionBar.ColorBarGroup);
            }
            if (ColorChangeAction.GetUndoAction() == null)
            {
                UndoButton.IsEnabled = false;
            }
        }
        // 重做
        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            ColorChangeAction action = ColorChangeAction.GetRedoAction();
            if (action != null)
            {
                action.ActionBar.Redo();
                if (currentShow != action.ActionBar.ColorBarGroup) showPage(action.ActionBar.ColorBarGroup);
            }
        }
        // 恢复默认
        private void SetToDefaultButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(String.Format(TS3Sky.Language.Dialog.SetToDefaultContent, currentShow.ColorName), TS3Sky.Language.Dialog.SetToDefaultTitle, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    currentShow.SetToDefault();
                    // 标记为已保存(未修改)
                    currentShow.Modified = false;
                    // 使保存按钮不可用
                    SaveButton.IsEnabled = false;
                    UndoButton.IsEnabled = false;
                    // 使撤消失效
                    ColorChangeAction.ClearActions();
                    // 去掉导航按钮后面的星号
                    int index = colorPickBarLink.SkyColorListAsIndex.IndexOf(currentShow);
                    if (index >= 0) colorPickBarLink.SkyColorButton[index].Content = currentShow.ColorName;
                    // 重新读取页面
                    currentShow.Reread();
                    RefreshPage(currentShow);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format(TS3Sky.Language.Dialog.SetToDefaultFailedContent, ex.Message), TS3Sky.Language.Dialog.SetToDefaultFailedTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }
        // 接收颜色条控件传来的已经点击颜色事件
        public void ColorPicked()
        {
            if (ColorChangeAction.GetUndoAction() == null) UndoButton.IsEnabled = false;
            else UndoButton.IsEnabled = true;
            if (currentShow.Modified) SaveButton.IsEnabled = true;
            else SaveButton.IsEnabled = false;
            foreach (SkyColor skyColor in WeatherSky.CurrentWeather.SkyColors)
            {
                Button button = FindButtonBySkyColor(skyColor);
                if (skyColor.Modified)
                {
                    if (!button.Content.ToString().EndsWith("*")) button.Content += "*";
                }
            }
            if (WeatherSky.IsWeatherModified)
            {
                if (!SkyColorWeather.Content.ToString().EndsWith("*")) SkyColorWeather.Content += "*";
            }
        }
        // 用于更新页面按钮是否可用(与上面的方法接收颜色点击事件作用一致)
        private void UpdatePageState()
        {
            ColorPicked();
        }
        // 执行操作后需要刷新颜色页
        private void RefreshPage(SkyColor skyColor)
        {
            int index = colorPickBarLink.SkyColorListAsIndex.IndexOf(skyColor);
            if (index >= 0)
            {
                for (int i = 0; i < colorPickBarLink.ColorPickBarList[index].Count; i++)
                {
                    colorPickBarLink.ColorPickBarList[index][i].Refresh();
                }
            }
            Button button = FindButtonBySkyColor(skyColor);
            if (button == null) return;
            if (skyColor.Modified)
            {
                if (!button.Content.ToString().EndsWith("*")) button.Content += "*";
            }
            else
            {
                if (button.Content.ToString().EndsWith("*")) button.Content = skyColor.ColorName;
            }
        }
        #endregion
        #endregion

        #region 关于页
        // 访问帖子 - 联系我们
        private void ProgramSiteButton_Click(object sender, RoutedEventArgs e)
        {
            VisitSite(TS3Sky.Language.Application.ProgramSite);
        }
        private void ContactUsButton_Click(object sender, RoutedEventArgs e)
        {
            VisitSite(TS3Sky.Language.Application.ContactSite);
        }
        // 访问帖子 - 下载世界
        private void DownloadWorldButton_Click(object sender, RoutedEventArgs e)
        {
            VisitSite(TS3Sky.Language.Application.WorldDownloadSite);
        }
        // 访问网站
        public static void VisitSite(string site)
        {
            System.Diagnostics.Process.Start(site);
        }
        #endregion




    }

    #region 封装一个绑定后台颜色组和前台颜色条的类
    class ColorPickBarLink
    {
        public List<SkyColor> SkyColorListAsIndex = new List<SkyColor>();
        public List<Button> SkyColorButton = new List<Button>();
        public List<List<ColorPickBar>> ColorPickBarList = new List<List<ColorPickBar>>();
    }
    #endregion
}


#region XAML 自定义滚动条
/*
<Style TargetType="{x:Type ScrollBar}">
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="{x:Type ScrollBar}">
                <StackPanel>
                    <!--上按钮-->
                    <RepeatButton HorizontalAlignment="Center" VerticalAlignment="Top" Command="ScrollBar.LineUpCommand" Content="^"/>
                    <Track  x:Name="PART_Track" IsDirectionReversed="True" Height="100">
                        <Track.DecreaseRepeatButton>
                            <!--上空白-->
                            <RepeatButton Command="ScrollBar.PageUpCommand" />
                        </Track.DecreaseRepeatButton>
                        <Track.Thumb>
                            <!--滑块-->
                            <Thumb Background="SteelBlue"/>
                        </Track.Thumb>
                        <Track.IncreaseRepeatButton>
                            <!--下空白-->
                            <RepeatButton Command="ScrollBar.PageDownCommand" />
                        </Track.IncreaseRepeatButton>
                    </Track>
                    <!--下按钮-->
                    <RepeatButton HorizontalAlignment="Center" VerticalAlignment="Top" Command="ScrollBar.LineDownCommand" Content="v"/>
                </StackPanel>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
*/
#endregion