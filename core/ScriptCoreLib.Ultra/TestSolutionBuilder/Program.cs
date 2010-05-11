using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio;
using System.IO;
using ScriptCoreLib.Ultra.Studio.Languages;

namespace TestSolutionBuilder
{
	class Program
	{
		static void Main(string[] args)
		{
			var sln = new SolutionBuilder
			{
				Name = "VisualCSharpProject1",
				Language = new VisualBasicLanguage()
			};

		
			sln.WriteToConsole();
		}
	}
}
