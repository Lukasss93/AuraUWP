using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Aura.Animations
{
    public class TapScaleEffect
    {
        private FrameworkElement Element;
        private Point Origin;
        private double X;
        private double Y;

        public TapScaleEffect()
        {
            Element = null;
            Origin = new Point(0, 0);
            X = 0;
            Y = 0;
        }

        public void SetFrameworkElement(FrameworkElement element)
        {
            Element = element;
        }

        public void SetOrigin(Point origin)
        {
            Origin = origin;
        }

        public void SetAxis(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Enable()
        {
            Element.PointerPressed += Started;

            Element.PointerCanceled += Stopped;
            Element.PointerReleased += Stopped;
            Element.PointerExited += Stopped;
            Element.PointerCaptureLost += Stopped;
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
            Element.RenderTransformOrigin = Origin;

            ScaleTransform scale = new ScaleTransform();
            scale.ScaleX = X;
            scale.ScaleY = Y;

            ((FrameworkElement)sender).RenderTransform = scale;
        }

        private void Stopped(object sender, PointerRoutedEventArgs e)
        {
            ((FrameworkElement)sender).RenderTransform = null;
        }
    }
}
