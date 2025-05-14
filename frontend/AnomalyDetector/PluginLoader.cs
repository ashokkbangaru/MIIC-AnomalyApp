using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using PluginContracts;

namespace AnomalyDetector
{
    public static class PluginLoader
    {
        public static List<IPlugin> LoadPlugins(string path)
        {
            var plugins = new List<IPlugin>();
            if (!Directory.Exists(path)) return plugins;

            foreach (var dll in Directory.GetFiles(path, "*.dll"))
            {
                try
                {
                    var asm = Assembly.LoadFrom(dll);
                    foreach (var type in asm.GetTypes())
                    {
                        if (typeof(IPlugin).IsAssignableFrom(type) && !type.IsInterface)
                        {
                            var plugin = (IPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin!);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading {dll}: {ex.Message}");
                }
            }

            return plugins;
        }
    }
}
