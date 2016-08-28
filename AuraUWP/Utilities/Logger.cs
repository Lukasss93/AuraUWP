using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuraUWP.Extensions;
using Windows.Storage;

namespace AuraUWP.Utilities
{
    public class Logger
    {
        private static StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        private static string fileName = "AppLog.txt";

        public static async Task SetLog(string log)
        {
            StorageFile file = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

            StringBuilder content = new StringBuilder();
            content.Append(await file.ReadStringAsync());
            content.AppendLine(log);

            await file.WriteStringAsync(content.ToString());
        }

        public static async Task Clear()
        {
            StorageFile file = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            await file.WriteStringAsync("");
        }

    }
}
