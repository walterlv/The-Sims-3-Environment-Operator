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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Seo.WindowParts
{
    /// <summary>
    /// WeatherSelector.xaml 的交互逻辑
    /// </summary>
    public partial class WeatherSelector : UserControl, ILanguage, IDisposable
    {
        public WeatherSelector()
        {
            InitializeComponent();
            LockWeights.IsChecked = Configs.LockWeights;
            Seo.Language.Register(this, Priority.Low);
        }

        public void Dispose()
        {
            Seo.Language.UnRegister(this);
        }

        public void LoadLanguage()
        {
            ChangeWeatherText.Text = Seo.Languages.Window.ChangeWeather;
            ToolTipService.SetToolTip(PickerBar, Seo.Languages.Window.WeatherDescription);
            ToolTipService.SetToolTip(ExpanderTip, Seo.Languages.Window.WeatherExpander);
            SetWeightsText.Text = Seo.Languages.Window.SetWeatherWeight;
            LockWeights.Content = Seo.Languages.Window.LockWeatherWeight;
        }

        #region 传递选择事件以隐藏子控件细节
        public event OnWeatherSelected Selected;
        private void WeatherPickBar_Selected(object sender, WeatherPickArgs e)
        { if (Selected != null) Selected(this, e); }
        #endregion

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            BeginStoryboard(FindResource("ExpandStory") as Storyboard);
            UpdateWeights();
        }

        public void UpdateWeights()
        {
            WeatherWeightOperator.Weights = EnvironmentOperator.Instance.GetWeatherWeights();
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            BeginStoryboard(FindResource("CollapseStory") as Storyboard);
        }

        private void WeatherWeightSetter_WeightChanged(object sender, WeightArgs e)
        {
            if (e.IsManual) EnvironmentOperator.Instance.SetWeatherWeights(e.Weights);
        }

        private void LockWeights_Checked(object sender, RoutedEventArgs e)
        {
            Configs.LockWeights = LockWeights.IsChecked == true;
        }
    }
}
