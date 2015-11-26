using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace AuraRT.Resources
{
    /// <summary>MyColors Class</summary>
    public class MyColors
    {
        /// <summary>PhoneAccentBrush</summary>
        public static SolidColorBrush PhoneAccentBrush = (SolidColorBrush)Application.Current.Resources["PhoneAccentBrush"];
        public static SolidColorBrush PhoneChromeBrush
        {
            get
            {
                if(Application.Current.RequestedTheme == ApplicationTheme.Dark)
                {
                    return new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
                }
                else
                {
                    return new SolidColorBrush(Color.FromArgb(255, 221, 221, 221));
                }
            }
        }
        public static SolidColorBrush PhoneLowBrush = (SolidColorBrush)Application.Current.Resources["PhoneLowBrush"];
        public static SolidColorBrush PhoneMidBrush = (SolidColorBrush)Application.Current.Resources["PhoneMidBrush"];

        public static SolidColorBrush PhoneForegroundBrush
        {
            get
            {
                if(Application.Current.RequestedTheme == ApplicationTheme.Dark)
                {
                    return new SolidColorBrush(Colors.White);
                }
                else
                {
                    return new SolidColorBrush(Colors.Black);
                }
            }
        }

        public static SolidColorBrush Transparent = new SolidColorBrush(Colors.Transparent);

        public static SolidColorBrush White = new SolidColorBrush(Colors.White);
        public static SolidColorBrush Black = new SolidColorBrush(Colors.Black);
        public static SolidColorBrush Blue = new SolidColorBrush(Colors.Blue);
        public static SolidColorBrush Brown = new SolidColorBrush(Colors.Brown);
        public static SolidColorBrush Crimson = new SolidColorBrush(Colors.Crimson);
        public static SolidColorBrush Cyan = new SolidColorBrush(Colors.Cyan);
        public static SolidColorBrush Fuchsia = new SolidColorBrush(Colors.Fuchsia);
        public static SolidColorBrush Gold = new SolidColorBrush(Colors.Gold);
        public static SolidColorBrush Gray = new SolidColorBrush(Colors.Gray);
        public static SolidColorBrush Green = new SolidColorBrush(Colors.Green);
        public static SolidColorBrush Indigo = new SolidColorBrush(Colors.Indigo);
        public static SolidColorBrush Lime = new SolidColorBrush(Colors.Lime);
        public static SolidColorBrush Magenta = new SolidColorBrush(Colors.Magenta);
        public static SolidColorBrush Olive = new SolidColorBrush(Colors.Olive);
        public static SolidColorBrush Orange = new SolidColorBrush(Colors.Orange);
        public static SolidColorBrush Pink = new SolidColorBrush(Colors.Pink);
        public static SolidColorBrush Purple = new SolidColorBrush(Colors.Purple);
        public static SolidColorBrush Red = new SolidColorBrush(Colors.Red);
        public static SolidColorBrush Sienna = new SolidColorBrush(Colors.Sienna);
        public static SolidColorBrush Silver = new SolidColorBrush(Colors.Silver);
        public static SolidColorBrush Snow = new SolidColorBrush(Colors.Snow);
        public static SolidColorBrush Tomato = new SolidColorBrush(Colors.Tomato);
        public static SolidColorBrush Violet = new SolidColorBrush(Colors.Violet);
        public static SolidColorBrush Yellow = new SolidColorBrush(Colors.Yellow);
    }
}
