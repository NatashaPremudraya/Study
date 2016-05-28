using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Files
{
    class CsvReader
    {
        public static IEnumerable<string> ReadCsvLines(string filename)
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

        public static IEnumerable<T> ReadCsv2<T>(string filename)
        {
            return ReadObjectsFromCsv(filename, ToObject<T>);
        }

        public static IEnumerable<Dictionary<string, object>> ReadCsv3(string filename)
        {
            return ReadObjectsFromCsv(filename, ToDictionary);
        }

        public static IEnumerable<dynamic> ReadCsv4(string filename)
        {
            return ReadObjectsFromCsv(filename, (x, y) => ToDinamicObject(x, y));
        }

        private static IEnumerable<T> ReadObjectsFromCsv<T>(string filename, Func<string[], string[], T> toResultObject)
        {
            var lines = ReadCsvLines(filename);
            var fieldNames = lines
                .First()
                .Split(',')
                .Select(x => x.Substring(1, x.Length - 2))
                .Select(x => x.Replace(".", ""))
                .ToArray();

            return lines
                .Skip(1)
                .Select(line => line.Split(','))
                .Select(lineValues => toResultObject(fieldNames, lineValues));
        }

        private static T ToObject<T>(string[] fieldNames, string[] lineValues)
        {
            var resultItemConstructor = typeof (T).GetConstructor(new Type[] {});
            var resultItem = resultItemConstructor.Invoke(new object[] {});
            for (var i = 0; i < fieldNames.Length; i++)
            {
                var type = typeof (T).GetProperty(fieldNames[i]).PropertyType;
                if (type == typeof (int))
                {
                    try
                    {
                        typeof (T).GetProperty(fieldNames[i]).SetValue(resultItem, int.Parse(lineValues[i]));
                    }
                    catch (Exception)
                    {
                        throw new FormatException("Формат CSV файла не сооветствует записываемой модели");
                    }
                    continue;
                }
                if (type == typeof (int?))
                {
                    try
                    {
                        typeof (T).GetProperty(fieldNames[i]).SetValue(resultItem, int.Parse(lineValues[i]));
                    }
                    catch (Exception)
                    {
                        typeof (T).GetProperty(fieldNames[i]).SetValue(resultItem, null);
                    }
                    continue;
                }
                if (type == typeof (double))
                {
                    try
                    {
                        typeof (T).GetProperty(fieldNames[i])
                            .SetValue(resultItem, double.Parse(lineValues[i], new CultureInfo("en-us")));
                    }
                    catch (Exception)
                    {
                        throw new FormatException("Формат CSV файла не сооветствует записываемой модели");
                    }
                    continue;
                }
                if (type == typeof (double?))
                {
                    try
                    {
                        typeof (T).GetProperty(fieldNames[i]).SetValue(resultItem, double.Parse(lineValues[i]));
                    }
                    catch (Exception)
                    {
                        typeof (T).GetProperty(fieldNames[i]).SetValue(resultItem, null);
                    }
                    continue;
                }
                if (type == typeof (string))
                {
                    try
                    {
                        typeof (T).GetProperty(fieldNames[i])
                            .SetValue(resultItem, lineValues[i].Substring(1, lineValues[i].Length - 2));
                    }
                    catch (Exception)
                    {
                        typeof (T).GetProperty(fieldNames[i]).SetValue(resultItem, null);
                    }
                    continue;
                }
                throw new ArgumentException("Неподдерживаемый тип значения");
            }
            return (T)resultItem;
        }



        private static Dictionary<string, object> ToDictionary(string[] fieldNames, string[] lineValues)
        {
            var resultItem = new Dictionary<string, object>();
            for (var i = 0; i < fieldNames.Length; i++)
            {
                if (lineValues[i].Equals("NA"))
                {
                    resultItem[fieldNames[i]] = null;
                    continue;
                }

                int iValue;
                if (int.TryParse(lineValues[i], out iValue))
                {
                    resultItem[fieldNames[i]] = iValue;
                    continue;
                }

                double dValue;
                if (double.TryParse(lineValues[i], out dValue))
                {
                    resultItem[fieldNames[i]] = dValue;
                    continue;
                }

                resultItem[fieldNames[i]] = lineValues[i].Substring(1, lineValues[i].Length - 2);
            }
            return resultItem;
        }

        private static dynamic ToDinamicObject(string[] fieldNames, string[] lineValues)
        {
            dynamic resultItem = new ExpandoObject();
            for (var i = 0; i < fieldNames.Length; i++)
            {
                if (lineValues[i].Equals("NA"))
                {
                    ((IDictionary<string, object>) resultItem)[fieldNames[i]] = null;
                    continue;
                }

                int iValue;
                if (int.TryParse(lineValues[i], out iValue))
                {
                    ((IDictionary<string, object>) resultItem)[fieldNames[i]] = iValue;
                    continue;
                }

                double dValue;
                if (double.TryParse(lineValues[i], NumberStyles.AllowDecimalPoint, new CultureInfo("en-us"),
                    out dValue))
                {
                    ((IDictionary<string, object>) resultItem)[fieldNames[i]] = dValue;
                    continue;
                }

                ((IDictionary<string, object>) resultItem)[fieldNames[i]] = lineValues[i].Substring(1,
                    lineValues[i].Length - 2);
            }
            return resultItem;
        }
    }
}
