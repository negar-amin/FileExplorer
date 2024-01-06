using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer
{

	public class ShowProblematicExtension
	{
		public static int Extensions  { get; set;} = 0;
		private string ProblematicExtensionFile => Path.Combine(Directory.GetCurrentDirectory(), "problematic_extension.txt");
		public ShowProblematicExtension()
        {
			StreamReader streamReader = new StreamReader(ProblematicExtensionFile);
			string context = streamReader.ReadToEnd();
            Console.WriteLine(context);
			streamReader.Close();
        }
    }
}
