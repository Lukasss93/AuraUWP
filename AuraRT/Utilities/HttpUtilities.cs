using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AuraRT.Utilities
{
    public class HttpUtilities
    {
        /// <summary>Restituisce un dizionario con gli argomenti di un url.</summary>
        /// <param name="queryString">Parametri da passare.</param>
        /// <returns>Dizionario chiave, valore.</returns>
        public static Dictionary<string, string> ParseQueryString(string queryString)
        {
            var dict = new Dictionary<string, string>();
            foreach(var s in queryString.Split('&'))
            {
                var index = s.IndexOf('=');
                if(index != -1 && index + 1 < s.Length)
                {
                    var key = s.Substring(0, index);
                    var value = Uri.UnescapeDataString(s.Substring(index + 1));
                    dict.Add(key, value);
                }
            }
            return dict;
        }
        
        public static string ParametersToString(Dictionary<string, string> parameters)
        {
            string output = "";

            if(parameters != null)
            {
                int i = 0;
                foreach(var parameter in parameters)
                {
                    output += parameter.Key + "=" + parameter.Value;

                    output += (i != parameters.Count() - 1) ? "&" : "";

                    i++;
                }
            }

            return output;
        }

        public static HttpContent ParametersToHttpContent(Dictionary<string,string> parameters)
        {
            MultipartFormDataContent form = new MultipartFormDataContent();
            foreach(var item in parameters)
            {
                form.Add(new StringContent(item.Value.ToString()), item.Key);
            }

            HttpContent postdata = form;

            return postdata;
        }

        public static bool IsConnectedToInternet()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
    }
}
