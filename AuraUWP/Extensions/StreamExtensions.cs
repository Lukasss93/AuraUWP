using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuraUWP.Extensions
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Reads all available bytes from the stream. 
        /// </summary>
        /// <param name="stream">The stream to read from. </param>
        /// <returns>The read byte array. </returns>
        public static byte[] ReadToEnd(this Stream stream)
        {
            var buffer = new byte[16 * 1024];
            using(var ms = new MemoryStream())
            {
                int read;
                while((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    ms.Write(buffer, 0, read);
                return ms.ToArray();
            }
        }

        /// <summary>Converts a string to a memory stream. </summary>
        /// <param name="str">The string to convert. </param>
        /// <returns>The converted string. </returns>
        public static Stream ToStream(this string str)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
