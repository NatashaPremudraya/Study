using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Framework;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var plugins = new Dictionary<string, IPlugin>();
            var exePath = new DirectoryInfo(Directory.GetCurrentDirectory());

            // Костыль какой-то... по нормальному dll-ки лежат где-то рядом с exe-шником
            var solutionPath = exePath.Parent.Parent.Parent.FullName;
            var files = Directory.GetFiles(solutionPath, @"Plugin*.dll", SearchOption.AllDirectories)
                .Where(x => x.Contains("bin"))
                .ToArray();

            foreach (var file in files)
                try
                {
                    var assembly = Assembly.LoadFile(file);

                    foreach (var type in assembly.GetTypes())
                    {
                        var iface = type.GetInterface("IPlugin");

                        if (iface != null)
                        {
                            var plugin = (IPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin.Name, plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка загрузки плагина\n" + ex.Message);
                }

            foreach (var plugin in plugins)
            {
                Console.WriteLine(plugin.Key);
            }
        }
    }
}
