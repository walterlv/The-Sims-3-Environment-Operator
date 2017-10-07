using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        #region 窗口界面操作
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { this.DragMove(); }
        private static Brush NormalStyle = new SolidColorBrush(Colors.Black);
        private static Brush HoverStyle = new SolidColorBrush(Colors.Red);
        private static Brush PressedStyle = new SolidColorBrush(Color.FromArgb(0xFF, 0x7F, 0x00, 0x00));
        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        { CloseButton.Foreground = HoverStyle; }
        private void CloseButton_MouseLeave(object sender, MouseEventArgs e)
        { CloseButton.Foreground = NormalStyle; }
        private void CloseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CloseButton.Foreground = HoverStyle;
            // TODO 加上结束线程语句
            this.Close();
        }
        #endregion

        #region 进行初始化
        BackgroundWorker LoadingWorker;
        enum ProgressState
        {
            Empty = 0,
            Start = 1,
            ReadLanguage = 2,
            ReadConfigs = 3,
            GetSimsDir = 4,
            AllLoaded = 50,
            SimsDirectoryNotFound = 99
        }
        ProgressState CurrentState = ProgressState.Start;
        ProgressState NormalState = ProgressState.Start;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BeginStoryboard(this.FindResource("LoadingStory") as Storyboard);
            CreateNewWorker();
            LoadingWorker.RunWorkerAsync();
        }

        // 执行异步操作 (这里的状态只正在执行的状态)
        void LoadingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (CurrentState)
            {
                // 表示开始初始化
                case ProgressState.Start:
                    CurrentState = ProgressState.ReadLanguage;
                    NormalState = CurrentState;
                    break;
                case ProgressState.ReadLanguage:
                    CultureInfo currentCultureInfo = CultureInfo.CurrentCulture;
                    Seo.Language.LoadLanguage(currentCultureInfo.Name);
                    CurrentState = ProgressState.ReadConfigs;
                    NormalState = CurrentState;
                    break;
                case ProgressState.ReadConfigs:
                    CurrentState = ProgressState.GetSimsDir;
                    NormalState = CurrentState;
                    break;
                case ProgressState.GetSimsDir:
                    try
                    {
                        FilesDirs.SimsFolder = FilesDirs.GetSimsDirectoryByRegistry();
                        CurrentState = ProgressState.AllLoaded;
                        NormalState = CurrentState;
                    }
                    catch (Exception)
                    {
                        if (Configs.SimsFolder == null) CurrentState = ProgressState.SimsDirectoryNotFound;
                        else
                        {
                            FilesDirs.SimsFolder = Configs.SimsFolder;
                            CurrentState = ProgressState.AllLoaded;
                            NormalState = CurrentState;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        // 当一个异步已经完成 (这里的状态代表下一步的状态)
        void LoadingWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null) MessageBox.Show("An error occored when loading the program.", "error");
            // 只在有特殊操作时才 case, 否则直接执行下一步.
            switch (CurrentState)
            {
                case ProgressState.AllLoaded:
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();
                    break;
                case ProgressState.SimsDirectoryNotFound:
                    SimsDirWindow sdf = new SimsDirWindow();
                    sdf.Owner = this;
                    sdf.ShowDialog();
                    if (sdf.SimsDir == null)
                    {
                        LoadingWorker.CancelAsync();
                        this.Close();
                    }
                    else
                    {
                        FilesDirs.SimsFolder = sdf.SimsDir;
                        Configs.SimsFolder = sdf.SimsDir;
                        CurrentState = NormalState;
                        CreateNewWorker();
                        LoadingWorker.RunWorkerAsync();
                    }
                    break;
                default:
                    CreateNewWorker();
                    LoadingWorker.RunWorkerAsync();
                    break;
            }
        }

        private void CreateNewWorker()
        {
            LoadingWorker = new BackgroundWorker();
            LoadingWorker.WorkerSupportsCancellation = true;
            LoadingWorker.DoWork += LoadingWorker_DoWork;
            LoadingWorker.RunWorkerCompleted += LoadingWorker_RunWorkerCompleted;
        }
        #endregion
    }
}
