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

namespace TS3Sky
{
    #region 枚举类型 - 三种可能的页
    enum TS3SkyPage
    {
        Welcome,
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

            #region 最原始的初始化 (语言)
            // 初始化语言 (如果要读取外部语言,填写true,如果读取内置语言,填写false)
            try { TS3Sky.Language.LanguageManager.Initialize(false); }
            catch { }
            #endregion
        }

        /// <summary>
        /// 所有可使用的环境配置包
        /// </summary>
        List<Package> Packages;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region 初始化应用程序资源路径
            string BackgroundPath = Environment.CurrentDirectory + @"\Images\Background.jpg";
            string WelcomePath = Environment.CurrentDirectory + @"\Images\Welcome.png";
            #endregion

            #region 初始化应用程序界面风格
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
            this.Title = TS3Sky.Language.Application.Name + " (内部测试版, 仅供制作预设方案)";
            // 初始化导航按钮
            SkyColorWelcome.Content = TS3Sky.Language.Navigation.Welcome;
            SkyColorAbout.Content = TS3Sky.Language.Navigation.About;
            try
            {
                SkyColor Sky_Clear1 = SkyColor.FromColorAssembly(ColorAssembly.Sky_Clear1);
                SkyColor Sky_Clear2 = SkyColor.FromColorAssembly(ColorAssembly.Sky_Clear2);
                SkyColor Sky_ClearLight = SkyColor.FromColorAssembly(ColorAssembly.Sky_ClearLight);
                SkyColor Sky_ClearSky = SkyColor.FromColorAssembly(ColorAssembly.Sky_ClearSky);
                SkyColor Sky_ClearSea = SkyColor.FromColorAssembly(ColorAssembly.Sky_ClearSea);
                //SkyColorList.Add(Sky_Clear1);
                //SkyColorList.Add(Sky_Clear2);
                //SkyColorList.Add(Sky_ClearLight);
                //SkyColorList.Add(Sky_ClearSky);
                //SkyColorList.Add(Sky_ClearSea);
                SkyColorButton0.Content = SkyColor.AllSkyColors[0].ColorName;
                SkyColorButton1.Content = SkyColor.AllSkyColors[1].ColorName;
                SkyColorButton2.Content = SkyColor.AllSkyColors[2].ColorName;
                SkyColorButton3.Content = SkyColor.AllSkyColors[3].ColorName;
                SkyColorButton4.Content = SkyColor.AllSkyColors[4].ColorName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(TS3Sky.Language.Dialog.InitialFailedContent, ex.Message), TS3Sky.Language.Dialog.InitialFailedTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            // 初始化欢迎页面
            AboutButton.Content = TS3Sky.Language.Navigation.About;
            // 初始化操作按钮
            DownloadWorldButton.Content = TS3Sky.Language.Operation.DownloadWorld;
            SaveButton.Content = TS3Sky.Language.Operation.Save;
            RevokeButton.Content = TS3Sky.Language.Operation.Revoke;
            SetToDefaultButton.Content = TS3Sky.Language.Operation.SetToDefault;
            // 初始化导航标签文字
            LabelText.Text = TS3Sky.Language.Navigation.Label;
            // 初始化关于画面
            AboutInfomation.Text = String.Format("{0}:\n{1}\n\n{2}: {3}\n\n{4}: {5}\n{6}",
                                    TS3Sky.Language.About.Author,
                                    TS3Sky.Language.About.AuthorName,
                                    TS3Sky.Language.About.Version,
                                    TS3Sky.Language.About.VersionText,
                                    TS3Sky.Language.About.Publish,
                                    TS3Sky.Language.About.PublishText,
                                    TS3Sky.Language.About.Contact);
            ContactUsButton.Content = TS3Sky.Language.About.ContactUs;
            CopyrightInfomation.Text = TS3Sky.Language.About.Copyright;
            #endregion

            #region 初始化预设方案列表
            try
            {
                Packages = Package.OpenAll();
                foreach (Package p in Packages)
                {
                    PackageListItem pli = new PackageListItem();
                    pli.ShowPackage = p;
                    PackageListBox.Items.Add(pli);
                }
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, ex.Message); }
            #endregion
        }

        #region 点击导航按钮按钮 (包括在欢迎页中的其它导航按钮)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = (string)((Button)sender).Content;
            if (name.Equals(TS3Sky.Language.Navigation.Welcome))
            {
                showPage(TS3SkyPage.Welcome);
            }
            else if (name.Equals(TS3Sky.Language.Navigation.About))
            {
                showPage(TS3SkyPage.About);
            }
            else if (name.Equals(TS3Sky.Language.Sky_Clear1.DayColorName))
            {
                showPage(SkyColor.AllSkyColors[0]);
            }
            else if (name.Equals(TS3Sky.Language.Sky_Clear2.DayColorName))
            {
                showPage(SkyColor.AllSkyColors[1]);
            }
            else if (name.Equals(TS3Sky.Language.Sky_ClearLight.DayColorName))
            {
                showPage(SkyColor.AllSkyColors[2]);
            }
            else if (name.Equals(TS3Sky.Language.Sky_ClearSky.DayColorName))
            {
                showPage(SkyColor.AllSkyColors[3]);
            }
            else if (name.Equals(TS3Sky.Language.Sky_ClearSea.DayColorName))
            {
                showPage(SkyColor.AllSkyColors[4]);
            }
            else
            {
            }
        }
        #endregion

        // 当选择一个方案时
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

        #region 点击功能按钮
        // 保存, 撤销, 恢复
        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            string name = (string)((Button)sender).Content;
            if (name.Equals(TS3Sky.Language.Operation.Save))
            {
                if (MessageBox.Show(String.Format(TS3Sky.Language.Dialog.SaveContent, currentShow.ColorName), TS3Sky.Language.Dialog.SaveTitle, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    while (true)
                    {
                        try { currentShow.Save(); break; }
                        catch (Exception ex)
                        {
                            if (MessageBox.Show(String.Format(TS3Sky.Language.Dialog.SaveRetryContent, ex.Message), TS3Sky.Language.Dialog.SaveRetryTitle,
                                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) continue;
                            else break;
                        }
                    }
                }
            }
            else if (name.Equals(TS3Sky.Language.Operation.Revoke))
            {
                if (MessageBox.Show(String.Format(TS3Sky.Language.Dialog.RevokeContent, currentShow.ColorName), TS3Sky.Language.Dialog.RevokeTitle, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    currentShow.Revoke();
                    RefreshPage(currentShow);
                }
            }
            else if (name.Equals(TS3Sky.Language.Operation.SetToDefault))
            {
                if (MessageBox.Show(String.Format(TS3Sky.Language.Dialog.SetToDefaultContent, currentShow.ColorName), TS3Sky.Language.Dialog.SetToDefaultTitle, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    try
                    {
                        currentShow.SetToDefault();
                        currentShow.Revoke();
                        RefreshPage(currentShow);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format(TS3Sky.Language.Dialog.SetToDefaultFailedContent, ex.Message), TS3Sky.Language.Dialog.SetToDefaultFailedTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                }
            }
            else
            {
            }
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
        }

        // 导出
        private void ExportPackageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("如果要导出你自定义的方案，你需要先保存你所有的配色。\n\n保存后继续吗？", "需要保存才能继续", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (SkyColor color in SkyColor.AllSkyColors)
                    {
                        color.Save();
                    }
                    ExportWindow ew = new ExportWindow();
                    ew.Owner = this;
                    ew.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, ex.Message);
            }
        }

        // 访问帖子 - 联系我们
        private void ContactUsButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(TS3Sky.Language.About.ContactSite);
        }
        // 访问帖子 - 下载世界
        private void DownloadWorldButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(TS3Sky.Language.About.WorldDownload);
        }
        #endregion

        #region 导航到页面
        #region 颜色页
        int ColorBarStartIndex = -1;
        ColorPickBarLink colorPickBarLink = new ColorPickBarLink();
        SkyColor currentShow = null;
        private void showPage(SkyColor skyColor)
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
                SkyTitleText.Text = skyColor.ColorName;
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
                        colorPickBar.Width = 482;
                        colorPickBar.ColorBar = skyColor.DayColors[i];
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
                ColorPageScroll.Visibility = Visibility.Visible;
                SkyAbout.Visibility = Visibility.Collapsed;
            }
            // 确保换页时滚动条滚到顶端
            ColorPageScroll.ScrollToHome();
            currentShow = skyColor;
        }
        #endregion
        #region 布局页
        private void showPage(TS3SkyPage page)
        {
            switch (page)
            {
                case TS3SkyPage.Welcome:
                    PackageListBox.SelectedIndex = -1;
                    if (SkyWelcome.Visibility != Visibility.Visible)
                    {
                        SkyWelcome.Visibility = Visibility.Visible;
                        ColorPageScroll.Visibility = Visibility.Collapsed;
                        SkyAbout.Visibility = Visibility.Collapsed;
                    }
                    currentShow = null;
                    break;
                case TS3SkyPage.Content:
                    break;
                case TS3SkyPage.About:
                    if (SkyAbout.Visibility != Visibility.Visible)
                    {
                        SkyWelcome.Visibility = Visibility.Collapsed;
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
        #endregion


        private void ApplyPackageButton_Click(object sender, RoutedEventArgs e)
        {
            int index = PackageListBox.SelectedIndex;
            if (MessageBox.Show(String.Format("应用方案 “{0}” 将会覆盖掉你现在正在使用的方案。\n\n是否继续？", Packages[index].Name), "应用方案", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try { Package.Apply(Packages[index]); MessageBox.Show(String.Format("已经成功应用 “{0}” 方案。", Packages[index].Name), "成功应用方案", MessageBoxButton.OK, MessageBoxImage.Information); }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "应用方案时出现问题", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }
        private void DeletePackageButton_Click(object sender, RoutedEventArgs e)
        {
            int index = PackageListBox.SelectedIndex;
            if (MessageBox.Show(String.Format("要删除环境配置方案 “{0}” 吗？", Packages[index].Name), "删除方案", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ((PackageListItem)PackageListBox.Items[index]).PackageImage = null;
                PackageListBox.Items.RemoveAt(index);
                try
                {
                    Packages[index].Delete();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "删除不完全", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                Packages.RemoveAt(index);
            }
        }
        private void ImportPackageButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();
            open.Title = "添加方案";
            open.Filter = String.Format("{1}|*.{0}", Package.Extension, Package.ExtensionDescription);
            open.AddExtension = true;
            open.AutoUpgradeEnabled = true;
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!Package.Exists(open.FileName))
                {
                    Package imp = Package.Import(open.FileName);
                    Packages.Add(imp);
                    PackageListItem pli = new PackageListItem();
                    pli.ShowPackage = imp;
                    PackageListBox.Items.Add(pli);
                }
                else
                {
                    MessageBox.Show("不能添加已经存在的方案。", "方案已经存在", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

    }

    #region 封装一个绑定后台颜色组和前台颜色条的类
    class ColorPickBarLink
    {
        public List<SkyColor> SkyColorListAsIndex = new List<SkyColor>();
        public List<List<ColorPickBar>> ColorPickBarList = new List<List<ColorPickBar>>();
    }
    #endregion
}
