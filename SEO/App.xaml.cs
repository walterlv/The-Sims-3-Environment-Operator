using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Seo
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static App This;

        protected override void OnStartup(StartupEventArgs e)
        {
            SplashScreen splash = new SplashScreen("Logo.png");
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
            This = this;
            base.OnStartup(e);
        }

        public void UpdateColors(Color basic)
        {
            int r = basic.R - 0x20;
            if (r < 0) r = 0x0;
            int g = basic.G - 0x20;
            if (g < 0) g = 0x0;
            int b = basic.B - 0x20;
            if (b < 0) b = 0x0;
            Color dark = Color.FromArgb(basic.A, (byte)r, (byte)r, (byte)r);
            Color trans = Color.FromArgb((byte)(basic.A / 2), basic.R, basic.G, basic.B);

            this.Resources.Remove("ForeBrush");
            this.Resources.Add("ForeBrush", new SolidColorBrush(basic));
            this.Resources.Remove("ForeDarkBrush");
            this.Resources.Add("ForeDarkBrush", new SolidColorBrush(dark));
            this.Resources.Remove("ForeTransBrush");
            this.Resources.Add("ForeTransBrush", new SolidColorBrush(trans));

            this.Resources.Remove("ForeColor");
            this.Resources.Add("ForeColor", basic);
            this.Resources.Remove("ForeTransColor");
            this.Resources.Add("ForeTransColor", trans);
        }
    }
}
