using System;
using System.Collections.Generic;
using Ionic.Zip;
using System.IO;

namespace TS3Sky
{
    public class Package
    {
        public const string Extension = "seo";
        public const string ExtensionDescription = "模拟人生3环境配置包";

        private static string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TS3EO\temp";
        private static string cachePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TS3EO\cache";
        private static string BasicSection = "Basic";
        private static string FileSection = "Files";

        private static string customPath = Environment.CurrentDirectory + @"\Custom";

        public static List<Package> Packages = new List<Package>();

        // 以下储存一个包的信息
        public string PackagePath { get; private set; }
        public string PackageDirectory { get; private set; }
        public string Name { get; private set; }
        public string Version { get; private set; }
        public string Creator { get; private set; }
        public string Description { get; private set; }
        public string ImageFile { get; private set; }
        public bool IsProtected { get; private set; }
        public string ProtectedReason { get; private set; }
        public List<string> Files { get; private set; }

        /// <summary>
        /// 打包环境配置
        /// </summary>
        /// <param name="path">存储路径</param>
        /// <param name="name">方案名</param>
        /// <param name="creator">创建者昵称</param>
        /// <param name="description">方案描述</param>
        /// <param name="image">预览图完全限定路径</param>
        public static void Create(string path, string name, string creator, string description, string image)
        {
            // 创建方案描述文件
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            string tempFilePath = tempPath + @"\info.ini";
            IniFiles desFile = new IniFiles(tempFilePath);
            desFile.WriteString(BasicSection, "Name", name);
            desFile.WriteString(BasicSection, "Version", TS3Sky.Language.About.VersionText);
            desFile.WriteString(BasicSection, "Creator", creator);
            desFile.WriteString(BasicSection, "Description", description);
            string imageName;
            int index = image.LastIndexOf('\\');
            imageName = image.Substring(index + 1, image.Length - index - 1);
            desFile.WriteString(BasicSection, "Preview", imageName);
            desFile.UpdateFile();
            // 打包方案的所有文件
            if (File.Exists(path)) File.Delete(path);
            using (ZipFile zip = new ZipFile(path))
            {
                foreach (SkyColor skyColor in SkyColor.AllSkyColors)
                {
                    zip.AddFile(skyColor.ColorPath, String.Empty);
                }
                zip.AddFile(image, String.Empty);
                zip.AddFile(tempFilePath, String.Empty);
                zip.Save();
            }
            File.Delete(tempFilePath);
        }

        public static bool Exists(string path)
        {
            if (File.Exists(path)) return true;
            else return false;
        }

        public static Package Import(string path)
        {
            int index = path.LastIndexOf('\\');
            string fileName = path.Substring(index + 1, path.Length - index - 1);
            File.Copy(path, customPath + "\\" + fileName, true);
            Package package = Open(cachePath + "\\" + fileName, path);
            Packages.Add(package);
            return package;
        }

        public static void Apply(Package package)
        {
            foreach (string file in package.Files)
            {
                int index = file.LastIndexOf('\\');
                File.Copy(file, SkyColor.ColorDirectory + file.Substring(index + 1, file.Length - index - 1), true);
            }
        }

        public static List<Package> OpenAll()
        {
            // 读取环境配置包
            if (!Directory.Exists(customPath)) Directory.CreateDirectory(customPath);
            DirectoryInfo folder = new DirectoryInfo(customPath);
            foreach (FileInfo next in folder.GetFiles())
            {
                if (!next.Name.EndsWith(String.Format(".{0}", Extension))) continue;
                string cacheFolder;
                // 解包
                using (ZipFile zip = ZipFile.Read(next.FullName))
                {
                    cacheFolder = cachePath + "\\" + next.Name;
                    zip.ExtractAll(cachePath + "\\" + next.Name, ExtractExistingFileAction.OverwriteSilently);
                }
                Packages.Add(Open(cacheFolder, next.FullName));
            }
            return Packages;
        }

        private static Package Open(string cacheFolder, string packagePath)
        {
            // 创建包类
            Package package = new Package();
            IniFiles info = new IniFiles(cacheFolder + @"\info.ini");
            package.PackagePath = packagePath;
            package.PackageDirectory = cacheFolder;
            package.Name = info.ReadString(BasicSection, "Name", String.Empty);
            package.Version = info.ReadString(BasicSection, "Version", String.Empty);
            package.Creator = info.ReadString(BasicSection, "Creator", String.Empty);
            package.Description = info.ReadString(BasicSection, "Description", String.Empty);
            package.ImageFile = cacheFolder + "\\" + info.ReadString(BasicSection, "Preview", String.Empty);
            package.IsProtected = info.ReadBool(BasicSection, "Protected", false);
            package.ProtectedReason = info.ReadString(BasicSection, "ProtectedReason", String.Empty);
            package.Files = new List<string>();
            string fn;
            for (int i = 1; ; i++)
            {
                fn = info.ReadString(FileSection, i.ToString(), String.Empty);
                if (fn.Length > 0) package.Files.Add(cacheFolder + "\\" + fn);
                else break;
            }
            return package;
        }

        public void Delete()
        {
            File.Delete(PackagePath);
            DirectoryInfo di = new DirectoryInfo(PackageDirectory);
            di.Delete(true);
        }
    }
}
