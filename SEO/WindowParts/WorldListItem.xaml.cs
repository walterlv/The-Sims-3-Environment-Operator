using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Seo.WindowParts
{
    public delegate void OnCrackRequired(object sender, EventArgs e);
    public delegate void OnRestoreRequired(object sender, EventArgs e);

    /// <summary>
    /// WorldListItem.xaml 的交互逻辑
    /// </summary>
    public partial class WorldListItem : UserControl, ILanguage, IDisposable
    {
        Storyboard HoverStory, LeaveStory;
        ColorAnimation HoverAnim;
        public WorldListItem()
        {
            InitializeComponent();
            HoverStory = FindResource("HoverStory") as Storyboard;
            LeaveStory = FindResource("LeaveStory") as Storyboard;
            HoverAnim = HoverStory.Children[0] as ColorAnimation;
            Seo.Language.Register(this, Priority.Low);
        }

        public void Dispose()
        {
            Seo.Language.UnRegister(this);
        }

        public void LoadLanguage()
        {
            this.CrackButton.Text = Seo.Languages.Window.WorldCrack;
            this.RestoreButton.Text = Seo.Languages.Window.WorldRestore;
        }

        public int Index { get; set; }

        private string name;
        public string WorldName
        {
            get { return name; }
            set { NameText.Text = value; name = value; }
        }

        private WorldState state;
        public WorldState State
        {
            get { return state; }
            set
            {
                switch (value)
                {
                    case WorldState.Existed:
                        CrackButton.IsCustomEnabled = true;
                        RestoreButton.IsCustomEnabled = false;
                        break;
                    case WorldState.Cracked:
                        CrackButton.IsCustomEnabled = false;
                        RestoreButton.IsCustomEnabled = true;
                        break;
                    default:
                        CrackButton.IsCustomEnabled = false;
                        RestoreButton.IsCustomEnabled = false;
                        break;
                }
                StateText.Text = World.WorldStateToString(value);
                state = value;
            }
        }

        public void UpdateStateWords()
        {
            StateText.Text = World.WorldStateToString(state);
        }

        public event OnCrackRequired CrackRequired;
        public event OnRestoreRequired RestoreRequired;
        private void CrackButton_Click(object sender, SimpleButtonArgs e)
        {
            if (CrackRequired != null) CrackRequired(this, new EventArgs());
            BackGrid_MouseEnter(this, null);
        }
        private void RestoreButton_Click(object sender, SimpleButtonArgs e)
        {
            if (RestoreRequired != null) RestoreRequired(this, new EventArgs());
            BackGrid_MouseEnter(this, null);
        }

        private static Color SupportedBrush = Colors.LightGreen;
        private static Color NotExistedBrush = Colors.Gray;
        private static Color ExistedBrush = Color.FromArgb(0x7F, 0xFF, 0x00, 0x00);
        private static Color CrackingBrush = Colors.Orange;
        private static Color RestoringBrush = Colors.Orange;
        private static Color CrackedBrush = Colors.LightGreen;
        private static Color UnknowBrush = Colors.Transparent;
        private void BackGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            switch (State)
            {
                case WorldState.NotExisted:
                    HoverAnim.To = NotExistedBrush;
                    break;
                case WorldState.Existed:
                    HoverAnim.To = ExistedBrush;
                    break;
                case WorldState.Cracking:
                    HoverAnim.To = CrackingBrush;
                    break;
                case WorldState.Cracked:
                    HoverAnim.To = CrackedBrush;
                    break;
                case WorldState.Restoring:
                    HoverAnim.To = RestoringBrush;
                    break;
                case WorldState.Supported:
                    HoverAnim.To = SupportedBrush;
                    break;
                default:
                    HoverAnim.To = UnknowBrush;
                    break;
            }
            BeginStoryboard(HoverStory);
        }

        private void BackGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            BeginStoryboard(LeaveStory);
        }
    }
}
