using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Seo
{
    public class FilesDirs
    {
        private static string configFile = Environment.CurrentDirectory + @"\Config.ini";
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string ConfigFile { get { return configFile; } }

        private static string errorLogFile = Environment.CurrentDirectory + @"\Logs\ErrorLog.log";
        /// <summary>
        /// 错误报告文件路径
        /// </summary>
        public static string ErrorLogFile { get { return errorLogFile; } }

        private static string languageDirectory = Environment.CurrentDirectory + @"\Languages";
        /// <summary>
        /// 语言文件目录
        /// </summary>
        public static string LanguageDirectory { get { return languageDirectory; } }

        /// <summary>
        /// 从注册表获取Sims3的路径
        /// </summary>
        /// <returns></returns>
        public static string GetSimsDirectoryByRegistry()
        {
            string simsDirectory = null;
            // 从注册表获取路径
            RegistryKey key = Registry.LocalMachine;
            key = key.OpenSubKey("SOFTWARE", true);
            key = key.OpenSubKey("Sims", true);
            key = key.OpenSubKey("The Sims 3", true);
            simsDirectory = key.GetValue("Install Dir").ToString();
            // 如果路径不存在, 则抛出异常
            if (!Directory.Exists(simsDirectory)) throw new DirectoryNotFoundException();
            return simsDirectory;
        }
        /// <summary>
        /// 从配置文件获取Sims3的路径
        /// </summary>
        /// <returns></returns>
        public static string GetSimsDirectoryByConfigs()
        {
            return Configs.SimsFolder;
        }
    }
}
