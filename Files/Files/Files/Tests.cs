using System.Linq;
using NUnit.Framework;

namespace Files
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestFirstReadCSV()
        {
            var filename = @"C:\GitHub\Study\Files\Files\Files\bin\Debug\airquality.csv";
            var lines = Program.ReadCSV1(filename);
            Assert.AreEqual("\"Sensor3\",12,149,12.6,74,5,3", lines.Take(4).Last());
        }

        [Test]
        public void TestSecondReadCSV2()
        {
            var filename = @"C:\GitHub\Study\Files\Files\Files\bin\Debug\airquality.csv";
            var objs = Program.ReadCSV2<AirQualityItem>(filename);
            var sensor1 = objs.Take(1).ToArray()[0];
            Assert.AreEqual("Sensor1",sensor1.Name);
            Assert.AreEqual(41, sensor1.Ozone);
        }


    }
}