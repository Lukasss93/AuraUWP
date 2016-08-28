using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.System.UserProfile;

namespace AuraUWP.Globalization
{
    public class LanguageHelper
    {

        //APP------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Restituisce il codice della lingua dell'app
        /// </summary>
        public static string GetAppLanguage()
        {
            return ApplicationLanguages.Languages[0];
        }

        /// <summary>
        /// Ottiene l'elenco dichiarato dell'app delle lingue supportate
        /// </summary>
        public static IReadOnlyList<string> GetAppAvailableLanguage()
        {
            return ApplicationLanguages.ManifestLanguages;
        }

        /// <summary>
        /// Imposta la lingua con il codice della lingua ricevuta
        /// </summary>
        public static void SetLanguage(string code)
        {
            var culture = new CultureInfo(code == "auto" ? GetPhoneLanguagesCode()[0] : code);

            ApplicationLanguages.PrimaryLanguageOverride = culture.Name;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }


        //PHONE------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Ottiene l'elenco dei codici delle lingue installate nel telefono in ordine di preferenza
        /// </summary>
        public static IReadOnlyList<string> GetPhoneLanguagesCode()
        {
            var langs = new List<string>();

            foreach(var lang in GlobalizationPreferences.Languages)
            {
                CultureInfo ci = new CultureInfo(lang);
                langs.Add(ci.Name);
            }

            return langs;
        }
    }
}
