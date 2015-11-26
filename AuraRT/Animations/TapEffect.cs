using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace AuraRT.Animations
{
    public class TapEffect
    {
        /// <summary>
        /// Abilita il TapEffect su un FrameworkElement
        /// </summary>
        public static void Enable(FrameworkElement ele)
        {

            ele.PointerPressed+=inizio;

            ele.PointerCanceled+=fine;
            ele.PointerReleased+=fine;
            ele.PointerExited+=fine;
            ele.PointerCaptureLost+=fine;
        }

        /// <summary>
        /// Evento fine tap
        /// </summary>
        private static void fine(object sender, PointerRoutedEventArgs e)
        {
            ((FrameworkElement)sender).RenderTransform = null;
        }

        /// <summary>
        /// Evento inizio tap
        /// </summary>
        private static void inizio(object sender, PointerRoutedEventArgs e)
        {
            ((FrameworkElement)sender).RenderTransform = new TranslateTransform() { X = 2, Y = 2 };
        }
    }
}
