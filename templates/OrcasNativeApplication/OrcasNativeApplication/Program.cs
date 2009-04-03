using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Threading;
using System.IO;

namespace OrcasNativeApplication
{
	[Script]
	public unsafe class NativeClass1
	{
		[Script(NoDecoration = true)]
		public static int main()
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("[NativeClass1]");
			Console.ForegroundColor = ConsoleColor.Yellow;

			File.WriteAllText("log.txt", "this is the result");

			var music = new List<int>
			{
				100,
				600,
				300,
				700,
				100,
				200,
				500,
				50,
				50,
				600
			};


			

			foreach (char c in "hello world. c# code has been converted to c code. and you are running it!")
			{
				Console.Beep(37 + c  , 10);
				Console.Write(c);

				if (c == '.')
					Thread.Sleep(100);
				else
					Thread.Sleep(20);
			}

			Console.WriteLine();

			for (int r = 0; r < 2; r++)
			{
				for (int i = 0; i < music.Count; i++)
				{
					var x = (int)music[i];

					Console.Write(x);
					Console.Write(" ");

					Console.Beep(x, 50);
					Thread.Sleep(20);
				}
			}
		


			Console.WriteLine("got music?");


			for (int i = 37; i < 1800; i += 100)
			{
				Console.Beep(i, 50);
				Thread.Sleep(20);
			}


			Console.Write('j');
			Console.Write('s');
			Console.Write('c');

			Console.Beep(1200, 400);

			return 0;
		}

	
	}

	class Program
	{
		static void Main(string[] args)
		{
			NativeClass1.main();
		}
	}
}
