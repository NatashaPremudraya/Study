using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Timer
{
    public class BitmapEditor : IDisposable
    {
        private Bitmap bitmap;
        private BitmapData bmpData;
        private int bytes;
        byte[] rgbValues;
        IntPtr ptr;

        public BitmapEditor(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            bmpData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite,
                bitmap.PixelFormat);
            ptr = bmpData.Scan0;
            bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
            rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes);
        }

        public void Dispose()
        {
            Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(bmpData);
        }

        public void SetPixel(int x, int y, byte red, byte green, byte blue)
        {
            rgbValues[x * (bmpData.Stride) + y] = red;
            rgbValues[x * (bmpData.Stride) + y + 1] = green;
            rgbValues[x * (bmpData.Stride) + y + 2] = blue;
        }
    }
}