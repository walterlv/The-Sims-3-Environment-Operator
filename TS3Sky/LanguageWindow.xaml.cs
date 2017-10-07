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
using System.Windows.Shapes;

namespace TS3Sky
{
    /// <summary>
    /// LanguageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LanguageWindow : Window
    {
        public LanguageWindow()
        {
            InitializeComponent();
        }

        public string[] Languages;
        public string SelectedLanguage;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = TS3Sky.Language.Application.Name;
            ChooseLanguageText.Text = TS3Sky.Language.Application.ChooseLanguage;
            OKButton.Content = TS3Sky.Language.Application.OK;
            if (Languages != null)
            {
                foreach (string lan in Languages)
                {
                    LanguageBox.Items.Add(TS3Sky.Language.LanguageManager.GetLanguageName(lan));
                }
            }
            LanguageBox.SelectedItem = TS3Sky.Language.LanguageManager.GetLanguageName(SelectedLanguage);
        }

        private void LanguageBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedLanguage = Languages[LanguageBox.SelectedIndex];
            OKButton.IsEnabled = true;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
