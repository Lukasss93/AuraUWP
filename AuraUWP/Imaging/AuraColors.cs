using System;
using System.Collections.Generic;
using System.Text;
using AuraUWP.Extensions;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace AuraUWP.Imaging
{
    public class AuraColors
    {
        public static Color AuraAccent
        {
            get
            {
                return (Color)Application.Current.Resources["SystemAccentColor"];
            }
        }

        public static Color AccentLight1 = AuraAccent.Shade(30);
        public static Color AccentLight2 = AuraAccent.Shade(50);
        public static Color AccentLight3 = AuraAccent.Shade(70);
        public static Color AccentDark1 = AuraAccent.Shade(-30);
        public static Color AccentDark2 = AuraAccent.Shade(-50);
        public static Color AccentDark3 = AuraAccent.Shade(-70);

        public static Color AuraChrome = Application.Current.RequestedTheme == ApplicationTheme.Dark ? Color.FromArgb(255, 31, 31, 31) : Color.FromArgb(255, 230, 230, 230);
        public static Color AuraChromeLight = Application.Current.RequestedTheme == ApplicationTheme.Dark ? Color.FromArgb(255, 43, 43, 43) : Color.FromArgb(255, 242, 242, 242);
        public static Color AuraChromeDark = Application.Current.RequestedTheme == ApplicationTheme.Dark ? Color.FromArgb(255, 23, 23, 23) : Color.FromArgb(255, 242, 242, 242);


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
