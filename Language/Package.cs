using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class Package
    {
        public static string Name = "{0}";
        public static string Creator = "Created by {0}";
        public static string Description = "{0}";
        public static string Extention = "The Sims 3 Environment Package";
        public static string ImportInvalidPackage = "The file added is not a valid *.seo file.";
        public static string ImportExistedPackage = "A same seo file has been added already.";
        public static string ImportUnknowPackage = "This seo file contains an invalid ID.";
        public static string BackupPackageName = "Backup";
        public static string BackupPackageCreator = "Auto";
        public static string BackupPackageDescription = "Backup time: {0}\n";

        private const string Section = "Package";
        public static void Initialize(LanguageReader lr)
        {
            Name = lr.Read(Section, "Name", Name);
            Creator = lr.Read(Section, "Creator", Creator);
            Description = lr.Read(Section, "Description", Description);
            Extention = lr.Read(Section, "Extention", Extention);
            ImportInvalidPackage = lr.Read(Section, "ImportInvalidPackage", ImportInvalidPackage);
            ImportExistedPackage = lr.Read(Section, "ImportExistedPackage", ImportExistedPackage);
            ImportUnknowPackage = lr.Read(Section, "ImportUnknowPackage", ImportUnknowPackage);
            BackupPackageName = lr.Read(Section, "BackupPackageName", BackupPackageName);
            BackupPackageCreator = lr.Read(Section, "BackupPackageCreator", BackupPackageCreator);
            BackupPackageDescription = lr.Read(Section, "BackupPackageDescription", BackupPackageDescription);
        }
    }
}
