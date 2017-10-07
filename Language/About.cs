using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class About
    {
        public static string Author = "软件作者";
        public static string AuthorName = "walterlv（视觉设计、开发程序）\nKuree（研究解决方案、提取游戏资源）";
        public static string Version = "版本";
        public static string VersionText = "1.0.0.6 Beta";
        public static string Publish = "软件发布";
        public static string PublishText = "3DMGAME M3小组";
        public static string Contact = "如果您对软件有任何疑问或建议，欢迎访问\nhttp://bbs.3dmgame.com/thread-3433574-1-1.html。";
        public static string Copyright = "Copyright © walterlv kuree 2012, Some Rights Reserved.";
        public static string Update = "更新信息";

        private const string Section = "About";
        public static void Initialize(LanguageReader lr)
        {
            Author = lr.Read(Section, "Author", Author);
            Version = lr.Read(Section, "Version", Version);
            Publish = lr.Read(Section, "Publish", Publish);
            PublishText = lr.Read(Section, "PublishText", PublishText);
            Contact = lr.Read(Section, "Contact", Contact);
            Update = lr.Read(Section, "Update", Update);
        }
    }
}
