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
    enum TS3SkyPage
    {
        Welcome,
        Content,
        About
    }

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 初始化语言
            try { TS3Sky.Language.LanguageManager.Initialize(false); }
            catch { }
        }

        /// <summary>
        /// 要修改的颜色列表，目前共有三组：天空，海洋，阴天海洋。
        /// </summary>
        private List<SkyColor> SkyColorList = new List<SkyColor>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 初始化应用程序资源路径
            string BackgroundPath = Environment.CurrentDirectory + @"\Images\Background.jpg";
            string WelcomePath = Environment.CurrentDirectory + @"\Images\Welcome.png";
            // 初始化应用程序界面风格
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
            // 初始化应用程序界面内容
            // 全局窗口
            this.Title = TS3Sky.Language.Application.Name;
            // 初始化导航按钮
            SkyColorWelcome.Content = TS3Sky.Language.Navigation.Welcome;
            SkyColorAbout.Content = TS3Sky.Language.Navigation.About;
            SkyColor Sky_CustomSky = SkyColor.FromColorAssembly(ColorAssembly.Sky_CustomSky);
            SkyColor Sky_ClearSea = SkyColor.FromColorAssembly(ColorAssembly.Sky_ClearSea);
            SkyColor Sky_OvercastSea = SkyColor.FromColorAssembly(ColorAssembly.Sky_OvercastSea);
            SkyColorList.Add(Sky_CustomSky);
            SkyColorList.Add(Sky_ClearSea);
            SkyColorList.Add(Sky_OvercastSea);
            SkyColorButton0.Content = SkyColorList[0].ColorName;
            SkyColorButton1.Content = SkyColorList[1].ColorName;
            SkyColorButton2.Content = SkyColorList[2].ColorName;
            // 初始化操作按钮
            SaveButton.Content = TS3Sky.Language.Operation.Save;
            RevokeButton.Content = TS3Sky.Language.Operation.Revoke;
            SetToDefaultButton.Content = TS3Sky.Language.Operation.SetToDefault;
            // 初始化导航标签文字
            LabelText.Text = TS3Sky.Language.Navigation.Label;
            // 初始化欢迎画面
            try
            {
                if (File.Exists(WelcomePath))
                {
                    Uri uri = new Uri(WelcomePath);
                    BitmapImage bitmap = new BitmapImage(uri);
                    WelcomeImage.Source = bitmap;
                    WelcomeText.Visibility = Visibility.Collapsed;
                }
            }
            catch { }
            // 初始化关于画面
            AboutInfomation.Text = String.Format("{0}:\n{1}\n\n{2}: {3}\n\n{4}: {5}\n{6}\n\n{7}",
                                    TS3Sky.Language.About.Author,
                                    TS3Sky.Language.About.AuthorName,
                                    TS3Sky.Language.About.Version,
                                    TS3Sky.Language.About.VersionText,
                                    TS3Sky.Language.About.Publish,
                                    TS3Sky.Language.About.PublishText,
                                    TS3Sky.Language.About.Contact,
                                    TS3Sky.Language.About.Copyright);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string uid = ((Button)sender).Uid;
            if (uid.Equals("TS3N0"))
            {
                showPage(TS3SkyPage.Welcome);
            }
            else if (uid.Equals("TS3N1"))
            {
                showPage(SkyColorList[0]);
            }
            else if (uid.Equals("TS3N2"))
            {
                showPage(SkyColorList[1]);
            }
            else if (uid.Equals("TS3N3"))
            {
                showPage(SkyColorList[2]);
            }
            else if (uid.Equals("TS3N4"))
            {
                showPage(TS3SkyPage.About);
            }
            else
            {
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string uid = ((Button)sender).Uid;
            if (uid.Equals("TS3O1"))
            {
                if (MessageBox.Show(String.Format(TS3Sky.Language.Dialog.SaveContent, currentShow.ColorName), TS3Sky.Language.Dialog.SaveTitle, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    currentShow.Save();
                }
            }
            else if (uid.Equals("TS3O2"))
            {
                if (MessageBox.Show(String.Format(TS3Sky.Language.Dialog.RevokeContent, currentShow.ColorName), TS3Sky.Language.Dialog.RevokeTitle, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    currentShow.Revoke();
                    RefreshPage(currentShow);
                }
            }
            else if (uid.Equals("TS3O3"))
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
                        MessageBox.Show("无法恢复到默认的颜色配置！可能是备份的颜色方案丢失。" + Environment.NewLine + Environment.NewLine + ex.Message, "无法恢复", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                }
            }
            else
            {
            }
        }

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
                            ColorBarStartIndex = SkyContent.Children.IndexOf(colorPickBar) - 1;
                        }
                    }
                    colorPickBarLink.SkyColorListAsIndex.Add(skyColor);
                    colorPickBarLink.ColorPickBarList.Add(colorPickBarList);
                }
            }
            catch (Exception ex)
            {
                debugText.Text = ex.Message + Environment.NewLine + ex.StackTrace;
            }
            if (SkyContent.Visibility != Visibility.Visible)
            {
                SkyWelcome.Visibility = Visibility.Collapsed;
                SkyContent.Visibility = Visibility.Visible;
                SkyAbout.Visibility = Visibility.Collapsed;
            }
            currentShow = skyColor;
        }

        private void showPage(TS3SkyPage page)
        {
            switch (page)
            {
                case TS3SkyPage.Welcome:
                    if (SkyWelcome.Visibility != Visibility.Visible)
                    {
                        SkyWelcome.Visibility = Visibility.Visible;
                        SkyContent.Visibility = Visibility.Collapsed;
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
                        SkyContent.Visibility = Visibility.Collapsed;
                        SkyAbout.Visibility = Visibility.Visible;
                    }
                    currentShow = null;
                    break;
                default:
                    currentShow = null;
                    break;
            }
        }

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
    }

    class ColorPickBarLink
    {
        public List<SkyColor> SkyColorListAsIndex = new List<SkyColor>();
        public List<List<ColorPickBar>> ColorPickBarList = new List<List<ColorPickBar>>();
    }
}
