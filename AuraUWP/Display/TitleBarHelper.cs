using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace AuraUWP.Display
{
    public class TitleBarHelper
    {
        public static void ChangeColors(Color backgroundColor, Color foregroundColor, Color buttonBackgroundHoverColor)
        {
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;

            titleBar.ForegroundColor = foregroundColor;
            titleBar.InactiveForegroundColor = foregroundColor;
            titleBar.ButtonForegroundColor = foregroundColor;
            titleBar.ButtonHoverForegroundColor = foregroundColor;
            titleBar.ButtonPressedForegroundColor = foregroundColor;
            titleBar.ButtonInactiveForegroundColor = foregroundColor;

            titleBar.BackgroundColor = backgroundColor;
            titleBar.InactiveBackgroundColor = backgroundColor;
            titleBar.ButtonBackgroundColor = backgroundColor;
            titleBar.ButtonInactiveBackgroundColor = backgroundColor;

            titleBar.ButtonPressedBackgroundColor = buttonBackgroundHoverColor;
            titleBar.ButtonHoverBackgroundColor = buttonBackgroundHoverColor;
        }
    }
}
