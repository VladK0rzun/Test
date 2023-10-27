using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Test
{
    internal class YourViewModel
    {
        public SeriesCollection YourSeriesCollection { get; set; }

        public YourViewModel()
        {
            YourSeriesCollection = new SeriesCollection
        {
            new LineSeries
            {
                Title = "Price", // Змініть заголовок на те, що вам підходить
                Values = new ChartValues<double> { /* Тут додайте ваші дані для графіка */ }
            }
        };
        }
    }
}
