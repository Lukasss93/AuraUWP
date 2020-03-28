using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace AuraUWP.Converters
{
    public class VisibilityToBooleanConverter : IValueConverter
    {
        private static readonly BooleanToVisibilityConverter BoolToVis = new BooleanToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return BoolToVis.ConvertBack(value, targetType, parameter, language);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return BoolToVis.Convert(value, targetType, parameter, language);
        }
    }
}
