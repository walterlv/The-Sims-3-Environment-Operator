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
        Sky_CustomSky,
        Sky_ClearSea,
        Sky_OvercastSea
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
            private set
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
            private set
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
                case ColorAssembly.Sky_CustomSky:
                    skyColor = new Sky_CustomSky();
                    skyColor.colorName = TS3Sky.Language.Sky_CustomSky.DayColorName;
                    skyColor.colorDescription = TS3Sky.Language.Sky_CustomSky.DayColorDescription;
                    break;
                case ColorAssembly.Sky_ClearSea:
                    skyColor = new Sky_ClearSea();
                    skyColor.colorName = TS3Sky.Language.Sky_ClearSea.DayColorName;
                    skyColor.colorDescription = TS3Sky.Language.Sky_ClearSea.DayColorDescription;
                    break;
                case ColorAssembly.Sky_OvercastSea:
                    skyColor = new Sky_OvercastSea();
                    skyColor.colorName = TS3Sky.Language.Sky_OvercastSea.DayColorName;
                    skyColor.colorDescription = TS3Sky.Language.Sky_OvercastSea.DayColorDescription;
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
            switch (colorName)
            {
                case ColorAssembly.Sky_CustomSky:
                    iniPath = @"\GameData\Shared\NonPackaged\Ini\Sky_CustomSky.ini";
                    break;
                case ColorAssembly.Sky_ClearSea:
                    iniPath = @"\GameData\Shared\NonPackaged\Ini\Sky_ClearSea.ini";
                    break;
                case ColorAssembly.Sky_OvercastSea:
                    iniPath = @"\GameData\Shared\NonPackaged\Ini\Sky_OvercastSea.ini";
                    break;
                default:
                    return null;
            }
            return simsPath + iniPath;
        }

        protected BasicColor ReadColor(string colorSection, int index)
        {
            IniFiles iniFile = new IniFiles(colorPath);
            string section = colorSection + index;
            int red = iniFile.ReadInteger(section, "red", 0);
            int green = iniFile.ReadInteger(section, "green", 0);
            int blue = iniFile.ReadInteger(section, "blue", 0);
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