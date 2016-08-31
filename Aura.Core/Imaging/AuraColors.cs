using System;
using System.Collections.Generic;
using System.Text;
using Aura.Extensions;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Aura.Imaging
{
    public class AuraColors
    {

#if WINDOWS_UWP
        public static Color Accent = (Color)Application.Current.Resources["SystemControlBackgroundAccentBrush"];
#else
        public static Color Accent = (Color)Application.Current.Resources["PhoneAccentBrush"];
#endif

        public static Color AccentLight1 = Accent.Shade(30);
        public static Color AccentLight2 = Accent.Shade(50);
        public static Color AccentLight3 = Accent.Shade(70);

        public static Color AccentDark1 = Accent.Shade(-30);
        public static Color AccentDark2 = Accent.Shade(-50);
        public static Color AccentDark3 = Accent.Shade(-70);

        public static Color Chrome
        {
            get
            {
                if(Application.Current.RequestedTheme == ApplicationTheme.Dark)
                    return Color.FromArgb(255, 31, 31, 31);
                return Color.FromArgb(255, 221, 221, 221);
            }
        }

        public static Color Foreground
        {
            get
            {
                if(Application.Current.RequestedTheme == ApplicationTheme.Dark)
                    return Colors.White;
                return Colors.Black;
            }
        }

        public static Color Background
        {
            get
            {
                if(Application.Current.RequestedTheme == ApplicationTheme.Dark)
                    return Colors.Black;
                return Colors.White;
            }
        }
    }
}
