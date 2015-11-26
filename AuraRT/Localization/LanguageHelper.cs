using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.System.UserProfile;

namespace AuraRT.Localization
{
    public class LanguageHelper
    {
        /// <summary>
        /// Restituisce il codice della lingua attuale
        /// </summary>
        public static string GetPhoneLanguage()
        {
            CultureInfo ci = new CultureInfo(GlobalizationPreferences.Languages[0]);
            return ci.Name;
        }

        /// <summary>
        /// Restituisce il codice della lingua dell'app
        /// </summary>
        public static string GetAppLanguage()
        {
            return ApplicationLanguages.Languages[0];
        }

        /// <summary>
        /// Imposta la lingua con il codice della lingua ricevuta
        /// </summary>
        public static void SetLanguage(string code)
        {
            string lang=null;

            if(code=="auto")
            {
                lang=GetPhoneLanguage();
            }
            else
            {
                lang=code;
            }

            ApplicationLanguages.PrimaryLanguageOverride=lang;
        }


        /// <summary>
        /// Ottiene l'elenco dichiarato dell'app delle lingue supportate
        /// </summary>
        public static IReadOnlyList<string> GetAppAvailableLanguage()
        {
            return ApplicationLanguages.ManifestLanguages;
        }
    }
}
