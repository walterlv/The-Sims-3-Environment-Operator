using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Seo.Languages;

namespace Seo
{
    public enum Priority
    {
        /// <summary>
        /// 最低. 如果加载此语言需要先加载其它语言, 则选择此.
        /// </summary>
        Lowest,
        /// <summary>
        /// 低. 如果此语言调用了其它语言, 但可能会被调用, 则选择此.
        /// </summary>
        Low,
        /// <summary>
        /// 一般. 如果加载此语言与其它任何地方无关, 则选择此.
        /// </summary>
        Normal,
        /// <summary>
        /// 高. 如果此语言可能调用其它语言, 但一定会被调用, 则选择此.
        /// </summary>
        High,
        /// <summary>
        /// 最高. 如果此语言需要被其它调用, 则选择此.
        /// </summary>
        Highest
    }

    public class Language
    {
        #region 默认语言
        public static string ApplicationName = null;
        public static string ApplicationTitle = null;
        public static string Version = "2.0.51.42";
        public static string VersionShortText = "2.0";
        public static string VersionLongText = "2.0 build 51 for 1.42";
        public static string OriginalPublishSite = "http://bbs.3dmgame.com/thread-3433574-1-1.html";
        public static string PublishSite = null;
        public static string UpdateSite = null;
        public static string EnviFileDownloadSite = null;
        #endregion

        #region 本地化语言
        public static string Local { get; private set; }
        public const string DefaultLocal = "en-US";
        public static string Folder = Environment.CurrentDirectory + @"\Languages";
        public const string Section = "Language";
        public const string Extension = ".xml";
        public static void LoadLanguage(string local)
        {
            Local = local;
            // 读取内部语言
            if (local.Equals("zh-CN"))
            {
                ApplicationName = "模拟人生3：编辑环境工具";
                ApplicationTitle = "模拟人生3：编辑环境工具";
                PublishSite = "http://bbs.3dmgame.com/thread-3433574-1-1.html";
                UpdateSite = PublishSite;
                EnviFileDownloadSite = "http://bbs.3dmgame.com/thread-3448055-1-1.html";
            }
            else if (local.Equals("zh-HK") || local.Equals("zh-MO") || local.Equals("zh-TW") || local.Equals("zh-SG"))
            {
                ApplicationName = "模擬市民：編輯環境工具";
                ApplicationTitle = "模擬市民：編輯環境工具";
                PublishSite = "http://bbs.3dmgame.com/thread-3433574-1-1.html";
                UpdateSite = PublishSite;
                EnviFileDownloadSite = "http://bbs.3dmgame.com/thread-3448055-1-1.html";
            }
            else
            {
                ApplicationName = "The Sims 3: Environment Operator";
                ApplicationTitle = "Sims Environment Operator";
                PublishSite = "http://www.modthesims.info/download.php?t=488621";
                UpdateSite = PublishSite;
                EnviFileDownloadSite = "http://www.modthesims.info/download.php?t=488621";
            }
            // 读取其它语言
            XmlFiles reader;
            string file;
            Stream stream;
            // 首先尝试在程序集中读取语言流
            stream = LanguageStream.GetLanguageStream(local);
            // 如果程序集中没有当前语言流
            if (stream == null)
            {
                // 则尝试去读文件
                file = Folder + "\\" + local + Extension;
                // 如果连文件都不存在, 则使用默认语言.
                if (!File.Exists(file))
                {
                    stream = LanguageStream.GetLanguageStream(DefaultLocal);
                    reader = new XmlFiles(stream);
                }
                // 否则, 则读取这个语言文件
                else
                {
                    reader = new XmlFiles(file);
                    // 如果发现这个语言是引用另外一个语言
                    file = reader.TryRead(String.Empty, Section, "Using");
                    // 则尝试继续在程序集中找这个语言流
                    stream = LanguageStream.GetLanguageStream(file);
                    // 如果程序集中没有这个语言流
                    if (stream == null)
                    {
                        // 则继续尝试去读文件
                        file = Folder + "\\" + file + Extension;
                        // 只有当文件存在时, 才重新创建读取语言对象
                        if (File.Exists(file))
                        {
                            reader.Dispose();
                            reader = new XmlFiles(file);
                        }
                        // 否则使用默认语言.
                        else
                        {
                            stream = LanguageStream.GetLanguageStream(DefaultLocal);
                            reader = new XmlFiles(stream);
                        }
                    }
                    // 而如果程序集中发现了这个新的语言流
                    else
                    {
                        // 则释放掉就掉流, 创建新的流.
                        reader.Dispose();
                        reader = new XmlFiles(stream);
                    }
                }
            }
            // 而如果一开始就在程序集中找到了流, 就创建这个语言流.
            else
            {
                reader = new XmlFiles(stream);
            }
            // 开始从流中读取语言
            Window.ReadFile(reader);
            Operator.ReadFile(reader);
            Information.ReadFile(reader);
            Sky_1.ReadFile(reader);
            Sky_2.ReadFile(reader);
            Sky_Light.ReadFile(reader);
            Sky_Sky.ReadFile(reader);
            Sky_Sea.ReadFile(reader);
            Worlds.ReadFile(reader);
            // 读取完之后, 释放流的资源
            reader.Dispose();
        }
        #endregion

