using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace AuraRT.Globalization
{
    public class Translate
    {
        private static ResourceLoader traduci = new ResourceLoader();

        /// <summary>
        /// Restituisce il valore stringa più appropriato di una risorsa, specificato da un identificatore di risorsa.
        /// </summary>
        public static string Get(string key)
        {
            string word = traduci.GetString(key);

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
