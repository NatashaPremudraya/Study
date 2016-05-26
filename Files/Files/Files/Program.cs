using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files
{
    class Program
    {
        public static IEnumerable<string> ReadCSV1(string filename)
        {
            using (var stream = new StreamReader(filename))
            {
                while (true)
                {
                    var line = stream.ReadLine();
                    if (line == null) break;
                    yield return line;
                }
            }
        }

        static void Main(string[] args)
        {
        }
    }
}
