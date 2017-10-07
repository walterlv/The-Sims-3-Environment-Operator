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

using Seo.WindowParts;
using Seo.WindowEffects;
using Seo.WindowPages;

namespace Seo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // 如果不是Win7, 则使用自定义窗口
            if (!Configs.GlassWindow || Environment.OSVersion.Version.Major != 6)
            {
                this.AllowsTransparency = true;
                this.Background = new SolidColorBrush(Colors.LightGray);
            }
            // 让标题栏的窗口控制按钮自动工作
            WindowTitleBox.AutoHandleWindow = this;
            WindowTitleBox.OnWindowBox_Click += OnWindowBox_Click;
            // 添加导航标签
            foreach (string pageName in PageManager.PageNames) NaviTabPanel.AddChild(pageName);
            NaviTabPanel.SelectedIndex = 0;
        }

        private void OnWindowBox_Click(object sender, WindowBoxArgs e)
        {
        }

        // 使窗口可被拖动
        private void TitleGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { this.DragMove(); }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 如果是Win7, 则使窗口毛玻璃透明; 否则使用自定义窗口
            if (Configs.GlassWindow && Environment.OSVersion.Version.Major == 6)
            { GlassHelper.ExtendGlassFrame(this); }
        }

        private void NavigationTab_SelectionChanged(object sender, NavigationTabArgs e)
        {
            PageFrame.Content = PageManager.GetPageByIndex(e.SelectedIndex);
        }
    }
}
