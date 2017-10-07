using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seo.Languages
{
    public class Operator
    {
        public static string EnvironmentFileDescription = null;

        public static string Clear = null;
        public static string PartlyCloudy = null;
        public static string Overcast = null;
        public static string Stormy = null;
        public static string Custom = null;

        public static string EnvironmentName = null;
        public static string EnvironmentCreator = null;
        public static string EnvironmentDescription = null;

        public static string BackupEnvironmentName = null;
        public static string BackupEnvironmentCreator = null;
        public static string BackupEnvironmentDescription = null;

        private const string Node = "Environment";
        public static void ReadFile(XmlFiles reader)
        {
            EnvironmentFileDescription = reader.TryRead(EnvironmentFileDescription, Node, "File");
            Clear = reader.TryRead(Clear, Node, "Weather/Clear");
            PartlyCloudy = reader.TryRead(PartlyCloudy, Node, "Weather/PartlyCloudy");
            Overcast = reader.TryRead(Overcast, Node, "Weather/Overcast");
            Stormy = reader.TryRead(Stormy, Node, "Weather/Stormy");
            Custom = reader.TryRead(Custom, Node, "Weather/Custom");

            EnvironmentName = reader.TryRead(EnvironmentName, Node, "Format/Name");
            EnvironmentCreator = reader.TryRead(EnvironmentCreator, Node, "Format/Creator");
            EnvironmentDescription = reader.TryRead(EnvironmentDescription, Node, "Format/Description");
            BackupEnvironmentName = reader.TryRead(BackupEnvironmentName, Node, "Backup/Name");
            BackupEnvironmentCreator = reader.TryRead(BackupEnvironmentCreator, Node, "Backup/Creator");
            BackupEnvironmentDescription = reader.TryRead(BackupEnvironmentDescription, Node, "Backup/Description");
        }
    }
}
