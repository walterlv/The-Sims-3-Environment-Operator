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
using System.Windows.Media.Animation;

namespace TS3Sky
{
    /// <summary>
    /// ColorPickBar.xaml 的交互逻辑
    /// </summary>
    public partial class ColorPickBar : UserControl
    {
        public ColorPickBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 表示所有的颜色矩形条
        /// </summary>
        private List<Rectangle> colorRectList = new List<Rectangle>();

        #region 设置颜色条
        private DayColor colorBar;
        /// <summary>
        /// 获取或者设置颜色拾取条的预设颜色条
        /// </summary>
        public DayColor ColorBar
        {
            get
            {
                return colorBar;
            }
            set
            {
                colorBar = value;
                Refresh();
            }
        }
        #endregion

        #region 设置标题
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                titleText.Text = value;
            }
        }
        #endregion

        #region 设置描述
        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                descriptionText.Text = value;
                if (value.Equals(String.Empty)) descriptionText.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        public void Refresh()
        {
            colorPanel.Children.Clear();
            colorRectList.Clear();
            // 这里设置颜色条的各项颜色属性
            int colorCount = colorBar.ColorList.Count;
            Rectangle colorRect = new Rectangle();
            double timeSpan = colorBar.ColorList[0].TimeValue;
            colorRect.Width = timeSpan * 20;
            colorRect.Fill = new SolidColorBrush(Color.FromArgb(colorBar.ColorList[colorCount - 1].A,
                colorBar.ColorList[colorCount - 1].R, colorBar.ColorList[colorCount - 1].G, colorBar.ColorList[colorCount - 1].B));
            colorPanel.Children.Add(colorRect);
            colorRectList.Add(colorRect);
            for (int i = 0; i < colorCount; i++)
            {
                colorRect = new Rectangle();
                if (i < colorBar.ColorList.Count - 1) timeSpan = colorBar.ColorList[i + 1].TimeValue - colorBar.ColorList[i].TimeValue;
                else timeSpan = 24 - colorBar.ColorList[i].TimeValue;
                colorRect.Width = timeSpan * 20;
                colorRect.Fill = new SolidColorBrush(Color.FromArgb(colorBar.ColorList[i].A,
                    colorBar.ColorList[i].R, colorBar.ColorList[i].G, colorBar.ColorList[i].B));
                colorPanel.Children.Add(colorRect);
                colorRectList.Add(colorRect);
            }
        }

        private int CurrentPickIndex = -1;
        private bool PickIndexChanged = true;
        private void colorPanel_MouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            Point mousePosition = e.GetPosition(element);
            double time = mousePosition.X / 20;
            double pickTime = 0.0;
            int pickIndex = 0;
            for (int i = 0; i < colorBar.ColorList.Count; i++)
            {
                if (colorBar.ColorList[i].TimeValue <= time) pickTime = colorBar.ColorList[i].TimeValue;
                else { pickIndex = i; break; }
            }
            if (CurrentPickIndex == pickIndex) PickIndexChanged = false;
            else { CurrentPickIndex = pickIndex; PickIndexChanged = true; }
            pickColorRect.Fill = colorRectList[pickIndex].Fill;
            pickColorRect.Width = colorRectList[pickIndex].ActualWidth;
            colorDetailText.Text = String.Format("{0}: {1} - {2}, {3}: {4}",
                                                TS3Sky.Language.ColorPickBar.TimeInterval,
                                                doubleToTime(pickTime),
                                                doubleToTime(colorBar.ColorList[pickIndex].TimeValue),
                                                TS3Sky.Language.ColorPickBar.ColorValue,
                                                brushToRGB(pickColorRect.Fill));

            if (PickIndexChanged)
            {
                #region 这里隐藏未实现的动画
                // 这里执行颜色条滚动的动画
                //Storyboard storyboard = new Storyboard();//新建一个动画板  
                //pickColorRect.RenderTransform = new TranslateTransform();
                ////添加平移运动的动画
                //DoubleAnimation doubleAnimation = new DoubleAnimation();
                //doubleAnimation.To = colorRectList[pickIndex].Width;
                //doubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                //Storyboard.SetTarget(doubleAnimation, pickColorRect);
                //Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
                //storyboard.Children.Add(doubleAnimation);
                ////添加缩放运动的动画  
                //doubleAnimation = new DoubleAnimation();
                //doubleAnimation.To = colorRectList[pickIndex].ActualWidth;
                //doubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                //Storyboard.SetTargetName(doubleAnimation, pickColorRect.Name);
                //Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Rectangle.WidthProperty));
                //storyboard.Children.Add(doubleAnimation);

                //if (!Resources.Contains("pickColorRect"))
                //{
                //    Resources.Add("pickColorRect", storyboard);
                //}

                //storyboard.Begin();//开始运行动画
                #endregion
            }
        }
        /// <summary>
        /// 鼠标移出时应该恢复原始外观
        /// </summary>
        private void colorPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            pickColorRect.Fill = Brushes.Transparent;
            colorDetailText.Text = String.Empty;
        }
        /// <summary>
        /// 点击颜色条时要选择颜色
        /// </summary>
        private void colorPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
            System.Windows.Interop.HwndSource source = PresentationSource.FromVisual(this) as System.Windows.Interop.HwndSource;
            System.Windows.Forms.IWin32Window win = new OldWindow(source.Handle);
            cd.Color = colorBar.ColorList[CurrentPickIndex].ColorValue;
            if (cd.ShowDialog(win) == System.Windows.Forms.DialogResult.OK)
            {
                if (CurrentPickIndex == 0)
                {
                    colorBar.ColorList[colorBar.ColorList.Count - 1].ColorValue = cd.Color;
                    colorRectList[0].Fill = new SolidColorBrush(Color.FromArgb(colorBar.ColorList[colorBar.ColorList.Count - 1].A,
                        colorBar.ColorList[colorBar.ColorList.Count - 1].R, colorBar.ColorList[colorBar.ColorList.Count - 1].G,
                        colorBar.ColorList[colorBar.ColorList.Count - 1].B));
                    colorRectList[colorRectList.Count - 1].Fill = colorRectList[0].Fill;
                }
                else
                {
                    colorBar.ColorList[CurrentPickIndex - 1].ColorValue = cd.Color;
                    colorRectList[CurrentPickIndex].Fill = new SolidColorBrush(Color.FromArgb(colorBar.ColorList[CurrentPickIndex - 1].A,
                        colorBar.ColorList[CurrentPickIndex - 1].R, colorBar.ColorList[CurrentPickIndex - 1].G, colorBar.ColorList[CurrentPickIndex - 1].B));
                }
                pickColorRect.Fill = Brushes.Transparent;
            }
        }
        private string doubleToTime(double time)
        {
            string timeString;
            int hour = (int)time;
            int minute = (int)((time - hour) * 60);
            if (hour >= 24) timeString = String.Format(TS3Sky.Language.ColorPickBar.TheNextDay);
            else if (minute < 10) timeString = hour + ":0" + minute;
            else timeString = hour + ":" + minute;
            return timeString;
        }
        private string brushToRGB(Brush brush)
        {
            return "" + brush.ToString().Substring(3, 6);
        }
    }

    public class OldWindow : System.Windows.Forms.IWin32Window
    {
        IntPtr _handle;
        public OldWindow(IntPtr handle)
        {
            _handle = handle;
        }
        #region IWin32Window Members
        IntPtr System.Windows.Forms.IWin32Window.Handle
        {
            get { return _handle; }
        }
        #endregion
    }
}
