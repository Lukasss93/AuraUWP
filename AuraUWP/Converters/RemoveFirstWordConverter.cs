using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AuraUWP.Converters
{
    public class RemoveFirstWordConverter : IValueConverter
    {
        /// <summary>
        /// Rimuove la prima parola da una stringa.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is string)
            {
                var thestring = ((string)value);
                var list = thestring.Split(' ').ToList();
                list.RemoveAt(0);

                return String.Join(" ", list.ToArray());
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
