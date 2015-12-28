using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace AuraRT.Serializer
{
    public class Json
    {
        /// <summary>
        /// Serializza un oggetto in stringa usando Json
        /// </summary>
        public static string Serialize(object obj, Formatting indentato=Formatting.Indented)
        {
            return JsonConvert.SerializeObject(obj, indentato);
        }

        /// <summary>
        /// Deserializza un oggetto da una stringa Json
        /// </summary>
        public static T Deserialize<T>(string str)
        {
            return (T)JsonConvert.DeserializeObject<T>(str);
        }

        /// <summary>
        /// Deserializza un oggetto dinamico da una stringa Json
        /// </summary>
        public static dynamic DeserializeDynamic(string str)
        {
            /*example
            dynamic json = Json.DeserializeDynamic(str);
             
            foreach (var feature in json.features)
            {
                Console.Write("{0},{1} - {2},{3} : ", 
                    feature.bounds[0][0], feature.bounds[0][1], 
                    feature.bounds[1][0], feature.bounds[1][1]);
            
                Console.WriteLine("{0} {1} {2} {3}", 
                    feature.location.country, feature.location.county, feature.location.city, feature.location.road);
            }
            */

            return (dynamic)JsonConvert.DeserializeObject(str);
        }

        /// <summary>
        /// Controlla se la stringa è in formato Json
        /// </summary>
        /// <param name="content">Stringa Json</param>
        /// <returns>Ritorna true se la stringa è in formato Json</returns>
        public static bool isValidJson(string content)
        {
            bool valid = true;

            try
            {
                var token = JToken.Parse(content);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

        public static bool isValid<T>(string str)
        {
            bool response = false;

            try
            {
                var jsonobject = Json.Deserialize<T>(str);
                response = true;
            }
            catch
            {
                response = false;
            }

            return response;
        }


    }
}
