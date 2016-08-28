using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.IO;
using Windows.UI.Xaml.Controls;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Media;

namespace AuraUWP.Imaging
{
    public class Base64
    {
        public static async Task<string> EncodeFromByte(byte[] image, uint height, uint width, double dpiX = 96, double dpiY = 96)
        {
            // encode image
            var encoded = new InMemoryRandomAccessStream();
            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, encoded);
            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, height, width, dpiX, dpiY, image);
            await encoder.FlushAsync();
            encoded.Seek(0);

            // read bytes
            var bytes = new byte[encoded.Size];
            await encoded.AsStream().ReadAsync(bytes, 0, bytes.Length);

            // create base64
            return Convert.ToBase64String(bytes);
        }

        public static async Task<string> EncodeFromStorageFile(StorageFile bitmap)
        {
            var stream = await bitmap.OpenAsync(Windows.Storage.FileAccessMode.Read);
            var decoder = await BitmapDecoder.CreateAsync(stream);
            var pixels = await decoder.GetPixelDataAsync();
            var bytes = pixels.DetachPixelData();
            return await EncodeFromByte(bytes, (uint)decoder.PixelWidth, (uint)decoder.PixelHeight, decoder.DpiX, decoder.DpiY);
        }

        public static async Task<string> EncodeFromRenderTargetBitmap(RenderTargetBitmap bitmap)
        {
            var bytes = (await bitmap.GetPixelsAsync()).ToArray();
            return await EncodeFromByte(bytes, (uint)bitmap.PixelWidth, (uint)bitmap.PixelHeight);
        }

        public static async Task<string> EncodeFromImage(Image control)
        {
            var bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(control);
            return await EncodeFromRenderTargetBitmap(bitmap);
        }

        public static async Task<string> EncodeFromWriteableBitmap(WriteableBitmap bitmap)
        {
            var bytes = bitmap.PixelBuffer.ToArray();
            return await EncodeFromByte(bytes, (uint)bitmap.PixelWidth, (uint)bitmap.PixelHeight);
        }

        public static BitmapImage DecodeToBitmapImage(string base64string)
        {
            var imageBytes = Convert.FromBase64String(base64string);
            BitmapImage image;
            using(InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream())
            {
                using(DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes((byte[])imageBytes);
                    writer.StoreAsync().GetResults();
                }

                image = new BitmapImage();
                image.SetSource(ms);
            }
            return image;
        }

        public static async Task<ImageSource> DecodeToImageSource(string base64)
        {
            // read stream
            var bytes = Convert.FromBase64String(base64);
            var image = bytes.AsBuffer().AsStream().AsRandomAccessStream();

            // decode image
            var decoder = await BitmapDecoder.CreateAsync(image);
            image.Seek(0);

            // create bitmap
            var output = new WriteableBitmap((int)decoder.PixelHeight, (int)decoder.PixelWidth);
            await output.SetSourceAsync(image);
            return output;
        }

    }
}
