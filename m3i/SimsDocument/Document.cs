using System;
using System.Collections.Generic;
using System.IO;
using m3i.SimsRegistry;

namespace m3i.SimsDocument
{
    public class Document
    {
        private static string _head;
        /// <summary>
        /// 获取文档的路径头 (即Electronic Arts文件夹的完全限定路径)
        /// </summary>
        public static string Head
        {
            get
            {
                if (_head==null) _head = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Electronic Arts";
                return _head;
            }
        }

        private string _name;
        /// <summary>
        /// 获取文档名
        /// </summary>
        public string Name
        {
            get { return _name; }
            private set
            {
                _name = value;
                _fullName = Head + '\\' + value;
            }
        }

        private string _fullName;
        /// <summary>
        /// 获取文档的完整路径
        /// </summary>
        public string FullName
        {
            get { return _fullName; }
            private set { _fullName = value; }
        }

        /// <summary>
        /// 创建一个Document实例, 此实例是最有可能的有效模拟人生3文档.
        /// </summary>
        public Document()
        {
            SimsRegistryInfo info = new SimsRegistryInfo(GamePackNames.Base);
            string loc = info.Locale;
            string docName = Locales.NameToLocaleName(loc);
            this.Name = docName;
        }

        /// <summary>
        /// 创建一个name为文档名的Document实例
        /// </summary>
        /// <param name="name"></param>
        public Document(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 返回此文档的完全限定路径
        /// </summary>
        public override string ToString()
        {
            return this.FullName;
        }

        /// <summary>
        /// 获取有效存档的Document实例
        /// </summary>
        public static Document GetDocument()
        {
            SimsRegistryInfo info = new SimsRegistryInfo(GamePackNames.Base);
            string loc = info.Locale;
            string docName = Locales.NameToLocaleName(loc);
            Document doc = new Document(docName);
            return doc;
        }

        /// <summary>
        /// <para>获取所有可能的模拟人生3的Document实例</para>
        /// <para>如果开启自动筛选器, 则只返回最有可能是有效存档的Document实例.</para>
        /// </summary>
        /// <param name="autoFilter">自动筛选器: true开, false关.</param>
        public static Document[] GetAllDocuments(bool autoFilter)
        {
            List<Document> docList = new List<Document>();
            DirectoryInfo dir = new DirectoryInfo(Head);
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo di in dirs)
            {
                if (autoFilter)
                {
                    if (di.Name.EndsWith("3") && Directory.Exists(di.FullName + @"\Saves"))
                        docList.Add(new Document(di.Name));
                }
                else
                {
                    docList.Add(new Document(di.Name));
                }
            }
            return docList.ToArray();
        }
    }
}
