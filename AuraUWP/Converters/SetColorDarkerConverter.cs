using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace AuraUWP.Converters
{
    public class SetColorDarkerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Color color = ((SolidColorBrush)value).Color;
            Int32 param = Int32.Parse((string)parameter);
            if(param < 0 || param > 100)
            {
                throw new Exception("The parameter must be betweeen 0 and 100.");
            }
            double factor = Math.Round((double)param/100, 1);

            // The factor value value cannot be greater than 1 or smaller than 0.
            // Otherwise return the original colour
            if(factor < 0 || factor > 1)
                return color;

            int r = (int)(factor * color.R);
            int g = (int)(factor * color.G);
            int b = (int)(factor * color.B);

            return new SolidColorBrush(Color.FromArgb(color.A, (byte)r, (byte)g, (byte)b));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (SolidColorBrush)value;
        }
    }
}
