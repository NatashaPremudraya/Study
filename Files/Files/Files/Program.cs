using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;    
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Files
{
    public class AirQualityItem
    {
        public string Name { get; set; }
        public int? SolarR { get; set; }
        public int? Ozone { get; set; }
        public double Wind { get; set; }
        public int Temp { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }

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

        public static IEnumerable<T> ReadCSV2<T>(string filename)
        {
            var lines = ReadCSV1(filename);
            var fieldNames = lines
                .First()
                .Split(',')
                .Select(x => x.Substring(1, x.Length - 2))
                .Select(x => x.Replace(".",""))
                .ToArray();
            foreach (var line in lines.Skip(1))
            {
                var lineValues = line.Split(',');
                var resultItemConstructor = typeof (T).GetConstructor(new Type[] {});
                var resultItem = resultItemConstructor.Invoke(new object[] {});
                for (var i = 0; i < fieldNames.Length; i++)
                {
                    var type = typeof (T).GetProperty(fieldNames[i]).PropertyType;
                    if (type == typeof (int))
                    {
                        try
                        {
                            typeof(T).GetProperty(fieldNames[i]).SetValue(resultItem, int.Parse(lineValues[i]));
                        }
                        catch (Exception)
                        {
                            throw new FormatException();
                        }
                        continue;
                    }
                    if (type == typeof (int?))
                    {
                        try
                        {
                            typeof(T).GetProperty(fieldNames[i]).SetValue(resultItem, int.Parse(lineValues[i]));
                        }
                        catch (Exception)
                        {
                            typeof(T).GetProperty(fieldNames[i]).SetValue(resultItem, null);
                        }
                        continue;
                    }
                    if (type == typeof (double))
                    {
                        try
                        {
                            typeof(T).GetProperty(fieldNames[i]).SetValue(resultItem, double.Parse(lineValues[i], new CultureInfo("en-us")));
                        }
                        catch (Exception)
                        {
                            throw new FormatException();
                        }
                        continue;
                    }
                    if (type == typeof(double?))
                    {
                        try
                        {
                            typeof(T).GetProperty(fieldNames[i]).SetValue(resultItem, double.Parse(lineValues[i]));
                        }
                        catch (Exception)
                        {
                            typeof(T).GetProperty(fieldNames[i]).SetValue(resultItem, null);
                        }
                        continue;
                    }
                    if (type == typeof (string))
                    {
                        try
                        {
                            typeof(T).GetProperty(fieldNames[i]).SetValue(resultItem, lineValues[i].Substring(1, lineValues[i].Length-2));
                        }
                        catch (Exception)
                        {
                            typeof(T).GetProperty(fieldNames[i]).SetValue(resultItem, null);
                        }
                        continue;
                    }

                }
                yield return (T) resultItem;
            }
        }
    }
}
