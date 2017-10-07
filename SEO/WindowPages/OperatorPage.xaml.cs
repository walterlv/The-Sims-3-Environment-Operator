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
using Seo.Actions;
using Seo.WindowParts;

namespace Seo.WindowPages
{
    /// <summary>
    /// OperatorPage.xaml 的交互逻辑
    /// </summary>
    public partial class OperatorPage : Page, PageNavigation, ILanguage
    {
        public OperatorPage()
        {
            InitializeComponent();
            foreach (ColorAssemblies color in SkyColor.ColorAssemblyArray) PartTabsPanel.AddChild(SkyColor.ColorAssemblyToString(color));
            Seo.Language.Register(this, Priority.Lowest, false);
        }

        ~OperatorPage()
        {
            Seo.Language.UnRegister(this);
        }

        public bool NavigationIn()
        {
            if (!EnvironmentOperator.Instance.IsEditing) LoadContent();
            return true;
        }

        public bool NavigationOut()
        {
            return true;
        }

        private void LoadContent()
        {
            if (EnvironmentOperator.Instance.IsReady)
            {
                EnvironmentOperator.Instance.IsEditing = true;
                if (EnvironmentOperator.Instance.GetWeather(EditingWeather).IsError)
                {
                    WErrorMsgText.Visibility = System.Windows.Visibility.Visible;
                    PartTabsPanel.SelectedIndex = -1;
                }
                else
                {
                    if (PartTabsPanel.SelectedIndex < 0) PartTabsPanel.SelectedIndex = 0;
                }
            }
        }

        public void LoadLanguage()
        {
            this.Title = Seo.Languages.Window.OperatorPage;
            for (int i = 0; i < SkyColor.ColorAssemblyArray.Length; i++)
                PartTabsPanel.Children[i].Text = SkyColor.ColorAssemblyToString(SkyColor.ColorAssemblyArray[i]);
        }

        BarListAndAction BarActionList;

        private Weathers editingWeather = Weather.WeatherArray[0];
        private Weathers EditingWeather
        {
            get { return editingWeather; }
            set
            {
                editingWeather = value;
                LoadContent();
                EditingColor = EditingColor;
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
                    if (!skyColor.IsReady) skyColor.Prepare();
                    BarActionList = GetBarList(skyColor);
                    DayColorBarPanel.Children.Clear();
                    foreach (DayColorBar bar in BarActionList.BarList) DayColorBarPanel.Children.Add(bar);
                    CErrorMsgText.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }

        private void WeatherSelector_Selected(object sender, WeatherPickArgs e)
        {
            EditingWeather = e.Selected;
        }

        private void PartTab_Selected(object sender, WindowParts.PartTabArgs e)
        {
            EditingColor = SkyColor.ColorAssemblyArray[e.SelectedIndex];
        }

        private void bar_ColorChanged(object sender, ColorBarArgs e)
        {
        }

        #region 绑定颜色组与颜色条列表
        private Dictionary<SkyColor, BarListAndAction> SkyColorBarKeyValue = new Dictionary<SkyColor, BarListAndAction>();
        public BarListAndAction GetBarList(SkyColor skyColor)
        {
            BarListAndAction barList = new BarListAndAction();
            if (SkyColorBarKeyValue.ContainsKey(skyColor))
            {
                bool success = SkyColorBarKeyValue.TryGetValue(skyColor, out barList);
                if (success) return barList;
            }
            barList.BarList = new List<DayColorBar>();
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
                barList.BarList.Add(bar);
            }
            barList.Action = new DayColorAction();
            SkyColorBarKeyValue.Add(skyColor, barList);
            return barList;
        }
        public class BarListAndAction
        {
            public DayColorAction Action;
            public List<DayColorBar> BarList;
        }
        #endregion
    }
}
