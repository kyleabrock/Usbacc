using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Usbacc.UI.Utils
{
    [ValueConversion(typeof(string), typeof(Brush))]
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = value as string;
            if (result != null)
            {
                var convertFromString = ColorConverter.ConvertFromString(result);
                if (convertFromString != null)
                {
                    var color = (Color) convertFromString;
                    return new SolidColorBrush(color);
                }
            }
            
            return new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }
    }
}
