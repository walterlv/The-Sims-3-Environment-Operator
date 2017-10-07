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
    public delegate void OnWeatherSelected(object sender, WeatherPickArgs e);

    public class WeatherPickArgs : EventArgs
    {
        public readonly Weathers Selected;

        public WeatherPickArgs(Weathers select)
        {
            this.Selected = select;
        }
    }

    /// <summary>
    /// WeatherPickBar.xaml 的交互逻辑
    /// </summary>
    public partial class WeatherPickBar : UserControl, ILanguage, IDisposable
    {
        List<StackPanel> WeatherControls = new List<StackPanel>();
        public WeatherPickBar()
        {
            InitializeComponent();
            Seo.Language.Register(this, Priority.Low);
            WeatherControls.Add(Clear);
            WeatherControls.Add(PartlyCloudy);
            WeatherControls.Add(Overcast);
            WeatherControls.Add(Storm);
            WeatherControls.Add(Custom);
        }

        public void Dispose()
        {
            Seo.Language.UnRegister(this);
        }

        public void LoadLanguage()
        {
            ClearText.Text = Seo.Languages.Operator.Clear;
            PartlyCloudyText.Text = Seo.Languages.Operator.PartlyCloudy;
            OvercastText.Text = Seo.Languages.Operator.Overcast;
            StormText.Text = Seo.Languages.Operator.Stormy;
            CustomText.Text = Seo.Languages.Operator.Custom;
        }

        public event OnWeatherSelected Selected;

        private int selectedIndex = -1;
        private int SelectedIndex
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
        public Weathers SelectedWeather
        {
            get
            {
                if (SelectedIndex == 0) return Weathers.Clear;
                else if (SelectedIndex == 1) return Weathers.PartlyCloudy;
                if (SelectedIndex == 2) return Weathers.Overcast;
                if (SelectedIndex == 3) return Weathers.Stormy;
                if (SelectedIndex == 4) return Weathers.Custom;
                else return Weathers.Custom;
            }
            set
            {
                if (value == Weathers.Clear) SelectedIndex = 0;
                else if (value == Weathers.PartlyCloudy) SelectedIndex = 1;
                else if (value == Weathers.Overcast) SelectedIndex = 2;
                else if (value == Weathers.Stormy) SelectedIndex = 3;
                else if (value == Weathers.Custom) SelectedIndex = 4;
            }
        }

        private void WeatherBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedIndex == WeatherBox.SelectedIndex) return;
            selectedIndex = WeatherBox.SelectedIndex;
            if (Selected != null) Selected(this, new WeatherPickArgs(SelectedWeather));
        }
    }
}
