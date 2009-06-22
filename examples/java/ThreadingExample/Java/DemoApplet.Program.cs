using ScriptCoreLib;

using java.lang;

using System;
using ThreadingExample.Java.Businesslayer;

namespace ThreadingExample.Java
{
	partial class ThreadingExample
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("This example will show how to write multithreaded console.");

			var MyComputation = new LongComputation();

			MyComputation.Start();

			Console.WriteLine();
			Console.WriteLine("Press enter to stop current computation.");
			Console.ReadLine();

			MyComputation.Stop();

			Console.WriteLine("Value: " + MyComputation.Current.Value);

			Console.WriteLine();
			Console.WriteLine("Press enter to exit.");
			Console.ReadLine();
		}
	}
}
