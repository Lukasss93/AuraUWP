using AuraRT.Animations;
using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using AuraRT.Extensions;
using AuraRT.Imaging;

namespace AuraRT.Controls
{
    public class SettingsItem
    {
        private string _titolo { get; set; }
        private string _descrizione { get; set; }
        private Type _pagina { get; set; }
        private bool _pro { get; set; }
        private Uri _icon { get; set; }

        public SettingsItem(string title, string description, Type page, bool pro = false, Uri icon = null)
        {
            _titolo=title;
            _descrizione=description;
            _pagina=page;
            _pro=pro;
            _icon = icon;
        }

        public string GetTitle()
        {
            return _titolo;
        }

        public string GetDescription()
        {
            return _descrizione;
        }

        public Type GetPage()
        {
            return _pagina;
        }

        public bool GetPro()
        {
            return _pro;
        }

        public Uri GetIcon()
        {
            return _icon;
        }
    }

    public class Settings
    {
        public delegate void PropertyChangeHandler(object sender, TappedEventArgs e);
        public event PropertyChangeHandler SettingTapped;

        public void GenerateSettings(List<SettingsItem> list, StackPanel stackpanel, bool isPRO = true, SolidColorBrush foregroundicon = null)
        {
            stackpanel.Children.Clear();

            foreach(SettingsItem item in list)
            {
                Grid griglia = new Grid();
                griglia.Margin = new Thickness(0, 0, 0, 15);
                griglia.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
                griglia.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                griglia.Tag = item;
                griglia.Tapped += (sender, arg) =>
                {
                    SettingsItem settingstag = (SettingsItem)(sender as Grid).Tag;
                    SettingTapped(sender, new TappedEventArgs("SettingTapped",settingstag));
                };

                new TiltEffect().AddTilt(griglia);

                
                BitmapIcon bi = new BitmapIcon();
                bi.UriSource = item.GetIcon();
                bi.Foreground = foregroundicon;
                bi.Width = 40;
                bi.Height = 40;
                bi.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                bi.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                bi.Margin = new Thickness(0, 0, 15, 0);
                Grid.SetColumn(bi, 0);

                if(item.GetIcon() != null)
                {
                    griglia.Children.Add(bi);
                }
                

                StackPanel testi = new StackPanel();
                testi.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                Grid.SetColumn(testi, 1);


                TextBlock impo_tit = new TextBlock();
                impo_tit.FontSize = 20;
                impo_tit.FontFamily = new FontFamily("Segoe WP");
                //impo_tit.FontWeight = FontWeights.Light;
                impo_tit.TextLineBounds = TextLineBounds.Tight;
                impo_tit.Margin = new Thickness(0, 0, 0, 2);
                impo_tit.Text = item.GetTitle().ToUpperFirst();
                testi.Children.Add(impo_tit);

                TextBlock impo_sub = new TextBlock();
                impo_sub.FontSize = 18;
                impo_sub.FontFamily = new FontFamily("Segoe WP");
                impo_sub.Foreground = ColorUtilities.PhoneLowBrush;
                impo_sub.Text = item.GetDescription().ToUpperFirst();
                testi.Children.Add(impo_sub);

                if(!isPRO && item.GetPro())
                {
                    bi.Foreground = new SolidColorBrush(Color.FromArgb(80, 68, 35, 89));
                    impo_tit.Foreground = new SolidColorBrush()
                    {
                        Color = (Color)((SolidColorBrush)Application.Current.Resources["PhoneForegroundBrush"]).Color,
                        Opacity = 0.31
                    };
                    impo_sub.Foreground = new SolidColorBrush(Color.FromArgb(80, 255, 255, 255));
                }


                griglia.Children.Add(testi);

                stackpanel.Children.Add(griglia);
            }
        }
    }
    
    public class TappedEventArgs : EventArgs
    {
        public string PropertyName { get; internal set; }
        public SettingsItem SettingItem { get; internal set; }

        public TappedEventArgs(string propertyName, SettingsItem setting)
        {
            this.PropertyName = propertyName;
            this.SettingItem = setting;
        }
    }

}
