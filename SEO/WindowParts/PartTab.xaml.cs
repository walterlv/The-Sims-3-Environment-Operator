﻿using System;
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
    /// <summary>
    /// PartTab.xaml 的交互逻辑
    /// </summary>
    public partial class PartTab : UserControl
    {
        public PartTab()
        {
            InitializeComponent();
        }

        private string text = String.Empty;
        public string Text
        {
            get { return text; }
            set { text = value; ContentText.Text = value; }
        }

        private static Brush SelectedBrush = new SolidColorBrush(Colors.Black);
        private static Brush UnselectedBrush = new SolidColorBrush(Colors.White);
        private bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected == value) return;
                isSelected = value;
                if (value) { BeginStoryboard(FindResource("SelectStory") as Storyboard); ContentText.Foreground = SelectedBrush; }
                else { BeginStoryboard(FindResource("UnselectStory") as Storyboard); ContentText.Foreground = UnselectedBrush; }
            }
        }

        private void BackgroundEle_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsSelected) BeginStoryboard(FindResource("HoverEnterStory") as Storyboard);
        }

        private void BackgroundEle_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsSelected) BeginStoryboard(FindResource("HoverLeaveStory") as Storyboard);
        }
    }
}
