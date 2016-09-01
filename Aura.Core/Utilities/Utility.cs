using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Email;
using Windows.ApplicationModel.Store;
using Windows.Phone.Devices.Notification;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

#if WINDOWS_UWP
using Windows.ApplicationModel.DataTransfer;
#endif

namespace Aura.Utilities
{
    public class Utility
    {
        /// <summary>
        /// Verifica se la GUID inserita è corretta
        /// </summary>
        public static bool IsGUID(string expression)
        {
            if(expression == null)
            {
                return false;
            }
            return (new Regex("^(\\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\\}{0,1})$")).IsMatch(expression);
        }

        public static async Task GoToStoreDetail(string guid = null)
        {
            guid = guid == null ? CurrentApp.AppId.ToString() : guid;
            Uri uri = new Uri(string.Format("ms-windows-store:navigate?appid={0}", guid));
            await Launcher.LaunchUriAsync(uri);
        }

        public static async Task GoToStoreRateAndReview(string guid = null)
        {
            guid = guid == null ? CurrentApp.AppId.ToString() : guid;
            Uri uri = new Uri(string.Format("ms-windows-store:reviewapp?appid={0}", guid));
            await Launcher.LaunchUriAsync(uri);
        }

        public static async Task GoToStoreSearch(string keyword)
        {
            Uri uri = new Uri(string.Format(@"ms-windows-store:search?keyword={0}", keyword));
            await Launcher.LaunchUriAsync(uri);
        }
        
        public static async Task OpenUrl(string url)
        {
            await Launcher.LaunchUriAsync(new Uri(url));
        }

        public static async Task SendEmail(string email, string subject=null)
        {
            EmailMessage em = new EmailMessage();
            if(subject != null)
            {
                em.Subject = subject;
            }
            em.To.Add(new EmailRecipient(email));

            await EmailManager.ShowComposeNewEmailAsync(em);
        }

        /// <summary>
        /// Esegue una vibrazione in base ai millisecondi inseriti
        /// </summary>
        public static void Vibrate(int milliseconds)
        {
            VibrationDevice.GetDefault().Vibrate(new TimeSpan(0, 0, 0, 0, milliseconds));
        }
        
        public static bool IsValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if(re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        public enum MarginEdge { Top, Right, Bottom, Left }

        public static void SetMargin(FrameworkElement ele, MarginEdge edge, double value)
        {
            Thickness margin = ele.Margin;

            switch(edge)
            {
                case MarginEdge.Top:
                    margin.Top = value;
                    break;
                case MarginEdge.Right:
                    margin.Right = value;
                    break;
                case MarginEdge.Bottom:
                    margin.Bottom = value;
                    break;
                case MarginEdge.Left:
                    margin.Left = value;
                    break;
            }

            ele.Margin = margin;
        }

        public static bool IsValidMobileNum(string strln)
        {
            return Regex.IsMatch(strln, @"^[1]+[8,3,5,7,4]+\d{9}");
        }

        public static string MakeStringFromList(List<string> list)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < list.Count; i++)
            {
                var follow = list.ElementAt(i);
                sb.Append(follow);
                if(i != list.Count - 1)
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        public static FrameworkElement FindVisualChild(DependencyObject element, string nameOfChildToFind)
        {
            for(int x = 0; x < VisualTreeHelper.GetChildrenCount(element); x++)
            {
                var child = VisualTreeHelper.GetChild(element, x);

                if(child is FrameworkElement)
                {
                    string name = (string)child.GetValue(FrameworkElement.NameProperty);

                    if(name == nameOfChildToFind)
                    {
                        return (FrameworkElement)child;
                    }
                    else if(VisualTreeHelper.GetChildrenCount(child) > 0)
                    {
                        return FindVisualChild(child, nameOfChildToFind);
                    }
                }
            }

            return null;
        }

        public static bool IsInDesignMode
        {
            get { return DesignMode.DesignModeEnabled; }
        }

        
#if WINDOWS_UWP
        public static void CopyToClipBoard(string str)
        {
            var dp = new DataPackage
            {
                RequestedOperation = DataPackageOperation.Copy,
            };
            dp.SetText(str);
            Clipboard.SetContent(dp);
        }
#endif

    }
}
