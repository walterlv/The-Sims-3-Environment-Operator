using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky
{
    public class Configs
    {
        public static bool UnlockAllWeathers = false;
        public static bool LockWeatherWeight = false;

        public static string CustomInstallDir;

        private static string ConfigFile = Environment.CurrentDirectory + "\\" + "Config.ini";
        public static void Initialize()
        {
            IniFiles config = new IniFiles(ConfigFile);
            UnlockAllWeathers = config.ReadBool("Advance", "UnlockAllWeathers", UnlockAllWeathers);
            LockWeatherWeight = config.ReadBool("Advance", "LockWeatherWeight", LockWeatherWeight);
            CustomInstallDir = config.ReadString("Custom", "CustomInstallDir", CustomInstallDir);
            if (CustomInstallDir.Equals(String.Empty)) CustomInstallDir = null;
            config.UpdateFile();
        }

        public static void Save()
        {
            IniFiles config = new IniFiles(ConfigFile);
            config.WriteBool("Advance", "lockWeatherWeight", LockWeatherWeight);
            if (CustomInstallDir != null) config.WriteString("Custom", "CustomInstallDir", CustomInstallDir);
            config.UpdateFile();
        }
    }
}
