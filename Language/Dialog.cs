using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Dialog
    {
        public static string InitialFailedTitle = "模拟人生3：编辑环境工具";
        public static string InitialFailedContent = "初始化“模拟人生3“的环境时出现了一些问题，导致程序不能正常运行。问题描述如下：\n\n{0}";  // {0} = 初始化出错的原因
        public static string ReadEnvironmentFileFailedTitle = "读取配置文件时出现了问题";
        public static string ReadEnvironmentFileFailedContent = "{0}";      // {0} = 读取出错的原因

        public static string EnvironmentFileNotFound = "无法找到《模拟人生3》的环境配置文件！";
        public static string WriteEnvironmentFileFailed = "写入《模拟人生3》的环境配置文件失败，可能是这些文件正在被打开的游戏占用。";
        public static string CannotDeleteColor = "保存时发现无法删除多余的颜色";

        public static string SaveTitle = "保存";
        public static string SaveContent = "保存对 “{0}” 的更改？";        // {0} = 要保存的颜色组名称
        public static string SaveRetryTitle = "保存失败";
        public static string SaveRetryContent = "{0}\n\n要再次尝试保存吗？"; // {0} = 保存出错的原因
        public static string RevokeTitle = "撤销";
        public static string RevokeContent = "您确定要放弃自上次保存后对 “{0}” 所作的任何修改吗？";   // {0} = 要撤销的颜色组名称
        public static string SetToDefaultTitle = "默认";
        public static string SetToDefaultContent = "把 “{0}” 恢复成 “模拟人生3” 的默认方案吗？";   // {0} = 要恢复的颜色组名称
        public static string SetToDefaultFailedTitle = "无法恢复";
        public static string SetToDefaultFailedContent = "无法恢复到默认的颜色配置！可能是备份的颜色方案丢失，也可能是《模拟人生3》正在使用需要恢复的文件。\n\n如果是后者，你可以尝试再次恢复。\n\n{0}";   // {0} = 恢复出错的原因

        private const string Section = "Dialog";
        public static void Initialize(LanguageReader lr)
        {
            SaveTitle = lr.Read(Section, "SaveTitle", SaveTitle);
            SaveContent = lr.Read(Section, "SaveContent", SaveContent);
            RevokeTitle = lr.Read(Section, "RevokeTitle", RevokeTitle);
            RevokeContent = lr.Read(Section, "RevokeContent", RevokeContent);
            SetToDefaultTitle = lr.Read(Section, "SetToDefaultTitle", SetToDefaultTitle);
            SetToDefaultContent = lr.Read(Section, "SetToDefaultContent", SetToDefaultContent);
        }
    }
}
