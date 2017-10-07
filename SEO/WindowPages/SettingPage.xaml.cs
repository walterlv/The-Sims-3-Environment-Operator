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
    public partial class SettingPage : Page, PageNavigation, ILanguage
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
            isLoaded = true;
        }

        ~SettingPage()
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
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void LanguageBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLoaded)
            {
                Seo.Language.LoadLanguage(Languages.ElementAt(LanguageBox.SelectedIndex).Key);
                Seo.Language.LoadLanguageOfRegList();
            }
        }
    }
}
