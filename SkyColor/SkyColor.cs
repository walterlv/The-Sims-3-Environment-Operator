using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Win32;
using System.IO;

namespace TS3Sky
{
    public enum ColorAssembly
    {
        Sky_Clear1,
        Sky_Clear2,
        Sky_ClearLight,
        Sky_ClearSea,
        Sky_ClearSky
    }

    public class SkyColor
    {
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
        private String colorPath;
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

        public static SkyColor FromColorAssembly(ColorAssembly colorName)
        {
            SkyColor skyColor;
            switch (colorName)
            {
                case ColorAssembly.Sky_Clear1:
                    skyColor = new Sky_Clear1();
                    break;
                case ColorAssembly.Sky_Clear2:
                    skyColor = new Sky_Clear2();
                    break;
                case ColorAssembly.Sky_ClearLight:
                    skyColor = new Sky_ClearLight();
                    break;
                case ColorAssembly.Sky_ClearSea:
                    skyColor = new Sky_ClearSea();
                    break;
                case ColorAssembly.Sky_ClearSky:
                    skyColor = new Sky_ClearSky();
                    break;
                default:
                    return null;
            }
            skyColor.colorType = colorName;
            skyColor.colorPath = skyColor.getColorPath(colorName);
            if (skyColor.colorPath == null) return null;
            skyColor.Read();
            return skyColor;
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
            foreach (DayColor color in DayColors)
            {
                for (int i = 0; i < color.ColorList.Count; i++)
	            {
		            SaveColor(color.ColorSection, i + 1, color.ColorList[i]);
	            }
            }
            // 修改晴天出现的概率为非常大，以确保修改后又很大概率能生效
            if (colorType == ColorAssembly.Sky_ClearSky)
            {
                IniFiles iniFile = new IniFiles(colorPath);
                iniFile.WriteDouble("MiscParams", "ProbabilityWeight", 100);
                iniFile.UpdateFile();
            }
        }

        public void Revoke()
        {
            Read();
        }

        public void SetToDefault()
        {
            File.Copy(Environment.CurrentDirectory + @"\backups\" + colorType + ".ini", colorPath, true);
        }

        private string getColorPath(ColorAssembly colorName)
        { 
            string simsPath;
            string iniPath;
            RegistryKey key = Registry.LocalMachine;
            key = key.OpenSubKey("SOFTWARE",true);
            key = key.OpenSubKey("Sims",true);
            key = key.OpenSubKey("The Sims 3",true);
            simsPath = key.GetValue("Install Dir").ToString();
            if (simsPath.EndsWith("\\")) simsPath.Substring(0, simsPath.Length - 1);
            iniPath = @"\GameData\Shared\NonPackaged\Ini\" + colorFileName + ".ini";
            return simsPath + iniPath;
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
        }
    }
}