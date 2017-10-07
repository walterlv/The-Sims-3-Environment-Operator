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
    public partial class MainWindow : Window, ILanguage
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // 使窗口可被拖动
        private void TitleGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { this.DragMove(); }
        // 初始化窗口
        PageManager pageManager;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 注册受保护的语言
            Seo.Language.Register(this, Priority.Lowest);
            // 如果是Win7, 则使窗口毛玻璃透明; 否则使用自定义窗口
            if (Configs.GlassWindow && Environment.OSVersion.Version.Major == 6)
            { GlassHelper.ExtendGlassFrame(this); }
            else { this.Background = new SolidColorBrush(Color.FromRgb(0x3F, 0x3F, 0x3F)); }
            // 设置选择第一个标签
            pageManager = new PageManager();
            foreach (string name in pageManager.PageNames) NaviTabPanel.AddChild(name);
            NaviTabPanel.SelectedIndex = 0;
        }
        // 加载语言
        public void LoadLanguage()
        {
            this.Title = Seo.Language.ApplicationTitle;
            for (int i = 0; i < NaviTabPanel.Children.Count; i++) NaviTabPanel.Children[i].Text = pageManager.PageNames[i];
        }
        // 选择导航标签
        private void NavigationTab_Selected(object sender, NavigationTabArgs e)
        {
            bool naviIn = (pageManager.GetPageByIndex(e.SelectedIndex) as PageNavigation).NavigationIn();
            PageFrame.Content = pageManager.GetPageByIndex(e.SelectedIndex);
        }
    }
}
