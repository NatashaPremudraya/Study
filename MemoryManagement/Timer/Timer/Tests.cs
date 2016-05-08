using System;
using System.Drawing;
using System.IO;
using System.Threading;
using NUnit.Framework;

namespace Timer
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestTimer()
        {
            var timer = new Timer();
            using (timer.Start())
            {
                Thread.Sleep(500);
            }
            Console.WriteLine(timer.ElapsedMilliseconds);

            using (timer.Continue())
            {
                Thread.Sleep(500);
            }
            Console.WriteLine(timer.ElapsedMilliseconds);
        }

        [Test]
        public void TestBitMap()
        {
            var bitmap = (Bitmap)Bitmap.FromFile(@"C:\Users\Sony\Desktop\_Photoshop\cat.jpg");
            var timer = new Timer();
            using (timer.Start())
            {
                using (var bitmapEditor = new BitmapEditor(bitmap))
                {
                    for (int x = 0; x < bitmap.Width; x++)
                        for (int y = 0; y < bitmap.Height; y++)
                            bitmapEditor.SetPixel(0, 1, 255, 255, 255);
                }
                Console.WriteLine($"bitmapEditorTime: {timer.ElapsedMilliseconds}");
            }

            var timer2 = new Timer();
            using (timer2.Start())
            {
                for (int x = 0; x < bitmap.Width; x++)
                    for (int y = 0; y < bitmap.Height; y++)
                        bitmap.SetPixel(x, y, Color.White);
            }
            Console.WriteLine($"bitmapTime: {timer2.ElapsedMilliseconds}");

            Console.WriteLine(Environment.CurrentDirectory);
        }
    }
}