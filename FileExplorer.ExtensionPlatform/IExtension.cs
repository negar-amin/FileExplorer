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
		//FileType property defined for defining which file type extension is written.
		FileTypes FileType { get; }
        public string searchType { get; }
        List<string> SearchFiles(string dir, string fileName, List<string> filePaths);
	}
}