using Aura.Net.Animations;
using Aura.Net.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkID=390556

namespace Aura.Net.Pages
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class Information : Page
    {
        InformationParameter data;

        public Information()
        {
            this.InitializeComponent();

            new TiltEffect().AddTilt(border0);
            new TiltEffect().AddTilt(border1);
            new TiltEffect().AddTilt(border2);
            new TiltEffect().AddTilt(border3);

            border0.Tapped += (sender, e) => { changePage(InformationParameter.Pages.INFO); };
            border1.Tapped += (sender, e) => { changePage(InformationParameter.Pages.VERSIONS); };
            border2.Tapped += (sender, e) => { changePage(InformationParameter.Pages.APPS); };
            border3.Tapped += (sender, e) => { changePage(InformationParameter.Pages.PRO); };

            body_pivot.SelectionChanged += body_pivot_SelectionChanged;
        }

        void body_pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = (sender as Pivot).SelectedIndex;

            switch(index)
            {
                case 0: changePage(InformationParameter.Pages.INFO); break;
                case 1: changePage(InformationParameter.Pages.VERSIONS); break;
                case 2: changePage(InformationParameter.Pages.APPS); break;
                case 3: changePage(InformationParameter.Pages.PRO); break;
            }
        }

        /// <summary>
        /// Richiamato quando la pagina sta per essere visualizzata in un Frame.
        /// </summary>
        /// <param name="e">Dati dell'evento in cui vengono descritte le modalità con cui la pagina è stata raggiunta.
        /// Questo parametro viene in genere utilizzato per configurare la pagina.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter != null && e.Parameter is InformationParameter)
            {
                data = (InformationParameter)e.Parameter;

                if(data.Pro == false) { body_pivot.Items.RemoveAt(3); }
                border3.Visibility = data.Pro == false ? Visibility.Collapsed : Visibility.Visible;

                changePage(data.Page);

                icon0.Visibility = data.NavbarIcons.UriInfo==null?Visibility.Collapsed:Visibility.Visible;
                icon1.Visibility = data.NavbarIcons.UriVersions == null ? Visibility.Collapsed : Visibility.Visible;
                icon2.Visibility = data.NavbarIcons.UriApps == null ? Visibility.Collapsed : Visibility.Visible;
                icon3.Visibility = data.NavbarIcons.UriPro == null ? Visibility.Collapsed : Visibility.Visible;

                icon0.UriSource = data.NavbarIcons.UriInfo;
                icon1.UriSource = data.NavbarIcons.UriVersions;
                icon2.UriSource = data.NavbarIcons.UriApps;
                icon3.UriSource = data.NavbarIcons.UriPro;

                avatar.Fill = new ImageBrush() { ImageSource = new BitmapImage(data.Info.UriAvatar) };

                Changelog.GenerateChangelog(data.Changelog, stack_versions);
            }
            else
            {
                Utilities.goBack();
                throw new Exception("Non è stato passato il parametro InformationParameter.");                
            }
        }

        private void changePage(InformationParameter.Pages p)
        {
            border0.BorderBrush = null;
            border1.BorderBrush = null;
            border2.BorderBrush = null;
            border3.BorderBrush = null;

            icon0.Foreground = Resources["PhoneForegroundBrush"] as Brush;
            icon1.Foreground = Resources["PhoneForegroundBrush"] as Brush;
            icon2.Foreground = Resources["PhoneForegroundBrush"] as Brush;
            icon3.Foreground = Resources["PhoneForegroundBrush"] as Brush;

            text0.Foreground = Resources["PhoneForegroundBrush"] as Brush;
            text1.Foreground = Resources["PhoneForegroundBrush"] as Brush;
            text2.Foreground = Resources["PhoneForegroundBrush"] as Brush;
            text3.Foreground = Resources["PhoneForegroundBrush"] as Brush;

            switch(p)
            {
                case InformationParameter.Pages.INFO:
                    border0.BorderBrush = Resources["PhoneAccentBrush"] as Brush;
                    icon0.Foreground = Resources["PhoneAccentBrush"] as Brush;
                    text0.Foreground = Resources["PhoneAccentBrush"] as Brush;
                    body_pivot.SelectedIndex = 0;
                    break;
                case InformationParameter.Pages.VERSIONS:
                    border1.BorderBrush = Resources["PhoneAccentBrush"] as Brush;
                    icon1.Foreground = Resources["PhoneAccentBrush"] as Brush;
                    text1.Foreground = Resources["PhoneAccentBrush"] as Brush;
                    body_pivot.SelectedIndex = 1;
                    break;
                case InformationParameter.Pages.APPS:
                    border2.BorderBrush = Resources["PhoneAccentBrush"] as Brush;
                    icon2.Foreground = Resources["PhoneAccentBrush"] as Brush;
                    text2.Foreground = Resources["PhoneAccentBrush"] as Brush;
                    body_pivot.SelectedIndex = 2;
                    break;
                case InformationParameter.Pages.PRO:
                    border3.BorderBrush = Resources["PhoneAccentBrush"] as Brush;
                    icon3.Foreground = Resources["PhoneAccentBrush"] as Brush;
                    text3.Foreground = Resources["PhoneAccentBrush"] as Brush;
                    body_pivot.SelectedIndex = 3;
                    break;
            }
        }
    }
}
