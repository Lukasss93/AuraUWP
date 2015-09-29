using Aura.Net.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Aura.Net.Storage
{
    public class SettingsHelper
    {
        private static ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;

        /// <summary>
        /// Inizializza l'impostazione
        /// </summary>
        public static void Initialize(string key, object oggetto)
        {
            if(!Contains(key))
            {
                settings.Values.Add(key, oggetto);                
            }
        }

        /// <summary>
        /// Setta l'impostazione
        /// </summary>
        public static void Set(string key, object oggetto)
        {
             settings.Values[key] = oggetto;            
        }

        /// <summary>
        /// Ritorna un valore dall'impostazione
        /// </summary>
        public static object Get(string key)
        {
            return settings.Values[key];
        }



        public static void InitializeWithXML(string key, object oggetto)
        {
            if(!Contains(key))
            {
                settings.Values.Add(key, XML.Serialize(oggetto));
            }
        }

        public static void SetWithXML(string key, object oggetto)
        {
            settings.Values[key] = XML.Serialize(oggetto);
        }

        public static T GetWithXML<T>(string key)
        {
            return XML.Deserialize<T>(settings.Values[key].ToString());
        }
        


        
        /// <summary>
        /// Rimuove l'impostazione se esiste
        /// </summary>
        public static void Remove(string key)
        {
            if(Contains(key))
            {
                settings.Values.Remove(key);
            }
        }

        /// <summary>
        /// Controlla se esiste l'impostazione
        /// </summary>
        public static bool Contains(string key)
        {
            return settings.Values.ContainsKey(key);
        }

        /// <summary>
        /// Rimuove tutte le impostazioni
        /// </summary>
        public static void RemoveAll(string key)
        {
            foreach(var a in settings.Values)
            {
                Remove(a.Key);
            }
        }





        
    }
}
