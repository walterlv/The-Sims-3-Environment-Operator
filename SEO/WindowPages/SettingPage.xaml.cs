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

namespace Seo.WindowPages
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page, PageNavigation, ILanguage, IDisposable
    {
        bool isLoaded = false;
        Dictionary<string, string> Languages;
        public SettingPage()
        {
            InitializeComponent();
            Seo.Language.Register(this, Priority.Low);
            Languages = Seo.Language.GetLanguages();
            int i = 0;
            foreach (KeyValuePair<string, string> pair in Languages)
            {
                LanguageBox.Items.Add(pair.Value);
                if (pair.Key == Seo.Language.Local) LanguageBox.SelectedIndex = i;
                i++;
            }
            if (Configs.ForeColor != null) CustomColorCheckBox.IsChecked = true;
            else CustomColorCheckBox.Visibility = System.Windows.Visibility.Collapsed;
            AutoUpdateCheckBox.IsChecked = Configs.AutoUpdate;
            AeroGlassCheckBox.IsChecked = Configs.GlassWindow;
            BackgroundImageCheckBox.IsChecked = Configs.UseBackgroundImage;
            isLoaded = true;
        }

        public void Dispose()
        {
            Seo.Language.UnRegister(this);
        }

        public bool NavigationIn()
        {
            return true;
        }

        public bool NavigationOut()
        {
            return true;
        }

        public void LoadLanguage()
        {
            SelectLanguageText.Text = Seo.Languages.Window.SelectLanguage;
            ProgramHeader.Header = Seo.Languages.Window.ProgramHeader;
            AutoUpdateCheckBox.Content = Seo.Languages.Window.AutoUpdate;
            VisualHeader.Header = Seo.Languages.Window.VisualHeader;
            CustomColorCheckBox.Content = Seo.Languages.Window.CustomForeColor;
            AeroGlassCheckBox.Content = Seo.Languages.Window.AeroGlass;
            BackgroundImageCheckBox.Content = Seo.Languages.Window.BackgroundImage;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void LanguageBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLoaded)
            {
                Configs.Local = Languages.ElementAt(LanguageBox.SelectedIndex).Key;
                Seo.Language.LoadLanguage(Configs.Local);
                Seo.Language.LoadLanguageOfRegList();
            }
        }

        private void AutoUpdateCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                Configs.AutoUpdate = AutoUpdateCheckBox.IsChecked == true;
            }
        }

        private void AeroGlassCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                bool c = AeroGlassCheckBox.IsChecked == true;
                MainWindow.Current.AeroGlass = c;
                BackgroundImageCheckBox.IsEnabled = !c;
                Configs.GlassWindow = c;
            }
        }

        private void BackgroundImageCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                if (BackgroundImageCheckBox.IsChecked == true)
                {
                    System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();
                    open.Title = Seo.Languages.Window.SelectImageTitle;
                    open.Filter = String.Format("{0}|*.png;*.bmp;*.jpg|PNG|*.png|BMP|*.bmp|JPG|*.jpg|{1}|*.*", Seo.Languages.Window.AllImage, Seo.Languages.Window.AllFiles);
                    open.AddExtension = true;
                    open.AutoUpgradeEnabled = true;
                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Configs.BackgroundImage = open.FileName;
                        MainWindow.Current.AeroGlass = false;
                    }
                    else
                    {
                        BackgroundImageCheckBox.IsChecked = false;
                    }
                }
                else
                {
                    Configs.BackgroundImage = null;
                    MainWindow.Current.AeroGlass = false;
                }
            }
        }

        private void CustomColorCheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                if (CustomColorCheckBox.IsChecked == true)
                {
                    System.Windows.Forms.ColorDialog cd = Seo.WindowParts.DayColorBar.colorDialog;
                    if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Color c = Color.FromArgb(cd.Color.A, cd.Color.R, cd.Color.G, cd.Color.B);
                        App.This.UpdateColors(c);
                        Configs.SetForeColor(c.A, c.R, c.G, c.B);
                    }
                }
            }
        }
    }
}
