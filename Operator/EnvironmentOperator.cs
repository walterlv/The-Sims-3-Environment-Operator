using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace Seo
{
    public delegate void OnSaveCompleted(object sender, OperatorArgs e);

    public class OperatorArgs : EventArgs
    {
        public readonly bool IsSuccess;
        public readonly Exception Error;
        public OperatorArgs()
        {
            IsSuccess = true;
            Error = null;
        }
        public OperatorArgs(Exception ex)
        {
            if (ex == null) IsSuccess = true;
            else IsSuccess = false;
            Error = ex;
        }
    }

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
            get { return isEditing; }
            set { isEditing = value; }
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
        public bool CheckForRead()
        {
            try { Weather.Initialize(); return true; }
            catch { return false; }
        }
        /// <summary>
        /// 根据初始化的对象读取相应的文件
        /// </summary>
        public bool Read()
        {
            // 对外表现为读取文件, 然而实质上是置所有的对象为未读. (以便真正要使用时才去读.)
            // 这样做是为了把整块的读取时间分散到各个小块.
            foreach (Weather weather in Weather.AllWeathers)
            {
                foreach (SkyColor color in weather.SkyColors)
                {
                    color.IsReady = false;
                }
            }
            IsReady = true;
            return true;
        }
        /// <summary>
        /// 准备好以便不影响工作
        /// </summary>
        public bool Prepare()
        {
            bool success = false;
            if (CheckForRead()) success = Read();
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
            else if (!color.IsReady) color.Prepare();
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
        double[] Weights = new double[5];
        bool IsWeightsReady = false;
        public double[] GetWeatherWeights()
        {
            if (!IsWeightsReady)
            {
                Weather.ReadWeight();
                for (int i = 0; i < Weights.Length; i++)
                    Weights[i] = Weather.AllWeathers[i].WeatherWeight;
                IsWeightsReady = true;
            }
            return Weights;
        }
        #endregion

        #region 设置数据
        public void SetTimeColorValue(SkyColor color, TimeColor tc, Color c)
        {
            color.IsModified = true;
            tc.ColorValue = c;
            IsModified = true;
        }
        public void SetWeatherWeights(double[] weights)
        {
            Weights = weights;
            for (int i = 0; i < weights.Length; i++)
                Weather.AllWeathers[i].WeatherWeight = weights[i];
            IsModified = true;
        }
        // 接受通知, 已经应用了一个环境.
        public void EnviFileApplied()
        {
            if (Configs.LockWeights)
            {
                // 重新写入刚刚的权值
                SetWeatherWeights(Weights);
            }
            else
            {
                // 读取新的权值并要求重绘权值图
                IsWeightsReady = false;
                GetWeatherWeights();
            }
        }
        #endregion

        #region 全局操作
        private bool IsDoing = false;
        private bool isModified = false;
        public bool IsModified
        {
            get { return isModified; }
            private set { isModified = value; }
        }
        public void RecheckModified()
        {
            bool mo = false;
            foreach (Weather weather in Weather.AllWeathers)
            {
                foreach (SkyColor color in weather.SkyColors)
                {
                    if (color.IsModified) { mo = true; break; }
                }
                if (mo) break;
            }
            IsModified = mo;
        }
        public bool Save()
        {
            if (!IsModified) return true;
            try { foreach (Weather weather in Weather.AllWeathers) weather.Save(); IsModified = false; return true; }
            catch { return false; }
        }
        public event OnSaveCompleted SaveCompleted;
        public void SaveAsync()
        {
            BackgroundWorker saveWorker = new BackgroundWorker();
            saveWorker.DoWork += saveWorker_DoWork;
            saveWorker.RunWorkerCompleted += saveWorker_RunWorkerCompleted;
            saveWorker.RunWorkerAsync();
        }
        void saveWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!IsModified) return;
            if (IsDoing) throw new InvalidOperationException("Another operation is working.");
            IsDoing = true;
            foreach (Weather weather in Weather.AllWeathers) weather.Save();
            IsModified = false;
        }
        void saveWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (SaveCompleted != null)
            {
                if (e.Error == null) SaveCompleted(this, new OperatorArgs());
                else SaveCompleted(this, new OperatorArgs(e.Error));
            }
            IsDoing = false;
        }
        public bool SetToDefault(Weathers w, ColorAssemblies c)
        {
            try
            {
                SkyColor color = GetSkyColor(w, c);
                if (color.IsError) return false;
                File.Copy(EnviFile.GetDefaultFile(color.ColorFileName + ".ini"), color.ColorFile, true);
                color.IsReady = false;
                color.IsModified = false;
                SetWeatherWeights(Weights);
                RecheckModified();
                return true;
            }
            catch { return false; }
        }
        #endregion
    }
}
