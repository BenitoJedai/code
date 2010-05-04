using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Studio;
using System.IO;

namespace TestSolutionBuilder
{
	class Program
	{
		static void Main(string[] args)
		{
			new SolutionBuilder
			{
				Name = "VisualCSharpProject1"
			}.WriteToConsole();
		}
	}
}
