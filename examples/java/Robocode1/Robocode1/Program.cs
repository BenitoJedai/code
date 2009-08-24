using System.Threading;
using System;

using ScriptCoreLib;


namespace Robocode1
{

	[Script]
	public class Program
	{
		public static string Text { get; set; }


		public static void Main(string[] args)
		{
			// Use Release Build to use jsc to generate java program
			// Use Debug Build to develop on .net

			Console.WriteLine("Robocode1. Crosscompiled from C# to Java.");

			// Extension methods...
			("This will take a few seconds...").ToConsole();

			Text = "Hello World";


			var w = new Worker { Count = 3, Delay = 1000 };

			w.Handler +=
				delegate
				{
					var n = DateTime.Now;

					Console.WriteLine(n.Hour + ":" + n.Minute + ":" + n.Second);
				};

			w.Invoke();

			Console.WriteLine(Text);
		}

		[Script]
		public class Worker
		{
			public event Action Handler;

			public int Count { get; set; }
			public int Delay { get; set; }

			public void Invoke()
			{
				for (int i = 0; i < Count; i++)
				{
					Handler();
					Thread.Sleep(Delay);
				}
			}
		}
	}


}
