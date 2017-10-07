using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace Seo
{
    public delegate void OnEnviFileOpen(object sender, EventArgs e);
    public delegate void OnEnviFileOpenCompleted(object sender, EventArgs e);

    public class EnviFile
    {
        // 包扩展名
        public const string Extension = "seo";
        public static string ExtensionDescription = Seo.Languages.Operator.EnvironmentFileDescription;
        // 包ID
        public const string DefaultPackageId = "0000-0000-0000-0000";
        // 包路径
        public static string CustomPath = Environment.CurrentDirectory + @"\Custom";
        // 临时路径
        public static string AppDataPath = FilesDirs.AppDataDirectory;
        public static string TempPath = AppDataPath + @"\temp";
        public static string CachePath = AppDataPath + @"\cache";
        // 包配置文件区名
        private const string BasicSection = "Basic";
        private const string FileSection = "Files";

        // 储存所有包
        public static List<EnviFile> Packages = new List<EnviFile>();

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
        public string DirPath { get; private set; }
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
        public EnviFile()
        {
        }
        /// <summary>
        /// 创建一个错误包
        /// </summary>
        /// <param name="isValid">必须是false才能指定为错误包</param>
        /// <param name="errorMessage">错误原因</param>
        private EnviFile(bool isValid, string errorMessage)
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
                id = CreateHexId();
                if (id.StartsWith("0")) continue;
                else break;
            }
            // 创建方案描述文件
            if (!Directory.Exists(TempPath)) Directory.CreateDirectory(TempPath);
            string tempFilePath = TempPath + @"\info.ini";
            IniFiles desFile = new IniFiles(tempFilePath);
            desFile.WriteString(BasicSection, "Id", id);
            desFile.WriteString(BasicSection, "Name", name);
            desFile.WriteString(BasicSection, "Version", Seo.Language.Version);
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
            int num = 1;
            foreach (Weather weather in Weather.AllWeathers)
            {
                foreach (SkyColor skyColor in weather.SkyColors)
                {
                    desFile.WriteString(FileSection, num.ToString(), skyColor.ColorFileName + ".ini");
                    num++;
                }
            }
            desFile.UpdateFile();
            // 打包方案的所有文件
            if (File.Exists(path)) File.Delete(path);
            using (ZipFile zip = new ZipFile(path))
            {
                foreach (Weather weather in Weather.AllWeathers)
                {
                    foreach (SkyColor skyColor in weather.SkyColors)
                    {
                        zip.AddFile(skyColor.ColorFile, String.Empty);
                    }
                }
                if (image != null && image.Length > 0) zip.AddFile(image, String.Empty);
                zip.AddFile(tempFilePath, String.Empty);
                zip.Save();
                zip.Dispose();
            }
            File.Delete(tempFilePath);
        }

        public static string CreateHexId()
        {
            string id;
            Random ram = new Random(unchecked((int)DateTime.Now.Ticks));
            Int32 idNum1 = ram.Next();
            Int32 idNum2 = ram.Next();
            string id1 = String.Format("{0:X}", idNum1).ToString().PadLeft(8, '0').ToUpper();
            string id2 = String.Format("{0:X}", idNum2).ToString().PadLeft(8, '0').ToUpper();
            id = String.Format("{0}-{1}-{2}-{3}", id1.Substring(0, 4), id1.Substring(4, 4), id2.Substring(0, 4), id2.Substring(4, 4));
            return id;
        }

        private static bool IsDefaultLoaded = false;
        private static EnviFile DefaultEnviFile;
        /// <summary>
        /// 从文件名获取默认文件的完全限定路径
        /// </summary>
        /// <param name="file">包含扩展名的文件名</param>
        /// <returns>默认文件的完全限定路径</returns>
        public static string GetDefaultFile(string file)
        {
            if (!IsDefaultLoaded) OpenDefault();
            return DefaultEnviFile.DirPath + '\\' + file;
        }

        public static void OpenDefault()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            Stream stream = asm.GetManifestResourceStream("Seo.Custom.0000-0000-0000-0000." + Extension);
            DefaultEnviFile = Open(stream);
        }

        public static bool Exists(string file)
        {
            if (File.Exists(file)) return true;
            else return false;
        }

        public static EnviFile Import(string path)
        {
            EnviFile package = Open(path);
            package.PackagePath = CustomPath + "\\" + package.Id + "." + Extension;
            if (package.IsValid)
            {
                File.Copy(path, package.PackagePath, true);
            }
            return package;
        }

        string[] importFiles;
        public void ImportAsync(string[] files)
        {
            importFiles = files;
            OpenWorker = new BackgroundWorker();
            OpenWorker.WorkerReportsProgress = true;
            OpenWorker.DoWork += ImportWorker_DoWork;
            OpenWorker.ProgressChanged += OpenWorker_ProgressChanged;
            OpenWorker.RunWorkerCompleted += OpenWorker_RunWorkerCompleted;
            OpenWorker.RunWorkerAsync();
        }

        void ImportWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (importFiles == null) return;
            int i = 0;
            foreach (string file in importFiles)
            {
                EnviFile ef = Import(file);
                OpenWorker.ReportProgress(i++, ef);
            }
        }

        public static void Apply(EnviFile package)
        {
            foreach (string file in package.Files)
            {
                int index = file.LastIndexOf('\\');
                string desDir = EnvironmentOperator.Instance.WorkDirectory + "\\" + file.Substring(index + 1, file.Length - index - 1);
                File.Copy(file, desDir, true);
            }
            // 通知主接口, 已经应用了环境, 要求做相应处理.
            EnvironmentOperator.Instance.EnviFileApplied();
        }

        public static List<EnviFile> OpenAll()
        {
            if (!IsDefaultLoaded) OpenDefault();
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

        public event OnEnviFileOpen EnviFileOpen;
        public event OnEnviFileOpenCompleted EnviFileOpenCompleted;
        BackgroundWorker OpenWorker;
        public void OpenAllAsync()
        {
            OpenWorker = new BackgroundWorker();
            OpenWorker.WorkerReportsProgress = true;
            OpenWorker.DoWork += OpenWorker_DoWork;
            OpenWorker.ProgressChanged += OpenWorker_ProgressChanged;
            OpenWorker.RunWorkerCompleted += OpenWorker_RunWorkerCompleted;
            OpenWorker.RunWorkerAsync();
        }

        void OpenWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!IsDefaultLoaded)
            {
                OpenDefault();
                OpenWorker.ReportProgress(Packages.Count, DefaultEnviFile);
            }
            if (!Directory.Exists(CustomPath)) Directory.CreateDirectory(CustomPath);
            DirectoryInfo folder = new DirectoryInfo(CustomPath);
            foreach (FileInfo next in folder.GetFiles())
            {
                if (!next.Name.EndsWith(String.Format(".{0}", Extension))) continue;
                EnviFile ef = Open(next.FullName);
                OpenWorker.ReportProgress(Packages.Count, ef);
            }
        }

        void OpenWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (EnviFileOpen != null) EnviFileOpen(e.UserState, new EventArgs());
        }

        void OpenWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (EnviFileOpenCompleted != null) EnviFileOpenCompleted(Packages, new EventArgs());
            OpenWorker.Dispose();
        }

        private static EnviFile Open(string packagePath)
        {
            return OpenByZip(ZipFile.Read(packagePath), packagePath);
        }

        private static EnviFile Open(Stream packageStream)
        {
            return OpenByZip(ZipFile.Read(packageStream), null);
        }

        private static EnviFile OpenByZip(ZipFile zip, string packagePath)
        {
            // 获取或定义路径
            if (!Directory.Exists(TempPath)) Directory.CreateDirectory(TempPath);
            string tempFilePath = TempPath + @"\info.ini";
            string cacheFolder;
            // 临时解包查看Id
            try
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
            catch
            {
                return new EnviFile(false, Seo.Languages.Information.ImportInvalidError);
            }
            // 检查Id是否合法
            IniFiles idf = new IniFiles(tempFilePath);
            string id = idf.ReadString(BasicSection, "id", String.Empty);
            // 文件用完就删
            File.Delete(tempFilePath);
            if (Regex.IsMatch(id, @"\b[0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F]-[0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F]-[0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F]-[0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F]\b"))
            {
                // 检查合格则解包
                cacheFolder = CachePath + "\\" + id;
                zip.ExtractAll(cacheFolder, ExtractExistingFileAction.OverwriteSilently);
                // 创建包类
                EnviFile package = new EnviFile();
                IniFiles info = new IniFiles(cacheFolder + @"\info.ini");
                package.DirPath = cacheFolder;
                package.PackagePath = packagePath;
                package.Id = info.ReadString(BasicSection, "Id", String.Empty);
                package.Name = info.ReadString(BasicSection, "Name", String.Empty);
                package.Name = info.ReadString(BasicSection, "Name-" + Seo.Language.Local, package.Name);
                package.Version = info.ReadString(BasicSection, "Version", String.Empty);
                package.Creator = info.ReadString(BasicSection, "Creator", String.Empty);
                package.Creator = info.ReadString(BasicSection, "Creator-" + Seo.Language.Local, package.Creator);
                package.Description = info.ReadString(BasicSection, "Description", String.Empty);
                package.Description = info.ReadString(BasicSection, "Description-" + Seo.Language.Local, package.Description);
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
                    return new EnviFile(false, Seo.Languages.Information.ImportExistedError);
                }
                zip.Dispose();
                return package;
            }
            else
            {
                zip.Dispose();
                return new EnviFile(false, Seo.Languages.Information.ImportUnknowError);
            }
        }

        public static bool AddPackage(EnviFile package)
        {
            bool existed = false;
            foreach (EnviFile p in Packages)
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
            Packages.Remove(this);
            File.Delete(PackagePath);
        }

        public static void ClearTempBackupAndCache()
        {
            foreach (EnviFile p in Packages)
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
