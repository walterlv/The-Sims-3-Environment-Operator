using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Seo
{
    public enum WorldState
    {
        Unknow = 0,
        NotExisted,
        Existed,
        Cracking,
        Cracked,
        Restoring,
        Supported
    }

    public delegate void OnWorldLoaded(object sender, WorldLoadArgs e);
    public delegate void OnWorldLoadCompleted(object sender, EventArgs e);

    public class WorldLoadArgs : EventArgs
    {
        public readonly int Index;
        public readonly WorldInfo World;
        public WorldLoadArgs(int index, WorldInfo world)
        {
            this.Index = index;
            this.World = world;
        }
    }

    public class WorldList : ILanguage, IDisposable
    {
        private static WorldList instance = null;
        public static WorldList Instance
        {
            get
            {
                if (instance == null) instance = new WorldList();
                return instance;
            }
        }
        public WorldInfo[] Worlds;
        public WorldInfo this[int index]
        {
            get { return Worlds[index]; }
        }
        public int EpCount = 9;
        public int CustomCount { get; private set; }
        public int Count { get; private set; }
        private string DirTail = @"\GameData\Shared\NonPackaged\Worlds\";
        private string DocDirTail = @"\InstalledWorlds";
        private const string WorldExtension = @".world";
        private List<string> DocWorldFiles = new List<string>();
        private List<string> DocWorldName = new List<string>();
        BackgroundWorker LoadWorker;
        public void Initialize()
        {
            Count = EpCount;
            string path = Configs.DocumentPath;
            if (path != null)
            {
                DirectoryInfo di = new DirectoryInfo(path + DocDirTail);
                FileInfo[] fis = di.GetFiles();
                foreach (FileInfo fi in fis)
                {
                    if (fi.Extension == WorldExtension)
                    {
                        DocWorldName.Add(fi.Name.Substring(0, fi.Name.LastIndexOf('.')));
                        DocWorldFiles.Add(fi.FullName);
                    }
                }
                CustomCount = DocWorldFiles.Count;
                Count = EpCount + CustomCount;
            }
            else
            {
                CustomCount = 0;
            }
            Worlds = new WorldInfo[Count];
            for (int i = 0; i < Worlds.Length; i++) Worlds[i] = new WorldInfo();
            Seo.Language.Register(this, Priority.Highest);
            LoadWorker = new BackgroundWorker();
            LoadWorker.WorkerReportsProgress = true;
            LoadWorker.DoWork += LoadWorker_DoWork;
            LoadWorker.ProgressChanged += LoadWorker_ProgressChanged;
            LoadWorker.RunWorkerCompleted += LoadWorker_RunWorkerCompleted;
        }
        public void LoadLanguage()
        {
            Worlds[0].WorldName = Seo.Languages.Worlds.O;
            Worlds[1].WorldName = Seo.Languages.Worlds.EP011;
            Worlds[2].WorldName = Seo.Languages.Worlds.EP012;
            Worlds[3].WorldName = Seo.Languages.Worlds.EP013;
            Worlds[4].WorldName = Seo.Languages.Worlds.EP02;
            Worlds[5].WorldName = Seo.Languages.Worlds.EP03;
            Worlds[6].WorldName = Seo.Languages.Worlds.EP05;
            Worlds[7].WorldName = Seo.Languages.Worlds.EP06;
            Worlds[8].WorldName = Seo.Languages.Worlds.EP07;
            for (int i = 0; i < CustomCount; i++)
                Worlds[i + EpCount].WorldName = DocWorldName[i];
        }
        public void Dispose()
        {
            Seo.Language.UnRegister(this);
        }
        public void StartLoad()
        {
            LoadWorker.RunWorkerAsync();
        }
        private void LoadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try { Worlds[0].FileName = FilesDirs.GetSimsDirectoryByRegistry("The Sims 3") + DirTail + "Sunset Valley.world"; }
            catch { Worlds[0].State = WorldState.NotExisted; }
            try { Worlds[1].FileName = FilesDirs.GetSimsDirectoryByRegistry("The Sims 3 World Adventures") + DirTail + "China.world"; }
            catch { Worlds[1].State = WorldState.NotExisted; }
            try { Worlds[2].FileName = FilesDirs.GetSimsDirectoryByRegistry("The Sims 3 World Adventures") + DirTail + "Egypt.world"; }
            catch { Worlds[2].State = WorldState.NotExisted; }
            try { Worlds[3].FileName = FilesDirs.GetSimsDirectoryByRegistry("The Sims 3 World Adventures") + DirTail + "France.world"; }
            catch { Worlds[3].State = WorldState.NotExisted; }
            try { Worlds[4].FileName = FilesDirs.GetSimsDirectoryByRegistry("The Sims 3 Ambitions") + DirTail + "Twinbrook.world"; }
            catch { Worlds[4].State = WorldState.NotExisted; }
            try { Worlds[5].FileName = FilesDirs.GetSimsDirectoryByRegistry("The Sims 3 Late Night") + DirTail + "Bridgeport.world"; }
            catch { Worlds[5].State = WorldState.NotExisted; }
            try { Worlds[6].FileName = FilesDirs.GetSimsDirectoryByRegistry("The Sims 3 Pets") + DirTail + "AppaloosaPlains.world"; }
            catch { Worlds[6].State = WorldState.NotExisted; }
            try { Worlds[7].FileName = FilesDirs.GetSimsDirectoryByRegistry("The Sims 3 Showtime") + DirTail + "Starlight Shores.world"; }
            catch { Worlds[7].State = WorldState.NotExisted; }
            try { Worlds[8].FileName = FilesDirs.GetSimsDirectoryByRegistry("The Sims 3 Supernatural") + DirTail + "Moonlight Falls.world"; }
            catch { Worlds[8].State = WorldState.NotExisted; }
            for (int i = 0; i < CustomCount; i++) Worlds[i + EpCount].FileName = DocWorldFiles[i];
            int n = 0;
            foreach (WorldInfo item in Worlds)
            {
                if (Worlds[0].State != WorldState.NotExisted)
                {
                    // 确定状态
                    if (!File.Exists(item.FileName)) item.State = WorldState.NotExisted;
                    else
                    {
                        PackageFile pack = new PackageFile(item.FileName);
                        if (pack.IsInnerEnvironmentExisted) item.State = WorldState.Existed;
                        else if (pack.IsBackupExisted) item.State = WorldState.Cracked;
                        else item.State = WorldState.Supported;
                        pack.Dispose();
                    }
                }
                LoadWorker.ReportProgress(n++, item);
            }
        }
        public event OnWorldLoaded WorldLoaded;
        private void LoadWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (WorldLoaded != null) WorldLoaded(this, new WorldLoadArgs(e.ProgressPercentage, e.UserState as WorldInfo));
        }
        public event OnWorldLoadCompleted WorldLoadCompleted;
        private void LoadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (WorldLoadCompleted != null) WorldLoadCompleted(this, new EventArgs());
        }
    }

    public delegate void OnCracked(object sender, WorldEventArgs e);
    public delegate void OnRestored(object sender, WorldEventArgs e);

    public class WorldEventArgs : EventArgs
    {
        public readonly int Index;
        public WorldEventArgs(int index)
        {
            this.Index = index;
        }
    }

    public class WorldInfo
    {
        public string FileName { get; internal set; }
        public string WorldName { get; internal set; }
        public WorldState State { get; internal set; }
        public WorldInfo()
        { State = WorldState.Unknow; }

        BackgroundWorker CrackWorker;
        BackgroundWorker RestoreWorker;
        public event OnCracked Cracked;
        public event OnRestored Restored;
        public void Crack()
        {
            PackageFile pack = new PackageFile(FileName, true);
            pack.DeleteInnerEnvironment();
            pack.Dispose();
            State = WorldState.Cracked;
        }
        int Index;
        public void CrackAsync(int index)
        {
            Index = index;
            CrackWorker = new BackgroundWorker();
            CrackWorker.DoWork += CrackWorker_DoWork;
            CrackWorker.RunWorkerCompleted += CrackWorker_RunWorkerCompleted;
            CrackWorker.RunWorkerAsync();
        }
        void CrackWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Crack();
        }
        void CrackWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Cracked != null) Cracked(this, new WorldEventArgs(Index));
        }
        public void Restore()
        {
            PackageFile pack = new PackageFile(FileName);
            pack.RestoreInnerEnvironment();
            pack.Dispose();
            State = WorldState.Existed;
        }
        public void RestoreAsync(int index)
        {
            this.Index = index;
            RestoreWorker = new BackgroundWorker();
            RestoreWorker.DoWork += RestoreWorker_DoWork;
            RestoreWorker.RunWorkerCompleted += RestoreWorker_RunWorkerCompleted;
            RestoreWorker.RunWorkerAsync();
        }
        void RestoreWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Restore();
        }
        void RestoreWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Restored != null) Restored(this, new WorldEventArgs(Index));
        }
    }

    public class World
    {
        public static string WorldStateToString(WorldState state)
        {
            switch (state)
            {
                case WorldState.NotExisted:
                    return Seo.Languages.Window.WorldNotExisted;
                case WorldState.Existed:
                    return Seo.Languages.Window.WorldExisted;
                case WorldState.Cracking:
                    return Seo.Languages.Window.WorldCracking;
                case WorldState.Cracked:
                    return Seo.Languages.Window.WorldCracked;
                case WorldState.Restoring:
                    return Seo.Languages.Window.WorldRestoring;
                case WorldState.Supported:
                    return Seo.Languages.Window.WorldSupported;
                default:
                    return Seo.Languages.Window.WorldUnknow;
            }
        }
    }
}
