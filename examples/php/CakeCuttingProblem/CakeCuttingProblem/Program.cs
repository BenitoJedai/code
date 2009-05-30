using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CakeCuttingProblem.Library;

namespace CakeCuttingProblem
{
	class Program
	{


		static void Main(string[] args)
		{
			// http://en.wikipedia.org/wiki/Fair_division
			// http://deepzone0.ttu.ee/aa/modules.php?name=News&file=article&sid=226

			Func<ConsoleColor, Action<string>> WithColor =
				color =>
					text =>
					{
						Console.ForegroundColor = color;
						Console.WriteLine(text);
					};


			DemoSituation.Demo(
				WithColor(ConsoleColor.Red),
				WithColor(ConsoleColor.Green),
				WithColor(ConsoleColor.Blue),
				WithColor(ConsoleColor.Yellow)
			);



	
		}






	}

}
