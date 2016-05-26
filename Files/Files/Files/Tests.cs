using System.Linq;
using NUnit.Framework;

namespace Files
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestReadCSV1()
        {
            var filename = @"C:\GitHub\Study\Files\Files\Files\bin\Debug\airquality.csv";
            var lines = Program.ReadCSV1(filename);
            Assert.AreEqual("\"Sensor3\",12,149,12.6,74,5,3", lines.Take(4).Last());
        }
    }
}