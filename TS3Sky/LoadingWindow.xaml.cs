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
using System.Windows.Threading;
using System.Threading;

namespace TS3Sky
{
    /// <summary>
    /// LoadingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();
        }

        private delegate void ThreadDelegate();

        // 使窗口可以拖动
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ThreadDelegate load = new ThreadDelegate(InitializeApplication);
            load.BeginInvoke(null, null);
            VersionText.Content = TS3Sky.Language.Application.ShowVersion;
        }

        private void UpdateLogo(string local)
        {
            if (local.Equals("zh-cn") || local.Equals("zh-tw"))
                LogoImage.Source = new BitmapImage(new Uri(String.Format("/Images/Logo-{0}.png", local), UriKind.Relative));
        }

        private void UpdateLoadingState(int n)
        {
            ThreadDelegate updateState = delegate()
            {
                switch (n)
                {
                    case 0:
                        UpdateLogo(TS3Sky.Language.LanguageManager.LocalLanguage);
                        LoadingText.Content = TS3Sky.Language.Application.LoadLanguage;
                        break;
                    case 1:
                        LoadingText.Content =TS3Sky.Language.Application.LoadEnvironment;
                        break;
                    case 2:
                        LoadingText.Content = TS3Sky.Language.Application.LoadPackage;
                        break;
                    case 3:
                        LoadingText.Content = TS3Sky.Language.Application.LoadCompleted;
                        MainWindow mw = new MainWindow();
                        mw.Show();
                        this.Close();
                        break;
                    case -1:
                        #region 打开语言选择对话框
                        UpdateLogo(TS3Sky.Language.LanguageManager.GetSystemLanguage());
                        LoadingText.Content = TS3Sky.Language.Application.LoadStart;
                        LanguageWindow lw = new LanguageWindow();
                        lw.Owner = this;
                        lw.Languages = TS3Sky.Language.LanguageManager.GetLanguages();
                        lw.SelectedLanguage = TS3Sky.Language.LanguageManager.GetSystemLanguage();
                        lw.ShowDialog();
                        if (lw.DialogResult == true)
                        {
                            TS3Sky.Language.LanguageManager.SaveLanguage(lw.SelectedLanguage);
                            ThreadDelegate load = new ThreadDelegate(InitializeApplication);
                            load.BeginInvoke(null, null);
                        }
                        else
                        {
                            this.Close();
                        }
                        #endregion
                        break;
                    default:
                        Environment.Exit(-1);
                        break;
                }
            };
            this.Dispatcher.BeginInvoke(DispatcherPriority.Send, updateState);
        }

        private void InitializeApplication()
        {
            #region 初始化应用程序
            if (TS3Sky.Language.LanguageManager.IsSettingExisted)
            {
                TS3Sky.Language.LanguageManager.InitializeLocal();
            }
            else
            {
                string tempLocal = TS3Sky.Language.LanguageManager.GetSystemLanguage();
                TS3Sky.Language.Application.Initialize(tempLocal);
                UpdateLoadingState(-1);
                return;
            }
            #endregion

            UpdateLoadingState(0);

            #region 初始化语言
            // 初始化语言 (如果要读取外部语言,填写true,如果读取内置语言,填写false)
            try
            {
                TS3Sky.Language.LanguageManager.ReadExternalLanguage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Language Not Found", MessageBoxButton.OK, MessageBoxImage.Stop); Environment.Exit(-1);
            }
            #endregion

            UpdateLoadingState(1);

            #region 读取模拟人生3环境配置文件
            try
            {
                SkyColor Sky_Clear1 = SkyColor.FromColorAssembly(ColorAssembly.Sky_Clear1);
                SkyColor Sky_Clear2 = SkyColor.FromColorAssembly(ColorAssembly.Sky_Clear2);
                SkyColor Sky_ClearLight = SkyColor.FromColorAssembly(ColorAssembly.Sky_ClearLight);
                SkyColor Sky_ClearSky = SkyColor.FromColorAssembly(ColorAssembly.Sky_ClearSky);
                SkyColor Sky_ClearSea = SkyColor.FromColorAssembly(ColorAssembly.Sky_ClearSea);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(TS3Sky.Language.Dialog.InitialFailedContent, ex.Message), TS3Sky.Language.Dialog.InitialFailedTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                Environment.Exit(-1);
            }
            #endregion

            UpdateLoadingState(2);

            #region 读取方案文件
            try { Package.OpenAll(); }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, ex.Message); }
            #endregion

            UpdateLoadingState(3);
        }
    }
}
