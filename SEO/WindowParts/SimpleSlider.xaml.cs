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

namespace Seo.WindowParts
{
    public delegate void OnValueChaged(object sender, SliderValueArgs e);

    public class SliderValueArgs : EventArgs
    {
        /// <summary>
        /// 0~1之间的值.
        /// </summary>
        public readonly double Value;
        /// <summary>
        /// 获取此次值改变是否是手动改变 (反之由代码改变)
        /// </summary>
        public readonly bool IsManual;
        public SliderValueArgs(double value, bool isManual)
        {
            Value = value;
            IsManual = isManual;
        }
    }

    /// <summary>
    /// SimpleSlider.xaml 的交互逻辑
    /// </summary>
    public partial class SimpleSlider : UserControl
    {
        public SimpleSlider()
        {
            InitializeComponent();
        }

        #region 设置或动画设置属性
        Storyboard progStory = null;
        Storyboard ProgStory
        {
            get
            {
                if (progStory == null) progStory = FindResource("ProgressStory") as Storyboard;
                return progStory;
            }
        }
        DoubleAnimation progAnim = null;
        DoubleAnimation ProgAnim
        {
            get
            {
                if (progAnim == null) progAnim = ProgStory.Children[0] as DoubleAnimation;
                return progAnim;
            }
        }

        public Brush ForeBrush
        {
            get { return ShowEle.Fill; }
            set { ShowEle.Fill = value; }
        }

        private double progValue = 0;
        public event OnValueChaged ValueChanged;
        public double Value
        {
            get { return progValue; }
            set { ChangeValue(value, false); }
        }
        private double ManualValue
        {
            get { return progValue; }
            set { ChangeValue(value, true); }
        }
        private void ChangeValue(double val, bool man)
        {
            if (val > 0.995) val = 1;
            else if (val < 0.005) val = 0;
            progValue = val; ProgAnim.To = val * BackEle.ActualWidth; BeginStoryboard(ProgStory);
            if (ValueChanged != null) ValueChanged(this, new SliderValueArgs(val, man));
        }
        public void Redraw()
        {
            this.Value = progValue;
        }
        #endregion

        #region 操作滚动条以达到拖动改变值的效果
        private bool IsPressing = false;
        private void ShowEle_MouseEnter(object sender, MouseEventArgs e)
        { }
        private void ShowEle_MouseLeave(object sender, MouseEventArgs e)
        { IsPressing = false; }
        private void ShowEle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        { IsPressing = false; }
        private void ShowEle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsPressing = true;
            Point point = e.GetPosition(sender as Grid);
            this.ManualValue = point.X / BackEle.ActualWidth;
        }
        private void ShowEle_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsPressing)
            {
                Point point = e.GetPosition(sender as Grid);
                this.ManualValue = point.X / BackEle.ActualWidth;
            }
        }
        private void ShowEle_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.ManualValue += e.Delta / 12000;
        }
        #endregion
    }
}
