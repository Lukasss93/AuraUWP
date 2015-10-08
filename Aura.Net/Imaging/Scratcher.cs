using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Aura.Net.Imaging
{
    public class Scratcher
    {
        private Image ImageUI;
        private Uri StartImageUri;
        private Uri EndImageUri;
        private Uri TextureImageUri;

        private WriteableBitmap StartImage;
        private WriteableBitmap EndImage;
        private WriteableBitmap TextureImage;

        private const int min = 20;
        private const int max = 30;
        private bool isMouseDown;
        private Random rand = new Random();
        private int drawSize;
        private double lastX;
        private double lastY;

        public Scratcher(Image imageUI, Uri startImageUri, Uri endImageUri, Uri textureImageUri)
        {
            this.ImageUI = imageUI;
            this.StartImageUri = startImageUri;
            this.EndImageUri = endImageUri;
            this.TextureImageUri = textureImageUri;

            StartImage = new WriteableBitmap(1, 1);
            EndImage = new WriteableBitmap(1, 1);
            TextureImage = new WriteableBitmap(1, 1);
        }

        public async void Inizialize()
        {
            await Reset();
            ImageUI.PointerPressed += ImageUI_PointerPressed;
            ImageUI.PointerReleased += ImageUI_PointerReleased;
            ImageUI.PointerMoved += ImageUI_PointerMoved;
        }

        private void ImageUI_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            lastX = 0;
            lastY = 0;
            drawSize = rand.Next(10, 12);
            isMouseDown = true;
        }

        private void ImageUI_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            isMouseDown = false;
        }

        private void ImageUI_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if(!isMouseDown)
            {
                return;
            }
            double x = e.GetCurrentPoint(ImageUI).Position.X;
            double y = e.GetCurrentPoint(ImageUI).Position.Y;

            Debug.WriteLine(Convert.ToInt32(x) + "X, " + Convert.ToInt32(y) + "Y");

            draw((int)x, (int)y);
            if(lastX == 0 && lastY == 0)
            {
                lastX = x;
                lastY = y;
                return;
            }
            double num = Math.Sqrt(Math.Pow(x - lastX, 2) + Math.Pow(y - lastY, 2));
            if(num > 12)
            {
                double num1 = num / 12 * 3;
                for(int i = 0; (double)i < num1; i++)
                {
                    double num2 = lastX + (x - lastX) / num1 * (double)i;
                    double num3 = lastY + (y - lastY) / num1 * (double)i;
                    draw((int)num2, (int)num3);
                }
            }
            lastX = x;
            lastY = y;
        }

        private void draw(int x, int y)
        {
            drawSize = drawSize + rand.Next(-2, 3);
            if(drawSize < min)
            {
                drawSize = min;
            }
            if(drawSize > max)
            {
                drawSize = max;
            }
            x = x - drawSize / 2;
            y = y - drawSize / 2;

            Rect rect = new Rect((double)x, (double)y, (double)drawSize, (double)drawSize);
            TextureImage.Blit(rect, EndImage, rect);
            rect = new Rect((double)(x - 3 + rand.Next(-1, 2)), (double)(y - 3 + rand.Next(-1, 2)), (double)(drawSize + 6 + rand.Next(-1, 2)), (double)(drawSize + 6 + rand.Next(-1, 2)));
            StartImage.Blit(rect, TextureImage, rect);
            ImageUI.Source = StartImage;
        }

        public async Task Reset()
        {
            StartImage = await StartImage.FromContent(StartImageUri);
            EndImage = await EndImage.FromContent(EndImageUri);
            TextureImage = await TextureImage.FromContent(TextureImageUri);

            ImageUI.Source = StartImage;
        }
    }
}
