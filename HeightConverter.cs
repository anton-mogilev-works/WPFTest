using System;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace WPFTest
{
    [ValueConversion(typeof(double), typeof(double))]
    public class HeightConverter : IValueConverter
    {
        private static readonly double difference = 70;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double) value - difference;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double) value + difference;
        }
    }
}
