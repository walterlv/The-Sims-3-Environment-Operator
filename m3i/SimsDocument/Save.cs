using System;
using System.Collections.Generic;
using System.IO;
namespace m3i.SimsDocument
{
    #region SaveTypes
    /// <summary>
    /// 表示存档的种类
    /// </summary>
    public enum SaveTypes
    {
        /// <summary>
        /// 表示一个存档是标准存档, 可被游戏直接显示和使用的.
        /// </summary>
        Normal,
        /// <summary>
        /// 表示一个存档是游戏自动备份的存档.
        /// </summary>
        Backup,
        /// <summary>
        /// 表示一个存档是自定义存档.
        /// </summary>
        Custom
    }
    #endregion

    /// <summary>
    /// 表示玩家的一个存档
    /// </summary>
    public class Save
    {
        #region 存档名称
        /// <summary>
        /// 游戏默认的标准存档后缀
        /// </summary>
        public const string StandardExtension = ".sims3";
        /// <summary>
        /// 游戏默认的备份存档后缀
        /// </summary>
        public const string BackupExtension = ".backup";
        /// <summary>
        /// 获取存档的名称 (即在游戏列表中显示的名称)
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 获取存档文件的名称
        /// </summary>
        public string FileName { get; private set; }
        /// <summary>
        /// 获取此存档文件的完全限定路径
        /// </summary>
        public string FileFullName { get; private set; }
        /// <summary>
        /// 获取此文件夹的文件夹名称
        /// </summary>
        public string DirectoryName { get; private set; }
        /// <summary>
        /// 获取此存档文件夹的完全限定路径
        /// </summary>
        public string DirectoryFullName { get; private set; }
        #endregion

        #region 存档类型
        /// <summary>
        /// 获取存档类型 (要设置存档类型, 请使用 SetSaveType方法.)
        /// </summary>
        public SaveTypes SaveType { get; private set; }
        /// <summary>
        /// 获取该存档是否是普通类型的 (可在游戏存档列表中显示的)
        /// </summary>
        public bool IsNormal { get; private set; }
        /// <summary>
        /// 获取该存档是否是备份类型 (游戏自动生成的备份存档)
        /// </summary>
        public bool IsBackup { get; private set; }
        /// <summary>
        /// 获取该存档是否是自定义类型 (使用此实例修改过的有自定义后缀的存档, 这样的存档不在游戏存档列表中显示.)
        /// </summary>
        public bool IsCustom { get; private set; }
        private string _customExtension = null;
        /// <summary>
        /// 获取该存档的自定义后缀
        /// </summary>
        public string CustomExtension
        {
            get
            {
                if (IsCustom) return _customExtension;
                else return null;
            }
            set { _customExtension = value; }
        }
        /// <summary>
        /// 设置此存档实例的类型
        /// </summary>
        public void SetSaveType(SaveTypes type)
        {
            SetSaveType(type, null);
        }
        /// <summary>
        /// 设置此存档实例的类型, 如果是自定义存档类型, 则同时指定自定义后缀.
        /// </summary>
        public void SetSaveType(SaveTypes type, string extension)
        {
            switch (type)
            {
                case SaveTypes.Normal:
                    IsNormal = true;
                    IsBackup = false;
                    IsCustom = false;
                    break;
                case SaveTypes.Backup:
                    IsNormal = false;
                    IsBackup = true;
                    IsCustom = false;
                    break;
                case SaveTypes.Custom:
                    IsNormal = false;
                    IsBackup = false;
                    IsCustom = true;
                    break;
                default:
                    break;
            }
            CustomExtension = extension;
            this.SaveType = type;
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 根据存档文件夹创建该存档的一个实例
        /// </summary>
        /// <param name="dir">存档文件夹</param>
        public Save(string dir)
            : this(new DirectoryInfo(dir))
        { }

        /// <summary>
        /// 根据存档文件夹创建该存档的一个实例
        /// </summary>
        /// <param name="dir">存档文件夹</param>
        internal Save(DirectoryInfo dir)
        {
            this.DirectoryName = dir.Name;
            this.DirectoryFullName = dir.FullName;
            this.Name = dir.Name.Substring(0, dir.Name.IndexOf(".sims3"));
            if (this.Name.Length + 6 == dir.Name.Length)
            {
                SetSaveType(SaveTypes.Normal);
            }
            else
            {
                int exIndex = dir.Name.IndexOf(".sims3") + 6;
                string extra = dir.Name.Substring(exIndex, dir.Name.Length - exIndex);
                if (extra.Equals(BackupExtension)) SetSaveType(SaveTypes.Backup);
                else SetSaveType(SaveTypes.Custom, extra);
            }
        }
        #endregion

        #region 父类方法
        public override string ToString()
        {
            return this.DirectoryName;
        }
        #endregion
    }
}
