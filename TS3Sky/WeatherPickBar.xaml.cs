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

namespace TS3Sky
{
    /// <summary>
    /// WeatherPickBar.xaml 的交互逻辑
    /// </summary>
    public partial class WeatherPickBar : UserControl
    {
        List<StackPanel> WeatherControls = new List<StackPanel>();
        public WeatherPickBar()
        {
            InitializeComponent();
            ClearLabel.Content = TS3Sky.Language.Weather.Clear;
            PartlyCloudyLabel.Content = TS3Sky.Language.Weather.PartlyCloudy;
            OvercastLabel.Content = TS3Sky.Language.Weather.Overcast;
            StormLabel.Content = TS3Sky.Language.Weather.Storm;
            CustomLabel.Content = TS3Sky.Language.Weather.Custom;
            WeatherControls.Add(Clear);
            WeatherControls.Add(PartlyCloudy);
            WeatherControls.Add(Overcast);
            WeatherControls.Add(Storm);
            WeatherControls.Add(Custom);
        }

        public MainWindow Owner;

        private int selectedIndex = -1;
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                WeatherBox.SelectedIndex = value;
                selectedIndex = value;
            }
        }
        public Weather SelectedWeather
        {
            get
            {
                if (SelectedIndex == 0) return Weather.Clear;
                else if (SelectedIndex == 1) return Weather.PartlyCloudy;
                if (SelectedIndex == 2) return Weather.Overcast;
                if (SelectedIndex == 3) return Weather.Stormy;
                if (SelectedIndex == 4) return Weather.Custom;
                else return Weather.Custom;
            }
            set
            {
                if (value == Weather.Clear) SelectedIndex = 0;
                else if (value == Weather.PartlyCloudy) SelectedIndex = 1;
                else if (value == Weather.Overcast) SelectedIndex = 2;
                else if (value == Weather.Stormy) SelectedIndex = 3;
                else if (value == Weather.Custom) SelectedIndex = 4;
            }
        }

        public void CollapseWeather()
        {
            Overcast.Visibility = Visibility.Collapsed;
            Storm.Visibility = Visibility.Collapsed;
            Custom.Visibility = Visibility.Collapsed;
        }

        private void WeatherBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedIndex == WeatherBox.SelectedIndex) return;
            selectedIndex = WeatherBox.SelectedIndex;
            if (Owner != null) Owner.WeatherSelectionChanged();
        }
    }
}
