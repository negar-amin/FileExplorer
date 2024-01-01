namespace FileExplorer.ExtensionPlatform
{
	public enum FileTypes
	{
		txt, 
		json, 
		xml
	}
	public interface IExtension
	{
		FileTypes FileType { get; }
		List<string> SearchFiles(string dir, string fileName, List<string> filePaths);
	}
}