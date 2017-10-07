using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace TS3Sky
{
    public class SkyColor
    {
        /// <summary>
        /// 指示颜色配置自上次保存后是否已经被修改
        /// </summary>
        public bool Modified = false;

        #region 获取或设置颜色组的名字
        private string colorName;
        /// <summary>
        /// 获取或设置颜色组的名字
        /// </summary>
        public string ColorName
        {
            get
            {
                return colorName;
            }
            protected set
            {
                colorName = value;
            }
        }
        #endregion

        #region 获取或设置颜色组的描述
        private string colorDescription;
        /// <summary>
        /// 获取或设置颜色组的描述
        /// </summary>
        public string ColorDescription
        {
            get
            {
                return colorDescription;
            }
            protected set
            {
                colorDescription = value;
            }
        }
        #endregion

        #region 是否已经成功读取了颜色配置文件
        private bool isRead = false;
        /// <summary>
        /// 是否已经成功读取了颜色配置文件
        /// </summary>
        public bool IsRead
        {
            get
            {
                return isRead;
            }
            private set
            {
                isRead = value;
            }
        }
        #endregion

        #region 颜色配置文件的名称 (即颜色类型)
        private ColorAssembly colorType;
        /// <summary>
        /// 颜色配置文件的名称 (即颜色类型)
        /// </summary>
        public ColorAssembly ColorType
        {
            get
            {
                return colorType;
            }
            private set
            {
                colorType = value;
            }
        }
        #endregion

        #region 颜色配置文件的名称
        private string colorFileName;
        /// <summary>
        /// 颜色配置文件的名称 (即颜色类型)
        /// </summary>
        public string ColorFileName
        {
            get
            {
                return colorFileName;
            }
            protected set
            {
                colorFileName = value;
            }
        }
        #endregion

        #region 颜色配置文件的路径
        private string colorPath;
        /// <summary>
        /// 颜色配置文件的路径
        /// </summary>
        public String ColorPath
        {
            get
            {
                return colorPath;
            }
            private set
            {
                colorPath = value;
            }
        }
        #endregion


        #region 一天中的所有颜色的种类
        /// <summary>
        /// 一天中的所有颜色的种类
        /// </summary>
        public List<DayColor> DayColors = new List<DayColor>();
        #endregion

        protected SkyColor()
        {
        }

        public static SkyColor FromColorAssembly(ColorAssembly colorName, Weather weather)
        {
            SkyColor skyColor;
            switch (colorName)
            {
                case ColorAssembly.Sky_1:
                    skyColor = new Sky_1();
                    skyColor.colorFileName = String.Format("Sky_{0}1", WeatherSky.GetWeatherString(weather));
                    break;
                case ColorAssembly.Sky_2:
                    skyColor = new Sky_2();
                    skyColor.colorFileName = String.Format("Sky_{0}2", WeatherSky.GetWeatherString(weather));
                    break;
                case ColorAssembly.Sky_Light:
                    skyColor = new Sky_Light();
                    skyColor.colorFileName = String.Format("Sky_{0}Light", WeatherSky.GetWeatherString(weather));
                    break;
                case ColorAssembly.Sky_Sea:
                    skyColor = new Sky_Sea();
                    skyColor.colorFileName = String.Format("Sky_{0}Sea", WeatherSky.GetWeatherString(weather));
                    break;
                case ColorAssembly.Sky_Sky:
                    skyColor = new Sky_Sky();
                    skyColor.colorFileName = String.Format("Sky_{0}Sky", WeatherSky.GetWeatherString(weather));
                    break;
                default:
                    return null;
            }
            skyColor.colorType = colorName;
            skyColor.colorPath = WeatherSky.ColorDirectory + "\\" + skyColor.ColorFileName + ".ini";
            if (skyColor.colorPath == null) return null;
            skyColor.Read();
            return skyColor;
        }

        public static void ChangeWeather(Weather newWeather)
        {
            foreach (SkyColor skyColor in WeatherSky.GetWeatherByName(newWeather).SkyColors)
            {
                skyColor.colorFileName = String.Format("Sky_{0}Sky", WeatherSky.GetWeatherString(newWeather));
            }
        }

        protected void Read()
        {
            foreach (DayColor color in DayColors)
            {
                // 读取颜色组
                BasicColor temp;
                int i = 1;
                color.ColorList.Clear();
                while (true)
                {
                    temp = ReadColor(color.ColorSection, i);
                    i++;
                    if (temp.IsValid) color.ColorList.Add(temp);
                    else break;
                }
                // 排序颜色组
                for (i = 0; i < color.ColorList.Count; i++)
                {
                    for (int j = i; j < color.ColorList.Count; j++)
                    {
                        if (color.ColorList[i].TimeValue > color.ColorList[j].TimeValue)
                        {
                            temp = color.ColorList[i];
                            color.ColorList[i] = color.ColorList[j];
                            color.ColorList[j] = temp;
                        }
                    }
                }
            }
            isRead = true;
        }

        public void Save()
        {
            // 保存颜色配置文件
            foreach (DayColor color in DayColors)
            {
                for (int i = 0; i < color.ColorList.Count; i++)
	            {
		            SaveColor(color.ColorSection, i + 1, color.ColorList[i]);
	            }
            }
            // 确保颜色配置文件后面没有额外的颜色
            IniFiles iniFile = new IniFiles(colorPath);
            if (iniFile.ValueExists(DayColors[DayColors.Count - 1].ColorSection, "red"))
            {
                iniFile.EraseSection(DayColors[DayColors.Count - 1].ColorSection);
            }
            iniFile.UpdateFile();
        }

        public void Reread()
        {
            Read();
        }

        public void SetToDefault()
        {
            File.Copy(Package.CachePath + @"\0000-0000-0000-0000\" + colorType + ".ini", colorPath, true);
        }


        protected BasicColor ReadColor(string colorSection, int index)
        {
            IniFiles iniFile = new IniFiles(colorPath);
            string section = colorSection + index;
            int red = iniFile.ReadInteger(section, "red", 0);
            if (red < 0) red = 0;
            else if (red > 255) red = 255;
            int green = iniFile.ReadInteger(section, "green", 0);
            if (green < 0) green = 0;
            else if (green > 255) green = 255;
            int blue = iniFile.ReadInteger(section, "blue", 0);
            if (blue < 0) blue = 0;
            else if (blue > 255) blue = 255;
            double timeOfDay = iniFile.ReadDouble(section, "timeOfDay", -1.0);
            BasicColor color = new BasicColor(red, green, blue, timeOfDay);
            if (timeOfDay < 0) color.IsValid = false;
            return color;
        }
        protected void SaveColor(string colorSection, int index, BasicColor color)
        {
            IniFiles iniFile = new IniFiles(colorPath);
            string section = colorSection + index;
            iniFile.WriteInteger(section, "red", color.R);
            iniFile.WriteInteger(section, "green", color.G);
            iniFile.WriteInteger(section, "blue", color.B);
            iniFile.WriteDouble(section, "timeOfDay", color.TimeValue);
            iniFile.UpdateFile();
            Modified = false;
        }
    }
}