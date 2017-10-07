using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Seo
{
    public enum ColorAssemblies
    {
        Sky_1,
        Sky_2,
        Sky_Light,
        Sky_Sea,
        Sky_Sky
    }

    public class SkyColor
    {
        #region IsReady
        private bool isReady = false;
        /// <summary>
        /// 获取颜色组对象是否已经正确读取
        /// </summary>
        public bool IsReady
        {
            get { return isReady; }
            private set { isReady = value; }
        }
        #endregion

        #region ColorName
        private string colorName;
        /// <summary>
        /// 获取颜色组的名字
        /// </summary>
        public string ColorName
        {
            get { return colorName; }
            internal set { colorName = value; }
        }
        #endregion

        #region ColorDescription
        private string colorDescription;
        /// <summary>
        /// 获取颜色组的描述
        /// </summary>
        public string ColorDescription
        {
            get { return colorDescription; }
            internal set { colorDescription = value; }
        }
        #endregion

        #region ColorAssembly
        private ColorAssemblies colorAssembly;
        /// <summary>
        /// 颜色组的类型 (与文件名一一对应)
        /// </summary>
        public ColorAssemblies ColorAssembly
        {
            get { return colorAssembly; }
            internal set { colorAssembly = value; }
        }
        #endregion

        #region ColorFileName
        private string colorFileName;
        /// <summary>
        /// 颜色配置文件的名称 (与颜色组类型一一对应)
        /// </summary>
        public string ColorFileName
        {
            get { return colorFileName; }
            internal set { colorFileName = value; }
        }
        #endregion

        #region ColorFile
        private string colorFile;
        /// <summary>
        /// 颜色配置文件的完全限定路径
        /// </summary>
        public String ColorFile
        {
            get { return colorFile; }
            internal set { colorFile = value; }
        }
        #endregion

        #region DayColors
        /// <summary>
        /// 一天中的所有颜色的种类
        /// </summary>
        public List<DayColor> DayColors = new List<DayColor>();
        #endregion

        /// <summary>
        /// 五种颜色组分别列出的数组
        /// </summary>
        public static readonly ColorAssemblies[] ColorAssemblyArray = new ColorAssemblies[5] { ColorAssemblies.Sky_Sky, ColorAssemblies.Sky_Light, ColorAssemblies.Sky_1, ColorAssemblies.Sky_2, ColorAssemblies.Sky_Sea };

        #region 构造方法
        public SkyColor() { }
        private bool isError = false;
        public bool IsError
        {
            get { return isError; }
            private set { isError = value; }
        }
        internal SkyColor(bool error, ColorAssemblies colorAssembly)
        {
            if (error) this.IsError = true;
            else throw new Exception("Only error SkyColor object can be created");
            this.ColorAssembly = colorAssembly;
        }
        #endregion

        /// <summary>
        /// 从颜色类型创建颜色组对象
        /// </summary>
        /// <param name="colorName">颜色类型</param>
        /// <param name="weather">在特定天气下才有颜色组对象</param>
        /// <returns></returns>
        internal static SkyColor FromColorAssembly(ColorAssemblies colorAssembly, Weathers weather)
        {
            SkyColor skyColor;
            switch (colorAssembly)
            {
                case ColorAssemblies.Sky_1:
                    skyColor = new Sky_1();
                    skyColor.ColorFileName = String.Format("Sky_{0}1", Weather.WeatherToName(weather));
                    break;
                case ColorAssemblies.Sky_2:
                    skyColor = new Sky_2();
                    skyColor.ColorFileName = String.Format("Sky_{0}2", Weather.WeatherToName(weather));
                    break;
                case ColorAssemblies.Sky_Light:
                    skyColor = new Sky_Light();
                    skyColor.ColorFileName = String.Format("Sky_{0}Light", Weather.WeatherToName(weather));
                    break;
                case ColorAssemblies.Sky_Sea:
                    skyColor = new Sky_Sea();
                    skyColor.ColorFileName = String.Format("Sky_{0}Sea", Weather.WeatherToName(weather));
                    break;
                case ColorAssemblies.Sky_Sky:
                    skyColor = new Sky_Sky();
                    skyColor.ColorFileName = String.Format("Sky_{0}Sky", Weather.WeatherToName(weather));
                    break;
                default:
                    return null;
            }
            skyColor.ColorAssembly = colorAssembly;
            skyColor.ColorFile = EnvironmentOperator.Instance.WorkDirectory + "\\" + skyColor.ColorFileName + ".ini";
            if (!File.Exists(skyColor.ColorFile)) skyColor = new SkyColor(true, colorAssembly);
            return skyColor;
        }

        public void Prepare()
        {
            Read();
        }

        internal void Read()
        {
            foreach (DayColor color in DayColors)
            {
                // 读取颜色组
                TimeColor temp;
                int i = 1;
                color.ColorList.Clear();
                IniFiles reader = new IniFiles(ColorFile);
                while (true)
                {
                    temp = ReadColor(reader, color.ColorSection, i);
                    i++;
                    if (temp.IsValid) color.ColorList.Add(temp);
                    else break;
                }
                // 排序颜色组
                for (i = 0; i < color.ColorList.Count; i++)
                {
                    for (int j = i + 1; j < color.ColorList.Count; j++)
                    {
                        if (color.ColorList[i].TimeValue > color.ColorList[j].TimeValue)
                        {
                            temp = color.ColorList[i];
                            color.ColorList[i] = color.ColorList[j];
                            color.ColorList[j] = temp;
                        }
                    }
                }
                // 设置时间段
                for (i = 1; i < color.ColorList.Count; i++)
                {
                    color.ColorList[i - 1].TimeSpan = color.ColorList[i].TimeValue - color.ColorList[i - 1].TimeValue;
                    color.ColorList[i - 1].TimeDaySpan = color.ColorList[i - 1].TimeSpan;
                }
                i--;
                color.ColorList[i].TimeDaySpan = 24 - color.ColorList[i].TimeValue;
                color.ColorList[i].TimeSpan = color.ColorList[i].TimeDaySpan + color.ColorList[0].TimeValue;
            }
            // 读取完成
            IsReady = true;
        }

        internal TimeColor ReadColor(IniFiles reader, string colorSection, int index)
        {
            string section = colorSection + index;
            int red = reader.ReadInteger(section, "red", 0);
            if (red < 0) red = 0;
            else if (red > 255) red = 255;
            int green = reader.ReadInteger(section, "green", 0);
            if (green < 0) green = 0;
            else if (green > 255) green = 255;
            int blue = reader.ReadInteger(section, "blue", 0);
            if (blue < 0) blue = 0;
            else if (blue > 255) blue = 255;
            double timeOfDay = reader.ReadDouble(section, "timeOfDay", -1.0);
            TimeColor color = new TimeColor(red, green, blue, timeOfDay);
            return color;
        }

        private static string ColorAssemblyToName(ColorAssemblies color)
        {
            return Enum.GetName(typeof(ColorAssemblies), color);
        }

        public static string ColorAssemblyToString(ColorAssemblies color)
        {
            switch (color)
            {
                case ColorAssemblies.Sky_1:
                    return Seo.Languages.Sky_1.DayColorName;
                case ColorAssemblies.Sky_2:
                    return Seo.Languages.Sky_2.DayColorName;
                case ColorAssemblies.Sky_Light:
                    return Seo.Languages.Sky_Light.DayColorName;
                case ColorAssemblies.Sky_Sea:
                    return Seo.Languages.Sky_Sea.DayColorName;
                case ColorAssemblies.Sky_Sky:
                    return Seo.Languages.Sky_Sky.DayColorName;
                default:
                    return null;
            }
        }
    }
}
