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
			Dictionary<string, List<Type>> fileTypes = new Dictionary<string, List<Type>>();
			foreach (var plugin in plugins)
			{
				var searcher = (IExtension)Activator.CreateInstance(plugin);
				if (!fileTypes.ContainsKey(searcher.FileType.ToString().ToUpper()))
				{
					fileTypes.Add(searcher.FileType.ToString().ToUpper(), new List<Type> { plugin });
				}
				else
				{
					fileTypes[searcher.FileType.ToString().ToUpper()].Add(plugin);
				}

				//Search(searcher);
			}
			Dictionary<int, string> searchOptions = new Dictionary<int, string>();
			int optionCount = 1;
			foreach (KeyValuePair<string, List<Type>> kvp in fileTypes)
			{
				searchOptions.Add(optionCount++, kvp.Key);
			}
			Console.Write("Select one of these file types: ");
			foreach (var item in searchOptions)
			{
				Console.Write($"[{item.Key}]{item.Value} ");

			}
			Console.WriteLine(":");
			try
			{

				var AvailableExtensions = fileTypes[searchOptions[int.Parse(Console.ReadLine())]];
				AvailableExtensions.ForEach(x => Console.WriteLine(x.Name));
			}
			catch (Exception ex)
			{
				Console.WriteLine("Enter option correctly!!");
			}
		}
	}
}