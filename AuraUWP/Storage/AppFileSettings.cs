using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using AuraUWP.Extensions;
using AuraUWP.Serializer;

namespace AuraUWP.Storage
{
    public class AppFileSettings
    {
        private static StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        private static string folderName = "__AppFileSettings";
        private static string fileExtension = ".xml";

        public static async Task Initialize(string key, object value)
        {
            StorageFolder folder = await localFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            if(!await Contains(key))
            {
                await Set(key, value);
            }
        }

        public static async Task Set(string key, object value)
        {
            StorageFolder folder = await localFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            StorageFile file = await folder.CreateFileAsync(key + fileExtension, CreationCollisionOption.OpenIfExists);
            await file.WriteStringAsync(XmlSerializer.Serialize(value));
        }
        public static async Task<T> Get<T>(string key)
        {
            StorageFolder folder = await localFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            StorageFile file = await folder.GetFileAsync(key + fileExtension);
            return XmlSerializer.Deserialize<T>(await file.ReadStringAsync());
        }

        public static async Task Remove(string key)
        {
            StorageFolder folder = await localFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);            
            if(await Contains(key))
            {
                StorageFile file = await folder.GetFileAsync(key + fileExtension);
                await file.DeleteAsync();
            }
        }

        public static async Task<bool> Contains(string key)
        {
            StorageFolder folder = await localFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            return await StorageHelper.FileExist(key + fileExtension, folder);
        }

        public static async Task RemoveAll(string key)
        {
            StorageFolder folder = await localFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            IReadOnlyList<StorageFile> files = await folder.GetFilesAsync();
            foreach(StorageFile file in files)
            {
                await file.DeleteAsync();
            }
        }
    }
}
