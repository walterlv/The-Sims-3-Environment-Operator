using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TS3Sky
{
    public class Sky_OvercastSea : SkyColor
    {
        public Sky_OvercastSea()
        {
            DayColors.Add(new DayColor("WaterColor", "水的颜色", "在不叠加上太阳光和天空反射光的情况下水的颜色。"));
            DayColors.Add(new DayColor("SunMoonColor", "日月倒影的颜色", "太阳或者月亮在水中反射的颜色。因为阴天天空并没有月亮，所以就不要让水反射它的光。"));
        }
    }
}
