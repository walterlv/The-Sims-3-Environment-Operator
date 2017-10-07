using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Seo
{
    public class TimeColor
    {
        /// <summary>
        /// 颜色时间对是否合法
        /// </summary>
        public bool IsValid { get; internal set; }

        /// <summary>
        /// 获取一个颜色值
        /// </summary>
        public Color ColorValue { get; internal set; }

        /// <summary>
        /// 获取这个颜色值对应的时间起点
        /// </summary>
        public double TimeValue { get; internal set; }

        /// <summary>
        /// 获取这个时间点相对于1的偏移
        /// </summary>
        public double TimeOffset { get; internal set; }
        /// <summary>
        /// 获取这个时间点可以持续的时间长度
        /// </summary>
        public double TimeSpan { get; internal set; }
        /// <summary>
        /// 获取这个时间点在一天的范围内可以持续的时间长度
        /// </summary>
        public double TimeDaySpan { get; internal set; }

        /// <summary>
        /// 创建一个黑色, 零点的颜色时间对
        /// </summary>
        public TimeColor()
            : this(Color.Black, 0.0)
        { }
        /// <summary>
        /// 创建一个有规定颜色和时间的颜色时间对
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="time">时间</param>
        public TimeColor(Color color, double time)
        {
            this.ColorValue = color;
            this.TimeValue = time;
            this.TimeOffset = time / 24;
            if (this.TimeValue < 0 || this.TimeValue > 24) this.IsValid = false;
            else this.IsValid = true;
        }
        /// <summary>
        /// 创建一个有规定颜色和时间的颜色时间对
        /// </summary>
        /// <param name="red">红</param>
        /// <param name="green">绿</param>
        /// <param name="blue">蓝</param>
        /// <param name="time">时间</param>
        public TimeColor(int red, int green, int blue, double time)
            : this(Color.FromArgb(red, green, blue), time)
        { }

        public byte A { get { return ColorValue.A; } }
        public byte R { get { return ColorValue.R; } }
        public byte G { get { return ColorValue.G; } }
        public byte B { get { return ColorValue.B; } }

        public override string ToString()
        {
            return "(" + R + ", " + G + ", " + B + ")";
        }
    }
}
