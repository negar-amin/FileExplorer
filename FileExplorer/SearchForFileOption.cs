using FileExplorer.ExtensionPlatform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer
{
	public class SearchForFileOption
	{
		List<Type> _plugins;
		// Search get selected extension by user and call its SearchFiles method
		public static void Search(IExtension searcher)
		{
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
			SaveToSearchHistory saveToSearchHistory = new SaveToSearchHistory(rootPath, fileName, results);
		}
		// constructor of SearchForFileOption get all Extensions and ask users which Extensions they wanted to run.
		public SearchForFileOption(List<Type> plugins)
		{
			_plugins = plugins;

			//Each Extension will be add to a list and categorize by type of file they search for.
			//fileType Dictionary keys: file types for example: "TXT", "XML", ...
			//fileType Dictionary values: list of extensions that wrote to search for that file type.
			Dictionary<string, List<Type>> fileTypes = new Dictionary<string, List<Type>>();

			foreach (var plugin in _plugins)
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

			}

			//searchOptions Dictionary declared to contain all file types that has at least one extension
			//that is written to search for that file type.
			Dictionary<int, string> searchOptions = new Dictionary<int, string>();

			int optionCount = 1;
			foreach (KeyValuePair<string, List<Type>> kvp in fileTypes)
			{
				searchOptions.Add(optionCount++, kvp.Key);
			}


			Console.Write("Select among these file types (for selecting multiple options enter them with space between): ");
			foreach (var item in searchOptions)
			{
				Console.Write($"[{item.Key}]{item.Value} ");

			}


			Console.WriteLine(":");
			try
			{
				Console.ReadLine().Split(" ").ToList().ForEach(t =>
				{
					try
					{
						int key = int.Parse(t);

						//Get all extensions that implemented to search for the type user chose and show them to user;
						var AvailableExtensions = fileTypes[searchOptions[key]];
						Console.WriteLine($"Choose among these extensions for searching {searchOptions[key]} files (for selecting multiple options enter them with space between) : ");
						optionCount = 0;
						AvailableExtensions.ForEach(x => Console.WriteLine($"{++optionCount}) {x.Name}"));

						try
						{
							//Run chosen extensions one by one.
							Console.ReadLine().Split(" ").ToList().ForEach(e =>
							{
								try
								{
									var extension = AvailableExtensions[int.Parse(e) - 1];
									var searcher = (IExtension)Activator.CreateInstance(extension);
									Console.WriteLine($"\nExtension {extension.Name} running ....");
									Search(searcher);

								}
								catch
								{
									Console.WriteLine($"{e} is not a correct option");
								}
							});
						}
						catch (Exception ex) { Console.WriteLine(ex.Message); }
					}
					catch
					{
						Console.WriteLine($"{t} is not a correct option");
					}
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine("Enter option correctly!!");
			}
		}
	}
}
