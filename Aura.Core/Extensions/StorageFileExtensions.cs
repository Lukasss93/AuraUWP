using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Aura.Extensions
{
    public static class StorageFileExtensions
    {
        /// <summary>
        /// Legge un file in una stringa
        /// </summary>
        public static async Task<string> ReadStringAsync(this StorageFile file)
        {
            return await FileIO.ReadTextAsync(file,Windows.Storage.Streams.UnicodeEncoding.Utf8);
        }

        /// <summary>
        /// Scrive una stringa in un file
        /// </summary>
        public static async Task WriteStringAsync(this StorageFile file, string input)
        {
            await FileIO.WriteTextAsync(file, input,Windows.Storage.Streams.UnicodeEncoding.Utf8);
        }

        /// <summary>
        /// Controlla se un file esiste
        /// </summary>
        public static async Task<bool> Exist(this StorageFile sf)
        {
            try
            {
                StorageFile file = await StorageFile.GetFileFromPathAsync(sf.Path);
                return file != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
