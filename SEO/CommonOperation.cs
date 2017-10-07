using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Media.Imaging;
using Seo.WindowParts;

namespace Seo
{
    public class CommonOperation
    {
        #region 确保只有一个程序具有操作权 (只读模式)
        private static string RunningFile = FilesDirs.AppDataDirectory + @"\seo.tmp";
        private static bool? isReadonlyMode = null;
        private static FileStream writer;
        public static bool IsReadonlyMode
        {
            get
            {
                if (isReadonlyMode == null)
                {
                    if (!Directory.Exists(FilesDirs.AppDataDirectory)) Directory.CreateDirectory(FilesDirs.AppDataDirectory);
                    try
                    {
                        if (File.Exists(RunningFile)) writer = File.OpenWrite(RunningFile);
                        else writer = File.Create(RunningFile);
                        writer.WriteByte(0x00);
                        isReadonlyMode = false;
                        // writer.Close();  // 这句仅在调试的时候才启用
                    }
                    catch { isReadonlyMode = true; }
                }
                if (isReadonlyMode == true) return true;
                else if (isReadonlyMode == false) return false;
                else throw new InvalidOperationException();
            }
        }
        public static void ClearReadonly()
        {
            if (writer != null) writer.Close();
        }
        public static void ShowReadonlyTip()
        {
            StatusBar.Show(Status.Warning, Seo.Languages.Window.ReadonlyModeTip, 12000);
        }
        #endregion

        public static BitmapImage GetBitmapImage(string imageFile)
        {
            // 读取图片源文件到byte[]中
            BinaryReader binReader = new BinaryReader(File.Open(imageFile, FileMode.Open));
            FileInfo fileInfo = new FileInfo(imageFile);
            byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
            binReader.Close();
            // 将图片字节赋值给Bitmap Image
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(bytes);
            bitmap.EndInit();
            return bitmap;
        }

        public static void VisitSite(string site)
        {
            System.Diagnostics.Process.Start(site);
        }

        public event OnUpdateChecked UpdateChecked;
        private VersionInfo newVersion;
        /*public static VersionInfo GetUpdate()
        {
            return null;
        }*/
        public void GetUpdateAsync()
        {
            BackgroundWorker updateWorker = new BackgroundWorker();
            updateWorker.DoWork += UpdateWorker_DoWork;
            updateWorker.RunWorkerCompleted += UpdateWorker_RunWorkerCompleted;
            updateWorker.RunWorkerAsync();
        }
        private void UpdateWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (newVersion != null) return;
            try
            {
                string name, version, features;
                string text;
                getWeb(Seo.Language.UpdateSite, out text);
                int start = text.IndexOf("<UpdateInfo>");
                int end = text.IndexOf("</UpdateInfo>");
                text = text.Substring(start, end - start);
                start = text.IndexOf("<Name>");
                end = text.IndexOf("</Name>");
                name = text.Substring(start + 6, end - start - 6);
                start = text.IndexOf("<Version>");
                end = text.IndexOf("</Version>");
                version = text.Substring(start + 9, end - start - 9);
                start = text.IndexOf("<Features>");
                end = text.IndexOf("</Features>");
                features = text.Substring(start + 10, end - start - 10).Replace("<br/>", Environment.NewLine);
                // 比较版本新旧
                Version web = new Version(version);
                Version here = new Version(Seo.Language.Version);
                if (web.CompareTo(here) > 0) newVersion = new VersionInfo(name, version, features);
                else newVersion = new VersionInfo();
            }
            catch { newVersion = null; }
        }
        private void UpdateWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (UpdateChecked != null)
            {
                if (e.Error != null || newVersion == null) UpdateChecked(this, new UpdateArgs());
                else UpdateChecked(this, new UpdateArgs(newVersion));
            }
        }


        /// <summary>
        /// 获取网页文本
        /// </summary>
        /// <param name="strURL">网页地址</param>
        /// <param name="buf">输出网页文本</param>
        /// <returns>是否获取成功</returns>
        private bool getWeb(string strURL, out string buf)
        {
            buf = String.Empty;
            try
            {
                HttpWebRequest request;
                request = (HttpWebRequest)WebRequest.Create(strURL);
                request.Method = "POST"; //Post请求方式
                request.ContentType = "text/html;charset=utf-8"; //内容类型
                string paraUrlCoded = System.Web.HttpUtility.UrlEncode(""); //参数经过URL编码
                byte[] payload;
                payload = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(paraUrlCoded); //将URL编码后的字符串转化为字节
                request.ContentLength = payload.Length; //设置请求的ContentLength
                Stream writer = request.GetRequestStream(); //获得请求流
                writer.Write(payload, 0, payload.Length); //将请求参数写入流
                writer.Close(); //关闭请求流
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse(); //获得响应流
                Stream s;
                s = response.GetResponseStream();
                StreamReader objReader = new StreamReader(s, System.Text.Encoding.GetEncoding("UTF-8"));
                string HTML = "";
                string sLine = "";
                int i = 0;
                while (sLine != null)
                {
                    i++;
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        HTML += sLine;
                }
                HTML = HTML.Replace("&lt;","<");
                HTML = HTML.Replace("&gt;",">");
                buf = HTML;
                return true;
            }
            catch (Exception x)
            {
                buf = x.Message.ToString();
                return false;
            }
        }
    }

    public delegate void OnUpdateChecked(object sender, UpdateArgs e);

    public class UpdateArgs : EventArgs
    {
        public readonly bool IsSuccess;
        public readonly VersionInfo NewVersion;
        public UpdateArgs()
        {
            IsSuccess = false;
            NewVersion = null;
        }
        public UpdateArgs(VersionInfo version)
        {
            IsSuccess = true;
            NewVersion = version;
        }
    }

    public class VersionInfo
    {
        public readonly bool UpdateExisted;
        public readonly string Name;
        public readonly string Version;
        public readonly string Features;
        public VersionInfo()
        {
            UpdateExisted = false;
            this.Name = Seo.Language.ApplicationName;
            this.Version = Seo.Language.Version;
            this.Features = String.Empty;
        }
        public VersionInfo(string name, string version, string features)
        {
            UpdateExisted = true;
            this.Name = name;
            this.Version = version;
            this.Features = features;
        }
        public override string ToString()
        {
            return Name + Environment.NewLine + Version + Environment.NewLine + Features;
        }
    }
}
