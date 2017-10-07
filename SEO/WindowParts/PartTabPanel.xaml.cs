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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Seo.WindowParts
{
    public delegate void OnPartTabSelected(object sender, PartTabArgs e);

    public class PartTabArgs : EventArgs
    {
        public readonly int SelectedIndex;
        public readonly PartTab SelectedTab;
        public readonly bool Changed;
        public PartTabArgs(int index, PartTab tab, bool changed)
        {
            SelectedIndex = index;
            SelectedTab = tab;
            Changed = changed;
        }
    }

    /// <summary>
    /// PartTabPanel.xaml 的交互逻辑
    /// </summary>
    public partial class PartTabPanel : UserControl
    {
        public PartTabPanel()
        {
            InitializeComponent();
        }

        public event OnPartTabSelected Selected;

        public List<PartTab> Children = new List<PartTab>();
        private int selectedIndex = -1;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                if (value < 0) SelectedTab = null;
                else SelectedTab = Children[value];
            }
        }
        private PartTab selectedTab;
        public PartTab SelectedTab
        {
            get { return selectedTab; }
            set
            {
                if (selectedTab == value) { Selected(this, new PartTabArgs(Children.IndexOf(value), value, false)); return; }
                selectedTab = value;
                selectedIndex = Children.IndexOf(value);
                foreach (PartTab tab in Children) tab.IsSelected = false;
                value.IsSelected = true;
                if (SelectedIndex >= 0 && Selected != null)
                    Selected(this, new PartTabArgs(Children.IndexOf(value), value, true));
            }
        }

        public void AddChild(PartTab tab)
        {
            tab.MouseLeftButtonDown += tab_MouseLeftButtonDown;
            ContentPanel.ColumnDefinitions.Add(new ColumnDefinition());
            Grid.SetColumn(tab, Children.Count);
            ContentPanel.Children.Add(tab);
            Children.Add(tab);
        }

        public void AddChild(string text)
        {
            PartTab naviTab = new PartTab();
            naviTab.Text = text;
            AddChild(naviTab);
        }

        private void tab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedTab = (PartTab)sender;
        }
    }
}
