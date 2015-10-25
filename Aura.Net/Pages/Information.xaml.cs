using Aura.Net.Animations;
using Aura.Net.Common;
using Aura.Net.Controls;
using Aura.Net.Resources;
using Aura.Net.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Aura.Net.Pages
{
    public sealed partial class Information : Page
    {
        InformationOptions options;

        public Information()
        {
            this.InitializeComponent();

            cb_rate.Click += Cb_rate_Click;
        }

        private async void Cb_rate_Click(object sender, RoutedEventArgs e)
        {
            await Utilities.Rate();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                if(e.Parameter != null && e.Parameter is string && Json.isValidJson(e.Parameter as string))
                {
                    options = Json.Deserialize<InformationOptions>((string)e.Parameter);

                    ChangePage(options.Page);

                    body_pivot_info.Header = options.AboutMePage.Header;
                    if(options.AboutMePage.Avatar != null) { aboutme_avatar.Background = new ImageBrush() { ImageSource = new BitmapImage(options.AboutMePage.Avatar) }; }
                    aboutme_fullname.Text = options.AboutMePage.FullName;
                    aboutme_nickname.Text = options.AboutMePage.Nickname;
                    GenerateLinks(options.AboutMePage.Links);

                    body_pivot_versions.Header = options.ChangelogPage.Header;
                    if(options.ChangelogPage.AppLogo != null) { changelog_applogo.Source = new BitmapImage(options.ChangelogPage.AppLogo); }
                    changelog_appname.Text = options.ChangelogPage.AppName;
                    Changelog.GenerateChangelog(options.ChangelogPage.Changes, changelog_changes,options.ChangelogPage.Current);

                    body_pivot_apps.Header = options.MyAppsPage.Header;
                    MyApps.GenerateItems(myapps_stack, options.MyAppsPage.MyAppsList);

                    body_pivot_pro.Header = options.ProPage.Header;
                    if(options.ProPage.ProEnabled == false) { body_pivot.Items.RemoveAt(3); }

                    cb_rate.Label = options.ChangelogPage.Rate;
                }
            }
            catch(Exception ex)
            {
                MessageDialogHelper.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void GenerateLinks(List<Link> links)
        {
            aboutme_links_stack.Children.Clear();
            foreach(var link in links)
            {
                StackPanel stack = new StackPanel();
                stack.Margin = new Thickness(0, 0, 0, 10);
                stack.VerticalAlignment = VerticalAlignment.Top;

                TextBlock header = new TextBlock();
                header.FontFamily = new FontFamily("Segoe WP");
                header.FontWeight = FontWeights.Bold;
                header.VerticalAlignment = VerticalAlignment.Top;
                header.FontSize=22;
                header.Text = link.Header.ToUpper();
                stack.Children.Add(header);

                HyperlinkButton value = new HyperlinkButton();
                value.HorizontalAlignment = HorizontalAlignment.Left;
                value.VerticalAlignment = VerticalAlignment.Top;
                value.Foreground = MyColors.PhoneAccentBrush;
                value.FontSize = 20;
                value.Margin = new Thickness(0,-10,0,0);
                value.Content = link.Content;
                value.NavigateUri = link.NavUri;
                stack.Children.Add(value);

                aboutme_links_stack.Children.Add(stack);
            }
        }

        private void ChangePage(InformationOptions.Pages page)
        {
            switch(page)
            {
                default:
                case InformationOptions.Pages.ABOUTME:
                    body_pivot.SelectedIndex = 0;
                    break;

                case InformationOptions.Pages.CHANGELOG:
                    body_pivot.SelectedIndex = 1;
                    break;

                case InformationOptions.Pages.MYAPPS:
                    body_pivot.SelectedIndex = 2;
                    break;

                case InformationOptions.Pages.PRO:
                    body_pivot.SelectedIndex = options.ProPage.ProEnabled ? 3 : 0;
                    break;
            }
        }
    }
}
