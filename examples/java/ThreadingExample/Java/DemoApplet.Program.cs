using ScriptCoreLib;

using java.lang;

using System;
using ThreadingExample.Java.Businesslayer;
using System.Diagnostics;

namespace ThreadingExample.Java
{
	partial class ThreadingExample
	{
		public static void Main(string[] args)
		{
			
			Console.WriteLine("This example will show how to write multithreaded console.");

			var MyComputation = new LongComputation();

			var s = new Stopwatch();
			s.Start();
			MyComputation.Start();

			Console.WriteLine();
			Console.WriteLine("Press enter to stop current computation.");
			Console.ReadLine();

			MyComputation.Stop();
			s.Stop();

			Console.WriteLine("Value: " + MyComputation.Current.Value);
			Console.WriteLine(s.ToString());
			Console.WriteLine();
			Console.WriteLine("Press enter to exit.");
			Console.ReadLine();
		}
	}
}
