using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.IO;

namespace TS3Sky
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        #region 导入API用于获取窗体句柄并激活窗体
        //   用于激活已打开的窗体 
        [DllImport("user32.dll ")]
        public static extern void SetForegroundWindow(IntPtr hwnd);

        //   用于获取当前激活的窗体句柄 
        [DllImport("user32.dll ")]
        public static extern IntPtr GetForegroundWindow();
        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            #region 只允许运行一个程序
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            if (processes.Length > 1)
            {
                foreach (Process p in processes) SetForegroundWindow(p.MainWindowHandle);
                Environment.Exit(0);
            }
            else
            {
                base.OnStartup(e);
            }
            #endregion

            SplashScreen s = new SplashScreen("Splash.png");
            s.Show(true);
            s.Close(new TimeSpan(0, 0, 0));
        }
    }
}
