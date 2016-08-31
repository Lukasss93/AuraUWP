using System;
using System.Globalization;
using System.Reflection;
using Aura.Extensions;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Aura.Imaging
{
    public class ColorsHelper
    {
        public static Color Shade(Color color, int percent)
        {
            if(percent < -100 || percent > 100)
                throw new Exception("Percent parameter must be between -100 and 100");

            if(percent == 0)
                return color;

            double factor = percent / 100;

            int r, g, b;

            if(factor<0)//darker
            {
                factor = factor * -1;
                factor = 1 - factor;

                r = Convert.ToInt32(factor * color.R);
                g = Convert.ToInt32(factor * color.G);
                b = Convert.ToInt32(factor * color.B);
            }
            else//lighter
            {
                factor = 1 - factor;
                r = Convert.ToInt32(factor * color.R + (1 - factor) * 255);
                g = Convert.ToInt32(factor * color.G + (1 - factor) * 255);
                b = Convert.ToInt32(factor * color.B + (1 - factor) * 255);
            }

            return Color.FromArgb(255, (byte)r, (byte)g, (byte)b);
        }

        public static Color GetForegroundContrast(Color background)
        {
            var r = background.R;
            var g = background.G;
            var b = background.B;
            var yiq = ((r * 299) + (g * 587) + (b * 114)) / 1000;
            return (yiq >= 128) ? Colors.Black : Colors.White;
        }
        
        public static Color Mix(Color foreground, double alpha, Color background)
        {
            var diff = 1.0 - alpha;
            var color = Color.FromArgb(foreground.A,
                (byte)(foreground.R * alpha + background.R * diff),
                (byte)(foreground.G * alpha + background.G * diff),
                (byte)(foreground.B * alpha + background.B * diff));
            return color;
        }
        
        public static Color RemoveAlpha(Color foreground, Color background)
        {
            if(foreground.A == 255)
                return foreground;

            var alpha = foreground.A / 255.0;
            var diff = 1.0 - alpha;
            return Color.FromArgb(255,
                (byte)(foreground.R * alpha + background.R * diff),
                (byte)(foreground.G * alpha + background.G * diff),
                (byte)(foreground.B * alpha + background.B * diff));
        }


        public static Color ChangeAlpha(Color color, byte alpha)
        {
            return Color.FromArgb(alpha, color.R, color.G, color.B);
        }


        public static Color ChangeAlpha(Color color, string alpha)
        {
            var value = UInt32.Parse(alpha, NumberStyles.HexNumber);
            return ChangeAlpha(color, (byte)(value & 0xff));
        }


        public static string ToHex(Color color, bool includeAlpha = false)
        {
            if(includeAlpha)
                return "#" +
                    Convert.ToInt32(color.A).ToString("X2") +
                    Convert.ToInt32(color.R).ToString("X2") +
                    Convert.ToInt32(color.G).ToString("X2") +
                    Convert.ToInt32(color.B).ToString("X2");

            return "#" +
                Convert.ToInt32(color.R).ToString("X2") +
                Convert.ToInt32(color.G).ToString("X2") +
                Convert.ToInt32(color.B).ToString("X2");
        }


        public static Color FromHex(string colorCode)
        {
            colorCode = colorCode.Replace("#", "");
            if(colorCode.Length == 6)
                colorCode = "FF" + colorCode;
            return FromHex(UInt32.Parse(colorCode, NumberStyles.HexNumber));
        }


        public static Color FromHex(uint argb)
        {
            return Color.FromArgb((byte)((argb & -16777216) >> 0x18),
                (byte)((argb & 0xff0000) >> 0x10),
                (byte)((argb & 0xff00) >> 8),
                (byte)(argb & 0xff));
        }

        public static Color FromString(string value)
        {
            var property = typeof(Colors).GetRuntimeProperty(value);
            if(property != null)
                return (Color)property.GetValue(null);
            return FromHex(value);
        }
    }
}
