using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo
{
    public class DayColor
    {
        /// <summary>
        /// 获取或设置颜色区段
        /// </summary>
        public string ColorSection { get; internal set; }

        /// <summary>
        /// 获取或设置颜色名字 (比如太阳的颜色, 光晕的颜色.)
        /// </summary>
        public string ColorName { get; internal set; }

        /// <summary>
        /// 获取或设置颜色的描述
        /// </summary>
        public string ColorDescription { get; internal set; }

        /// <summary>
        /// 获取一个可以操作所有相关颜色的颜色列表
        /// </summary>
        public List<TimeColor> ColorList { get; internal set; }

        /// <summary>
        /// 创建一个表示所有相关颜色的颜色列表
        /// </summary>
        /// <param name="colorSection">颜色名(类型)</param>
        internal DayColor(string colorSection)
        {
            ColorSection = colorSection;
            ColorList = new List<TimeColor>();
        }
    }
}
