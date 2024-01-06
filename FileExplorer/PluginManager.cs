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
		public string pluginFolder => Path.Combine(Directory.GetCurrentDirectory(), "plugins");
		public string ProblematicExtensionFile => Path.Combine(Directory.GetCurrentDirectory(), "problematic_extension.txt");
		public PluginManager()
		{
			InitializePluginFolder();
		}
		public void InitializePluginFolder()
		{
			if (!Directory.Exists(pluginFolder))
			{
				Directory.CreateDirectory(pluginFolder);
			}
		}
		public List<Type> LoadPlugins()
		{
			//Get all dll files from plugins folder.
			var pluginFiles = Directory.GetFiles(pluginFolder, "*.dll");
			//Add all Types from original project that implement IExtension interface from FileExplorer.ExtensionPlatform
			var plugins = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IExtension).IsAssignableFrom(t)).ToList();

			StreamWriter sw = new StreamWriter(ProblematicExtensionFile);
			foreach (var pluginFile in pluginFiles)
			{
				try
				{
					var assembly = Assembly.LoadFrom(pluginFile);
					//Get all extensions that implement IExtension interface from FileExplorer.ExtensionPlatform
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
				// There was problem in loading extensions from pluginFile
				catch (Exception ex)
				{
					//Adding problematic extensions and their issues to problematic_extension.txt
					sw.WriteLine($"{ex.Message}\nSource of problem: {pluginFile}\n");
					ShowProblematicExtension.Extensions++;

				}
			}
			sw.Close();
			return plugins;
		}
	}
}
