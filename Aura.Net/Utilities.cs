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

namespace Aura.Net
{
    public class Utilities
    {
        /// <summary>
        /// Restituisce l'ID univoco del device
        /// </summary>
        public static string GetDeviceID(int encodeto=64)
        {
            string value=null;
            switch(encodeto)
            {
                case 16:
                    HardwareToken token = HardwareIdentification.GetPackageSpecificToken(null);
                    IBuffer hardwareID = token.Id;
                    HashAlgorithmProvider hasher = HashAlgorithmProvider.OpenAlgorithm("MD5");
                    IBuffer hashed = hasher.HashData(hardwareID);
                    string hashedString = CryptographicBuffer.EncodeToHexString(hashed);
                    
                    value = hashedString;
                    break;

                case 64:
                default:
                    value= CryptographicBuffer.EncodeToBase64String(HardwareIdentification.GetPackageSpecificToken(null).Id);
                    break;
            }
            return value;
        }
        


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

        /// <summary>
        /// Restituisce la versione dell'app
        /// </summary>
        public static string Appversion()
        {
            PackageVersion version = Package.Current.Id.Version;
            Object[] major = new Object[] { version.Major, version.Minor, version.Build, version.Revision };
            return String.Format("{0}.{1}.{2}.{3}", major);
        }

        /// <summary>
        /// Restituisce il nome dell'app
        /// </summary>
        public static String Appname()
        {
            String[] strArray = Package.Current.Id.Name.Split(new Char[] { '.' });
            if(strArray.Count<String>() == 1)
            {
                return Package.Current.Id.Name;
            }
            return strArray[strArray.Count<String>() - 1];
        }

        

        /// <summary>
        /// Apre la finestra per votare l'app
        /// </summary>
        public static async Task Rate()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId));
        }


        public static async Task OpenFacebook() { await Launcher.LaunchUriAsync(new Uri("http://www.facebook.com/Lukasss93Dev")); }
        public static async Task OpenTwitter() { await Launcher.LaunchUriAsync(new Uri("http://twitter.com/JonnyRosworth")); }
        public static async Task OpenWebSite() { await Launcher.LaunchUriAsync(new Uri("http://windowsphone.lucapatera.it/")); }

        /// <summary>
        /// Apre la finestra per contattare lo sviluppatore
        /// </summary>
        public static async Task ContactMe()
        {
            EmailMessage em = new EmailMessage();
            em.Subject="[FEEDBACK] "+Appname();
            em.To.Add(new EmailRecipient("windowsphone@lucapatera.it"));

            await EmailManager.ShowComposeNewEmailAsync(em);
        }

        /// <summary>
        /// Esegue una vibrazione in base ai millisecondi inseriti
        /// </summary>
        public static void Vibrate(int milliseconds)
        {
            VibrationDevice.GetDefault().Vibrate(new TimeSpan(0, 0, 0, 0, milliseconds));
        }

        /// <summary>
        /// Restituisce la risoluzione X del telefono
        /// </summary>
        public static int GetResolutionX()
        {
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            double resX=Window.Current.Bounds.Width * scaleFactor;
            return Convert.ToInt32(resX);
        }

        /// <summary>
        /// Restituisce la risoluzione Y del telefono
        /// </summary>
        public static int GetResolutionY()
        {
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            double resY=Window.Current.Bounds.Height * scaleFactor;
            return Convert.ToInt32(resY);
        }

        /// <summary>
        /// Restituisce un dizionario con gli argomenti di un url
        /// </summary>
        public static Dictionary<string, string> ParseUrlArguments(string args)
        {
            Dictionary<string, string> strs = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(args))
            {
                char[] chrArray = new char[] { '&' };
                string[] strArrays = args.Split(chrArray);
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    string str = strArrays[i];
                    chrArray = new char[] { '=' };
                    string[] strArrays1 = str.Split(chrArray);
                    if ((int)strArrays1.Length > 0)
                    {
                        strs.Add(strArrays1[0], ((int)strArrays1.Length > 1 ? strArrays1[1] : string.Empty));
                    }
                }
            }
            return strs;
        }

        public static string getCurrentMAC()
        {
            var networkProfiles = Windows.Networking.Connectivity.NetworkInformation.GetConnectionProfiles();

            //takes the first network adapter
            var adapter = networkProfiles.First<Windows.Networking.Connectivity.ConnectionProfile>().NetworkAdapter;

            //produces a string in the format: 90de0377-d988-4e1b-b89b-475bbca46e1d
            string networkAdapterId = adapter.NetworkAdapterId.ToString();

            return networkAdapterId.ToUpper();
        }

        public static string md5(string input)
        {
            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer buffer = CryptographicBuffer.ConvertStringToBinary(input, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buffer);
            var result = CryptographicBuffer.EncodeToHexString(hashed);
            return result;
        }

        public static void goBack()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if(rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
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

        public static Thickness SetMarginTop(FrameworkElement ele, double value)
        {
            Thickness margin = ele.Margin;
            margin.Top = value;
            return margin;
        }

        public static Thickness SetMarginBottom(FrameworkElement ele, double value)
        {
            Thickness margin = ele.Margin;
            margin.Bottom = value;
            return margin;
        }

        public static Thickness SetMarginLeft(FrameworkElement ele, double value)
        {
            Thickness margin = ele.Margin;
            margin.Left = value;
            return margin;
        }

        public static Thickness SetMarginRight(FrameworkElement ele, double value)
        {
            Thickness margin = ele.Margin;
            margin.Right = value;
            return margin;
        }
    }
}
