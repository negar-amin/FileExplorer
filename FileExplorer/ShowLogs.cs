using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer
{
	public class ShowLogs
	{
		public string historyFileDirectory => Path.Combine(Directory.GetCurrentDirectory(), "history.txt");
        public ShowLogs()
        {
            Console.WriteLine("Results");
            StreamReader sr = new StreamReader(historyFileDirectory);
            string result = sr.ReadToEnd();
            Console.WriteLine(result);
            sr.Close();
            Console.WriteLine("\n");
        }
    }
}
