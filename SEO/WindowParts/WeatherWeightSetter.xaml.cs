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
    public delegate void OnWeightChanged(object sender, WeightArgs e);

    public class WeightArgs : EventArgs
    {
        public readonly bool IsManual;
        public readonly double[] Weights;
        public WeightArgs(bool man, double[] weights)
        {
            IsManual = man;
            Weights = weights;
        }
    }

    /// <summary>
    /// WeatherWeightSetter.xaml 的交互逻辑
    /// </summary>
    public partial class WeatherWeightSetter : UserControl, ILanguage, IDisposable
    {
        SimpleSlider[] sliders = new SimpleSlider[5];
        private TextBlock[] valueTexts = new TextBlock[5];
        private double[] weights = new double[5];
        public double[] Weights
        {
            get { return weights; }
            set { weights = value; RecomputeWeight(); DrawSliders(); DrawPercent(); }
        }
        public WeatherWeightSetter()
        {
            InitializeComponent();
            sliders[0] = Slider0;
            sliders[1] = Slider1;
            sliders[2] = Slider2;
            sliders[3] = Slider3;
            sliders[4] = Slider4;
            valueTexts[0] = Value0;
            valueTexts[1] = Value1;
            valueTexts[2] = Value2;
            valueTexts[3] = Value3;
            valueTexts[4] = Value4;
            Seo.Language.Register(this, Priority.Low);
        }

        public void Dispose()
        {
            Seo.Language.UnRegister(this);
        }

        public void LoadLanguage()
        {
            Name0.Text = Seo.Languages.Operator.Clear;
            Name1.Text = Seo.Languages.Operator.PartlyCloudy;
            Name2.Text = Seo.Languages.Operator.Overcast;
            Name3.Text = Seo.Languages.Operator.Stormy;
            Name4.Text = Seo.Languages.Operator.Custom;
        }

        public event OnWeightChanged WeightChanged;
        private void SimpleSlider_ValueChanged(object sender, SliderValueArgs e)
        {
            if (e.IsManual) { DrawPercent(); }
            if (WeightChanged != null) WeightChanged(this, new WeightArgs(e.IsManual, weights));
        }

        private void DrawSliders()
        {
            for (int i = 0; i < sliders.Length; i++)
            {
                sliders[i].Value = weights[i];
            }
        }
        private void DrawPercent()
        {
            for (int i = 0; i < weights.Length; i++) weights[i] = sliders[i].Value;
            RecomputeWeight();
            for (int i = 0; i < valueTexts.Length; i++)
                valueTexts[i].Text = weights[i].ToString("0%");
        }
        private void RecomputeWeight()
        {
            double total = 0.0;
            foreach (double d in weights) total += d;
            if (total <= 0)
            {
                for (int i = 0; i < valueTexts.Length; i++) weights[i] = 0.2;
            }
            else
            {
                for (int i = 0; i < valueTexts.Length; i++) weights[i] = weights[i] / total;
            }
        }
    }
}
