using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace Seo
{
    public class EnvironmentOperator
    {
        #region IsReady
        private bool isReady = false;
        /// <summary>
        /// 已经准备好, 可以正常工作
        /// </summary>
        public bool IsReady
        {
            get { return isReady; }
            private set { isReady = value; }
        }
        #endregion

        #region IsEditing
        private bool isEditing = false;
        /// <summary>
        /// 表示当前正在修改这个环境
        /// </summary>
        public bool IsEditing
        {
            get;
            set;
        }
        #endregion

        #region WorkDirectory
        private string workDirectory = null;
        /// <summary>
        /// Sims 3 的环境文件目录
        /// </summary>
        public string WorkDirectory
        {
            get
            {
                if (workDirectory == null)
                {
                    try
                    {
                        workDirectory = FilesDirs.SimsFolder + SimsDirectoryTail;
                        if (!Directory.Exists(workDirectory))
                            throw new FileNotFoundException("Not a valid Sims folder", workDirectory);
                    }
                    catch (Exception ex)
                    {
                        workDirectory = null;
                        throw ex;
                    }
                }
                return workDirectory;
            }
            set { workDirectory = value; }
        }
        #endregion

        #region SimsDirectoryTail
        /// <summary>
        /// Sims 3 环境文件的路径尾巴
        /// </summary>
        public const string SimsDirectoryTail = @"\GameData\Shared\NonPackaged\Ini";
        #endregion

        private EnvironmentOperator() { }

        private static EnvironmentOperator instance;
        public static EnvironmentOperator Instance
        {
            get
            {
                if (instance == null) instance = new EnvironmentOperator();
                return instance;
            }
        }

        #region 加载过程
        /// <summary>
        /// 检查所有要读取的文件, 并初始化对象
        /// </summary>
        public bool CheckWeathers()
        {
            try { Weather.Initialize(); return true; }
            catch { return false; }
        }
        /// <summary>
        /// 根据初始化的对象读取相应的文件
        /// </summary>
        public bool ReadWeathers()
        {
            try { foreach (Weather weather in Weather.AllWeathers) weather.Read(); IsReady = true; return true; }
            catch { return false; }
        }
        /// <summary>
        /// 准备好以便不影响工作
        /// </summary>
        public bool Prepare()
        {
            bool success = false;
            if (CheckWeathers()) success = ReadWeathers();
            return success;
        }
        #endregion

        #region 获取对象
        public SkyColor GetSkyColor(Weathers w, ColorAssemblies c)
        {
            SkyColor color = null;
            Weather weather = GetWeather(w);
            if (!weather.IsError) color = weather.GetSkyColor(c);
            if (color == null) color = new SkyColor(true, c);
            return color;
        }
        public Weather GetWeather(Weathers w)
        {
            Weather weather = Weather.GetWeather(w);
            if (weather == null) return new Weather(true, w);
            else return weather;
        }
        #endregion

        #region 获取数据
        #endregion

        #region 设置数据
        public void SetTimeColorValue(TimeColor tc, Color c)
        {
            tc.ColorValue = c;
        }
        public void SetDayTimeColor(DayColor day, List<TimeColor> colors)
        {
            day.ColorList = colors;
        }
        #endregion
    }
}
