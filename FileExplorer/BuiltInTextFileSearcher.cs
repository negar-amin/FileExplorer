using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FileExplorer.ExtensionPlatform;
namespace FileExplorer
{
	public class BuiltInTextFileSearcher : IExtension
	{
		//This extension is written to search for txt files.
		private FileTypes _fileType = FileTypes.txt;

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
					//Add files that contain Query entered by user in their title.
					string[] files = Directory.GetFiles(dir, "*" + fileName + $"*.{FileType}");
					foreach (string file in files)
					{
						//Add path of found files to results
						filePaths.Add(file);
					}
					string[] subDirs = Directory.GetDirectories(dir);

					//For each sub directorys start a new thread to search for the Query.
					foreach (string subDir in subDirs)
					{
						Thread searchThread = new Thread(() =>
						{
							SearchForFiles(subDir, fileName, filePaths);
						});

						searchThread.Start();

					}
				}

				//For when access to a file or dirctory is denied.
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
