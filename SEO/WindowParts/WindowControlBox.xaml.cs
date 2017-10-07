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

namespace Seo.WindowParts
{
    /// <summary>
    /// 窗口按钮被点击时的委托
    /// </summary>
    public delegate void WindowBox_Click(object sender, WindowBoxArgs e);
    /// <summary>
    /// 表示窗口按钮的所有种类
    /// </summary>
    public enum WindowBox
    {
        MaximizeBox,
        MinimizeBox,
        CloseBox
    }
    /// <summary>
    /// 表示窗口按钮被点击时是哪个按钮被点击了
    /// </summary>
    public class WindowBoxArgs : EventArgs
    {
        public readonly WindowBox Box;
        public WindowBoxArgs(WindowBox box)
        {
            this.Box = box;
        }
    }

    /// <summary>
    /// WindowControlBox.xaml 的交互逻辑
    /// </summary>
    public partial class WindowControlBox : UserControl
    {
        public WindowControlBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当窗口控制按钮被单击时发生
        /// </summary>
        public event WindowBox_Click OnWindowBox_Click;

        public Window handleWindow;
        /// <summary>
        /// 获取或设置控制按钮自动控制的窗口
        /// </summary>
        public Window AutoHandleWindow
        {
            get { return handleWindow; }
            set { handleWindow = value; }
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            if (AutoHandleWindow != null) AutoHandleWindow.WindowState = WindowState.Minimized;
            if (OnWindowBox_Click != null) OnWindowBox_Click(this, new WindowBoxArgs(WindowBox.MinimizeBox));
        }

        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            if (AutoHandleWindow != null)
            {
                if (AutoHandleWindow.WindowState == WindowState.Normal) AutoHandleWindow.WindowState = WindowState.Maximized;
                else if (AutoHandleWindow.WindowState == WindowState.Maximized) AutoHandleWindow.WindowState = WindowState.Normal;
            }
            if (OnWindowBox_Click != null) OnWindowBox_Click(this, new WindowBoxArgs(WindowBox.MaximizeBox));
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (AutoHandleWindow != null) AutoHandleWindow.Close();
            if (OnWindowBox_Click != null) OnWindowBox_Click(this, new WindowBoxArgs(WindowBox.CloseBox));
        }
    }
}
