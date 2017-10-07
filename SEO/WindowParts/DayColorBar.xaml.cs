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
    public delegate void OnColorChanged(object sender, ColorBarArgs e);

    public class ColorBarArgs : EventArgs
    {
        public readonly bool ChangeAll;
        public readonly int ChangedIndex;
        public readonly System.Drawing.Color OldColor;
        public readonly System.Drawing.Color NewColor;

        public ColorBarArgs(int index, System.Drawing.Color oldC, System.Drawing.Color newC)
        {
            this.ChangeAll = false;
            this.ChangedIndex = index;
            this.OldColor = oldC;
            this.NewColor = newC;
        }
    }

    /// <summary>
    /// DayColorBar.xaml 的交互逻辑
    /// </summary>
    public partial class DayColorBar : UserControl, ILanguage, IDisposable
    {
        public event OnColorChanged ColorChanged;

        public DayColorBar()
        {
            InitializeComponent();
            Seo.Language.Register(this, Priority.High, false);
        }

        public void Dispose()
        {
            Seo.Language.UnRegister(this);
        }

        /// <summary>
        /// 表示所有的颜色矩形条
        /// </summary>
        private List<Rectangle> ColorRectList = new List<Rectangle>();

        #region OneDayColor
        private DayColor oneDayColor;
        /// <summary>
        /// 获取或者设置颜色拾取条的预设颜色条
        /// </summary>
        public DayColor OneDayColor
        {
            get { return oneDayColor; }
            set { oneDayColor = value; ColorList = value.ColorList; Draw(); }
        }
        List<TimeColor> colorList;
        /// <summary>
        /// 获取颜色条中的所有颜色时间列表
        /// </summary>
        public List<TimeColor> ColorList
        {
            get { return colorList; }
            private set { colorList = value; }
        }
        #endregion

        #region DayColorGroup
        private SkyColor dayColorGroup;
        /// <summary>
        /// 获取或者设置颜色拾取条的预设颜色条
        /// </summary>
        public SkyColor DayColorGroup
        {
            get { return dayColorGroup; }
            set { dayColorGroup = value; }
        }
        #endregion

        #region Title
        private string title;
        /// <summary>
        /// 设置色谱标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; TitleText.Text = value; }
        }
        #endregion

        #region Description
        private string description;
        /// <summary>
        /// 设置色谱描述
        /// </summary>
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                DescriptionText.Text = value;
            }
        }
        #endregion

        // 初始化
        Storyboard PickStory;
        DoubleAnimation MoveAnim;
        DoubleAnimation SizeAnim;
        ColorAnimation ColorAnim;
        Storyboard XPickStory;
        DoubleAnimation XMoveAnim;
        DoubleAnimation XSizeAnim;
        ColorAnimation XColorAnim;
        Storyboard ResetPickStory;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PickStory = FindResource("PickMoveStory") as Storyboard;
            MoveAnim = PickStory.Children[0] as DoubleAnimation;
            SizeAnim = PickStory.Children[1] as DoubleAnimation;
            ColorAnim = PickStory.Children[2] as ColorAnimation;
            XPickStory = FindResource("XPickMoveStory") as Storyboard;
            XMoveAnim = XPickStory.Children[0] as DoubleAnimation;
            XSizeAnim = XPickStory.Children[1] as DoubleAnimation;
            XColorAnim = XPickStory.Children[2] as ColorAnimation;
            ResetPickStory = FindResource("ResetPickMoveStory") as Storyboard;
        }

        public void LoadLanguage()
        {
            this.Title = OneDayColor.ColorName;
            this.Description = OneDayColor.ColorDescription;
        }

        public void Draw()
        {
            // 填充准备
            LinearGradientBrush BarBrush = new LinearGradientBrush();
            BarBrush.StartPoint = new Point(0, 0);
            BarBrush.EndPoint = new Point(1, 0);
            GradientStop gStop;
            Color cStop;
            Double oStop;   // o = offset
            // 数据准备
            List<TimeColor> ColorList = OneDayColor.ColorList;
            int colorCount = ColorList.Count;
            // 颜色准备
            int last = colorCount - 1;
            cStop = Color.FromArgb(ColorList[last].A, ColorList[last].R, ColorList[last].G, ColorList[last].B);
            oStop = 0;
            gStop = new GradientStop(cStop, oStop);
            BarBrush.GradientStops.Add(gStop);
            oStop = (ColorList[last].TimeSpan - ColorList[last].TimeDaySpan) / 24 - 0.0001;
            gStop = new GradientStop(cStop, oStop);
            BarBrush.GradientStops.Add(gStop);
            for (int i = 0; i < ColorList.Count; i++)
            {
                cStop = Color.FromArgb(ColorList[i].A, ColorList[i].R, ColorList[i].G, ColorList[i].B);
                oStop = ColorList[i].TimeValue / 24;
                gStop = new GradientStop(cStop, oStop);
                BarBrush.GradientStops.Add(gStop);
                oStop = oStop - 0.0001 + ColorList[i].TimeDaySpan / 24;
                gStop = new GradientStop(cStop, oStop);
                BarBrush.GradientStops.Add(gStop);
            }
            // 设置背景色
            ColorPanel.Fill = BarBrush;
        }

        int HoverIndex = -1;
        int PickIndex = -1;
        bool IsHoverIndexChanged = true;
        static StringBuilder detailText;
        const string timeSep = " - ";
        const string colorSep = "  ";
        private void ColorPanel_MouseMove(object sender, MouseEventArgs e)
        {
            // 数据准备
            Point mousePoint = e.GetPosition(ColorPanel);
            double time = mousePoint.X / 20;
            double pickTime = -1.0;
            int pickIndex = 0;
            for (int i = 0; i < ColorList.Count; i++)
            {
                if (ColorList[i].TimeValue <= time) pickTime = ColorList[i].TimeValue;
                else { pickIndex = i; break; }
            }
            if (pickTime < 0) pickTime = ColorList[ColorList.Count - 1].TimeValue;
            if (HoverIndex == pickIndex) { IsHoverIndexChanged = false; }
            else { HoverIndex = pickIndex; PickIndex = pickIndex; IsHoverIndexChanged = true; }
            if (IsHoverIndexChanged)
            {
                int animIndex = GetListIndex(pickIndex);
                // 显示颜色条详情
                TimeColor tc = ColorList[animIndex];
                if (detailText == null) detailText = new StringBuilder();
                detailText.Remove(0, detailText.Length);
                detailText.Append(TimeToString(tc.TimeValue));
                detailText.Append(timeSep);
                detailText.Append(TimeToString(tc.TimeValue + tc.TimeSpan));
                detailText.Append(colorSep);
                try
                {
                    if (tc.ColorValue.Name.StartsWith("ff")) detailText.Append(tc.ColorValue.Name.ToUpper().Substring(2, 6));
                    else detailText.Append(tc.ColorValue.Name.ToLower());
                }
                catch { }
                ColorDetailText.Text = detailText.ToString();
                // 播放颜色条移动动画
                MoveAnim.To = ColorList[animIndex].TimeValue * 20;
                SizeAnim.To = ColorList[animIndex].TimeDaySpan * 20;
                ColorAnim.To = Color.FromArgb(ColorList[animIndex].A, ColorList[animIndex].R, ColorList[animIndex].G, ColorList[animIndex].B);
                // 如果时间段隔天, 则播放额外动画
                if (animIndex == ColorList.Count - 1)
                {
                    XMoveAnim.To = 0;
                    XSizeAnim.To = (ColorList[animIndex].TimeSpan - ColorList[animIndex].TimeDaySpan) * 20;
                    XColorAnim.To = Color.FromArgb(ColorList[animIndex].A, ColorList[animIndex].R, ColorList[animIndex].G, ColorList[animIndex].B);
                }
                else
                {
                    XMoveAnim.To = MoveAnim.To;
                    XSizeAnim.To = SizeAnim.To;
                    XColorAnim.To = ColorAnim.To;
                }
                BeginStoryboard(XPickStory);
                BeginStoryboard(PickStory);
            }
        }

        private string TimeToString(double t)
        {
            int h, m;
            h = (int)t;
            if (h > 24) h -= 24;
            m = (int)((t - (int)t) * 60);
            return h + ":" + m.ToString().PadLeft(2, '0');
        }

        private static System.Windows.Forms.ColorDialog _colorDialog;
        public static System.Windows.Forms.ColorDialog colorDialog
        {
            get
            {
                if (_colorDialog == null) _colorDialog = new System.Windows.Forms.ColorDialog();
                _colorDialog.AnyColor = true;
                _colorDialog.FullOpen = true;
                _colorDialog.AllowFullOpen = true;
                return _colorDialog;
            }
        }
        private void ColorPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int index = GetListIndex(PickIndex);
            colorDialog.CustomColors = Configs.CustomColors;
            colorDialog.Color = ColorList[index].ColorValue;
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // 保存当前动作以便撤消
                System.Drawing.Color oldColor;
                oldColor = ColorList[index].ColorValue;
                if (ColorChanged != null) ColorChanged(this, new ColorBarArgs(index, oldColor, colorDialog.Color));
                // 设置颜色
                setColor(index, colorDialog.Color);
            }
            Configs.CustomColors = colorDialog.CustomColors;
        }

        private void ColorPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            BeginStoryboard(ResetPickStory);
            ColorDetailText.Text = null;
            HoverIndex = -1;
            IsHoverIndexChanged = true;
        }

        private int GetListIndex(int pickIndex)
        {
            if (pickIndex == 0) return OneDayColor.ColorList.Count - 1;
            else return pickIndex - 1;
        }

        // 把一个颜色设置进颜色条
        private void setColor(int index, System.Drawing.Color color)
        {
            EnvironmentOperator.Instance.SetTimeColorValue(DayColorGroup, ColorList[index], color);
            Draw();
        }
    }
}
