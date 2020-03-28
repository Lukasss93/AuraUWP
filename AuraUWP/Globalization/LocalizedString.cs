using Windows.ApplicationModel.Resources;

namespace AuraUWP.Globalization
{
    public class LocalizedString
    {
        private static ResourceLoader resource = new ResourceLoader();

        /// <summary>
        /// Restituisce il valore stringa più appropriato di una risorsa, specificato da un identificatore di risorsa.
        /// </summary>
        public static string Get(string key)
        {
            string word = resource.GetString(key);

            if(word!="")
            {
                return word;
            }
            else
            {
                return "\"" + key + "\"";
            }
        }
    }
}
