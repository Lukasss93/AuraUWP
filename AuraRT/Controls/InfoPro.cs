using AuraRT.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace AuraRT.Controls
{
    public class InfoPro
    {
        public string nome { get; set; }
        public string gratis { get; set; }
        public string pro { get; set; }

        public InfoPro(string n, string g, string p)
        {
            nome = n;
            gratis = g;
            pro = p;
        }

        public static void GetTable(List<InfoPro> lista, StackPanel element, string gratis, string pro)
        {
            Grid infopro_griglia = new Grid();
            infopro_griglia.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            infopro_griglia.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(80, GridUnitType.Pixel) });
            infopro_griglia.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(80, GridUnitType.Pixel) });

            for(int i = 0; i < lista.Count() + 1; i++)
            {
                infopro_griglia.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            }


            TextBlock title_gratis = new TextBlock();
            title_gratis.FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe WP");
            title_gratis.FontWeight = FontWeights.Bold;
            title_gratis.FontSize = 14;
            title_gratis.Text = gratis;
            title_gratis.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            title_gratis.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            Grid.SetRow(title_gratis, 0);
            Grid.SetColumn(title_gratis, 1);
            infopro_griglia.Children.Add(title_gratis);

            TextBlock title_pro = new TextBlock();
            title_pro.FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe WP");
            title_pro.FontWeight = FontWeights.Bold;
            title_pro.FontSize = 14;
            title_pro.Text = pro;
            title_pro.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            title_pro.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            Grid.SetRow(title_pro, 0);
            Grid.SetColumn(title_pro, 2);
            infopro_griglia.Children.Add(title_pro);

            Border border_gratis = new Border();
            border_gratis.BorderBrush = MyColors.PhoneChromeBrush;
            border_gratis.BorderThickness = new Thickness(1, 0, 0, 0);
            border_gratis.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            border_gratis.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
            Grid.SetRow(border_gratis, 0);
            Grid.SetColumn(border_gratis, 1);
            Grid.SetRowSpan(border_gratis, lista.Count() + 1);
            infopro_griglia.Children.Add(border_gratis);

            Border border_pro = new Border();
            border_pro.BorderBrush = MyColors.PhoneChromeBrush;
            border_pro.BorderThickness = new Thickness(1, 0, 0, 0);
            border_pro.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
            border_pro.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
            Grid.SetRow(border_pro, 0);
            Grid.SetColumn(border_pro, 2);
            Grid.SetRowSpan(border_pro, lista.Count() + 1);
            infopro_griglia.Children.Add(border_pro);

            int r = 1;
            foreach(var item in lista)
            {
                TextBlock infopro_desc = new TextBlock();
                infopro_desc.FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe WP");
                infopro_desc.FontSize = 14;
                infopro_desc.Text = item.nome;
                infopro_desc.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                infopro_desc.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                Grid.SetRow(infopro_desc, r);
                Grid.SetColumn(infopro_desc, 0);
                infopro_griglia.Children.Add(infopro_desc);

                TextBlock infopro_gratis = new TextBlock();
                infopro_gratis.FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe UI Symbol");
                infopro_gratis.FontSize = 14;
                infopro_gratis.Text = item.gratis;
                infopro_gratis.Foreground = MyColors.Red;
                infopro_gratis.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                infopro_gratis.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                Grid.SetRow(infopro_gratis, r);
                Grid.SetColumn(infopro_gratis, 1);
                infopro_griglia.Children.Add(infopro_gratis);

                TextBlock infopro_pro = new TextBlock();
                infopro_pro.FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe UI Symbol");
                infopro_pro.FontSize = 14;
                infopro_pro.Text = item.pro;
                infopro_pro.Foreground = new SolidColorBrush(Color.FromArgb(255, 30, 160, 30));
                infopro_pro.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                infopro_pro.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                Grid.SetRow(infopro_pro, r);
                Grid.SetColumn(infopro_pro, 2);
                infopro_griglia.Children.Add(infopro_pro);

                Border border_riga = new Border();
                border_riga.BorderBrush = MyColors.PhoneChromeBrush;
                border_riga.BorderThickness = new Thickness(0, 1, 0, 0);
                border_riga.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
                border_riga.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                Grid.SetRow(border_riga, r);
                Grid.SetColumn(border_riga, 0);
                Grid.SetColumnSpan(border_riga, 3);
                infopro_griglia.Children.Add(border_riga);

                r++;
            }

            element.Children.Add(infopro_griglia);
        }
    }
}
