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
        /// <summary>
        /// 获取或设置模拟人生3根目录
        /// </summary>
        public static string SimsFolder { get; set; }

        /// <summary>
        /// 获取程序漫游路径
        /// </summary>
        public static readonly string AppDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Seo";

        /// <summary>
        /// 从注册表获取Sims3的路径
        /// </summary>
        /// <returns></returns>
        public static string GetSimsDirectoryByRegistry()
        {
            return GetSimsDirectoryByRegistry("The Sims 3");
        }
        public static string GetSimsDirectoryByRegistry(string ep)
        {
            string simsDirectory = null;
            // 从注册表获取路径
            RegistryKey key = Registry.LocalMachine;
            key = key.OpenSubKey("SOFTWARE");
            key = key.OpenSubKey("Sims");
            key = key.OpenSubKey(ep);
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
