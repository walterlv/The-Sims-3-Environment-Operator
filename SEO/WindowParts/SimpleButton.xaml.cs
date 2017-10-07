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
    public delegate void SimpleButton_Click(object sender, SimpleButtonArgs e);

    public class SimpleButtonArgs : EventArgs
    {
    }

    /// <summary>
    /// SimpleButton.xaml 的交互逻辑
    /// </summary>
    public partial class SimpleButton : UserControl, ILanguage, IDisposable
    {
        public SimpleButton()
        {
            InitializeComponent();
            Seo.Language.Register(this, Priority.Lowest);
        }

        public void Dispose()
        {
            Seo.Language.UnRegister(this);
        }

        public event SimpleButton_Click Click;

        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; InnerText.Text = value; }
        }

        private bool isCustomEnabled = true;
        public bool IsCustomEnabled
        {
            get { return isCustomEnabled; }
            set
            {
                if (CommonOperation.IsReadonlyMode && IsReadonlyDisabled) return;
                isCustomEnabled = value;
                if (value) { BeginStoryboard(FindResource("EnableStory") as Storyboard); DisableTip = null; }
                else { BeginStoryboard(FindResource("DisableStory") as Storyboard); }
            }
        }
        private string disableTip;
        public string DisableTip
        {
            get { return disableTip; }
            set
            {
                if (CommonOperation.IsReadonlyMode && IsReadonlyDisabled) return;
                if (IsCustomEnabled) disableTip = null;
                else disableTip = value;
                ToolTipService.SetToolTip(this, value);
            }
        }
        private bool isReadonlyDisabled = false;
        public bool IsReadonlyDisabled
        {
            get { return isReadonlyDisabled; }
            set
            {
                isReadonlyDisabled = value;
                if (CommonOperation.IsReadonlyMode)
                {
                    isCustomEnabled = false;
                    BeginStoryboard(FindResource("DisableStory") as Storyboard);
                    ToolTipService.SetToolTip(this, Seo.Languages.Window.ReadonlyModeTip);
                }
            }
        }
        public void LoadLanguage()
        {
            if (CommonOperation.IsReadonlyMode && IsReadonlyDisabled) ToolTipService.SetToolTip(this, Seo.Languages.Window.ReadonlyModeTip);
        }

        private bool isDown = false;

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IsCustomEnabled) BeginStoryboard(FindResource("EnterStory") as Storyboard);
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (IsCustomEnabled) BeginStoryboard(FindResource("LeaveStory") as Storyboard);
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CommonOperation.IsReadonlyMode && IsReadonlyDisabled)
            {
                CommonOperation.ShowReadonlyTip();
                return;
            }
            if (IsCustomEnabled)
            {
                BeginStoryboard(FindResource("DownStory") as Storyboard);
                isDown = true;
            }
        }

        private void Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsCustomEnabled)
            {
                BeginStoryboard(FindResource("UpStory") as Storyboard);
                if (isDown && Click != null) Click(this, new SimpleButtonArgs());
                isDown = false;
            }
        }
    }
}
