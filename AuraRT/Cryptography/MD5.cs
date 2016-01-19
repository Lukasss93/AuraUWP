using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;

namespace AuraRT.Cryptography
{
    public class MD5
    {
        private const int CHUNK_SIZE = 1024;

        public static string Hash(string str)
        {
            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buff);
            var res = CryptographicBuffer.EncodeToHexString(hashed);
            return res;
        }
        
        public static async Task<string> Hash(IStorageFile file)
        {
            string res = string.Empty;

            using(var streamReader = await file.OpenAsync(FileAccessMode.Read))
            {
                var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
                var algHash = alg.CreateHash();

                using(BinaryReader reader = new BinaryReader(streamReader.AsStream(), Encoding.UTF8))
                {
                    byte[] chunk;

                    chunk = reader.ReadBytes(CHUNK_SIZE);
                    while(chunk.Length > 0)
                    {
                        algHash.Append(CryptographicBuffer.CreateFromByteArray(chunk));
                        chunk = reader.ReadBytes(CHUNK_SIZE);
                    }
                }

                res = CryptographicBuffer.EncodeToHexString(algHash.GetValueAndReset());
                return res;
            }
        }
    }
}
