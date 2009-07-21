using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using ScriptCoreLib;

namespace DelegateExample
{
	[Script]
	public delegate void StopwatchDelegate(TimeSpan Elapsed);

	[Script]
	public static class StopwatchDelegateExtensions
	{
		/// <summary>
		/// This method will print to console on what is going to be done, then it will measure the action and
		/// after the action returns the time elapsed in milliseconds will be printed to the console.
		/// </summary>
		/// <param name="e"></param>
		/// <param name="h"></param>
		/// <returns></returns>
		public static TimeSpan Measure(this string e, Action h)
		{
			StopwatchDelegate s = (StopwatchConsoleHint)e;

			return s.Measure(h);
		}

		public static TimeSpan Measure(this StopwatchDelegate e, Action h)
		{
			var s = new Stopwatch();
			s.Start();
			h();
			s.Stop();
			e(s.Elapsed);

			return s.Elapsed;
		}

		[Script]
		class Measure_IDisposable : IDisposable
		{
			public Action Handler;

			#region IDisposable Members

			public void Dispose()
			{
				Handler();
			}

			#endregion
		}

		public static IDisposable Measure(this string e)
		{
			return Measure((StopwatchConsoleHint)e);
		}

		public static IDisposable Measure(this StopwatchDelegate h)
		{

			var s = new Stopwatch();
			s.Start();

			return new Measure_IDisposable
			{
				Handler =
					delegate
					{
						s.Stop();
						h(s.Elapsed);
					}
			};
		}
	}


	[Script]
	public class StopwatchConsoleHint
	{
		public string Text;

		public static implicit operator StopwatchConsoleHint(string e)
		{
			Console.WriteLine(e);
			return new StopwatchConsoleHint { Text = e };
		}

		public static implicit operator StopwatchDelegate(StopwatchConsoleHint e)
		{
			return t =>
			{
				Console.WriteLine(e.Text + " done in " + t.TotalMilliseconds + "ms");
			};
		}
	}
}
