using FileExplorer.ExtensionPlatform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer
{
	public class PluginManager
	{
		public PluginManager()
		{
			InitializePluginFolder();
		}
		public String pluginFolder => Path.Combine(Directory.GetCurrentDirectory(), "plugins");
		public void InitializePluginFolder()
		{
			if (!Directory.Exists(pluginFolder))
			{
				Directory.CreateDirectory(pluginFolder);
			}
		}
		public List<Type> LoadPlugins()
		{
			var pluginFiles = Directory.GetFiles(pluginFolder, "*.dll");
			var plugins = new List<Type>();
			foreach (var pluginFile in pluginFiles)
			{
                var assembly = Assembly.LoadFrom(pluginFile);
				var exTypes = assembly.GetTypes()
					.Where(t => typeof(IExtension).IsAssignableFrom(t) &&
					!t.IsAbstract &&
					!t.IsInterface)
					.ToList();
				foreach (var exType in exTypes)
				{
					plugins.Add(exType);
                }
			}
            return plugins;
		}
	}
}
