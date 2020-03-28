using System;
using AuraUWP.Extensions;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace AuraUWP.Converters
{
    public class ShadeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Color color = ((Color)value);
            int percent = System.Convert.ToInt32((string)parameter);

            if(percent < -100 || percent > 100)
                return color.ToSolidColorBrush();

            return color.Shade(percent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
