using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Dialog
    {
        // 未知错误
        public static string UnknownErrorTitle = "Unknow Error";
        public static string UnknownErrorContent = "An error occured but we don't know why it happened. You might know something from this message:\n\n{0}";
        // 初始化
        public static string InitialFailedTitle = "The Sims 3: Environment Operator";
        public static string InitialFailedContent = "Initial failed.\n\nDescriptions:\n{0}";
        public static string ReadEnvironmentFileFailedTitle = "Program failed when reading config files";
        public static string ReadEnvironmentFileFailedContent = "Descriptions:\n{0}";
        // 编辑配置文件
        public static string EnvironmentFileNotFound = "Cannot find the environment files from The Sims files.";
        public static string WriteEnvironmentFileFailed = "Cannot set the environment files. The files may be being used by The Sims game.";
        public static string CannotDeleteColor = "Cannot Delete redundant color when saving your settings.";
        // 方案
        public static string SaveClosingTitle = "Save";
        public static string SaveClosingContent = "Your Environment has been modified. Would you like to save it?";
        public static string ApplyPackageTitle = "Apply";
        public static string ApplyPackageContent = "Applying {0} will cover your environment. Continue?";
        public static string ApplyPackageSuccessfullyTitle = "Applied Successfully";
        public static string ApplyPackageSuccessfullyContent = "{0} has been successfully applied";
        public static string ApplyPackageFailedTitle = "Failed to Apply";
        public static string ApplyPackageFailedContent = "Desciption:\n{0}";
        public static string DeletePackageTitle = "Delete";
        public static string DeletePackageContent = "Delete {0}? If it's down, you cannot retrive it.";
        public static string DeletePackageFailedTitle = "Cannot Delete";
        public static string DeletePackageFailedContent = "Desciption:\n{0}";
        public static string ImportPackageTitle = "Open an Environment File";
        public static string ImportPackageFailedTitle = "Import Error";
        public static string ImportPackageFailedContent = "Desciption:\n{0}";
        public static string ExportPackageButNotSavedTitle = "Save";
        public static string ExportPackageButNotSavedContent = "You need to save your environment first to share it. Continue?";
        // 导出
        public static string ChooseExportPreview = "Choose Preview Image";
        public static string AllImageFiles = "All Images";
        public static string AllFiles = "All Files";
        public static string OpenPreviewImageFailedTitle = "Preview image";
        public static string OpenPreviewImageFailedDescription = "Cannot verify the image:\n\n";
        public static string InvalidCharInName = "Name cannot contain {0}";
        public static string InvalidCharInCreator = "Creator's name cannot contain (0}";
        public static string TooManyLinesInDescription = "Please do not enter a description with mroe than three lines";
        public static string ExportToCustomForbidden = "Cannot export to this folder!";
        public static string ExportPackageTitle = "Save Export File";
        public static string ExportPackageFailedTitle = "Export Failed";
        public static string ExportPackageFailedContent = "{0}";
        // 保存, 恢复文件
        public static string SaveSingleTitle = "Save";
        public static string SaveSingleContent = "Save all changes of {0}?";
        public static string SaveRetryTitle = "Save failed";
        public static string SaveRetryContent = "{0}\n\nRetry？";
        public static string RevokeTitle = "Undo";
        public static string RevokeContent = "All your changes of {0} will be discarded, Continue?";
        public static string SetToDefaultTitle = "Set to Default";
        public static string SetToDefaultContent = "Set the color of {0} to EA default?";
        public static string SetToDefaultFailedTitle = "Set to Default Failed";
        public static string SetToDefaultFailedContent = "The backup files might be lost, or The Sims game is using the files we are trying to recover.\nIf it is because of latter, you can retry it later.\n{0}";


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
            // 导出
            ChooseExportPreview = lr.Read(Section, "ChooseExportPreview", ChooseExportPreview);
            AllImageFiles = lr.Read(Section, "AllImageFiles", AllImageFiles);
            AllFiles = lr.Read(Section, "AllFiles", AllFiles);
            OpenPreviewImageFailedTitle = lr.Read(Section, "OpenPreviewImageFailedTitle", OpenPreviewImageFailedTitle);
            OpenPreviewImageFailedDescription = lr.Read(Section, "OpenPreviewImageFailedDescription", OpenPreviewImageFailedDescription);
            InvalidCharInName = lr.Read(Section, "InvalidCharInName", InvalidCharInName);
            InvalidCharInCreator = lr.Read(Section, "InvalidCharInCreator", InvalidCharInCreator);
            TooManyLinesInDescription = lr.Read(Section, "TooManyLinesInDescription", TooManyLinesInDescription);
            ExportToCustomForbidden = lr.Read(Section, "ExportToCustomForbidden", ExportToCustomForbidden);
            ExportPackageTitle = lr.Read(Section, "ExportPackageTitle", ExportPackageTitle);
            ExportPackageFailedTitle = lr.Read(Section, "ExportPackageFailedTitle", ExportPackageFailedTitle);
            ExportPackageFailedContent = lr.Read(Section, "ExportPackageFailedContent", ExportPackageFailedContent);
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
