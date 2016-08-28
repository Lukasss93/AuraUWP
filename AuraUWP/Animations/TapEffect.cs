using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace AuraUWP.Animations
{
    public class TapEffect
    {
        private FrameworkElement Element;
        private double X;
        private double Y;

        public TapEffect()
        {
            Element = null;
            X = 0;
            Y = 0;
        }

        public void SetFrameworkElement(FrameworkElement element)
        {
            Element = element;
        }

        public void SetAxis(double x, double y)
        {
            X = x;
            Y = y;
        }
        
        public void Enable()
        {
            Element.PointerPressed+= Started;

            Element.PointerCanceled+= Stopped;
            Element.PointerReleased+= Stopped;
            Element.PointerExited+= Stopped;
            Element.PointerCaptureLost+= Stopped;
        }

        public void Disable()
        {
            Element.PointerPressed -= Started;

            Element.PointerCanceled -= Stopped;
            Element.PointerReleased -= Stopped;
            Element.PointerExited -= Stopped;
            Element.PointerCaptureLost -= Stopped;
        }

        private void Started(object sender, PointerRoutedEventArgs e)
        {
            ((FrameworkElement)sender).RenderTransform = new TranslateTransform() { X = X, Y = Y };
        }

        private void Stopped(object sender, PointerRoutedEventArgs e)
        {
            ((FrameworkElement)sender).RenderTransform = null;
        }
        
        
    }
}
