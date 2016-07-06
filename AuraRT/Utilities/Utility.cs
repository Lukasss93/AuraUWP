using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Email;
using Windows.ApplicationModel.Store;
using Windows.Graphics.Display;
using Windows.Phone.Devices.Notification;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.System;
using Windows.System.Profile;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AuraRT.Utilities
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
        
        public static bool isValidEmail(string inputEmail)
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
    }
}
