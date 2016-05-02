using System;
using System.Threading;
using NUnit.Framework;

namespace Timer
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TistTimer()
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
    }
}