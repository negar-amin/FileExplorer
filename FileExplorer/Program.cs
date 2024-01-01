using FileExplorer.ExtensionPlatform;

namespace FileExplorer
{
	internal class Program
	{
		public static void Search(IExtension searcher)
		{
			string choice = "";
			Console.WriteLine("Pick the root path:");
			string rootPath = Console.ReadLine();

			Console.WriteLine("Query:");
			string fileName = Console.ReadLine();
			List<string> results = new List<string>();
			try
			{
				searcher.SearchFiles(rootPath, fileName, results);

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			Console.WriteLine("\nFound {0} {1} files:", results.Count, searcher.FileType);
			results.ForEach(Console.WriteLine);
		}
		static void Main(string[] args)
		{

			PluginManager pluginManager = new PluginManager();
			var plugins = pluginManager.LoadPlugins();
			Dictionary<FileTypes, List<Type>> fileTypes = new Dictionary<FileTypes, List<Type>>();
			foreach (var plugin in plugins)
			{
				var searcher = (IExtension)Activator.CreateInstance(plugin);
                if (!fileTypes.ContainsKey(searcher.FileType))
				{
					fileTypes.Add(searcher.FileType, new List<Type> { plugin });
				}
				else
				{
					fileTypes[searcher.FileType].Add(plugin);
				}

				//Search(searcher);
			}
			foreach (KeyValuePair<FileTypes, List<Type>> kvp in fileTypes)
			{
				Console.WriteLine(kvp.Key.ToString().ToUpper());
                kvp.Value.ForEach(t => Console.WriteLine(t.Name));

			}
		}
	}
}