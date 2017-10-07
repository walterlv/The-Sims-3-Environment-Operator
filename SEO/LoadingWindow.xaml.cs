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

namespace Seo
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
            VersionText.Content = Seo.Language.Application.ShowVersion;
        }

        private void UpdateLogo(string local)
        {
            if (local.Equals("zh-cn"))
                LogoImage.Source = new BitmapImage(new Uri(String.Format("/Images/Logo-zh-cn.png", local), UriKind.Relative));
            else if (local.Equals("zh-tw") || local.Equals("zh-hk") || local.Equals("zh-mo"))
                LogoImage.Source = new BitmapImage(new Uri(String.Format("/Images/Logo-zh-tw.png", local), UriKind.Relative));
        }

        private void UpdateLoadingState(int n)
        {
            ThreadDelegate updateState = delegate()
            {
                switch (n)
                {
                    case 0:
                        UpdateLogo(Seo.Language.LanguageManager.LocalLanguage);
                        LoadingText.Content = Seo.Language.Application.LoadLanguage;
                        break;
                    case 1:
                        LoadingText.Content =Seo.Language.Application.LoadEnvironment;
                        break;
                    case 2:
                        LoadingText.Content = Seo.Language.Application.LoadPackage;
                        break;
                    case 3:
                        LoadingText.Content = Seo.Language.Application.LoadCompleted;
                        MainWindow mw = new MainWindow();
                        mw.Show();
                        this.Close();
                        break;
                    case -1:
                        #region 打开语言选择对话框
                        UpdateLogo(Seo.Language.LanguageManager.GetSystemLanguage());
                        LoadingText.Content = Seo.Language.Application.LoadStart;
                        LanguageWindow lw = new LanguageWindow();
                        lw.Owner = this;
                        lw.Languages = Seo.Language.LanguageManager.GetLanguages();
                        lw.SelectedLanguage = Seo.Language.LanguageManager.GetSystemLanguage();
                        lw.ShowDialog();
                        if (lw.DialogResult == true)
                        {
                            Seo.Language.LanguageManager.SaveLanguage(lw.SelectedLanguage);
                            ThreadDelegate load = new ThreadDelegate(InitializeApplication);
                            load.BeginInvoke(null, null);
                        }
                        else
                        {
                            this.Close();
                        }
                        #endregion
                        break;
                    case -2:
                        #region 打开路径选择对话框
                        SetInstallDirWindow sidw = new SetInstallDirWindow();
                        sidw.Owner = this;
                        sidw.ShowDialog();
                        if (sidw.DialogResult == true)
                        {
                            WeatherSky.ColorDirectory = sidw.InstallDir;
                            Configs.CustomInstallDir = sidw.InstallDir;
                            Configs.Save();
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
            if (Seo.Language.LanguageManager.IsSettingExisted)
            {
                Seo.Language.LanguageManager.InitializeLocal();
            }
            else
            {
                string tempLocal = Seo.Language.LanguageManager.GetSystemLanguage();
                Seo.Language.Application.Initialize(tempLocal);
                UpdateLoadingState(-1);
                return;
            }
            #endregion

            UpdateLoadingState(0);

            #region 初始化语言
            // 初始化语言 (如果要读取外部语言,填写true,如果读取内置语言,填写false)
            try
            {
                Seo.Language.LanguageManager.ReadExternalLanguage();
            }
            // 如果没有对应的语言文件
            catch (Seo.Exceptions.LanguageFileNotFoundException)
            {
                string tempLocal = Seo.Language.LanguageManager.GetSystemLanguage();
                Seo.Language.Application.Initialize(tempLocal);
                UpdateLoadingState(-1);
                return;
            }
            // 如果读取语言异常
            catch (Exception ex)
            {
            }
            #endregion

            UpdateLoadingState(1);

            #region 读取模拟人生3环境配置文件
            try
            {
                Configs.Initialize();
                if (Configs.CustomInstallDir != null) WeatherSky.ColorDirectory = Configs.CustomInstallDir + WeatherSky.SimsDirectoryTail;
                WeatherSky.ReadAllWeathers();
            }
            // 如果找不到游戏目录, 则提示用户手动定位
            catch (Seo.Exceptions.CannotFindInstallDirException)
            {
                UpdateLoadingState(-2);
                return;
            }
            // 如果仍然出错, 则提示错误并记录日志
            catch (Exception ex)
            {
                Log.ErrorLog.WriteErrorLog(ex, -100);
                MessageBox.Show(Seo.Language.Dialog.ReadEnvironmentFileFailedContent, Seo.Language.Dialog.ReadEnvironmentFileFailedTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                Environment.Exit(-1);
            }
            #endregion

            UpdateLoadingState(2);

            #region 读取方案文件
            try { Package.OpenAll(); }
            catch (Exception ex)
            {
                Log.ErrorLog.WriteErrorLog(ex, -101);
                MessageBox.Show(Seo.Language.Dialog.ReadEnvironmentFileFailedContent, Seo.Language.Dialog.ReadEnvironmentFileFailedTitle);
            }
            #endregion

            UpdateLoadingState(3);
        }
    }
}
