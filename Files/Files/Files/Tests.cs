using System.Linq;
using NUnit.Framework;

namespace Files
{
    [TestFixture]
    public class Tests
    {
        private string filename;

        [SetUp]  
        public void SetUp()    
        {
            filename = @"C:\GitHub\Study\Files\Files\Files\bin\Debug\airquality.csv";
        }

        [Test]
        public void TestFirstReadCSV()
        {
            var lines = CsvReader.ReadCsvLines(filename);
            Assert.AreEqual("\"Sensor3\",12,149,12.6,74,5,3", lines.Take(4).Last());
        }

        [Test]
        public void TestSecondReadCSV()
        {
            var objs = CsvReader.ReadCsv2<AirQualityItem>(filename);
            var sensor1 = objs.Take(1).ToArray()[0];
            Assert.AreEqual("Sensor1",sensor1.Name);
            Assert.AreEqual(41, sensor1.Ozone);

            
        }

        [Test]
        public void TestThirdReadCSV()
        {
            var objs = CsvReader.ReadCsv3(filename);
            var sensor1 = objs.Take(1).ToArray()[0];
            Assert.AreEqual("Sensor1", (string)sensor1["Name"]);
            Assert.AreEqual(41, (int)sensor1["Ozone"]);
        }

        [Test]
        public void TestFourthReadCSV()
        {
            var windValues = CsvReader.ReadCsv4(filename).Where(z => z.Ozone > 10).Select(z => z.Wind);
            Assert.AreEqual(7.4, windValues.First());
            Assert.AreEqual(11.5, windValues.Last());
        }
    }
}