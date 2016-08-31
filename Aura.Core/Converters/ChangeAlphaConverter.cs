using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Aura.Converters
{
    public class ChangeAlphaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            SolidColorBrush color = (SolidColorBrush)value;
            Double param = Double.Parse((string)parameter);
            if(param < 0 || param > 100)
            {
                throw new Exception("The parameter must be betweeen 0 and 100.");
            }
            byte alpha = System.Convert.ToByte(Math.Floor((255 * param) / 100));

            return new SolidColorBrush(Color.FromArgb(alpha, color.Color.R, color.Color.G, color.Color.B));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            SolidColorBrush color = (SolidColorBrush)value;
            return new SolidColorBrush(Color.FromArgb(255, color.Color.R, color.Color.G, color.Color.B));
        }
    }
}

