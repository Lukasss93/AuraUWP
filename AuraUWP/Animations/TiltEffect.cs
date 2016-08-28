using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace AuraUWP.Animations
{
    public class TiltEffect
    {
        private Storyboard tilt_down;
        private Storyboard tilt_up;
        private FrameworkElement element;
        private Pointer point;
        private bool _isOn = false;


        /// <summary>
        /// Metodo costruttore
        /// </summary>
        public TiltEffect()
        {
            tilt_down = new Storyboard();
            tilt_down.Children.Add(new PointerDownThemeAnimation());

            tilt_up = new Storyboard();
            tilt_up.Children.Add(new PointerUpThemeAnimation());            
        }

        /// <summary>
        /// Aggiunge l'effetto tilt ad un FrameworkElement
        /// </summary>
        public void AddTilt(FrameworkElement item)
        {
            if(!_isOn)
            {
                element = item;

                element.Resources.Add("TiltDown", tilt_down);
                element.Resources.Add("TiltUp", tilt_up);

                if(string.IsNullOrEmpty(element.Name))
                {
                    item.Name = "Temp_" + Guid.NewGuid();
                }

                (tilt_down.Children[0] as PointerDownThemeAnimation).TargetName = element.Name;
                (tilt_up.Children[0] as PointerUpThemeAnimation).TargetName = element.Name;

                element.PointerPressed += TiltDown;
                element.PointerReleased += TiltUp;
                element.PointerExited += TiltUp;
                element.PointerCaptureLost += TiltUp;

                _isOn = true;
            }
        }

        /// <summary>
        /// Rimuove l'effetto tilt ad un FrameworkElement
        /// </summary>
        public void RemoveTilt()
        {
            if(_isOn)
            {
                element.PointerPressed -= TiltDown;
                element.PointerReleased -= TiltUp;
                element.PointerExited -= TiltUp;
                element.PointerCaptureLost -= TiltUp;

                tilt_down = null;
                tilt_up = null;
                element = null;
                point = null;

                _isOn = false;
            }
        }

        /// <summary>
        /// Restituisce true se l'elemento ha l'effetto altrimenti false
        /// </summary>
        public bool isOn()
        {
            return _isOn;
        }

        private void TiltDown(object sender, PointerRoutedEventArgs e)
        {
            point = e.Pointer;
            element.CapturePointer(point);
            element.Projection = new PlaneProjection();

            tilt_down.Stop();
            tilt_down.Begin();

        }

        private void TiltUp(object sender, PointerRoutedEventArgs e)
        {
            tilt_up.Stop();
            tilt_up.Begin();
        }
    }
}
