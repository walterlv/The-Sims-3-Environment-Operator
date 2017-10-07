using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Operation
    {
        public static string DownloadWorld = "下载世界（城镇）";

        public static string Save = "保存颜色设置";
        public static string Revoke = "撤销所有改变";
        public static string SetToDefault = "恢复默认设置";

        public static string ApplyPackage = "应用";
        public static string DeletePackage = "删除";
        public static string ImportPackage = "添加";
        public static string ExportPackage = "导出";

        public static string ExportTitle = "导出方案";
        public static string ExportName = "方案名字：";
        public static string ExportCreator = "作者：";
        public static string ExportPreview = "预览图（100×100）：";
        public static string ExportDescription = "方案描述：";
        public static string ExportTo = "导出到：";
        public static string ExportOK = "导出";
        public static string ExportCancel = "取消";

        private const string Section = "Operation";
        public static void Initialize(LanguageReader lr)
        {
            DownloadWorld = lr.Read(Section, "DownloadWorld", DownloadWorld);
            Save = lr.Read(Section, "Save", Save);
            Revoke = lr.Read(Section, "Revoke", Revoke);
            SetToDefault = lr.Read(Section, "SetToDefault", SetToDefault);
            ApplyPackage = lr.Read(Section, "ApplyPackage", ApplyPackage);
            DeletePackage = lr.Read(Section, "DeletePackage", DeletePackage);
            ImportPackage = lr.Read(Section, "ImportPackage", ImportPackage);
            ExportPackage = lr.Read(Section, "ExportPackage", ExportPackage);
            ExportTitle = lr.Read(Section, "ExportTitle", ExportTitle);
            ExportName = lr.Read(Section, "ExportName", ExportName);
            ExportCreator = lr.Read(Section, "ExportCreator", ExportCreator);
            ExportPreview = lr.Read(Section, "ExportPreview", ExportPreview);
            ExportDescription = lr.Read(Section, "ExportDescription", ExportDescription);
            ExportTo = lr.Read(Section, "ExportTo", ExportTo);
            ExportOK = lr.Read(Section, "ExportOK", ExportOK);
            ExportCancel = lr.Read(Section, "ExportCancel", ExportCancel);
        }
    }
}
