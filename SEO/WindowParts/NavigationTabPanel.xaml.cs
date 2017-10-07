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
    public delegate void OnNavigationTabSelected(object sender, NavigationTabArgs e);

    public class NavigationTabArgs : EventArgs
    {
        public readonly int SelectedIndex;
        public readonly int OldSelectedIndex;
        public readonly NavigationTab SelectedTab;
        public readonly NavigationTab OldSelectedTab;
        private bool cancel;
        public bool Cancel
        {
            get { return cancel; }
            set { cancel = value; }
        }
        public NavigationTabArgs(int index, int oldIndex, NavigationTab tab, NavigationTab oldTab)
        {
            SelectedIndex = index;
            OldSelectedIndex = oldIndex;
            SelectedTab = tab;
            OldSelectedTab = oldTab;
            cancel = false;
        }
    }

    /// <summary>
    /// NavigationTabPanel.xaml 的交互逻辑
    /// </summary>
    public partial class NavigationTabPanel : UserControl
    {
        public NavigationTabPanel()
        {
            InitializeComponent();
        }

        public event OnNavigationTabSelected NavigationTabSelected;

        public List<NavigationTab> Children = new List<NavigationTab>();
        private int selectedIndex = -1;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                SelectedTab = Children[value];
            }
        }
        private NavigationTab selectedTab;
        public NavigationTab SelectedTab
        {
            get { return selectedTab; }
            set
            {
                if (selectedTab == value) return;
                selectedIndex = Children.IndexOf(value);
                bool isCancel = false;
                if (NavigationTabSelected != null)
                {
                    NavigationTabArgs arg = new NavigationTabArgs(Children.IndexOf(value), Children.IndexOf(selectedTab), value, selectedTab);
                    NavigationTabSelected(this, arg);
                    isCancel = arg.Cancel;
                }
                if (!isCancel)
                {
                    if (selectedTab != null) selectedTab.IsSelected = false;
                    if (value != null) value.IsSelected = true;
                    selectedTab = value;
                }
            }
        }

        public void AddChild(NavigationTab tab)
        {
            tab.MouseLeftButtonDown += tab_MouseLeftButtonDown;
            ContentPanel.Children.Add(tab);
            Children.Add(tab);
        }

        public void AddChild(string text)
        {
            NavigationTab naviTab = new NavigationTab();
            naviTab.Text = text;
            AddChild(naviTab);
        }

        private void tab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedTab = (NavigationTab)sender;
        }
    }
}
