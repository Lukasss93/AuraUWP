using AuraRT.Animations;
using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Input;

namespace AuraRT.Controls
{
    public class MyApps
    {
        public MyApps(string guidapp, Uri urilogo, string name)
        {
            this.GuidApp = guidapp;
            this.UriLogo = urilogo;
            this.Name = name;
        }

        private string guidapp;

        public string GuidApp
        {
            get { return guidapp; }
            set { guidapp = value; }
        }

        private Uri urilogo;

        public Uri UriLogo
        {
            get { return urilogo; }
            set { urilogo = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }




        public static void GenerateItems(StackPanel stack, List<MyApps> list)
        {
            stack.Children.Clear();
            foreach(MyApps app in list)
            {
                //STACK app
                Grid pannello = new Grid();
                pannello.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                pannello.ColumnDefinitions.Add(new ColumnDefinition());

                new TiltEffect().AddTilt(pannello);
                pannello.Margin = new Thickness(0, 0, 0, 5);
                Color backgroundpanel = ((SolidColorBrush)Application.Current.Resources["PhoneBackgroundBrush"]).Color;
                backgroundpanel.A = 126;
                pannello.Background = new SolidColorBrush(backgroundpanel);

                //azione
                pannello.Tag = app;
                pannello.Tapped += async (sender, e) =>
                {
                    MyApps a = (sender as Grid).Tag as MyApps;
                    if(a != null)
                    {
                        await Launcher.LaunchUriAsync(new Uri("ms-windows-store:navigate?appid=" + a.GuidApp));
                    }
                };

                //immagine
                Image icona = new Image();
                icona.Width = 60;
                icona.Height = 60;
                icona.Source = new BitmapImage(app.UriLogo);
                Grid.SetColumn(icona, 0);
                pannello.Children.Add(icona);


                //nome
                TextBlock nome = new TextBlock();
                nome.FontFamily = new FontFamily("Segoe WP");
                nome.FontWeight = FontWeights.Black;
                nome.FontSize = 22;
                nome.TextWrapping = TextWrapping.Wrap;
                nome.Text = app.Name.ToUpper();
                nome.Margin = new Thickness(10, 0, 0, 0);
                nome.IsTextScaleFactorEnabled = false;
                Grid.SetColumn(nome, 1);
                pannello.Children.Add(nome);


                stack.Children.Add(pannello);
            }
        }
    }
}
