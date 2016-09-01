using Aura.Imaging;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Aura.Extensions
{
    public static class ColorExtensions
    {
        public static SolidColorBrush ToSolidColorBrush(this Color color)
        {
            return new SolidColorBrush(color);
        }

        public static Color Shade(this Color color, int percent)
        {
            return Imaging.ColorHelper.Shade(color, percent);
        }
    }
}
