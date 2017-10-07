using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo.Log
{
    public class File
    {
        private static string configFile = Environment.CurrentDirectory + @"\Config.ini";
        public static string ConfigFile { get { return configFile; } }

        private static string errorLogFile = Environment.CurrentDirectory + @"\Logs\ErrorLog.log";
        public static string ErrorLogFile { get { return errorLogFile; } }
    }
}
