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

        public SkyColor()
        {
        }

        public bool ReadColor(ColorAssembly colorName)
        {
            colorPath = getColorPath(colorName);
            return false;
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
            switch (colorName)
            {
                case ColorAssembly.Sky_CustomSky:
                    iniPath = @"GameData\Shared\NonPackaged\Ini\Sky_CustomSky.ini";
                    break;
                case ColorAssembly.Sky_ClearSea:
                    iniPath = @"GameData\Shared\NonPackaged\Ini\Sky_ClearSea.ini";
                    break;
                case ColorAssembly.Sky_OvercastSea:
                    iniPath = @"GameData\Shared\NonPackaged\Ini\Sky_OvercastSea.ini";
                    break;
                default:
                    return null;
            }
            return simsPath + iniPath;
        }
    }
}