using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace AuraUWP.Extensions
{
    public static class ColorExtensions
    {
        public static Color GetColorDarker(this Color color, double factor)
        {
            // The factor value value cannot be greater than 1 or smaller than 0.
            // Otherwise return the original colour
            if(factor < 0 || factor > 1)
                return color;

            int r = (int)(factor * color.R);
            int g = (int)(factor * color.G);
            int b = (int)(factor * color.B);
            return Color.FromArgb(color.A, (byte)r, (byte)g, (byte)b);
        }

        public static Color GetColorLighter(this Color color, double factor)
        {
            // The factor value value cannot be greater than 1 or smaller than 0.
            // Otherwise return the original colour
            if(factor < 0 || factor > 1)
                return color;

            int r = (int)(factor * color.R + (1 - factor) * 255);
            int g = (int)(factor * color.G + (1 - factor) * 255);
            int b = (int)(factor * color.B + (1 - factor) * 255);
            return Color.FromArgb(color.A, (byte)r, (byte)g, (byte)b);
        }
    }
}
