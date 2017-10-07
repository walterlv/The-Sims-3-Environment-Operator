using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo
{
    /// <summary>
    /// 表示游戏中所有可能的天气
    /// </summary>
    public enum Weathers { Clear, PartlyCloudy, Overcast, Stormy, Custom }

    /// <summary>
    /// 表示读取到的一个天气
    /// </summary>
    public class Weather
    {
        #region 读取环境文件时可能需要的关键字
        private const string MiscParams = "MiscParams";
        private const string ProbabilityWeight = "ProbabilityWeight";
        #endregion

        /// <summary>
        /// 五种天气分别列出的数组
        /// </summary>
        public static readonly Weathers[] WeatherArray = new Weathers[5] {Weathers.Clear, Weathers.PartlyCloudy, Weathers.Overcast, Weathers.Stormy, Weathers.Custom };
        /// <summary>
        /// 所有的五种天气
        /// </summary>
        public static List<Weather> AllWeathers = new List<Weather>();

        #region CurrentWeather
        private static Weather currentWeather;
        /// <summary>
        /// 当前正在编辑的天气
        /// </summary>
        public static Weather CurrentWeather
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
        #endregion

        /// <summary>
        /// 当前天气名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 当前天气类型
        /// </summary>
        public Weathers Type { get; private set; }
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
        /// 使外部不可创建此对象
        /// </summary>
        private Weather() { }

        internal static void Initialize()
        {
            Weather weather;
            foreach (Weathers w in WeatherArray)
            {
                weather = new Weather();
                SkyColor Sky_1 = SkyColor.FromColorAssembly(ColorAssemblies.Sky_1, w);
                weather.SkyColors.Add(Sky_1);
                SkyColor Sky_2 = SkyColor.FromColorAssembly(ColorAssemblies.Sky_2, w);
                weather.SkyColors.Add(Sky_2);
                SkyColor Sky_Light = SkyColor.FromColorAssembly(ColorAssemblies.Sky_Light, w);
                weather.SkyColors.Add(Sky_Light);
                SkyColor Sky_Sky = SkyColor.FromColorAssembly(ColorAssemblies.Sky_Sky, w);
                weather.SkyColors.Add(Sky_Sky);
                SkyColor Sky_Sea = SkyColor.FromColorAssembly(ColorAssemblies.Sky_Sea, w);
                weather.SkyColors.Add(Sky_Sea);
                weather.WeatherWeightFile = Sky_Sky.ColorFile;
                IniFiles ww = new IniFiles(weather.WeatherWeightFile);
                weather.WeatherWeight = ww.ReadDouble(MiscParams, ProbabilityWeight, 0.0);
                weather.Name = WeatherToString(w);
                weather.Type = w;
                AllWeathers.Add(weather);
            }
            CurrentWeather = AllWeathers[0];
        }

        internal void Read()
        {
            foreach (SkyColor skyColor in SkyColors) skyColor.Read();
        }

        public static string WeatherToString(Weathers w)
        {
            switch (w)
            {
                case Weathers.Clear:
                    return "Clear";
                case Weathers.PartlyCloudy:
                    return "PartlyCloudy";
                case Weathers.Overcast:
                    return "Overcast";
                case Weathers.Stormy:
                    return "Stormy";
                case Weathers.Custom:
                    return "Custom";
                default:
                    return null;
            }
        }
    }
}
