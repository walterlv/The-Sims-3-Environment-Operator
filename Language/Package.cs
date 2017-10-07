using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Package
    {
        public static string Name = "{0}";
        public static string Creator = "创建者：{0}";
        public static string Description = "{0}";

        private const string Section = "Package";
        public static void Initialize(LanguageReader lr)
        {
            Name = lr.Read(Section, "Name", Name);
            Creator = lr.Read(Section, "Creator", Creator);
            Description = lr.Read(Section, "Description", Description);
        }
    }
}
