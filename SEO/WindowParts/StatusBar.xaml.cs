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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Seo.WindowParts
{
    public enum Status
    {
        None,
        Information,
        Progress,
        Success,
        Warning,
        Error
    }
    /// <summary>
    /// StatusBar.xaml 的交互逻辑
    /// </summary>
    public partial class StatusBar : UserControl
    {
        public StatusBar()
        {
            InitializeComponent();
            CurrentStatus = Status.None;
            timer = new DispatcherTimer();
            timer.Tick += ToNormalStatus;
            Instance = this;
        }

        public static StatusBar Instance;

        DispatcherTimer timer;
        private Status CurrentStatus;
        public static void Show(Status status, string text)
        {
            if (Instance != null) Instance.ShowStatus(status, text, 0);
        }
        public static void Show(Status status, string text, int time)
        {
            if (Instance != null) Instance.ShowStatus(status, text, time);
        }
        private void ShowStatus(Status status, string text, int time)
        {
            if (CurrentStatus != status)
            {
                if (status == Status.None) BeginStoryboard(Instance.FindResource("NormalStory") as Storyboard);
                else if (status == Status.Information) BeginStoryboard(Instance.FindResource("InformationStory") as Storyboard);
                else if (status == Status.Progress) BeginStoryboard(Instance.FindResource("ProgressStory") as Storyboard);
                else if (status == Status.Success) BeginStoryboard(Instance.FindResource("SuccessStory") as Storyboard);
                else if (status == Status.Warning) BeginStoryboard(Instance.FindResource("WarningStory") as Storyboard);
                else if (status == Status.Error) BeginStoryboard(Instance.FindResource("ErrorStory") as Storyboard);
            }
            StatusText.Text = text;
            CurrentStatus = status;
            timer.Stop();
            if (time > 0)
            {
                timer.Interval = new TimeSpan(0, 0, 0, 0, time);
                timer.Start();
            }
        }
        private void ToNormalStatus(object sender, EventArgs e)
        {
            timer.Stop();
            CurrentStatus = Status.None;
            BeginStoryboard(Instance.FindResource("NormalStory") as Storyboard);
        }
    }
}
