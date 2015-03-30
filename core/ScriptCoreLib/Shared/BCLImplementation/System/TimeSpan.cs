using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
	// http://referencesource.microsoft.com/#mscorlib/system/timespan.cs
	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/TimeSpan.cs
	// https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/TimeSpan.cs


	[Script(Implements = typeof(global::System.TimeSpan))]
	internal class __TimeSpan
	{
		public __TimeSpan()
		{

		}

		public const long TicksPerMillisecond = 10000;

		public long Ticks
		{
			get
			{
				// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131224

				return (long)(TotalMilliseconds * TicksPerMillisecond);
			}
		}

		public double TotalMilliseconds { get; set; }

		public static TimeSpan Parse(string e)
		{
			return default(TimeSpan);
		}

		public static TimeSpan FromSeconds(double value)
		{
			return new __TimeSpan { TotalMilliseconds = value * 1000 };
		}

		public static TimeSpan FromMilliseconds(double value)
		{
			return new __TimeSpan { TotalMilliseconds = value };
		}

		public static TimeSpan FromTicks(long value)
		{
			return new __TimeSpan { TotalMilliseconds = value / TicksPerMillisecond };
		}


		public static TimeSpan FromMinutes(double value)
		{
			return new __TimeSpan { TotalMilliseconds = value * 1000 * 60 };
		}

		public static TimeSpan FromHours(double value)
		{
			return new __TimeSpan { TotalMilliseconds = value * 1000 * 60 * 60 };
		}

		public static TimeSpan FromDays(double value)
		{
			return new __TimeSpan { TotalMilliseconds = value * 1000 * 60 * 60 * 24 };
		}

		public override string ToString()
		{
			// X:\jsc.svn\examples\actionscript\Test\TestDateTimeToTimeSpan\TestDateTimeToTimeSpan\ApplicationCanvas.cs

			var w =
				("" + Hours).PadLeft(2, '0') + ":"
				+ ("" + Minutes).PadLeft(2, '0') + ":"
				+ ("" + Seconds).PadLeft(2, '0')

				+ "." + this.TotalMilliseconds;

			if (Days == 0)
				return w;


			return Days + "." + w;
		}


		public double TotalDays
		{
			get
			{
				return
					TotalMilliseconds / (1000 * 60 * 60 * 24)
				;
			}
		}

		public double TotalHours
		{
			get
			{
				return Convert.ToInt32(
					TotalMilliseconds / (1000 * 60 * 60)
				);
			}
		}

		public double TotalMinutes
		{
			get
			{
				return Convert.ToInt32(
					TotalMilliseconds / (1000 * 60)
				);
			}
		}


		// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyColumns\ChromeShaderToyColumns\Library\ShaderToy.cs
		public double TotalSeconds
		{
			get
			{
				return Convert.ToInt32(
					TotalMilliseconds / (1000)
				);
			}
		}

		public int Milliseconds
		{
			get
			{
				return (int)((long)TotalMilliseconds) % 1000;
			}
		}

		public int Seconds
		{
			get
			{
				return Convert.ToInt32( TotalSeconds) % 60;
			}
		}


		public int Minutes
		{
			get
			{
				return Convert.ToInt32(TotalMinutes) % 60;
			}
		}

		public int Hours
		{
			get
			{
				return Convert.ToInt32(TotalHours) % 24;
			}
		}

		public int Days
		{
			get
			{
				return Convert.ToInt32(TotalDays);
			}
		}

		public static implicit operator TimeSpan(__TimeSpan e)
		{
			return (TimeSpan)(object)e;
		}


		// 2015!
		public static TimeSpan operator +(__TimeSpan x, __TimeSpan y)
		{

			return TimeSpan.FromMilliseconds(
				x.TotalMilliseconds + y.TotalMilliseconds
			);
		}
	}
}
