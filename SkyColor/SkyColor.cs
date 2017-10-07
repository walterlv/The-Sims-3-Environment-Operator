using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Win32;

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

        /// <summary>
        /// 一天中的所有颜色的种类
        /// </summary>
        public List<DayColor> DayColors = new List<DayColor>();

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
                    skyColor.colorName = "天空";
                    skyColor.colorDescription = "改变天空的颜色：太阳、月亮、天空、地平线。";
                    break;
                case ColorAssembly.Sky_ClearSea:
                    skyColor = new Sky_ClearSea();
                    skyColor.colorName = "海洋";
                    skyColor.colorDescription = "改变晴天时海洋的颜色。";
                    break;
                case ColorAssembly.Sky_OvercastSea:
                    skyColor = new Sky_OvercastSea();
                    skyColor.colorName = "海洋（阴天）";
                    skyColor.colorDescription = "当乌云密布不见日月，这时候的海洋颜色就在这里设置。";
                    break;
                default:
                    return null;
            }
            skyColor.colorType = colorName;
            skyColor.colorPath = skyColor.getColorPath(colorName);
            if (skyColor.colorPath == null) return null;
            foreach (DayColor color in skyColor.DayColors)
            {
                BasicColor temp;
                int i = 1;
                while (true)
                {
                    temp = skyColor.ReadColor(color.ColorSection, i);
                    i++;
                    if (temp.IsValid) color.ColorList.Add(temp);
                    else break;
                }
            }
            skyColor.isRead = true;
            return skyColor;
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
            int red = iniFile.ReadInteger(colorSection + index, "red", 0);
            int green = iniFile.ReadInteger(colorSection + index, "green", 0);
            int blue = iniFile.ReadInteger(colorSection + index, "blue", 0);
            double timeOfDay = iniFile.ReadDouble(colorSection + index, "timeOfDay", -1.0);
            BasicColor color = new BasicColor(red, green, blue, timeOfDay);
            if (timeOfDay < 0) color.IsValid = false;
            return color;
        }
    }
}