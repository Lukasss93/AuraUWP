using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Aura.Net.Serializer
{
    public class XML
    {
        /// <summary>
        /// Serializza un oggetto in stringa usando XML
        /// </summary>
        public static string Serialize(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using(StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        /// <summary>
        /// Deserializza un oggetto da una stringa XML
        /// </summary>
        public static T Deserialize<T>(string xml)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(T));
            using(StringReader reader = new StringReader(xml))
            {
                return (T)deserializer.Deserialize(reader);
            }
        }
    }
}
