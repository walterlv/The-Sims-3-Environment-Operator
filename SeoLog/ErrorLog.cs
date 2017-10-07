using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;

namespace Seo.Log
{
    public class ErrorLog
    {
        private const string LogSeparation = "================================================================";
        private const string LogStart = "= ";
        private const string ExName = "Name: ";
        private const string ExMessage = "Message: ";
        private const string ExMethod = "Method: ";
        private const string ExTrackTrace = "TrackTrace: ";

        private static string ErrorLogFile = Seo.Log.File.ErrorLogFile;

        private StreamWriter writer = null;

        private ErrorLog()
        {
            string dir = ErrorLogFile.Substring(0, ErrorLogFile.LastIndexOf("\\"));
            if (!System.IO.Directory.Exists(dir)) System.IO.Directory.CreateDirectory(dir);
            bool isNew = !System.IO.File.Exists(ErrorLogFile);
            writer = new System.IO.StreamWriter(ErrorLogFile, true, System.Text.Encoding.Default);
            if (isNew)
            {
                try
                {
                    writer.WriteLine("The Sims 3 Environment Operator Error Log File");
                    writer.Flush();
                }
                catch { }
            }
        }

        private static ErrorLog thisLog = null;
        private static StreamWriter ThisLog
        {
            get
            {
                if (thisLog == null) thisLog = new ErrorLog();
                return thisLog.writer;
            }
        }

        /// <summary>
        /// 把一个异常写入日志
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="code">错误码 (为0时表示不写错误码)</param>
        public static void WriteErrorLog(Exception ex, int code)
        {
                ThisLog.WriteLine(LogSeparation);
                ThisLog.WriteLine(LogStart + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                if (code != 0) ThisLog.WriteLine(LogStart + "Code: " + code);
                ThisLog.WriteLine(LogStart + "Object: " + ex.Source);
                ThisLog.WriteLine(LogStart + "Message: " + ex.Message);
                ThisLog.WriteLine(LogStart + "Method: " + ex.TargetSite);
                ThisLog.WriteLine(LogStart + "TrackTrace:");
                ThisLog.WriteLine(ex.StackTrace);
                ThisLog.WriteLine(LogSeparation);
                ThisLog.Flush();
        }
    }
}