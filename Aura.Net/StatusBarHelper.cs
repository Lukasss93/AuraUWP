using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media;

namespace Aura.Net
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
            await sb.ProgressIndicator.ShowAsync();
        }

        /// <summary>
        /// Nasconde il caricamento indeterminato nella statusbar
        /// </summary>
        public static void HideLoading()
        {
            var sb = StatusBar.GetForCurrentView();
            sb.ProgressIndicator.Text = "";
            sb.ProgressIndicator.ProgressValue = 0;
        }
    }
}
