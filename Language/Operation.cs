using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Operation
    {
        public static string Save = "保存颜色设置";
        public static string Revoke = "撤销所有改变";
        public static string SetToDefault = "恢复默认设置";

        private const string Section = "Operation";
        public static void Initialize(LanguageReader lr)
        {
            Save = lr.Read(Section, "Save", Save);
            Revoke = lr.Read(Section, "Revoke", Revoke);
            SetToDefault = lr.Read(Section, "SetToDefault", SetToDefault);
        }
    }
}
