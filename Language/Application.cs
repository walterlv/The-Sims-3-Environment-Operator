using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Application
    {
        public static string Name = "模拟人生3：编辑环境工具";
        public static string Description = "想让你的世界充满梦幻般的色彩吗？从这里开始。\n软件由 walterlv 开发，解决方案由 Kuree 提供。（3DMGAME M3 小组）";

        public static void Initialize(string local)
        {
            if (local.Equals("zh-cn"))
            {
                Name = "模拟人生3：编辑环境工具";
                Description = "想让你的世界充满梦幻般的色彩吗？从这里开始。";
            }
            else if (local.Equals("zh-tw"))
            {
                Name = "模擬市民3：編輯環境工具";
                Description = "模擬世界的色彩，從這裡開始！\n（軟體由 walterlv 開發，方案由 Kuree 提供。來自 3DMGAME M3 小組。）";
            }
            else if (local.Equals("en-us"))
            {
                Name = "The Sims 3: Environment Operator";
                Description = "Let your imagination fly. Fantastic worlds come from here!\n(Developed: walterlv. Solution provider: Kuree. From M3 in 3DMGAME.)";
            }
        }
    }
}
