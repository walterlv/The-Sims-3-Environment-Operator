using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.IO;

namespace TS3Sky
{
    public class WeatherSky
    {
        private const string MiscParams = "MiscParams";
        private const string ProbabilityWeight = "ProbabilityWeight";
        public const string SimsDirectoryTail = @"\GameData\Shared\NonPackaged\Ini";

        private static string colorDirectory;
        /// <summary>
        /// 获取存放配置文件的文件夹
        /// </summary>
        public static string ColorDirectory
        {
            get
            {
                if (colorDirectory == null)
                {
                    try
                    {
                        string simsPath;
                        RegistryKey key = Registry.LocalMachine;
                        key = key.OpenSubKey("SOFTWARE", true);
                        key = key.OpenSubKey("Sims", true);
                        key = key.OpenSubKey("The Sims 3", true);
                        simsPath = key.GetValue("Install Dir").ToString();
                        if (simsPath.EndsWith("\\")) simsPath.Substring(0, simsPath.Length - 1);
                        colorDirectory = simsPath + SimsDirectoryTail;
                    }
                    catch
                    {
                        throw new TS3Sky.Exceptions.CannotFindInstallDirException();
                    }
                }
                return colorDirectory;
            }
            set
            {
                colorDirectory = value;
            }
        }

        public static bool IsCurrentWeatherModified
        {
            get
            {
                bool isModified = false;
                foreach (SkyColor color in WeatherSky.CurrentWeather.SkyColors)
                {
                    if (color.Modified)
                    {
                        isModified = true;
                        break;
                    }
                }
                return isModified;
            }
        }
        public static bool IsWeatherModified
        {
            get
            {
                bool isModified = false;
                foreach (WeatherSky w in WeatherSky.AllWeathers)
                {
                    foreach (SkyColor color in w.SkyColors)
                    {
                        if (color.Modified)
                        {
                            isModified = true;
                            break;
                        }
                    }
                    if (isModified) break;
                }
                return isModified;
            }
        }

        /// <summary>
        /// 所有的五种天气
        /// </summary>
        public static List<WeatherSky> AllWeathers = new List<WeatherSky>();

        private static WeatherSky currentWeather;
        /// <summary>
        /// 当前正在编辑的天气
        /// </summary>
        public static WeatherSky CurrentWeather
        {
            get
            {
                return currentWeather;
            }
            private set
            {
                currentWeather = value;
            }
        }
        public static void SetCurrentWeather(Weather w)
        {
            switch (w)
            {
                case Weather.Clear:
                    CurrentWeather = AllWeathers[0];
                    break;
                case Weather.PartlyCloudy:
                    CurrentWeather = AllWeathers[1];
                    break;
                case Weather.Overcast:
                    CurrentWeather = AllWeathers[2];
                    break;
                case Weather.Stormy:
                    CurrentWeather = AllWeathers[3];
                    break;
                case Weather.Custom:
                    CurrentWeather = AllWeathers[4];
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 当前天气名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 当前天气类型
        /// </summary>
        public Weather Type { get; private set; }
        /// <summary>
        /// 此天气在所有天气中所占的比重
        /// </summary>
        public double WeatherWeight;
        /// <summary>
        /// 获取天气比重所在的文件
        /// </summary>
        public string WeatherWeightFile { get; private set; }
        /// <summary>
        /// 每种天气里的所有五种配色元素
        /// </summary>
        public List<SkyColor> SkyColors = new List<SkyColor>();

        /// <summary>
        /// 按顺序存储5个天气类型
        /// </summary>
        private static Weather[] Weathers = new Weather[5] {Weather.Clear, Weather.PartlyCloudy, Weather.Overcast, Weather.Stormy, Weather.Custom };

        /// <summary>
        /// 使外部不可创建此对象
        /// </summary>
        private WeatherSky() { }

        public static void ReadAllWeathers()
        {
            WeatherSky weatherSky;
            foreach (Weather w in Weathers)
            {
                weatherSky = new WeatherSky();
                SkyColor Sky_1 = SkyColor.FromColorAssembly(ColorAssembly.Sky_1, w);
                weatherSky.SkyColors.Add(Sky_1);
                SkyColor Sky_2 = SkyColor.FromColorAssembly(ColorAssembly.Sky_2, w);
                weatherSky.SkyColors.Add(Sky_2);
                SkyColor Sky_Light = SkyColor.FromColorAssembly(ColorAssembly.Sky_Light, w);
                weatherSky.SkyColors.Add(Sky_Light);
                SkyColor Sky_Sky = SkyColor.FromColorAssembly(ColorAssembly.Sky_Sky, w);
                weatherSky.SkyColors.Add(Sky_Sky);
                SkyColor Sky_Sea = SkyColor.FromColorAssembly(ColorAssembly.Sky_Sea, w);
                weatherSky.SkyColors.Add(Sky_Sea);
                weatherSky.WeatherWeightFile = Sky_Sky.ColorPath;
                IniFiles ww = new IniFiles(weatherSky.WeatherWeightFile);
                weatherSky.WeatherWeight = ww.ReadDouble(MiscParams, ProbabilityWeight, 0.0);
                weatherSky.Name = GetWeatherName(w);
                weatherSky.Type = w;
                AllWeathers.Add(weatherSky);
            }
            CurrentWeather = AllWeathers[0];
        }

        public void Save()
        {
            foreach (SkyColor color in CurrentWeather.SkyColors) color.Save();
        }

        public static void RereadWeight()
        {
            for (int i = 0; i < Weathers.Length; i++)
            {
                IniFiles sw = new IniFiles(AllWeathers[i].WeatherWeightFile);
                AllWeathers[i].WeatherWeight = sw.ReadDouble(MiscParams, ProbabilityWeight, AllWeathers[i].WeatherWeight);
            }
        }
        public static void SaveWeight()
        {
            for (int i = 0; i < Weathers.Length; i++)
            {
                IniFiles sw = new IniFiles(AllWeathers[i].WeatherWeightFile);
                sw.WriteDouble(MiscParams, ProbabilityWeight, AllWeathers[i].WeatherWeight);
            }
        }

        public static void SaveAll()
        {
        }

        public static WeatherSky GetWeatherByName(Weather w)
        {
            switch (w)
            {
                case Weather.Clear:
                    return AllWeathers[0];
                case Weather.PartlyCloudy:
                    return AllWeathers[1];
                case Weather.Overcast:
                    return AllWeathers[2];
                case Weather.Stormy:
                    return AllWeathers[3];
                case Weather.Custom:
                    return AllWeathers[4];
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取某个天气对应的文件名
        /// </summary>
        /// <param name="weather">天气</param>
        /// <returns>文件名</returns>
        public static string GetWeatherString(Weather weather)
        {
            switch (weather)
            {
                case Weather.Clear:
                    return "Clear";
                case Weather.PartlyCloudy:
                    return "PartlyCloudy";
                case Weather.Overcast:
                    return "Overcast";
                case Weather.Stormy:
                    return "Stormy";
                case Weather.Custom:
                    return "Custom";
                default:
                    return null;
            }
        }
        public static string GetWeatherName(Weather weather)
        {
            switch (weather)
            {
                case Weather.Clear:
                    return TS3Sky.Language.Weather.Clear;
                case Weather.PartlyCloudy:
                    return TS3Sky.Language.Weather.PartlyCloudy;
                case Weather.Overcast:
                    return TS3Sky.Language.Weather.Overcast;
                case Weather.Stormy:
                    return TS3Sky.Language.Weather.Storm;
                case Weather.Custom:
                    return TS3Sky.Language.Weather.Custom;
                default:
                    return null;
            }
        }
    }
}
