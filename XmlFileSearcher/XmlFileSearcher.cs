using FileExplorer.ExtensionPlatform;

namespace XmlFileSearcher
{
	public class XmlFileSearcher : IExtension
	{
		private FileTypes _fileType = FileTypes.xml;

		public FileTypes FileType
		{
			get => _fileType;
		}
		public string searchType { get; } = "title";

		public List<string> SearchFiles(string dir, string fileName, List<string> filePaths)
		{
			List<string> results = new List<string>();
			Thread searchThread = new Thread(() =>
			{
				results = SearchForFiles(dir, fileName, filePaths);
			});

			searchThread.Start();

			Console.Write("Searching ");
			while (searchThread.IsAlive)
			{
				Console.Write(".");
				Thread.Sleep(1000);

			}
			List<string> SearchForFiles(string dir, string fileName, List<string> filePaths)
			{
				try
				{

					string[] files = Directory.GetFiles(dir, "*" + fileName + $"*.{FileType}");
					foreach (string file in files)
					{
						filePaths.Add(file);
					}
					string[] subDirs = Directory.GetDirectories(dir);

					foreach (string subDir in subDirs)
					{
						Thread searchThread = new Thread(() =>
						{
							SearchForFiles(subDir, fileName, filePaths);
						});

						searchThread.Start();

					}
				}


				catch (Exception ex)
				{
					Console.WriteLine($"\n{ex.Message}");
				}


				return filePaths;
			}
			return results;
		}
	}
}