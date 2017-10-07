using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Dialog
    {
        // 初始化
        public static string InitialFailedTitle = "模拟人生3：编辑环境工具";
        public static string InitialFailedContent = "初始化“模拟人生3“的环境时出现了一些问题，导致程序不能正常运行。问题描述如下：\n\n{0}";  // {0} = 初始化出错的原因
        public static string ReadEnvironmentFileFailedTitle = "读取配置文件时出现了问题";
        public static string ReadEnvironmentFileFailedContent = "{0}";      // {0} = 读取出错的原因
        // 编辑配置文件
        public static string EnvironmentFileNotFound = "无法找到《模拟人生3》的环境配置文件！";
        public static string WriteEnvironmentFileFailed = "写入《模拟人生3》的环境配置文件失败，可能是这些文件正在被打开的游戏占用。";
        public static string CannotDeleteColor = "保存时发现无法删除多余的颜色";
        // 方案
        public static string SaveClosingTitle = "保存方案";
        public static string SaveClosingContent = "你已经修改了环境配置方案，是否保存？";
        public static string ApplyPackageTitle = "应用方案";
        public static string ApplyPackageContent = "应用 “{0}” 方案将会覆盖掉你现在正在使用的方案。\n\n是否继续？";  // {0} = 方案名
        public static string ApplyPackageSuccessfullyTitle = "成功应用方案";
        public static string ApplyPackageSuccessfullyContent = "已经成功应用 “{0}” 方案。";  // {0} = 方案名
        public static string ApplyPackageFailedTitle = "应用选中方案时出现了问题";
        public static string ApplyPackageFailedContent = "{0}";  // {0} = 错误原因
        public static string DeletePackageTitle = "删除方案";
        public static string DeletePackageContent = "要删除环境配置方案 “{0}” 吗？";  // {0} = 方案名
        public static string DeletePackageFailedTitle = "删除不完全";
        public static string DeletePackageFailedContent = "{0}";  // {0} = 错误原因
        public static string ImportPackageTitle = "添加方案";
        public static string ImportPackageFailedTitle = "添加方案错误";
        public static string ImportPackageFailedContent = "{0}";  // {0} = 错误原因
        public static string ExportPackageButNotSavedTitle = "你已经修改了你的方案，但是没有保存。而只有保存后才能导出你的配置。\n\n保存后继续吗？";
        public static string ExportPackageButNotSavedContent = "需要保存才能继续";
        
        // 保存, 恢复文件
        public static string SaveSingleTitle = "保存";
        public static string SaveSingleContent = "保存对 “{0}” 的更改？";        // {0} = 要保存的颜色组名称
        public static string SaveRetryTitle = "保存失败";
        public static string SaveRetryContent = "{0}\n\n要再次尝试保存吗？"; // {0} = 保存出错的原因
        public static string RevokeTitle = "撤销";
        public static string RevokeContent = "您确定要放弃自上次保存后对 “{0}” 所作的任何修改吗？";   // {0} = 要撤销的颜色组名称
        public static string SetToDefaultTitle = "默认";
        public static string SetToDefaultContent = "把 “{0}” 恢复成 “模拟人生3” 的默认方案吗？";   // {0} = 要恢复的颜色组名称
        public static string SetToDefaultFailedTitle = "无法恢复";
        public static string SetToDefaultFailedContent = "无法恢复到默认的颜色配置！可能是备份的颜色方案丢失，也可能是《模拟人生3》正在使用需要恢复的文件。\n\n如果是后者，你可以尝试再次恢复。\n\n{0}";   // {0} = 恢复出错的原因
        // 其它
        public static string UnknownErrorTitle = "出现了未知错误";
        public static string UnknownErrorContent = "应用程序运行期间出现了未知错误，错误描述如下：\n\n{0}"; // {0} = 错误原因


        // 读取语言文件
        private const string Section = "Dialog";
        public static void Initialize(LanguageReader lr)
        {
            // 初始化
            InitialFailedTitle = lr.Read(Section, "InitialFailedTitle", InitialFailedTitle);
            InitialFailedContent = lr.Read(Section, "InitialFailedContent", InitialFailedContent);
            ReadEnvironmentFileFailedTitle = lr.Read(Section, "ReadEnvironmentFileFailedTitle", ReadEnvironmentFileFailedTitle);
            ReadEnvironmentFileFailedContent = lr.Read(Section, "ReadEnvironmentFileFailedContent", ReadEnvironmentFileFailedContent);
            // 编辑配置文件
            EnvironmentFileNotFound = lr.Read(Section, "EnvironmentFileNotFound", EnvironmentFileNotFound);
            WriteEnvironmentFileFailed = lr.Read(Section, "WriteEnvironmentFileFailed", WriteEnvironmentFileFailed);
            CannotDeleteColor = lr.Read(Section, "CannotDeleteColor", CannotDeleteColor);
            // 方案
            SaveClosingTitle = lr.Read(Section, "SaveClosingTitle", SaveClosingTitle);
            SaveClosingContent = lr.Read(Section, "SaveClosingContent", SaveClosingContent);
            ApplyPackageTitle = lr.Read(Section, "ApplyPackageTitle", ApplyPackageTitle);
            ApplyPackageContent = lr.Read(Section, "ApplyPackageContent", ApplyPackageContent);
            ApplyPackageSuccessfullyTitle = lr.Read(Section, "ApplyPackageSuccessfullyTitle", ApplyPackageSuccessfullyTitle);
            ApplyPackageSuccessfullyContent = lr.Read(Section, "ApplyPackageSuccessfullyContent", ApplyPackageSuccessfullyContent);
            ApplyPackageFailedTitle = lr.Read(Section, "ApplyPackageFailedTitle", ApplyPackageFailedTitle);
            ApplyPackageFailedContent = lr.Read(Section, "ApplyPackageFailedContent", ApplyPackageFailedContent);
            DeletePackageTitle = lr.Read(Section, "DeletePackageTitle", DeletePackageTitle);
            DeletePackageContent = lr.Read(Section, "DeletePackageContent", DeletePackageContent);
            DeletePackageFailedTitle = lr.Read(Section, "DeletePackageFailedTitle", DeletePackageFailedTitle);
            DeletePackageFailedContent = lr.Read(Section, "DeletePackageFailedContent", DeletePackageFailedContent);
            ImportPackageTitle = lr.Read(Section, "ImportPackageTitle", ImportPackageTitle);
            ImportPackageFailedTitle = lr.Read(Section, "ImportPackageFailedTitle", ImportPackageFailedTitle);
            ImportPackageFailedContent = lr.Read(Section, "ImportPackageFailedContent", ImportPackageFailedContent);
            ExportPackageButNotSavedTitle = lr.Read(Section, "ExportPackageButNotSavedTitle", ExportPackageButNotSavedTitle);
            ExportPackageButNotSavedContent = lr.Read(Section, "ExportPackageButNotSavedContent", ExportPackageButNotSavedContent);
            // 保存, 恢复文件
            SaveSingleTitle = lr.Read(Section, "SaveSingleTitle", SaveSingleTitle);
            SaveSingleContent = lr.Read(Section, "SaveSingleContent", SaveSingleContent);
            RevokeTitle = lr.Read(Section, "RevokeTitle", RevokeTitle);
            RevokeContent = lr.Read(Section, "RevokeContent", RevokeContent);
            SetToDefaultTitle = lr.Read(Section, "SetToDefaultTitle", SetToDefaultTitle);
            SetToDefaultContent = lr.Read(Section, "SetToDefaultContent", SetToDefaultContent);
            // 其它
            UnknownErrorTitle = lr.Read(Section, "UnknownErrorTitle", UnknownErrorTitle);
            UnknownErrorContent = lr.Read(Section, "UnknownErrorContent", UnknownErrorContent);
        }
    }
}
