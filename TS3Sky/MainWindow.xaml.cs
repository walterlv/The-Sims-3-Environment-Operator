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

namespace TS3Sky
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private List<SkyColor> SkyColorList = new List<SkyColor>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 初始化应用程序界面
            SkyColor Sky_CustomSky = SkyColor.FromColorAssembly(ColorAssembly.Sky_CustomSky);
            SkyColor Sky_ClearSea = SkyColor.FromColorAssembly(ColorAssembly.Sky_ClearSea);
            SkyColor Sky_OvercastSea = SkyColor.FromColorAssembly(ColorAssembly.Sky_OvercastSea);
            SkyColorList.Add(Sky_CustomSky);
            SkyColorList.Add(Sky_ClearSea);
            SkyColorList.Add(Sky_OvercastSea);
            SkyColorButton0.Content = SkyColorList[0].ColorName;
            SkyColorButton1.Content = SkyColorList[1].ColorName;
            SkyColorButton2.Content = SkyColorList[2].ColorName;
            // 初始化颜色修改页面(临时把鼠标点击按钮的事件移到了这里)
            if (SkyContent.Visibility != Visibility.Visible) SkyContent.Visibility = Visibility.Visible;
            SkyColor skyColor = SkyColorList[0];
            try
            {
                SkyTitleText.Text = "改变" + skyColor.ColorName;
                SkyTitleDescription.Text = skyColor.ColorDescription;
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
                }
            }
            catch (Exception ex)
            {
                debugText.Text = ex.Message + Environment.NewLine + ex.StackTrace;
            }
            
        }

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("测试点击");
            string uid = ((Button)sender).Uid;
            if (uid.Equals("TS3N0"))
            {
            }
            else if (uid.Equals("TS3N1"))
            {
            }
            else if (uid.Equals("TS3N2"))
            {
            }
            else if (uid.Equals("TS3N3"))
            {
            }
            else if (uid.Equals("TS3N4"))
            {
            }
            else
            {
            }
        }

        private void Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("测试点击");
        }
    }
}
