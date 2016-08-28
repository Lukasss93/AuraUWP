using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace AuraUWP.Display
{
    public class StatusBarHelper
    {
        public static void SetBackground(Color color)
        {
            var status = StatusBar.GetForCurrentView();
            status.BackgroundOpacity = 1;
            status.BackgroundColor = color;
        }

        /// <summary>
        /// Mostra il caricamento indeterminato nella statusbar
        /// </summary>
        public static async Task ShowLoading(string loadingtext)
        {
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseVisible);

            var sb = StatusBar.GetForCurrentView();
            sb.ProgressIndicator.Text = loadingtext;
            sb.ProgressIndicator.ProgressValue = 0;
            await sb.ProgressIndicator.ShowAsync();
        }

        /// <summary>
        /// Nasconde il caricamento indeterminato nella statusbar
        /// </summary>
        public static async void HideLoading()
        {
            var sb = StatusBar.GetForCurrentView();
            await sb.ProgressIndicator.HideAsync();
        }

        public static Thickness SetTopMargin(FrameworkElement ele)
        {
            if(ApplicationView.GetForCurrentView().DesiredBoundsMode==ApplicationViewBoundsMode.UseCoreWindow)
            {
                Thickness margin = ele.Margin;
                margin.Top = StatusBar.GetForCurrentView().OccludedRect.Height;
                return margin;
            }
            else
            {
                return new Thickness(0);
            }
        }
    }
}
