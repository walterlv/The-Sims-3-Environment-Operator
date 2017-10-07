using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo.Language
{
    public class Application
    {
        // 程序基本信息
        public static string Name = "The Sims 3: Environment Operator";
        public static string Description = "Enter an iridescent game-world from this program!\nProgram by Waterlv; Solutions by Kuree. 3DM-M3 groups.";
        public static string Version = "1.3.0.48";
        public static string ShowVersion = "1.3 (48)";
        public static string Developer = "walterlv (UI Designer, Developer)\nKuree (Solution Provider)";
        public static string Copyright = "Copyright © walterlv kuree 2012, All Rights Reserved.";

        // 程序加载过程语句
        public static string ChooseLanguage = "This is your first time to run this program, please choose the language you want to use:";
        public static string OK = "OK";
        public static string LoadStart = "The Sims 3: Environment Operator";
        public static string LoadLanguage = "Loading language";
        public static string LoadEnvironment = "Reading sims3 language settings";
        public static string LoadPackage = "Loading seo files";
        public static string LoadCompleted = "Starting";

        // 网址
        public static string ProgramSite = "http://bbs.3dmgame.com/thread-3433574-1-1.html";
        public static string ContactSite = "http://www.modthesims.info/download.php?t=488621";
        public static string WorldDownloadSite = "http://www.modthesims.info/download.php?t=488621";
        public static string PackageDownloadSite = "http://bbs.3dmgame.com/thread-3448055-1-1.html";

        public static void Initialize(string local)
        {
            if (local.Equals("zh-cn") || local.Equals("zh-hans"))
            {
                Name = "模拟人生3：编辑环境工具";
                Description = "想让你的世界充满梦幻般的色彩吗？从这里开始。\n软件由 walterlv 开发，解决方案由 Kuree 提供。（3DMGAME M3 小组）";
                Developer = "walterlv（视觉设计、开发程序）\nKuree（研究解决方案、提供界面语言）";

                ChooseLanguage = String.Format("这是您第一次使用“{0}”，请选择您想要显示的语言：", Name);
                OK = "确定";
                LoadStart = Name;
                LoadLanguage = "加载语言";
                LoadEnvironment = "正在读取模拟人生3环境";
                LoadPackage = "正在读取您的方案";
                LoadCompleted = "准备中";

                ContactSite = "http://bbs.3dmgame.com/thread-3433574-1-1.html";
                WorldDownloadSite = "http://bbs.3dmgame.com/forum.php?mod=redirect&goto=findpost&ptid=3433574&pid=66576405&fromuid=2474653";
                PackageDownloadSite = "http://bbs.3dmgame.com/thread-3448055-1-1.html";
            }
            else if (local.Equals("zh-tw") || local.Equals("zh-hk") || local.Equals("zh-mo") || local.Equals("zh-sg") || local.Equals("zh-hant"))
            {
                Name = "模擬市民3：編輯環境工具";
                Description = "模擬世界的色彩，從這裡開始！\n（軟體由 walterlv 開發，方案由 Kuree 提供。來自 3DMGAME M3 小組。）";
                Developer = "walterlv（視覺設計、開發程式）\nKuree（研究解決方案、提供介面語言）";

                ChooseLanguage = String.Format("這是您第一次使用“{0}”，請選擇您想要顯示的語言：", Name);
                OK = "確定";
                LoadStart = Name;
                LoadLanguage = "裝載語言文件";
                LoadEnvironment = "正在讀取模擬市民3的環境";
                LoadPackage = "正在讀取您的方案";
                LoadCompleted = "準備中";

                ContactSite = "http://bbs.3dmgame.com/thread-3433574-1-1.html";
                WorldDownloadSite = "http://bbs.3dmgame.com/forum.php?mod=redirect&goto=findpost&ptid=3433574&pid=66576405&fromuid=2474653";
                PackageDownloadSite = "http://bbs.3dmgame.com/thread-3448055-1-1.html";
            }
            else if (local.Equals("it-it"))
            {
                Name = "The Sims 3: Environment Operator";
                Description = "Entra in un mondo colorato grazie a questo programma!\nProgram: Waterlv; Solutions: Kuree; 3DM-M3. (Italian Translation by Xidian.)";
                Developer = "walterlv (UI Designer, Developer)\nKuree (Solution Provider)\nanteen (Super Tester)";

                ChooseLanguage = "Questa ?la prima volta che il programma viene avviato, seleziona la lingua da utilizzare:";
                OK = "OK";
                LoadStart = "The Sims 3: Environment Operator";
                LoadLanguage = "Caricamento lingua";
                LoadEnvironment = "Lettura delle impostazioni ambientali";
                LoadPackage = "Caricamento dei file seo";
                LoadCompleted = "Avvio";

                ContactSite = "http://www.modthesims.info/download.php?t=488621";
                WorldDownloadSite = "http://www.modthesims.info/download.php?t=488621";
                PackageDownloadSite = "http://bbs.3dmgame.com/thread-3448055-1-1.html";
            }
            else
            {
                Name = "The Sims 3: Environment Operator";
                Description = "Enter an iridescent game-world from this program!\nProgram by Waterlv; Solutions by Kuree. 3DM-M3 groups.";
                Developer = "walterlv (UI Designer, Developer)\nKuree (Solution Provider)";

                ChooseLanguage = "This is your first time to run this program, please choose the language you want to use:";
                OK = "OK";
                LoadStart = "The Sims 3: Environment Operator";
                LoadLanguage = "Loading language";
                LoadEnvironment = "Reading sims3 language settings";
                LoadPackage = "Loading seo files";
                LoadCompleted = "Starting";

                ContactSite = "http://www.modthesims.info/download.php?t=488621";
                WorldDownloadSite = "http://www.modthesims.info/download.php?t=488621";
                PackageDownloadSite = "http://bbs.3dmgame.com/thread-3448055-1-1.html";
            }
        }
    }
}
