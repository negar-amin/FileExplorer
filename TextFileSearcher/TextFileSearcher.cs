using FileExplorer.ExtensionPlatform;

namespace TextFileSearcher
{
	public class TextFileSearcher : IExtension
	{
		private FileTypes _fileType = FileTypes.txt;

		public FileTypes FileType
		{
			get => _fileType;
		}

		public List<string> SearchFiles(string dir, string fileName, List<string> filePaths)
		{
			string[] files = Directory.GetFiles(dir, "*" + fileName + $"*.{FileType}");

			foreach (string file in files)
			{
				filePaths.Add(file);
			}

			string[] subDirs = Directory.GetDirectories(dir);

			foreach (string subDir in subDirs)
			{

				SearchFiles(subDir, fileName, filePaths);

			}
			return filePaths;
		}
	}
}