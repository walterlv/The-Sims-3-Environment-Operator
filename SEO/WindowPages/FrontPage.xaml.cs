using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Seo.WindowParts;

namespace Seo.WindowPages
{
    /// <summary>
    /// FrontPage.xaml 的交互逻辑
    /// </summary>
    public partial class FrontPage : Page, PageNavigation, ILanguage, IDisposable
    {
        #region 初始化工作
        WorldList Worlds = WorldList.Instance;
        public FrontPage()
        {
            InitializeComponent();
            EnvironmentOperator.Instance.Prepare();
            LoadWorlds();
            Seo.Language.Register(this, Priority.Low);
            BeginStoryboard(FindResource("LoadStory") as Storyboard);
        }
        public void Dispose() { Seo.Language.UnRegister(this); }
        public bool NavigationIn() { return true; }
        public bool NavigationOut() { return true; }
        public void LoadLanguage()
        {
            this.Title = Seo.Languages.Window.FrontPage;
            this.TitleText.Text = Seo.Language.ApplicationTitle;
            this.DescriptionText.Text = Seo.Languages.Window.ApplicationAd;
            this.WorldListText.Text = Seo.Languages.Window.WorldList;
            for (int i = 0; i < WorldList.Instance.Count; i++)
            {
                WorldItems[i].WorldName = Worlds[i].WorldName;
                WorldItems[i].UpdateStateWords();
            }
        }
        private void Storyboard_Completed(object sender, EventArgs e)
        {
            ContentPanel.Children.Remove(CollRect0);
            ContentPanel.Children.Remove(CollRect1);
            ContentPanel.Children.Remove(CollRect2);
            ContentPanel.Children.Remove(CollRect3);
        }
        #endregion

        #region 处理世界
        WorldListItem[] WorldItems;
        private void LoadWorlds()
        {
            // 检查我的文档文件夹
            string doc = Configs.DocumentName;
            if (doc == null)
            {
                if (Configs.HasMultiDocument == true)
                {
                    DocumentWindow dw = new DocumentWindow();
                    dw.Owner = MainWindow.Current;
                    dw.ShowDialog();
                    Configs.DocumentName = dw.DocName;
                }
            }
            // 开始加载世界文件
            Worlds.Initialize();
            Worlds.WorldLoaded += Worlds_WorldLoaded;
            Worlds.WorldLoadCompleted += Worlds_WorldLoadCompleted;
            WorldItems = new WorldListItem[WorldList.Instance.Count];
            for (int i = 0; i < WorldList.Instance.Count; i++)
            {
                WorldItems[i] = new WorldListItem();
                WorldItems[i].Index = i;
                WorldItems[i].CrackRequired += FrontPage_CrackRequired;
                WorldItems[i].RestoreRequired += FrontPage_RestoreRequired;
                WorldItems[i].WorldName = Worlds[i].WorldName;
                WorldPanel.Children.Add(WorldItems[i]);
            }
            Worlds.StartLoad();
        }

        private void Worlds_WorldLoaded(object sender, WorldLoadArgs e)
        {
            WorldItems[e.Index].State = Worlds[e.Index].State;
        }

        private void Worlds_WorldLoadCompleted(object sender, EventArgs e)
        {
        }

        private void FrontPage_CrackRequired(object sender, EventArgs e)
        {
            int index = (sender as WorldListItem).Index;
            if (WorldItems[index].State == WorldState.Existed)
            {
                WorldItems[index].State = WorldState.Cracking;
                StatusBar.Show(Status.Progress, String.Format(Seo.Languages.Information.WorldCracking, Worlds[index].WorldName));
                Worlds[index].Cracked += FrontPage_Cracked;
                Worlds[index].CrackAsync(index);
            }
        }

        void FrontPage_Cracked(object sender, WorldEventArgs e)
        {
            WorldItems[e.Index].State = Worlds[e.Index].State;
            StatusBar.Show(Status.Success, String.Format(Seo.Languages.Information.WorldCracked, Worlds[e.Index].WorldName), 5000);
        }

        private void FrontPage_RestoreRequired(object sender, EventArgs e)
        {
            int index = (sender as WorldListItem).Index;
            if (WorldItems[index].State == WorldState.Cracked)
            {
                WorldItems[index].State = WorldState.Restoring;
                StatusBar.Show(Status.Progress, String.Format(Seo.Languages.Information.WorldRestoring, Worlds[index].WorldName));
                Worlds[index].Restored += FrontPage_Restored;
                Worlds[index].RestoreAsync(index);
            }
        }

        void FrontPage_Restored(object sender, WorldEventArgs e)
        {
            WorldItems[e.Index].State = Worlds[e.Index].State;
            StatusBar.Show(Status.Success, String.Format(Seo.Languages.Information.WorldRestored, Worlds[e.Index].WorldName), 5000);
        }
        #endregion
    }
}
