using ScriptCoreLib;

using java.lang;

using System;
using ThreadingExample.Java.Businesslayer;
using System.Diagnostics;

namespace ThreadingExample.Java
{
	[Script]
	public delegate void StopwatchDelegate(TimeSpan Elapsed);

	[Script]
	public static class StopwatchDelegateExtensions
	{
		public static TimeSpan Measure(this StopwatchDelegate e, Action h)
		{
			var s = new Stopwatch();
			s.Start();
			h();
			s.Stop();
			e(s.Elapsed);

			return s.Elapsed;
		}
	}

	partial class ThreadingExample
	{
		public static void Main(string[] args)
		{
			
			Console.WriteLine("This example will show how to write multithreaded console.");

			var MyComputation = new LongComputation();

			StopwatchDelegate GotTime =
				e =>
				{
					Console.WriteLine("time: " + e.TotalMilliseconds + "ms");
				};

			GotTime.Measure(
				delegate
				{
					MyComputation.Start();

					Console.WriteLine();
					Console.WriteLine("Press enter to stop current computation.");
					Console.ReadLine();

					MyComputation.Stop();

					Console.WriteLine("Value: " + MyComputation.Current.Value);
				}
			);

			Console.WriteLine();
			Console.WriteLine("Press enter to exit.");
			Console.ReadLine();
		}
	}
}
