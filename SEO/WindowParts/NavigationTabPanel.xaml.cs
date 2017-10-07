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
        public readonly NavigationTab SelectedTab;
        public NavigationTabArgs(int index, NavigationTab tab)
        {
            SelectedIndex = index;
            SelectedTab = tab;
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
                selectedTab = value;
                selectedIndex = Children.IndexOf(value);
                foreach (NavigationTab tab in Children) tab.IsSelected = false;
                value.IsSelected = true;
                if (NavigationTabSelected != null)
                    NavigationTabSelected(this, new NavigationTabArgs(Children.IndexOf(value), value));
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
