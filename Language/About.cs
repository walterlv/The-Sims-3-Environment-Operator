using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky.Language
{
    public class About
    {
        public static string Author = "Developer";
        public static string Version = "Version";
        public static string Publish = "Publisher";
        public static string PublishText = "3DMGAME Forum (http://bbs.3dmgame.com/)";
        public static string Contact = "Click to contact us if you have troubles when using this product.";
        public static string ContactUs = "Contact Us";
        public static string DownloadWorldsDescription = "Click here to download worlds";
        public static string DownloadWorlds = "Download Worlds";
        public static string Update = "Update History";

        private const string Section = "About";
        public static void Initialize(LanguageReader lr)
        {
            Author = lr.Read(Section, "Author", Author);
            Version = lr.Read(Section, "Version", Version);
            Publish = lr.Read(Section, "Publish", Publish);
            PublishText = lr.Read(Section, "PublishText", PublishText);
            Contact = lr.Read(Section, "Contact", Contact);
            ContactUs = lr.Read(Section, "ContactUs", ContactUs);
            DownloadWorldsDescription = lr.Read(Section, "DownloadWorldsDescription", DownloadWorldsDescription);
            DownloadWorlds = lr.Read(Section, "DownloadWorlds", DownloadWorlds);
            Update = lr.Read(Section, "Update", Update);
        }
    }
}
