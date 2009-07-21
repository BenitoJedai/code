using System.Threading;
using System;

using ScriptCoreLib;


namespace InlineLambdaTest
{

	[Script]
	public class Program
	{
		public static string Text { get; set; }


		public static void Main(string[] args)
		{
			// Use Release Build to use jsc to generate java program
			// Use Debug Build to develop on .net

			Console.WriteLine("InlineLambdaTest. Crosscompiled from C# to Java.");


			Worker.Invoke(
				() => new Worker { Count = 0, Delay = 0 }
			);
		}

		[Script]
		public class Worker
		{
			public int Count { get; set; }
			public int Delay { get; set; }

			public static void Invoke(Action w)
			{

			}
		}
	}


}
