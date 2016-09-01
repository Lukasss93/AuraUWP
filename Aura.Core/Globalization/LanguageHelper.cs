using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Windows.Globalization;
using Windows.System.UserProfile;

namespace Aura.Globalization
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

        //UTILITY------------------------------------------------------------------------------------------------------------------

        public static bool IsValidISO6392(string expression)
        {
            if(expression == null)
            {
                return false;
            }

            return (new Regex("^[a-zA-Z]{2,3}([-][a-zA-Z]{2,3})?$")).IsMatch(expression);
        }
    }
}
