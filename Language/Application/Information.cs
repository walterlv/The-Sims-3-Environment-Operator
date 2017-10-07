using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo.Languages
{
    public class Information
    {
        public static string CloseSaveTitle = null;
        public static string CloseSaveContent = null;

        public static string WorldCracking = null;
        public static string WorldCracked = null;
        public static string WorldRestoring = null;
        public static string WorldRestored = null;

        public static string ReadingEnvironment = null;
        public static string ReadEnvironmentCompleted = null;
        public static string ImportEnvironmentCompleted = null;
        public static string ReadEnvironmentErrorTitle = null;
        public static string ReadEnvironmentErrorContent = null;
        public static string ImportExistedError = null;
        public static string ImportInvalidError = null;
        public static string ImportUnknowError = null;

        public static string ApplyEnvironmentTitle = null;
        public static string ApplyEnvironmentContent = null;
        public static string ApplyEnvironmentOkay = null;

        public static string DeleteEnvironmentTitle = null;
        public static string DeleteEnvironmentContent = null;
        public static string DeleteEnvironmentError = null;

        public static string ImportingEnvironment = null;

        public static string ExportEnvironmentEmptyName = null;
        public static string ExportEnvironmentInvalidName = null;
        public static string ExportEnvironmentEmptyCreator = null;
        public static string ExportEnvironmentInvalidCreator = null;
        public static string ExportEnvironmentInvalidImage = null;
        public static string ExportEnvironmentEmptyPath = null;
        public static string ExportEnvironmentCustomPathForbidden = null;
        public static string ExportEnvironmentInvalidPath = null;
        public static string ExportEnvironmentOkay = null;
        public static string ExportEnvironmentFailed = null;

        public static string SaveingEnvironment = null;
        public static string SaveEnvironmentOkay = null;
        public static string SaveEnvironmentFailed = null;

        public static string DefaultEnvironmentTitle = null;
        public static string DefaultEnvironmentContent = null;
        public static string DefaultEnvironmentOkay = null;
        public static string DefaultEnvironmentFailed = null;

        private const string Node = "Information";
        public static void ReadFile(XmlFiles reader)
        {
            CloseSaveTitle = reader.TryRead(CloseSaveTitle, Node, "CloseSave/Title");
            CloseSaveContent = reader.TryRead(CloseSaveContent, Node, "CloseSave/Content");

            WorldCracking = reader.TryRead(WorldCracking, Node, "Worlds/Cracking");
            WorldCracked = reader.TryRead(WorldCracked, Node, "Worlds/Cracked");
            WorldRestoring = reader.TryRead(WorldRestoring, Node, "Worlds/Restoring");
            WorldRestored = reader.TryRead(WorldRestored, Node, "Worlds/Restored");

            ReadingEnvironment = reader.TryRead(ReadingEnvironment, Node, "ReadEnvironment/Reading");
            ReadEnvironmentCompleted = reader.TryRead(ReadEnvironmentCompleted, Node, "ReadEnvironment/Completed");
            ReadEnvironmentErrorTitle = reader.TryRead(ReadEnvironmentErrorTitle, Node, "ReadEnvironmentError/Title");
            ReadEnvironmentErrorContent = reader.TryRead(ReadEnvironmentErrorContent, Node, "ReadEnvironmentError/Content");
            ImportExistedError = reader.TryRead(ImportExistedError, Node, "ReadEnvironmentError/ExistedError");
            ImportInvalidError = reader.TryRead(ImportInvalidError, Node, "ReadEnvironmentError/InvalidError");
            ImportUnknowError = reader.TryRead(ImportUnknowError, Node, "ReadEnvironmentError/UnknowError");

            ApplyEnvironmentTitle = reader.TryRead(ApplyEnvironmentTitle, Node, "ApplyEnvironment/Title");
            ApplyEnvironmentContent = reader.TryRead(ApplyEnvironmentContent, Node, "ApplyEnvironment/Content");
            ApplyEnvironmentOkay = reader.TryRead(ApplyEnvironmentOkay, Node, "ApplyEnvironment/Okay");
            DeleteEnvironmentTitle = reader.TryRead(DeleteEnvironmentTitle, Node, "DeleteEnvironment/Title");
            DeleteEnvironmentContent = reader.TryRead(DeleteEnvironmentContent, Node, "DeleteEnvironment/Content");
            DeleteEnvironmentError = reader.TryRead(DeleteEnvironmentError, Node, "DeleteEnvironment/Error");

            ImportingEnvironment = reader.TryRead(ImportingEnvironment, Node, "ImportEnvironment/Importing");
            ImportEnvironmentCompleted = reader.TryRead(ImportEnvironmentCompleted, Node, "ImportEnvironment/Completed");

            ExportEnvironmentEmptyName = reader.TryRead(ExportEnvironmentEmptyName, Node, "ExportEnvironment/EmptyName");
            ExportEnvironmentInvalidName = reader.TryRead(ExportEnvironmentInvalidName, Node, "ExportEnvironment/InvalidName");
            ExportEnvironmentEmptyCreator = reader.TryRead(ExportEnvironmentEmptyCreator, Node, "ExportEnvironment/EmptyCreator");
            ExportEnvironmentInvalidCreator = reader.TryRead(ExportEnvironmentInvalidCreator, Node, "ExportEnvironment/InvalidCreator");
            ExportEnvironmentInvalidImage = reader.TryRead(ExportEnvironmentInvalidImage, Node, "ExportEnvironment/InvalidImage");
            ExportEnvironmentEmptyPath = reader.TryRead(ExportEnvironmentEmptyPath, Node, "ExportEnvironment/EmptyPath");
            ExportEnvironmentCustomPathForbidden = reader.TryRead(ExportEnvironmentCustomPathForbidden, Node, "ExportEnvironment/CustomPathForbidden");
            ExportEnvironmentInvalidPath = reader.TryRead(ExportEnvironmentInvalidPath, Node, "ExportEnvironment/InvalidPath");
            ExportEnvironmentOkay = reader.TryRead(ExportEnvironmentOkay, Node, "ExportEnvironment/Okay");
            ExportEnvironmentFailed = reader.TryRead(ExportEnvironmentFailed, Node, "ExportEnvironment/Failed");

            SaveingEnvironment = reader.TryRead(SaveingEnvironment, Node, "SaveEnvironment/Saving");
            SaveEnvironmentOkay = reader.TryRead(SaveEnvironmentOkay, Node, "SaveEnvironment/Okay");
            SaveEnvironmentFailed = reader.TryRead(SaveEnvironmentFailed, Node, "SaveEnvironment/Failed");

            DefaultEnvironmentTitle = reader.TryRead(DefaultEnvironmentTitle, Node, "DefaultEnvironment/Title");
            DefaultEnvironmentContent = reader.TryRead(DefaultEnvironmentContent, Node, "DefaultEnvironment/Content");
            DefaultEnvironmentOkay = reader.TryRead(DefaultEnvironmentOkay, Node, "DefaultEnvironment/Okay");
            DefaultEnvironmentFailed = reader.TryRead(DefaultEnvironmentFailed, Node, "DefaultEnvironment/Failed");

        }
    }
}
