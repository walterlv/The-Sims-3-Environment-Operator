using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using s3pi.Interfaces;
using s3pi.Package;

namespace Seo
{
    public class PackageFile : IDisposable
    {
        #region 使用 s3pi 前预定义
        /// <summary>
        /// 使用的s3pi版本编号
        /// </summary>
        private const int S3piApiVersion = 0;
        #endregion

        /// <summary>
        /// 获取正在读取或写入(如果可写)的包名
        /// </summary>
        public string FileName { get; private set; }
        /// <summary>
        /// 获取这个包是否可写
        /// </summary>
        public bool Writable { get; private set; }
        /// <summary>
        /// 获取这个包的操作接口
        /// </summary>
        public IPackage Pack { get; private set; }


        /// <summary>
        /// 创建一个读取 file 包的实例.
        /// </summary>
        /// <param name="file">读取的包文件的完全限定路径</param>
        public PackageFile(string file) : this(file, false) { }

        /// <summary>
        /// 创建一个读取 file 包的实例.
        /// </summary>
        /// <param name="file">读取的包文件的完全限定路径</param>
        /// <param name="readwrite">是否允许修改包</param>
        public PackageFile(string file, bool readwrite)
        {
            FileName = file;
            Writable = readwrite;
            Pack = Package.OpenPackage(S3piApiVersion, file, readwrite);
        }

        /// <summary>
        /// 获取环境资源ID.
        /// </summary>
        private ulong[] EnviSNAP = new ulong[] {
                0xECCBBCB773C02131,
                0x7529C26EE8E2A9E6,
                0x28C841C9462BCDE0,
                0xE422CDDE7FE1F25F,
                0xF0A86F660985BF20
            };
        private const string BackupExtention = ".bakup";
        /// <summary>
        /// 获取内部环境资源是否存在
        /// </summary>
        public bool IsInnerEnvironmentExisted
        {
            get
            {
                List<IResourceIndexEntry> Entries = Pack.FindAll((IResourceIndexEntry Entry) => EnviSNAP.Contains(Entry.Instance));
                if (Entries.Count > 0) return true;
                else return false;
            }
        }
        /// <summary>
        /// 删除包内自带的环境
        /// </summary>
        public void DeleteInnerEnvironment()
        {
            // 备份
            if (File.Exists(FileName) && !File.Exists(FileName + BackupExtention)) File.Copy(FileName, FileName + BackupExtention, false);
            // 删除
            List<IResourceIndexEntry> Entries = Pack.FindAll((IResourceIndexEntry Entry) => EnviSNAP.Contains(Entry.Instance));
            foreach (IResourceIndexEntry Entry in Entries) Pack.DeleteResource(Entry);
        }
        /// <summary>
        /// 获取世界的备份是否存在
        /// </summary>
        public bool IsBackupExisted
        {
            get
            {
                if (File.Exists(FileName + BackupExtention)) return true;
                return false;
            }
        }
        /// <summary>
        /// 恢复包内自带的环境
        /// </summary>
        public void RestoreInnerEnvironment()
        {
            if (File.Exists(FileName + BackupExtention))
            {
                File.Copy(FileName + BackupExtention, FileName, true);
                File.Delete(FileName + BackupExtention);
            }
        }

        public void Dispose()
        {
            if (Writable) Pack.SavePackage();
        }
    }
}
