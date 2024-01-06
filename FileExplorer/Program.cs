using FileExplorer.ExtensionPlatform;

namespace FileExplorer
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//First all Extensions will be loaded in PluginManager Class all Extensions that loaded with problem will be detected.
			PluginManager pluginManager = new PluginManager();
			var plugins = pluginManager.LoadPlugins();

			bool exit = false;
			while (!exit)
			{
				Console.WriteLine("=== BOOTCAMP SEARCH :: An extendible command-line search tool **Credit To: Negar Amin Gheydari** ===\n");
				Console.WriteLine("1. Search for files\n2. View search history\n3.Manage Extensions\n4. Exit");
				if (ShowProblematicExtension.Extensions > 0)
					Console.WriteLine($"\nNOTE: There was a problem with loading {ShowProblematicExtension.Extensions} extensions. View them in Manage Extensions section.");
				Console.WriteLine("\nPlease input an option: ");
				try
				{

					int choice = int.Parse(Console.ReadLine());
					switch (choice)
					{
						//Search for files
						case 1:
							SearchForFileOption searchForFileOption = new SearchForFileOption(plugins);
							break;
						//View search history
						case 2:
							ShowLogs showLogs = new ShowLogs();
							break;
						//Manage Extensions
						case 3:
							ShowProblematicExtension problematicExtensions = new ShowProblematicExtension();
							break;
						//Exit
						case 4:
							exit = true;
							break;
						//When number that user entered is not in list of options
						default:
							Console.WriteLine("Enter Option correctly!");
							break;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}


			}



		}
	}
}