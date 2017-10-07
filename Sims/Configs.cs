using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Seo
{
    public class Configs
    {
        /// <summary>
        /// 获取保存有配置的配置文件名
        /// </summary>
        private static readonly string ConfigFile = Environment.CurrentDirectory + "\\Configs.xml";
        private const string programNode = "Program";
        private const string userNode = "User";
        private const string simsNode = "Sims";
        /// <summary>
        /// 获取自定义颜色组的结点路径
        /// </summary>
        private const string customColorNode = "Custom/Colors";

        #region FirstRun
        private static bool? firstRun = true;
        private static bool firstRunModified = false;
        /// <summary>
        /// 获取或设置程序是否是第一次运行
        /// </summary>
        public static bool FirstRun
        {
            get { return firstRun == true; }
            set { firstRunModified = true; firstRun = value; }
        }
        #endregion

        #region Local
        private static string local = null;
        private static bool localModified = false;
        /// <summary>
        /// 获取或设置用户自定义的语言
        /// </summary>
        public static string Local
        {
            get
            {
                if (local == null) local = CultureInfo.CurrentCulture.Name;
                return local;
            }
            set { localModified = true; local = value; }
        }
        #endregion

        #region DocumentName
        private static string documentName;
        private static bool documentNameModified = false;
        private static bool? hasMultiDocument;
        /// <summary>
        /// 获取是否存在多个文档
        /// </summary>
        public static bool? HasMultiDocument
        {
            get { return hasMultiDocument; }
            private set { hasMultiDocument = value; }
        }
        /// <summary>
        /// 获取文档名
        /// </summary>
        public static string DocumentName
        {
            get
            {
                if (documentName == null)
                {
                    DirectoryInfo dirs = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Electronic Arts");
                    int count = dirs.GetDirectories().Length;
                    if (count == 1) documentName = dirs.GetDirectories()[0].Name;
                    else if (count > 1) hasMultiDocument = true;
                    else hasMultiDocument = false;
                }
                return documentName;
            }
            set
            {
                if (value == null || value.Trim().Length <= 0) return;
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Electronic Arts\" + value))
                { documentName = null; documentNameModified = false; }
                else { documentNameModified = true; documentName = value; }
            }
        }
        /// <summary>
        /// 获取包括EA文件夹和模拟人生3文件夹在内的我的文档位置
        /// </summary>
        public static string DocumentPath
        {
            get
            {
                if (DocumentName == null) return null;
                else return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Electronic Arts\" + DocumentName;
            }
        }
        #endregion

        #region AutoUpdate
        private static bool? autoUpdate;
        private static bool autoUpdateModified = false;
        /// <summary>
        /// 获取或设置应用程序启动时是否自动检查更新的选项.
        /// </summary>
        public static bool AutoUpdate
        {
            get
            {
                if (autoUpdate == null) autoUpdate = true;
                return autoUpdate == true;
            }
            set { autoUpdateModified = true; autoUpdate = value; }
        }
        #endregion

        #region GlassWindow
        private static bool? glassWindow = false;
        private static bool glassWindowModified = false;
        /// <summary>
        /// 是否启用毛玻璃效果
        /// </summary>
        public static bool GlassWindow
        {
            get
            {
                if (IsAeroEnabled) return glassWindow == true;
                else return false;
            }
            set
            {
                if (IsAeroEnabled)
                {
                    glassWindowModified = true;
                    glassWindow = value;
                }
            }
        }
        /// <summary>
        /// 是否允许启用毛玻璃效果
        /// </summary>
        public static bool IsAeroEnabled
        {
            get
            {
                if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor < 2) return true;
                else return false;
            }
        }
        #endregion

        #region BackgroundImage
        private static string backgroundImage = null;
        private static bool backgroundImageModified = false;
        public static bool UseBackgroundImage = false;
        /// <summary>
        /// 获取或设置用户自定义的背景图片
        /// </summary>
        public static string BackgroundImage
        {
            get
            {
                if (!File.Exists(backgroundImage)) backgroundImage = null;
                return backgroundImage;
            }
            set { backgroundImageModified = true; backgroundImage = value; }
        }
        #endregion

        #region CustomColors
        private static int[] customColors;
        private static bool customColorsModified = false;
        /// <summary>
        /// 获取或设置用户自定义颜色组
        /// </summary>
        public static int[] CustomColors
        {
            get { return customColors; }
            set { customColorsModified = true; customColors = value; }
        }
        #endregion

        #region CustomCreator
        private static string customCreator = null;
        private static bool customCreatorModified = false;
        /// <summary>
        /// 获取或设置用户的名称, 这个名称会出现在创建环境包时的作者中.
        /// </summary>
        public static string CustomCreator
        {
            get
            {
                if (customCreator == null) customCreator = Environment.UserName;
                return customCreator;
            }
            set { customCreatorModified = true; customCreator = value; }
        }
        #endregion

        #region LockWeights
        private static bool? lockWeights = false;
        private static bool lockWeightsModified = false;
        public static bool LockWeights
        {
            get { return lockWeights == true; }
            set { lockWeightsModified = true; lockWeights = value; }
        }
        #endregion

        #region SimsFolder
        private static string simsFolder = null;
        private static bool simsFolderModified = false;
        /// <summary>
        /// <para>模拟人生3原版根目录</para>
        /// <para>这个目录一般不储存，只在无法获取M3路径，由用户手动指定路径是储存。</para>
        /// </summary>
        public static string SimsFolder
        {
            get { return simsFolder; }
            set { simsFolderModified = true; simsFolder = value; }
        }
        #endregion

        /// <summary>
        /// 读取用户设置
        /// </summary>
        public static void Read()
        {
            XmlFiles reader = new XmlFiles(ConfigFile);
            firstRun = reader.ReadBool(programNode, "FirstRun");
            documentName = reader.Read(programNode, "Document");
            customCreator = reader.Read(userNode, "Name");
            lockWeights = reader.ReadBool(userNode, "LockWeights");
            local = reader.Read(userNode, "Local");
            autoUpdate = reader.ReadBool(programNode, "AutoUpdate");
            glassWindow = reader.ReadBool(userNode, "GlassWindow");
            backgroundImage = reader.Read(userNode, "BackgroundImage");
            customColors = reader.ReadIntArray(0, 16, "color", userNode, "CustomColors");
            simsFolder = reader.Read(simsNode, "Path");
            reader.Dispose();
        }

        /// <summary>
        /// 保存用户设置
        /// </summary>
        public static void Save()
        {
            XmlFiles writer = new XmlFiles(ConfigFile, true);
            writer.InvalidFileOverwrite = true;
            writer.Write(false, programNode, "FirstRun");
            if (documentNameModified) writer.Write(documentName, programNode, "Document");
            if (customCreatorModified) writer.Write(customCreator, userNode, "Name");
            if (lockWeightsModified) writer.Write(lockWeights == true, userNode, "LockWeights");
            if (localModified) writer.Write(Local, userNode, "Local");
            if (autoUpdateModified) writer.Write(autoUpdate == true, programNode, "AutoUpdate");
            if (glassWindowModified) writer.Write(glassWindow == true, userNode, "GlassWindow");
            if (backgroundImageModified) writer.Write(backgroundImage, userNode, "BackgroundImage");
            if (customColorsModified) writer.WriteArray(customColors, "color", userNode, "CustomColors");
            if (simsFolderModified) writer.Write(simsFolder, simsNode, "Path");
            writer.Update();
        }
    }
}
