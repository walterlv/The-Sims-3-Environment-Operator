using System;
using System.Collections.Generic;
using System.IO;

namespace m3i.SimsDocument
{
    /// <summary>
    /// 存档集合
    /// </summary>
    public class SaveCollection : System.Collections.IEnumerable, System.Collections.IEnumerator
    {
        #region 基本属性
        private Document _document;
        /// <summary>
        /// 获取与此存档集合类关联的Document实例
        /// </summary>
        public Document SaveDocument
        {
            get { return _document; }
            private set { _document = value; }
        }

        private string _saveDir;
        /// <summary>
        /// 获取存档集合所在的完全限定路径
        /// </summary>
        public string SaveDirectory
        {
            get
            {
                if (_saveDir == null) _saveDir = SaveDocument.FullName + @"\Saves";
                return _saveDir;
            }
        }
        #endregion

        #region 存档集合
        private Save[] _allSaves = null;
        /// <summary>
        /// 获取Save实例集合
        /// </summary>
        private Save[] AllSaves
        {
            get
            {
                if (_allSaves == null)
                {
                    List<Save> saveList = new List<Save>();
                    DirectoryInfo dir = new DirectoryInfo(SaveDirectory);
                    DirectoryInfo[] dirs = dir.GetDirectories();
                    foreach (DirectoryInfo di in dirs)
                    {
                        if (di.Name.Contains(".sims3")) saveList.Add(new Save(di));
                    }
                    _allSaves = saveList.ToArray();
                }
                return _allSaves;
            }
        }
        private void UpdateAllSaves()
        {
            _allSaves = null;
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 以用户当前正在玩的Document实例创建一个在此文档路径下的存档集合
        /// </summary>
        public SaveCollection()
            : this(Document.GetDocument())
        { }
        /// <summary>
        /// 以一个Document实例创建一个在此文档路径下的存档集合
        /// </summary>
        public SaveCollection(Document doc)
        {
            this.SaveDocument = doc;
            UpdateAllSaves();
        }
        #endregion

        #region 获取存档
        /// <summary>
        /// 获取所有有效的存档（即游戏列表中显示的存档）
        /// </summary>
        public Save[] GetSaves()
        {
            return GetSaves(false);
        }

        /// <summary>
        /// 获取包括备份存档和其它自定义类型存档在内的所有存档
        /// </summary>
        /// <param name="?">是否获取备份类型和自定义类型的存档</param>
        public Save[] GetSaves(bool all)
        {
            List<Save> saveList = new List<Save>();
            foreach (Save s in AllSaves)
            {
                if (all) saveList.Add(s);
                else if (s.IsNormal) saveList.Add(s);
            }
            return saveList.ToArray();
        }

        /// <summary>
        /// 获取自定义类型的存档
        /// </summary>
        /// <param name="custom">自定义存档类型</param>
        public Save[] GetSaves(string custom)
        {
            List<Save> saveList = new List<Save>();
            foreach (Save s in AllSaves)
            {
                if (s.IsCustom && s.CustomExtension == custom) saveList.Add(s);
            }
            return saveList.ToArray();
        }
        #endregion

        #region 循环枚举
        public Save this[int index]
        {
            get { return AllSaves[index]; }
            private set { AllSaves[index] = value; }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return (System.Collections.IEnumerator)this;
        }

        int position = -1;
        public object Current
        {
            get
            {
                try
                {
                    return AllSaves[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
        public bool MoveNext()
        {
            position++;
            return (position < AllSaves.Length);
        }
        public void Reset()
        {
            position = -1;
        }
        #endregion
    }
}
