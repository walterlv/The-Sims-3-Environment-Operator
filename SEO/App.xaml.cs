using System;
using System.Globalization;
using System.Windows;

namespace Seo
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SplashScreen splash = new SplashScreen("Splash.png");
            splash.Show(false);
            splash.Close(new TimeSpan(0));
            // 获取只读字段, 尽快占用文件资源, 确保只有一个程序可操作.
            bool iro = CommonOperation.IsReadonlyMode;
            // 读取用户配置
            Configs.Read();
            // 加载语言
            Seo.Language.LoadLanguage(Configs.Local);
            // 获取M3安装路径
            try { FilesDirs.SimsFolder = FilesDirs.GetSimsDirectoryByRegistry(); }
            catch
            {
                if (Configs.SimsFolder == null)
                {
                    SimsDirWindow sdf = new SimsDirWindow();
                    sdf.ShowDialog();
                    if (sdf.SimsDir == null) { Environment.Exit(0); return; }
                    else
                    {
                        FilesDirs.SimsFolder = sdf.SimsDir;
                        Configs.SimsFolder = sdf.SimsDir;
                        sdf.Dispose();
                    }
                }
                FilesDirs.SimsFolder = Configs.SimsFolder;
            }
            // 启动主窗口 (什么也不用做即可)
            // 其它
            base.OnStartup(e);
        }
    }
}
