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
using Seo.WindowParts;

namespace Seo.WindowPages
{
    /// <summary>
    /// OperatorPage.xaml 的交互逻辑
    /// </summary>
    public partial class OperatorPage : Page, PageNavigation, ILanguage, IDisposable
    {
        public OperatorPage()
        {
            InitializeComponent();
            foreach (ColorAssemblies color in SkyColor.ColorAssemblyArray) PartTabsPanel.AddChild(SkyColor.ColorAssemblyToString(color));
            Seo.Language.Register(this, Priority.Lowest);
        }

        public void Dispose()
        {
            Seo.Language.UnRegister(this);
        }

        public bool NavigationIn()
        {
            LoadContent();
            return true;
        }

        public bool NavigationOut()
        {
            return true;
        }

        private void LoadContent()
        {
            EnvironmentOperator.Instance.IsEditing = true;
            if (EnvironmentOperator.Instance.GetWeather(EditingWeather).IsError)
            {
                WErrorMsgText.Visibility = System.Windows.Visibility.Visible;
                PartTabsPanel.SelectedIndex = 0;
            }
            else
            {
                if (PartTabsPanel.SelectedIndex < 0) PartTabsPanel.SelectedIndex = 0;
            }
            EditingColor = editingColor;
            Selector.UpdateWeights();
        }

        public void LoadLanguage()
        {
            this.Title = Seo.Languages.Window.OperatorPage;
            ApplyButton.Text = Seo.Languages.Window.Save;
            DefaultButton.Text = Seo.Languages.Window.Default;
            for (int i = 0; i < SkyColor.ColorAssemblyArray.Length; i++)
                PartTabsPanel.Children[i].Text = SkyColor.ColorAssemblyToString(SkyColor.ColorAssemblyArray[i]);
        }

        List<DayColorBar> BarList;

        private Weathers editingWeather = Weather.WeatherArray[0];
        private Weathers EditingWeather
        {
            get { return editingWeather; }
            set
            {
                editingWeather = value;
                LoadContent();
            }
        }
        private ColorAssemblies editingColor = SkyColor.ColorAssemblyArray[0];
        private ColorAssemblies EditingColor
        {
            get { return editingColor; }
            set
            {
                editingColor = value;
                SkyColor skyColor = EnvironmentOperator.Instance.GetSkyColor(EditingWeather, value);
                if (skyColor.IsError)
                {
                    DayColorBarPanel.Children.Clear();
                    CErrorMsgText.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    DescriptionText.Text = skyColor.ColorDescription;
                    BarList = GetBarList(skyColor);
                    DayColorBarPanel.Children.Clear();
                    foreach (DayColorBar bar in BarList) DayColorBarPanel.Children.Add(bar);
                    if (skyColor.IsUpdateNeeded)
                    {
                        skyColor.IsUpdateNeeded = false;
                        foreach (DayColorBar bar in BarList) bar.Draw();
                    }
                    CErrorMsgText.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }

        private void WeatherSelector_Selected(object sender, WeatherPickArgs e)
        {
            EditingWeather = e.Selected;
            DayColorScroll.ScrollToHome();
        }

        private void PartTab_Selected(object sender, WindowParts.PartTabArgs e)
        {
            EditingColor = SkyColor.ColorAssemblyArray[e.SelectedIndex];
            DayColorScroll.ScrollToHome();
        }

        private void bar_ColorChanged(object sender, ColorBarArgs e)
        {
        }

        #region 绑定颜色组与颜色条列表
        private Dictionary<SkyColor, List<DayColorBar>> SkyColorBarKeyValue = new Dictionary<SkyColor, List<DayColorBar>>();
        public List<DayColorBar> GetBarList(SkyColor skyColor)
        {
            List<DayColorBar> barList = new List<DayColorBar>();
            if (SkyColorBarKeyValue.ContainsKey(skyColor))
            {
                bool success = SkyColorBarKeyValue.TryGetValue(skyColor, out barList);
                if (success) return barList;
            }
            barList = new List<DayColorBar>();
            DayColorBar bar;
            foreach (DayColor dayColor in skyColor.DayColors)
            {
                bar = new DayColorBar();
                bar.Width = 522;
                bar.OneDayColor = dayColor;
                bar.DayColorGroup = skyColor;
                bar.Title = dayColor.ColorName;
                bar.Description = dayColor.ColorDescription;
                bar.ColorChanged += bar_ColorChanged;
                barList.Add(bar);
            }
            SkyColorBarKeyValue.Add(skyColor, barList);
            return barList;
        }
        #endregion

        private void ApplyButton_Click(object sender, SimpleButtonArgs e)
        {
            ApplyButton.IsCustomEnabled = false;
            DefaultButton.IsCustomEnabled = false;
            StatusBar.Show(Status.Progress, Seo.Languages.Information.SaveingEnvironment);
            EnvironmentOperator.Instance.SaveCompleted += Instance_SaveCompleted;
            EnvironmentOperator.Instance.SaveAsync();
        }

        private void Instance_SaveCompleted(object sender, OperatorArgs e)
        {
            ApplyButton.IsCustomEnabled = true;
            DefaultButton.IsCustomEnabled = true;
            if (e.IsSuccess) StatusBar.Show(Status.Success, Seo.Languages.Information.SaveEnvironmentOkay, 3000);
            else StatusBar.Show(Status.Error, Seo.Languages.Information.SaveEnvironmentFailed, 5000);
        }

        private void DefaultButton_Click(object sender, SimpleButtonArgs e)
        {
            MessageBoxResult result = MessageBox.Show(String.Format(Seo.Languages.Information.DefaultEnvironmentContent,
                    Weather.WeatherToString(EditingWeather), SkyColor.ColorAssemblyToString(EditingColor)),
                    Seo.Languages.Information.DefaultEnvironmentTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;
            bool success = EnvironmentOperator.Instance.SetToDefault(EditingWeather, EditingColor);
            if (success)
            {
                StatusBar.Show(Status.Success, String.Format(Seo.Languages.Information.DefaultEnvironmentOkay,
                    Weather.WeatherToString(EditingWeather), SkyColor.ColorAssemblyToString(EditingColor)), 5000);
                EditingColor = EditingColor;
            }
            else
                StatusBar.Show(Status.Error, String.Format(Seo.Languages.Information.DefaultEnvironmentFailed,
                    Weather.WeatherToName(EditingWeather), SkyColor.ColorAssemblyToName(EditingColor)), 8000);
        }
    }
}
