using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace TS3Sky
{
    /**/
    /// <summary> 
    /// IniFiles的类 
    /// </summary> 
    public class LanguageReader
    {
        public string FileName; //INI文件名 
        //声明读写INI文件的API函数 
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
        //类的构造函数，传递INI文件名 
        public LanguageReader(string AFileName)
        {
            // 判断文件是否存在 
            FileInfo fileInfo = new FileInfo(AFileName);
            //Todo:搞清枚举的用法 
            if ((!fileInfo.Exists))
            { //|| (FileAttributes.Directory in fileInfo.Attributes)) 
                //文件不存在，建立文件 
                System.IO.StreamWriter sw = new System.IO.StreamWriter(AFileName, false, System.Text.Encoding.Default);
                try
                {
                    sw.Write("# Created by walterlv in M3 Group, 3DMGAME Forum");
                    sw.Close();
                }
                catch
                {
                    throw (new ApplicationException("Ini文件不存在"));
                }
            }
            //必须是完全路径，不能是相对路径 
            FileName = fileInfo.FullName;
        }

        //读取INI文件指定 
        public string Read(string Section, string Ident, string Default)
        {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), FileName);
            //必须设定0（系统默认的代码页）的编码方式，否则无法支持中文 
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            s = s.Substring(0, bufLen);
            return s.Trim().Replace("\0", "").Replace(@"\n", "\n");
        }
        public void Write(string Section, string Ident, string Value)
        {
            if (!WritePrivateProfileString(Section, Ident, Value.Trim().Replace(Environment.NewLine, "\\n").Replace("\n", "\\n"), FileName))
            {
                // Todo:抛出自定义的异常 
                throw (new ApplicationException(TS3Sky.Language.Dialog.WriteEnvironmentFileFailed));
            }
        }

        //Note:对于Win9X，来说需要实现UpdateFile方法将缓冲中的数据写入文件 
        //在Win NT, 2000和XP上，都是直接写文件，没有缓冲，所以，无须实现UpdateFile 
        //执行完对Ini文件的修改之后，应该调用本方法更新缓冲区。 
        public void UpdateFile()
        {
            WritePrivateProfileString(null, null, null, FileName);
        }

        //确保资源的释放 
        ~LanguageReader()
        {
            UpdateFile();
        }
    }
}