using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Dialog
    {
        public static string SaveTitle = "保存";
        public static string SaveContent = "保存对 “{0}” 的更改？";
        public static string RevokeTitle = "撤销";
        public static string RevokeContent = "您确定要放弃自上次保存后对 “{0}” 所作的任何修改吗？";
        public static string SetToDefaultTitle = "默认";
        public static string SetToDefaultContent = "把 “{0}” 恢复成 “模拟人生3” 的默认方案吗？";

        private const string Section = "Dialog";
        public static void Initialize(LanguageReader lr)
        {
            SaveTitle = lr.Read(Section, "SaveTitle", SaveTitle);
            SaveContent = lr.Read(Section, "SaveContent", SaveContent);
            RevokeTitle = lr.Read(Section, "RevokeTitle", RevokeTitle);
            RevokeContent = lr.Read(Section, "RevokeContent", RevokeContent);
            SetToDefaultTitle = lr.Read(Section, "SetToDefaultTitle", SetToDefaultTitle);
            SetToDefaultContent = lr.Read(Section, "SetToDefaultContent", SetToDefaultContent);
        }
    }
}
