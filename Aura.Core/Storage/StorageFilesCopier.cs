using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace Aura.Storage
{
    public class StorageFilesCopier
    {
        private List<StorageFile>files;
        public List<StorageFile> Files
        {
            get { return files; }
            private set { files = value; }
        }

        private StorageFolder folder;
        public StorageFolder Folder
        {
            get { return folder; }
            set { folder = value; }
        }

        private CreationCollisionOption collisionoption;
        public CreationCollisionOption CollisionOption
        {
            get { return collisionoption; }
            set { collisionoption = value; }
        }


        
        public StorageFilesCopier() 
        {
            this.Files = new List<StorageFile>();
            this.Folder = null;
            this.CollisionOption = CreationCollisionOption.GenerateUniqueName;
        }

        public StorageFilesCopier(List<StorageFile> files, StorageFolder folder, CreationCollisionOption collisionoption=CreationCollisionOption.GenerateUniqueName)
        {
            this.Files = files;
            this.Folder = folder;
            this.CollisionOption = collisionoption;
        }

        public void AddFile(StorageFile file)
        {
            this.Files.Add(file);
        }

        public async Task CopyAsync()
        {
            foreach(StorageFile input_file in this.Files)
            {
                bool outofmemory = await StorageHelper.isThereFreeSpace(input_file, this.Folder);

                if(!outofmemory)
                {
                    throw new Exception("Out of Memory");
                }
                else
                {
                    //creo file vuoto nella cartella
                    StorageFile output_file = await this.Folder.CreateFileAsync(input_file.Name, this.CollisionOption);

                    //apro gli stream
                    Stream input_stream = await input_file.OpenStreamForReadAsync();
                    Stream output_stream = await output_file.OpenStreamForWriteAsync();

                    //imposto la dimensione del buffer ad 1 MB
                    const int BUFFER_SIZE = 1048576;
                    byte[] buf = new byte[BUFFER_SIZE];

                    //copio il file
                    int bytesread = 0;
                    while((bytesread = await input_stream.ReadAsync(buf, 0, BUFFER_SIZE)) > 0)
                    {
                        await output_stream.WriteAsync(buf, 0, bytesread);
                    }

                    //rilascio gli stream
                    output_stream.Dispose();
                    input_stream.Dispose();
                }
            }
        }


    }
}
