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
using System.Windows.Media.Animation;

namespace Seo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, ILanguage
    {
        public static MainWindow Current;
        public MainWindow()
        {
            InitializeComponent();
            Current = this;
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
            if (Configs.GlassWindow) { AeroGlass = true; }
            else { AeroGlass = false; }
            // 如果自定义过颜色, 则显示自定义颜色
            if (Configs.ForeColor != null)
                App.This.UpdateColors(Color.FromArgb(Configs.ForeColor.A, Configs.ForeColor.R, Configs.ForeColor.G, Configs.ForeColor.B));
            // 设置选择第一个标签
            pageManager = new PageManager();
            foreach (string name in pageManager.PageNames) NaviTabPanel.AddChild(name);
            NaviTabPanel.SelectedIndex = 0;
            // 提示只读模式
            if (CommonOperation.IsReadonlyMode) CommonOperation.ShowReadonlyTip();
            // 检查更新
            if (Configs.AutoUpdate)
            {
                CommonOperation update = new CommonOperation();
                update.UpdateChecked += update_UpdateChecked;
                update.GetUpdateAsync();
            }
        }

        public bool AeroGlass
        {
            set
            {
                if (value) GlassHelper.ExtendGlassFrame(this);
                else
                {
                    GlassHelper.ExtendGlassFrame(this, new Thickness(0));
                    try
                    {
                        ImageBrush imageBrush = new ImageBrush();
                        imageBrush.ImageSource = new BitmapImage(new Uri(Configs.BackgroundImage));
                        imageBrush.Stretch = Stretch.Fill;
                        this.Background = imageBrush;
                        Configs.UseBackgroundImage = true;
                    }
                    catch
                    {
                        this.Background = new SolidColorBrush(Color.FromRgb(0x3F, 0x3F, 0x3F));
                    }
                }
            }
        }

        // 加载语言
        public void LoadLanguage()
        {
            // 判断只读模式
            if (CommonOperation.IsReadonlyMode) this.Title = Seo.Language.ApplicationTitle + " (" + Seo.Languages.Window.ReadonlyMode + ")";
            else this.Title = Seo.Language.ApplicationTitle;
            for (int i = 0; i < NaviTabPanel.Children.Count; i++) NaviTabPanel.Children[i].Text = pageManager.PageNames[i];
        }
        // 关闭窗口时处理
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CommonOperation.IsReadonlyMode) return;
            if (EnvironmentOperator.Instance.IsModified)
            {
                MessageBoxResult result = MessageBox.Show(Seo.Languages.Information.CloseSaveContent, Seo.Languages.Information.CloseSaveTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    this.Hide();
                    EnvironmentOperator.Instance.Save();
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    this.Hide();
                }
            }
            Configs.Save();
            CommonOperation.ClearReadonly();
            EnviFile.ClearTempBackupAndCache();
        }
        // 选择导航标签
        private void NavigationTab_Selected(object sender, NavigationTabArgs e)
        {
            bool navi = true;
            // 首先判断是否允许导航出去
            Page naviOut = pageManager.GetPageByIndex(e.OldSelectedIndex);
            if (naviOut != null) navi = (naviOut as PageNavigation).NavigationOut();
            if (!navi) e.Cancel = true;
            // 如果允许导航出去, 则判断是否允许导航进入
            else
            {
                Page naviIn = pageManager.GetPageByIndex(e.SelectedIndex);
                if (naviIn != null) navi = (naviIn as PageNavigation).NavigationIn();
                if (navi) PageFrame.Content = pageManager.GetPageByIndex(e.SelectedIndex);
                else e.Cancel = true;
            }
        }

        // 检查完更新
        private void update_UpdateChecked(object sender, UpdateArgs e)
        {
            if (e.IsSuccess && e.NewVersion.UpdateExisted)
            {
                UpdateTitleText.Text = Seo.Languages.Window.NewVersionTitle;
                UpdateVersionText.Text = e.NewVersion.Version;
                UpdateButton.Text = Seo.Languages.Window.NewVersionButton;
                if (e.NewVersion.Features.Length > 16) UpdateContentText.Text = e.NewVersion.Features.Substring(0, 16) + "...";
                else UpdateContentText.Text = e.NewVersion.Features;
                UpdatePanel.Visibility = System.Windows.Visibility.Visible;
                BeginStoryboard(FindResource("ShowUpdateStory") as Storyboard);
                StatusBar.Show(Status.Information, Seo.Languages.Window.NewVersionContent, 8000);
            }
        }
        private void UpdateButton_Click(object sender, SimpleButtonArgs e)
        {
            CommonOperation.VisitSite(Seo.Language.PublishSite);
            UpdateButton.IsCustomEnabled = false;
            BeginStoryboard(FindResource("HideUpdateStory") as Storyboard);
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            UpdatePanel.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
