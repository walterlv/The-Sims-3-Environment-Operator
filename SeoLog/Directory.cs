using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo.Log
{
    public class Directory
    {
        private static string language = Environment.CurrentDirectory + @"\Languages";
        public static string Language { get { return language; } }
    }
}
