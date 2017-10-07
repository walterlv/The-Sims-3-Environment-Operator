using System;
using System.Collections.Generic;
using Ionic.Zip;
using System.IO;
using System.Text.RegularExpressions;

namespace TS3Sky
{
    public class Package
    {
        // 包扩展名
        public const string Extension = "seo";
        public static string ExtensionDescription = TS3Sky.Language.Package.Extention;
        // 包ID
        public const string DefaultPackageId = "0000-0000-0000-0000";
        // 包路径
        public static string CustomPath = Environment.CurrentDirectory + @"\Custom";
        // 临时路径
        public static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\TS3EO";
        public static string TempPath = AppDataPath + @"\temp";
        public static string CachePath = AppDataPath + @"\cache";
        // 包配置文件区名
        private const string BasicSection = "Basic";
        private const string FileSection = "Files";

        // 储存所有包
        public static List<Package> Packages = new List<Package>();

        // 以下储存一个一般包的信息
        public string PackagePath { get; private set; }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Version { get; private set; }
        public string Creator { get; private set; }
        public string Description { get; private set; }
        public string ImageFile { get; private set; }
        public bool IsTemp { get; private set; }
        public bool IsProtected { get; private set; }
        public string ProtectedReason { get; private set; }
        public List<string> Files { get; private set; }
        // 以下储存一个错误包的信息
        public bool isValid = true;
        public bool IsValid
        {
            get { return isValid; }
            private set { isValid = value; }
        }
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// 创建一个包
        /// </summary>
        private Package()
        {
        }
        /// <summary>
        /// 创建一个错误包
        /// </summary>
        /// <param name="isValid">必须是false才能指定为错误包</param>
        /// <param name="errorMessage">错误原因</param>
        private Package(bool isValid, string errorMessage)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }

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
            Create(path, name, creator, description, image, false);
        }
        public static void Create(string path, string name, string creator, string description, string image, bool isTemp)
        {
            // 生成ID
            string id;
            while (true)
            {
                Random ram = new Random(unchecked((int)DateTime.Now.Ticks));
                Int32 idNum1 = ram.Next();
                Int32 idNum2 = ram.Next();
                string id1 = String.Format("{0:X}", idNum1).ToString().PadLeft(8, '0').ToUpper();
                string id2 = String.Format("{0:X}", idNum2).ToString().PadLeft(8, '0').ToUpper();
                id = String.Format("{0}-{1}-{2}-{3}", id1.Substring(0, 4), id1.Substring(4, 4), id2.Substring(0, 4), id2.Substring(4, 4));
                if (id.StartsWith("0000-")) continue;
                else break;
            }
            // 创建方案描述文件
            if (!Directory.Exists(TempPath)) Directory.CreateDirectory(TempPath);
            string tempFilePath = TempPath + @"\info.ini";
            IniFiles desFile = new IniFiles(tempFilePath);
            desFile.WriteString(BasicSection, "Id", id);
            desFile.WriteString(BasicSection, "Name", name);
            desFile.WriteString(BasicSection, "Version", TS3Sky.Language.Application.Version);
            desFile.WriteString(BasicSection, "Creator", creator);
            desFile.WriteString(BasicSection, "Description", description);
            if (isTemp) desFile.WriteBool(BasicSection, "IsTemp", true);
            if (image != null && image.Length > 0)
            {
                string imageName;
                int index = image.LastIndexOf('\\');
                imageName = image.Substring(index + 1, image.Length - index - 1);
                desFile.WriteString(BasicSection, "Preview", imageName);
            }
            desFile.WriteString(FileSection, "1", "Sky_Clear1.ini");
            desFile.WriteString(FileSection, "2", "Sky_Clear2.ini");
            desFile.WriteString(FileSection, "3", "Sky_ClearLight.ini");
            desFile.WriteString(FileSection, "4", "Sky_ClearSky.ini");
            desFile.WriteString(FileSection, "5", "Sky_ClearSea.ini");
            desFile.UpdateFile();
            // 打包方案的所有文件
            if (File.Exists(path)) File.Delete(path);
            using (ZipFile zip = new ZipFile(path))
            {
                foreach (SkyColor skyColor in SkyColor.AllSkyColors)
                {
                    zip.AddFile(skyColor.ColorPath, String.Empty);
                }
                if (image != null && image.Length > 0) zip.AddFile(image, String.Empty);
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
            Package package = Open(path);
            package.PackagePath = CustomPath + "\\" + package.Id + "." + Extension;
            if (package.IsValid)
            {
                File.Copy(path, package.PackagePath, true);
            }
            return package;
        }

        public static void Apply(Package package)
        {
            foreach (string file in package.Files)
            {
                int index = file.LastIndexOf('\\');
                string desDir = SkyColor.ColorDirectory + "\\" + file.Substring(index + 1, file.Length - index - 1);
                File.Copy(file, desDir, true);
            }
        }

        public static List<Package> OpenAll()
        {
            // 读取环境配置包
            if (!Directory.Exists(CustomPath)) Directory.CreateDirectory(CustomPath);
            DirectoryInfo folder = new DirectoryInfo(CustomPath);
            foreach (FileInfo next in folder.GetFiles())
            {
                if (!next.Name.EndsWith(String.Format(".{0}", Extension))) continue;
                Open(next.FullName);
            }
            return Packages;
        }

        private static Package Open(string packagePath)
        {
            // 获取或定义路径
            if (!Directory.Exists(TempPath)) Directory.CreateDirectory(TempPath);
            string tempFilePath = TempPath + @"\info.ini";
            string cacheFolder;
            // 临时解包查看Id
            try
            {
                using (ZipFile zip = ZipFile.Read(packagePath))
                {
                    foreach (ZipEntry e in zip)
                    {
                        if (e.FileName.Equals("info.ini"))
                        {
                            e.Extract(TempPath, ExtractExistingFileAction.OverwriteSilently);
                            break;
                        }
                    }
                }
            }
            catch
            {
                return new Package(false, TS3Sky.Language.Package.ImportInvalidPackage);
            }
            // 检查Id是否合法
            IniFiles idf = new IniFiles(tempFilePath);
            string id = idf.ReadString(BasicSection, "id", String.Empty);
            // 文件用完就删
            File.Delete(tempFilePath);
            if (Regex.IsMatch(id, @"\b[0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F]-[0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F]-[0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F]-[0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F]\b"))
            {
                // 检查合格则解包
                using (ZipFile zip = ZipFile.Read(packagePath))
                {
                    cacheFolder = CachePath + "\\" + id;
                    zip.ExtractAll(cacheFolder, ExtractExistingFileAction.OverwriteSilently);
                }
                // 创建包类
                Package package = new Package();
                IniFiles info = new IniFiles(cacheFolder + @"\info.ini");
                package.PackagePath = packagePath;
                package.Id = info.ReadString(BasicSection, "Id", String.Empty);
                package.Name = info.ReadString(BasicSection, "Name", String.Empty);
                package.Name = info.ReadString(BasicSection, "Name-" + TS3Sky.Language.LanguageManager.LocalLanguage, package.Name);
                package.Version = info.ReadString(BasicSection, "Version", String.Empty);
                package.Creator = info.ReadString(BasicSection, "Creator", String.Empty);
                package.Creator = info.ReadString(BasicSection, "Creator-" + TS3Sky.Language.LanguageManager.LocalLanguage, package.Creator);
                package.Description = info.ReadString(BasicSection, "Description", String.Empty);
                package.Description = info.ReadString(BasicSection, "Description-" + TS3Sky.Language.LanguageManager.LocalLanguage, package.Description);
                package.ImageFile = cacheFolder + "\\" + info.ReadString(BasicSection, "Preview", String.Empty);
                package.IsTemp = info.ReadBool(BasicSection, "IsTemp", false);
                package.IsProtected = info.ReadBool(BasicSection, "Protected", false);
                package.ProtectedReason = info.ReadString(BasicSection, "ProtectedReason", String.Empty);
                // 包内的文件列表
                package.Files = new List<string>();
                string fn;
                for (int i = 1; ; i++)
                {
                    fn = info.ReadString(FileSection, i.ToString(), String.Empty);
                    if (fn.Length > 0) package.Files.Add(cacheFolder + "\\" + fn);
                    else break;
                }
                // 添加到包列表
                if (!AddPackage(package))
                {
                    return new Package(false, TS3Sky.Language.Package.ImportExistedPackage);
                }
                return package;
            }
            else
            {
                return new Package(false, TS3Sky.Language.Package.ImportUnknowPackage);
            }
        }

        public static bool AddPackage(Package package)
        {
            bool existed = false;
            foreach (Package p in Packages)
            {
                if (p.Id == package.Id)
                {
                    existed = true;
                    break;
                }
            }
            if (!existed) Packages.Add(package);
            return !existed;
        }

        public void Delete()
        {
            File.Delete(PackagePath);
        }

        public static void ClearTempBackupAndCache()
        {
            foreach (Package p in Packages)
            {
                if (p.IsTemp) File.Delete(CustomPath + "\\" + p.Id + "." + Extension);
            }
            if (Directory.Exists(AppDataPath))
            {
                DirectoryInfo di = new DirectoryInfo(AppDataPath);
                di.Delete(true);
            }
        }
    }
}
