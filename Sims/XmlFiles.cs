using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Seo
{
    public class XmlFiles : IDisposable
    {
        /// <summary>
        /// 获取与此 xml 文档关联的 xml 文件的相对或绝对路径
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// 获取此 xml 文档对流的可读性.
        /// </summary>
        public bool IsReadOnly { get; private set; }

        /// <summary>
        /// <para>获取此 xml 文档是否是有效的文档.</para>
        /// <para>在只读模式时此标记有效.</para>
        /// <para>在可写模式时, 如果 InvalidFileOverwrite 为 true, 则会自动重建文件, 并标记此为 true.</para>
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        /// 设置此值为 true. 那么, 在可写模式下, 如果此 xml 文档无效, 则自动重建文件并覆盖.
        /// </summary>
        public bool InvalidFileOverwrite
        {
            get { return invalidFileOverwrite; }
            set
            {
                if (IsReadOnly) throw ReadonlyInvalidException;
                if (value && !IsValid)
                {
                    XmlWriter writer = XmlWriter.Create(FileName);
                    writer.WriteStartDocument();
                    writer.WriteStartElement(RootName);
                    writer.WriteEndElement();
                    writer.Flush();
                    writer.Close();
                    this.Document.Load(FileName);
                }
                invalidFileOverwrite = value;
            }
        }
        private bool invalidFileOverwrite = false;

        /// <summary>
        /// 当试图以只读权限写入时, 将抛出这个异常.
        /// </summary>
        private readonly InvalidOperationException ReadonlyException = new InvalidOperationException("The xml is readonly. Writing is forbidden.");
        /// <summary>
        /// 当试图写入一个无效的 xml 时, 将抛出这个异常.
        /// </summary>
        private readonly InvalidOperationException InvalidXmlException = new InvalidOperationException("Cannot write an invalid xml file. InvalidFileOverwrite should be set True.");
        /// <summary>
        /// 当试图给只读模式的 InvalidFileOverwrite 赋值时抛出这个异常.
        /// </summary>
        private readonly InvalidOperationException ReadonlyInvalidException = new InvalidOperationException("InvalidFileOverwrite can not be set in readonly mode.");

        /// <summary>
        /// 获取与此 xml 文档关联的 xml 流.
        /// </summary>
        public Stream XmlStream { get; private set; }

        /// <summary>
        /// 获取或设置与此 xml 文档关联的 xml 文档操作实例.
        /// </summary>
        private XmlDocument Document { get; set; }

        /// <summary>
        /// 获取用于构建 xml 节点路径的 StringBuilder 操作实例.
        /// </summary>
        private StringBuilder NodePath;

        /// <summary>
        /// 获取 xml 节点路径的分隔符
        /// </summary>
        private const char NodeSeparator = '/';

        private const string RootName = "root";
        /// <summary>
        /// 获取 xml 根节点头, 用于组成子节点.
        /// </summary>
        private const string RootPath = "/root";

        /// <summary>
        /// 使用只读模式打开 xml 文件, 并使用这个文件创建 xml 文档的新实例.
        /// </summary>
        /// <param name="file">xml 文件的相对路径或绝对路径</param>
        public XmlFiles(string file)
            : this(file, false) { }
        
        /// <summary>
        /// 使用指定读写模式打开 xml 文件, 并使用这个文件创建 xml 文档的新实例.
        /// </summary>
        /// <param name="file">xml 文件的相对路径或绝对路径</param>
        /// <param name="writable">是否可写</param>
        public XmlFiles(string file, bool writable)
        {
            this.FileName = file;
            this.IsReadOnly = !writable;
            this.XmlStream = null;
            this.invalidFileOverwrite = false;  // 这里必须使用私有成员, 否则会执行设置代码.
            this.Document = new XmlDocument();
            try { this.Document.Load(file); this.IsValid = true; }
            catch { this.IsValid = false; }
            this.NodePath = new StringBuilder();
        }

        /// <summary>
        /// 使用指定指定的流以只读模式创建 xml 文档的新实例.
        /// </summary>
        /// <param name="stream">xml 流</param>
        public XmlFiles(Stream stream)
        {
            this.FileName = null;
            this.IsReadOnly = false;
            this.ReadonlyException = null;
            this.XmlStream = stream;
            this.invalidFileOverwrite = false;  // 这里必须使用私有成员, 否则会执行设置代码.
            this.Document = new XmlDocument();
            this.Document.Load(stream);
            this.IsValid = true;
            this.NodePath = new StringBuilder();
        }

        /// <summary>
        /// 用于读取或写入的公共结点. 使用前必须赋值, 否则不可靠.
        /// </summary>
        private XmlNode Node;
        /// <summary>
        /// 用于读取或写入的公共元素. 使用前必须赋值, 否则不可靠.
        /// </summary>
        private XmlElement Element;

        /// <summary>
        /// 获取 nodes 表示的层级结点. 并将值赋给 this.Node.
        /// </summary>
        private XmlNode GetNode(params string[] nodes)
        {
            NodePath.Remove(0, NodePath.Length);
            NodePath.Append(RootPath);
            foreach (string n in nodes)
            {
                NodePath.Append(NodeSeparator);
                NodePath.Append(n);
            }
            this.Node = this.Document.SelectSingleNode(NodePath.ToString());
            return this.Node;
        }

        /// <summary>
        /// 获取 nodes 表示的层级结点. 并将值赋给 this.Node. 如果结点不存在, 则创建它.
        /// </summary>
        private XmlNode GetCreateNode(params string[] nodes)
        {
            this.Node = GetNode(nodes);
            if (this.Node == null)
            {
                this.NodePath.Remove(0, this.NodePath.Length);
                this.NodePath.Append(RootPath);
                this.Node = this.Document.SelectSingleNode(NodePath.ToString());
                foreach (string node in nodes)
                {
                    if (this.Node.SelectSingleNode(node) == null)
                    {
                        this.Element = this.Document.CreateElement(node);
                        this.Node.AppendChild(this.Element);
                        this.Node = this.Node.SelectSingleNode(node);
                    }
                    else
                    {
                        this.NodePath.Append(NodeSeparator);
                        this.NodePath.Append(node);
                        this.Node = this.Node.SelectSingleNode(this.NodePath.ToString());
                    }
                }
            }
            return this.Node;
        }

        /// <summary>
        /// 读取在 nodes 节点下字符串. 如果不存在, 则返回 null.
        /// </summary>
        /// <param name="nodes">读取的层级节点</param>
        public string Read(params string[] nodes)
        {
            if (!IsValid) return null;
            this.Node = this.GetNode(nodes);
            if (this.Node == null) return null;
            else return this.Node.InnerText;
        }

        /// <summary>
        /// 读取在 nodes 节点下字符串. 如果不存在, 则返回 fail 指定的值.
        /// </summary>
        /// <param name="fail">当读取失败时返回的值</param>
        /// <param name="node">读取的层级结点</param>
        public string TryRead(string fail, params string[] nodes)
        {
            if (!IsValid) return fail;
            string s = Read(nodes);
            if (s == null) return fail;
            else return s;
        }

        /// <summary>
        /// 读取在 nodes 节点下的布尔值. 如果不存在或不是布尔值, 则返回 null.
        /// </summary>
        /// <param name="nodes">读取的层级结点</param>
        public bool? ReadBool(params string[] nodes)
        {
            if (!IsValid) return null;
            try { bool result = Boolean.Parse(Read(nodes)); return result; }
            catch { return null; }
        }

        /// <summary>
        /// 读取在 nodes 节点下的布尔值. 如果不存在或不是布尔值, 则返回 fail.
        /// </summary>
        /// <param name="fail">当读取失败时返回的值</param>
        /// <param name="nodes">读取的层级结点</param>
        public bool ReadBool(bool fail, params string[] nodes)
        {
            if (!IsValid) return fail;
            bool? result = ReadBool(nodes);
            if (result == true) return true;
            else if (result == false) return false;
            else return fail;
        }

        /// <summary>
        /// 读取在 nodes 节点下的整数. 如果不存在或不是整数, 则返回 fail.
        /// </summary>
        /// <param name="fail">当读取失败时返回的值</param>
        /// <param name="nodes">读取的层级结点</param>
        public int ReadInt(int fail, params string[] nodes)
        {
            if (!IsValid) return fail;
            try { int result = Int32.Parse(Read(nodes)); return result; }
            catch { return fail; }
        }

        /// <summary>
        /// 读取在 nodes 节点下的双精度数. 如果不存在或不是双精度数, 则返回 fail.
        /// </summary>
        /// 
        /// <param name="fail">当读取失败时返回的值</param>
        /// <param name="nodes">读取的层级结点</param>
        public double ReadDouble(double fail, params string[] nodes)
        {
            if (!IsValid) return fail;
            try { double result = Double.Parse(Read(nodes)); return result; }
            catch { return fail; }
        }

        /// <summary>
        /// 读取在 nodes 节点下所有名字为 element 的字符串. 如果不存在, 则返回 null.
        /// </summary>
        /// <param name="element">数组名</param>
        /// <param name="nodes">读取的层级结点</param>
        public string[] ReadArray(string element, params string[] nodes)
        {
            if (!IsValid) return null;
            this.Node = this.GetNode(nodes);
            if (this.Node == null) return null;
            XmlNodeList nodeList = this.Node.SelectNodes(element);
            string[] result = new string[nodeList.Count];
            for (int i = 0; i < result.Length; i++)
                result[i] = nodeList.Item(i).InnerText;
            return result;
        }

        /// <summary>
        /// 读取在 nodes 节点下所有名字为 element 的整数. 如果不存在, 则返回 null.
        /// </summary>
        /// <param name="element">数组名</param>
        /// <param name="nodes">读取的层级结点</param>
        public int[] ReadIntArray(string element, params string[] nodes)
        {
            if (!IsValid) return null;
            string[] read = ReadArray(element, nodes);
            if (read == null) return null;
            int[] result = new int[read.Length];
            try
            {
                for (int i = 0; i < read.Length; i++)
                    result[i] = Int32.Parse(read[i]);
            }
            catch { result = null; }
            return result;
        }

        /// <summary>
        /// 读取在 nodes 节点下所有名字为 element 的整数. 并组成长度为 length 的数组.
        /// </summary>
        /// <param name="fail">当某个数不存在或不是整数时会用 fail 代替.</param>
        /// <param name="element">数组名</param>
        /// <param name="nodes">读取的层级结点</param>
        public int[] ReadIntArray(int fail, int length, string element, params string[] nodes)
        {
            int[] result = new int[length];
            if (!IsValid)
            {
                for (int i = 0; i < length; i++)
                    result[i] = fail;
                return result;
            }
            string[] read = ReadArray(element, nodes);
            if (read == null)
            {
                for (int i = 0; i < length; i++)
                    result[i] = fail;
                return result;
            }
            for (int i = 0; i < length; i++)
            {
                try { result[i] = Int32.Parse(read[i]); }
                catch { result[i] = fail; }
            }
            return result;
        }

        /// <summary>
        /// 在 nodes 指定的结点下写下 text 字符串.
        /// </summary>
        /// <param name="text">要写入的字符串</param>
        /// <param name="nodes">写入的层级结点</param>
        public void Write(string text, params string[] nodes)
        {
            //if (!IsValid) throw new 
            if (IsReadOnly) throw ReadonlyException;
            this.Node = GetCreateNode(nodes);
            this.Node.InnerText = text;
        }

        /// <summary>
        /// 在 nodes 指定的结点下写下 text 布尔值.
        /// </summary>
        /// <param name="text">要写入的布尔值</param>
        /// <param name="nodes">写入的层级结点</param>
        public void Write(bool text, params string[] nodes)
        {
            if (IsReadOnly) throw ReadonlyException;
            Write(text.ToString(), nodes);
        }

        /// <summary>
        /// 在 nodes 指定的结点下写下字符串数组.
        /// </summary>
        /// <param name="texts">要写下的数组</param>
        /// <param name="nodes">写入的层级结点</param>
        public void WriteArray(string[] texts, string element, params string[] nodes)
        {
            if (IsReadOnly) throw ReadonlyException;
            this.Node = GetCreateNode(nodes);
            XmlNodeList nodeList = this.Node.SelectNodes(element);
            foreach (XmlNode node in nodeList)
                this.Node.RemoveChild(node);
            foreach (string str in texts)
            {
                this.Element = this.Document.CreateElement(element);
                this.Element.InnerText = str.ToString();
                this.Node.AppendChild(this.Element);
            }
        }

        /// <summary>
        /// 在 nodes 指定的结点下写下布尔值数组.
        /// </summary>
        /// <param name="objs">要写下的数组</param>
        /// <param name="nodes">写入的层级结点</param>
        public void WriteArray(bool[] ns, string element, params string[] nodes)
        {
            if (IsReadOnly) throw ReadonlyException;
            string[] strings = new string[ns.Length];
            for (int i = 0; i < ns.Length; i++)
                strings[i] = ns[i].ToString();
            WriteArray(strings, element, nodes);
        }

        /// <summary>
        /// 在 nodes 指定的结点下写下整数数组.
        /// </summary>
        /// <param name="objs">要写下的数组</param>
        /// <param name="nodes">写入的层级结点</param>
        public void WriteArray(int[] ns, string element, params string[] nodes)
        {
            if (IsReadOnly) throw ReadonlyException;
            string[] strings = new string[ns.Length];
            for (int i = 0; i < ns.Length; i++)
                strings[i] = ns[i].ToString();
            WriteArray(strings, element, nodes);
        }

        /// <summary>
        /// 在 nodes 指定的结点下写下双精度数数组.
        /// </summary>
        /// <param name="objs">要写下的数组</param>
        /// <param name="nodes">写入的层级结点</param>
        public void WriteArray(double[] ns, string element, params string[] nodes)
        {
            if (IsReadOnly) throw ReadonlyException;
            string[] strings = new string[ns.Length];
            for (int i = 0; i < ns.Length; i++)
                strings[i] = ns[i].ToString();
            WriteArray(strings, element, nodes);
        }

        /// <summary>
        /// 将 xml 文档存入文件, 并释放关联的所有资源.
        /// </summary>
        public void Update()
        {
            if (!IsReadOnly) this.Document.Save(this.FileName);
            this.Dispose();
        }

        /// <summary>
        /// 释放与当前 xml 文档关联的所有资源
        /// </summary>
        public void Dispose()
        {
            if (this.XmlStream != null) this.XmlStream.Close();
        }
    }
}
