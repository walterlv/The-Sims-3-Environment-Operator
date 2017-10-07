using System;
using System.Collections.Generic;
using System.IO;
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
    public delegate void OnClick(object sender, EventArgs e);
    /// <summary>
    /// EnviFileItem.xaml 的交互逻辑
    /// </summary>
    public partial class EnviFileItem : UserControl
    {
        public EnviFileItem()
        {
            InitializeComponent();
            BeginStoryboard(FindResource("LoadingStory") as Storyboard);
        }

        private EnviFile enviFileShow = null;
        public EnviFile EnviFileShow
        {
            get { return enviFileShow; }
            set
            {
                enviFileShow = value;
                EnviName = String.Format(Seo.Languages.Operator.EnvironmentName, value.Name);
                EnviCreator = String.Format(Seo.Languages.Operator.EnvironmentCreator, value.Creator);
                EnviDescription = String.Format(Seo.Languages.Operator.EnvironmentDescription, value.Description);
                EnviImage = value.ImageFile;
            }
        }

        private string name;
        public string EnviName
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                EnviNameText.Text = value;
            }
        }
        private string creator;
        public string EnviCreator
        {
            get
            {
                return creator;
            }
            set
            {
                creator = value;
                EnviCreatorText.Text = value;
            }
        }
        private string description;
        public string EnviDescription
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                EnviDescriptionText.Text = value;
            }
        }

        private string image;
        public string EnviImage
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                try { PreviewImage.Source = CommonOperation.GetBitmapImage(value); }
                catch { }
            }
        }

        private bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected == value) return;
                isSelected = value;
                if (value) BeginStoryboard(FindResource("SelectStory") as Storyboard);
                else BeginStoryboard(FindResource("UnselectStory") as Storyboard);
            }
        }

        private void Item_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsSelected) BeginStoryboard(FindResource("HoverEnterStory") as Storyboard);
        }

        private void Item_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsSelected) BeginStoryboard(FindResource("HoverLeaveStory") as Storyboard);
        }

        public event OnClick Click;
        private void BackgroundEle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Click != null) Click(this, new EventArgs());
        }
    }
}