        #region 重新载入界面
        private static List<ILanguage> RegList = new List<ILanguage>();
        private static List<Priority> RegPriorityList = new List<Priority>();
        /// <summary>
        /// 向受保护的语言类注册, 要求提供受保护的语言. 注册后立即执行重载语言方法.
        /// </summary>
        /// <param name="obj">注册的对象</param>
        public static void Register(object obj, Priority p)
        {
            Register(obj, p, true);
        }
        /// <summary>
        /// 向受保护的语言类注册, 要求提供受保护的语言.
        /// </summary>
        /// <param name="obj">注册的对象</param>
        /// <param name="loadNow">如果为true, 则立即执行重载语言方法.</param>
        public static void Register(object obj, Priority p, bool loadNow)
        {
            ILanguage lang;
            try { lang = obj as ILanguage; RegList.Add(lang); RegPriorityList.Add(p); }
            catch { throw new NotImplementedException("Not implement the ILanguage interface!"); }
            if (loadNow && lang != null) lang.LoadLanguage();
        }
        /// <summary>
        /// 取消注册受保护的语言. 在对象使用完毕后应该执行取消注册.
        /// </summary>
        /// <param name="obj"></param>
        public static void UnRegister(object obj)
        {
            if (RegList.Contains(obj as ILanguage))
            {
                int index = RegList.IndexOf(obj as ILanguage);
                RegList.RemoveAt(index);
                RegPriorityList.RemoveAt(index);
            }
        }
        /// <summary>
        /// 所有注册的对象重新载入语言
        /// </summary>
        public static void LoadLanguageOfRegList()
        {
            for (int i = 0; i < RegList.Count; i++) if (RegPriorityList[i] == Priority.Highest) RegList[i].LoadLanguage();
            for (int i = 0; i < RegList.Count; i++) if (RegPriorityList[i] == Priority.High) RegList[i].LoadLanguage();
            for (int i = 0; i < RegList.Count; i++) if (RegPriorityList[i] == Priority.Normal) RegList[i].LoadLanguage();
            for (int i = 0; i < RegList.Count; i++) if (RegPriorityList[i] == Priority.Low) RegList[i].LoadLanguage();
            for (int i = 0; i < RegList.Count; i++) if (RegPriorityList[i] == Priority.Lowest) RegList[i].LoadLanguage();
        }
        #endregion

        #region 所有语言
        private static Dictionary<string, string> AllLanguages;
        public static Dictionary<string, string> GetLanguages()
        {
            // 如果已经设置过所有语言表, 则直接返回
            if (AllLanguages != null) return AllLanguages;
            // 否则创建新语言表
            AllLanguages = new Dictionary<string, string>();
            // 先从程序集中添加语言
            AllLanguages.Add("en-US", "English");
            AllLanguages.Add("zh-CN", "简体中文");
            AllLanguages.Add("zh-TW", "繁体中文 (台湾)");
            // 再从文件中添加语言
            if (!Directory.Exists(Folder)) return AllLanguages;
            DirectoryInfo folder = new DirectoryInfo(Folder);
            foreach (FileInfo next in folder.GetFiles())
            {
                if (next.Name.EndsWith(Extension))
                {
                    try
                    {
                        XmlFiles reader = new XmlFiles(next.FullName);
                        string local = reader.Read(Section, "Name");
                        if (local != null) AllLanguages.Add(next.Name.Substring(0, next.Name.IndexOf('.')), local);
                        reader.Dispose();
                    }
                    catch { }
                }
            }
            return AllLanguages;
        }
        #endregion
    }

    public class LanguageStream
    {
        public static Stream GetLanguageStream(string local)
        {
            LanguageStream ls = new LanguageStream();
            return ls.getLangStream("Seo.Languages.Languages." + local + ".xml");
        }

        private Stream getLangStream(string res)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceStream(res);
        }
    }
}
