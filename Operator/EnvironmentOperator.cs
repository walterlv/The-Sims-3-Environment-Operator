using System;
using System.Collections.Generic;
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

        #region WorkDirectory
        private string workDirectory;
        /// <summary>
        /// Sims 3 的环境文件目录
        /// </summary>
        public string WorkDirectory
        {
            get { return workDirectory; }
            private set { workDirectory = value; }
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
                if (instance == null)
                {
                    try
                    {
                        instance = new EnvironmentOperator();
                        // TODO 初始化
                        instance.WorkDirectory = FilesDirs.GetSimsDirectoryByConfigs() + SimsDirectoryTail;
                        if (instance.WorkDirectory == null || !Directory.Exists(instance.WorkDirectory))
                            instance.WorkDirectory = FilesDirs.GetSimsDirectoryByRegistry() + "\\" + SimsDirectoryTail;
                        instance.IsReady = true;
                    }
                    catch (Exception ex)
                    {
                        instance = null;
                        throw ex;
                    }
                }
                return instance;
            }
        }

        public void CheckWeathers()
        {
            Weather.Initialize();
        }
        public void ReadWeathers()
        {
            foreach (Weather weather in Weather.AllWeathers) weather.Read();
        }
    }
}
