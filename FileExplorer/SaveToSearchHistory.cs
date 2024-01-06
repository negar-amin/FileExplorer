using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer
{
	internal class SaveToSearchHistory
	{
		public string historyFileDirectory => Path.Combine(Directory.GetCurrentDirectory(), "history.txt");
		public SaveToSearchHistory(string rootPath, string fileName, List<string> results)
		{

			string history = $"\n\nRoot path: {rootPath}\nQuery: {fileName}\n\nFound {results.Count} files:";
			results.ForEach(r => { history = history + "\n" + r; });
			Console.WriteLine(history);
			Console.WriteLine($"\nHistory of recent search  saved to {historyFileDirectory}\n");
			try
			{
				StreamWriter sw = new StreamWriter(historyFileDirectory, true);
				sw.Write("-------------------------------------------------------------------------------------------------------------");
				sw.WriteLine(history);
				sw.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: " + e.Message);
			}
		}
	}
}
