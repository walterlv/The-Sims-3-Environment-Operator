using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Operation
    {
        public static string DownloadWorld = "Download World";
        public static string Save = "Save(Ctrl+S)";
        public static string Undo = "Undo(Ctrl+Z)";
        public static string Revoke = "Revoke";
        public static string SetToDefault = "Default";
        public static string ApplyPackage = "Apply";
        public static string DeletePackage = "Delete";
        public static string ImportPackage = "Import";
        public static string DownloadPackage = "Download";
        public static string ExportPackage = "Share";
        public static string ExportTitle = "Share My Environment Configration";
        public static string ExportName = "Configration's Name:";
        public static string ExportCreator = "Creator's Name:";
        public static string ExportPreview = "Preview Image:";
        public static string ExportPreviewTitle = "Choose a Preview Image";
        public static string ExportDescription = "Description:";
        public static string ExportTo = "Save to";
        public static string ExportToTitle = "Environment Package";
        public static string ExportOK = "Share";
        public static string ExportCancel = "Cancel";

        private const string Section = "Operation";
        public static void Initialize(LanguageReader lr)
        {
            DownloadWorld = lr.Read(Section, "DownloadWorld", DownloadWorld);
            Save = lr.Read(Section, "Save", Save);
            Undo = lr.Read(Section, "Undo", Undo);
            Revoke = lr.Read(Section, "Revoke", Revoke);
            SetToDefault = lr.Read(Section, "SetToDefault", SetToDefault);
            ApplyPackage = lr.Read(Section, "ApplyPackage", ApplyPackage);
            DeletePackage = lr.Read(Section, "DeletePackage", DeletePackage);
            ImportPackage = lr.Read(Section, "ImportPackage", ImportPackage);
            DownloadPackage = lr.Read(Section, "DownloadPackage", DownloadPackage);
            ExportPackage = lr.Read(Section, "ExportPackage", ExportPackage);
            ExportTitle = lr.Read(Section, "ExportTitle", ExportTitle);
            ExportName = lr.Read(Section, "ExportName", ExportName);
            ExportCreator = lr.Read(Section, "ExportCreator", ExportCreator);
            ExportPreview = lr.Read(Section, "ExportPreview", ExportPreview);
            ExportPreviewTitle = lr.Read(Section, "ExportPreviewTitle", ExportPreviewTitle);
            ExportDescription = lr.Read(Section, "ExportDescription", ExportDescription);
            ExportTo = lr.Read(Section, "ExportTo", ExportTo);
            ExportToTitle = lr.Read(Section, "ExportToTitle", ExportToTitle);
            ExportOK = lr.Read(Section, "ExportOK", ExportOK);
            ExportCancel = lr.Read(Section, "ExportCancel", ExportCancel);
        }
    }
}
