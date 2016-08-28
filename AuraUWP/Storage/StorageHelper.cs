using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace AuraUWP.Storage
{
    public class StorageHelper
    {
        /// <summary>
        /// Formatta i byte in altre misure
        /// </summary>
        public static string SizeConvert(ulong peso)
        {
            if(peso < 1000)
            {
                return Math.Round((Convert.ToDouble(peso)), 2).ToString() + " Byte";
            }
            else if(peso < 1000000)
            {
                return Math.Round((Convert.ToDouble(peso) / 1000), 2).ToString() + " KB";
            }
            else if(peso < 1000000000)
            {
                return Math.Round((Convert.ToDouble(peso) / 1000000), 2).ToString() + " MB";
            }
            else if(peso < 1000000000000)
            {
                return Math.Round((Convert.ToDouble(peso) / 1000000000), 2).ToString() + " GB";
            }
            else
            {
                return Math.Round((Convert.ToDouble(peso) / 1000000000000), 2).ToString() + " TB";
            }
        }


        /// <summary>
        /// Restituisce la dimensione di File o di una Cartella
        /// </summary>
        public static async Task<string> GetItemSize(IStorageItem item)
        {
            if(item.IsOfType(StorageItemTypes.File))
            {
                BasicProperties bp = await item.GetBasicPropertiesAsync();
                ulong peso = bp.Size;
                return SizeConvert(peso);
            }
            else if(item.IsOfType(StorageItemTypes.Folder))
            {
                StorageHelper total =new StorageHelper();
                await total.FilesNumber((StorageFolder)item);
                return SizeConvert(total.GetFilesSize());
            }
            else
            {
                return SizeConvert(0);
            }
        }

        /// <summary>
        /// Restituisce il numero di file in una cartella
        /// </summary>
        public static async Task<int> GetFilesNumber(StorageFolder cartella)
        {
            StorageHelper total =new StorageHelper();
            await total.FilesNumber(cartella);
            return total.GetFilesCount();
        }

        /// <summary>
        /// Restituisce true se c'è abbastanza spazio nel folder per salvare il filesize
        /// </summary>
        public static async Task<bool> isThereFreeSpace(StorageFile file, StorageFolder folder)
        {
            BasicProperties bp = await file.GetBasicPropertiesAsync();
            ulong filesize = bp.Size;


            var retrivedProperties = await folder.Properties.RetrievePropertiesAsync(new string[] { "System.FreeSpace" });
            ulong sizefree = (ulong)retrivedProperties["System.FreeSpace"];

            if(filesize<sizefree)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static async Task<bool> FileExist(string filename, StorageFolder folder)
        {
            try
            {
                StorageFile file = await folder.GetFileAsync(filename);
                return file != null;
            }
            catch
            {
                return false;
            }
        }

        

        //PRIVATE*******************************************************************************
        private int file_count=0;
        private ulong file_size=0;

        private StorageHelper()
        {
            file_count=0;
            file_size=0;
        }

        private async Task FilesNumber(StorageFolder cartella)
        {
            var Items = await cartella.GetItemsAsync();
            foreach(IStorageItem Item in Items)
            {
                if(Item.IsOfType(StorageItemTypes.Folder))
                {
                    await FilesNumber((StorageFolder)Item);
                }
                else if(Item.IsOfType(StorageItemTypes.File))
                {
                    file_count++;

                    BasicProperties bp = await ((StorageFile)Item).GetBasicPropertiesAsync();
                    file_size+= bp.Size;

                }
            }
        }

        private int GetFilesCount()
        {
            return file_count;
        }

        private ulong GetFilesSize()
        {
            return file_size;
        }

    }
}
