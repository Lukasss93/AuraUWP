using System;
using Windows.UI.Xaml.Data;

namespace Aura.Converters
{
    public class SubstractConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double size = (double)value;
            double tosub = Double.Parse((string)parameter);

            return size - tosub;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
