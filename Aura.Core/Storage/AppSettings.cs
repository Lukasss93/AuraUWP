using System.Collections.Generic;
using System.Linq;
using Windows.Storage;

namespace Aura.Storage
{
    public class AppSettings
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

        /// <summary>
        /// Ritorna una lista di tutte le impostazioni
        /// </summary>
        public static List<KeyValuePair<string,object>> GetAll()
        {
            return settings.Values.ToList();
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
