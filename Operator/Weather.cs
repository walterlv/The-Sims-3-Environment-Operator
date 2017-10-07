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
        private bool isError = false;
        public bool IsError
        {
            get { return isError; }
            private set { isError = value; }
        }
        internal Weather(bool error, Weathers w)
        {
            if (error) this.IsError = true;
            else throw new Exception("Only error SkyColor object can be created");
            this.Type = w;
        }

        internal static void Initialize()
        {
            Weather weather;
            foreach (Weathers w in WeatherArray)
            {
                weather = new Weather();
                SkyColor Sky_1 = SkyColor.FromColorAssembly(ColorAssemblies.Sky_1, w);
                if (Sky_1 != null) weather.SkyColors.Add(Sky_1);
                SkyColor Sky_2 = SkyColor.FromColorAssembly(ColorAssemblies.Sky_2, w);
                if (Sky_2 != null) weather.SkyColors.Add(Sky_2);
                SkyColor Sky_Light = SkyColor.FromColorAssembly(ColorAssemblies.Sky_Light, w);
                if (Sky_Light != null) weather.SkyColors.Add(Sky_Light);
                SkyColor Sky_Sky = SkyColor.FromColorAssembly(ColorAssemblies.Sky_Sky, w);
                if (Sky_Sky != null) weather.SkyColors.Add(Sky_Sky);
                SkyColor Sky_Sea = SkyColor.FromColorAssembly(ColorAssemblies.Sky_Sea, w);
                if (Sky_Sea != null) weather.SkyColors.Add(Sky_Sea);
                try
                {
                    weather.WeatherWeightFile = Sky_Sky.ColorFile;
                    weather.Name = WeatherToString(w);
                    weather.Type = w;
                    AllWeathers.Add(weather);
                }
                catch
                {
                    AllWeathers.Add(new Weather(true, w));
                }
            }
            if (AllWeathers.Count > 0) CurrentWeather = AllWeathers[0];
            else throw new Exception("Initialize Failed");
        }

        internal void Read()
        {
            foreach (SkyColor skyColor in SkyColors) skyColor.Read();
        }

        internal void Save()
        {
            foreach (SkyColor skyColor in SkyColors) skyColor.Save();
            SaveWeight();
        }

        internal static void ReadWeight()
        {
            for (int i = 0; i < WeatherArray.Length; i++)
            {
                IniFiles sw = new IniFiles(AllWeathers[i].WeatherWeightFile);
                AllWeathers[i].WeatherWeight = sw.ReadDouble(MiscParams, ProbabilityWeight, AllWeathers[i].WeatherWeight);
            }
        }
        internal static void SaveWeight()
        {
            for (int i = 0; i < WeatherArray.Length; i++)
            {
                IniFiles sw = new IniFiles(AllWeathers[i].WeatherWeightFile);
                sw.WriteDouble(MiscParams, ProbabilityWeight, AllWeathers[i].WeatherWeight);
            }
        }

        public static Weather GetWeather(Weathers w)
        {
            foreach (Weather weather in AllWeathers)
            {
                if (weather.Type == w) return weather;
            }
            return null;
        }

        public SkyColor GetSkyColor(ColorAssemblies c)
        {
            foreach (SkyColor color in this.SkyColors)
            {
                if (color.ColorAssembly == c) return color;
            }
            return null;
        }

        public static string WeatherToName(Weathers w)
        {
            return Enum.GetName(typeof(Weathers), w);
        }

        public static string WeatherToString(Weathers w)
        {
            switch (w)
            {
                case Weathers.Clear:
                    return Seo.Languages.Operator.Clear;
                case Weathers.PartlyCloudy:
                    return Seo.Languages.Operator.PartlyCloudy;
                case Weathers.Overcast:
                    return Seo.Languages.Operator.Overcast;
                case Weathers.Stormy:
                    return Seo.Languages.Operator.Stormy;
                case Weathers.Custom:
                    return Seo.Languages.Operator.Custom;
                default:
                    return null;
            }
        }
    }
}
