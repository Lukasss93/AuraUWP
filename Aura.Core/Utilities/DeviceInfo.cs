using System;
using System.Linq;
using Windows.ApplicationModel;
using Windows.Graphics.Display;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.System.Profile;
using Windows.UI.Xaml;

namespace Aura.Utilities
{
    public class DeviceInfo
    {

        /// <summary>
        /// Restituisce il nome dell'app
        /// </summary>
        /// <returns></returns>
        public static string Name(bool isshort = false)
        {
            if(isshort)
            {
                String[] strArray = Package.Current.Id.Name.Split(new Char[] { '.' });
                if(strArray.Count<String>() == 1)
                {
                    return Package.Current.Id.Name;
                }
                return strArray[strArray.Count<String>() - 1];
            }
            else
            {
                return Package.Current.Id.Name;
            }
        }

        /// <summary>
        /// Restituisce la versione dell'app
        /// </summary>
        public static Version Version()
        {
            return new Version(
                Package.Current.Id.Version.Major,
                Package.Current.Id.Version.Minor,
                Package.Current.Id.Version.Build,
                Package.Current.Id.Version.Revision);
        }

        public static string Publisher()
        {
            return Package.Current.Id.Publisher;
        }

        public static string PublisherId()
        {
            return Package.Current.Id.PublisherId;
        }

        public static string FullName()
        {
            return Package.Current.Id.FullName;
        }

        public static string FamilyName()
        {
            return Package.Current.Id.FamilyName;
        }

        /// <summary>
        /// Restituisce l'id univoco del dispositivo
        /// </summary>
        public static string Id(int encodeto = 64)
        {
            string value = null;
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
                    value = CryptographicBuffer.EncodeToBase64String(HardwareIdentification.GetPackageSpecificToken(null).Id);
                    break;
            }
            return value;
        }

        /// <summary>
        /// Restituisce la risoluzione X del telefono
        /// </summary>
        public static int ResolutionX()
        {
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            double resX = Window.Current.Bounds.Width * scaleFactor;
            return Convert.ToInt32(resX);
        }

        /// <summary>
        /// Restituisce la risoluzione Y del telefono
        /// </summary>
        public static int ResolutionY()
        {
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            double resY = Window.Current.Bounds.Height * scaleFactor;
            return Convert.ToInt32(resY);
        }

        public enum Resolution { WVGA, WXGA, HD, FULLHD, QSXGA, UNKNOWN }
        public static Resolution ResolutionType()
        {
            int x = ResolutionX();
            int y = ResolutionY();

            if(x==480 && y==800)
            {
                return Resolution.WVGA;
            }
            else if(x==768 && y==1280)
            {
                return Resolution.WXGA;
            }
            else if(x==720 && y==1280)
            {
                return Resolution.HD;
            }
            else if(x==1080 && y==1920)
            {
                return Resolution.FULLHD;
            }
            else if(x==2048 && y==2560)
            {
                return Resolution.QSXGA;
            }
            else
            {
                return Resolution.UNKNOWN;
            }
        }

        /// <summary>
        /// Restituisce il Mac Address nel formato esempio: 90de0377-d988-4e1b-b89b-475bbca46e1d
        /// </summary>
        public static string MacAddress()
        {
            var networkProfiles = Windows.Networking.Connectivity.NetworkInformation.GetConnectionProfiles();

            //takes the first network adapter
            var adapter = networkProfiles.First<Windows.Networking.Connectivity.ConnectionProfile>().NetworkAdapter;

            //produces a string in the format: 90de0377-d988-4e1b-b89b-475bbca46e1d
            string networkAdapterId = adapter.NetworkAdapterId.ToString();

            return networkAdapterId.ToUpper();
        }
    }
}
