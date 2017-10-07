using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky
{
    public class Sky_CustomSky : SkyColor
    {
        public Sky_CustomSky()
        {
            DayColors.Add(new DayColor("SunColor", "太阳的颜色", "太阳光包括两个部分，一个是太阳自己，一个是它的光晕。这里是设置它自己的颜色。"));
            DayColors.Add(new DayColor("HaloColor", "光晕的颜色", "这里是设置太阳光光晕的部分。"));
            DayColors.Add(new DayColor("SkyDark", "天空暗的一半", "下面这两种是设置天空颜色的。天空的颜色包含两个部分，一个是“亮”的，一个是“暗”的，它们分别是太阳在天空不同位置照射天空导致的。当太阳在天空中某一位置时，那么天空的这一边便是“亮”的，天空的另一边便是“暗”的。一天之内，天空中“亮”与“暗”的颜色随着太阳的运动、日出日落、月亮的运动而变化着。"));
            DayColors.Add(new DayColor("SkyLight", "天空亮的一半", "这里是设置天空“亮”的那一部分。"));
            DayColors.Add(new DayColor("HorizonLight", "地平线的亮部分", "下面这两种是设置地平线颜色的。它们跟天空的颜色一样，白天、黑夜、日出日落也分为两个部分的颜色。最终的结果是，各个时刻天空和地平线的各种颜色交织混合，影响着你地图上每一个位置的颜色。"));
            DayColors.Add(new DayColor("HorizonDark", "地平线的暗部分", "这里是设置地平线“暗”的那一部分。"));
        }
    }
}
