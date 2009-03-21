using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;
using System;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.TimeSpan))]
	internal class __TimeSpan
	{
		public __TimeSpan()
		{

		}

		public long Ticks { get; set; }

		public static TimeSpan Parse(string e)
		{
			return default(TimeSpan);
		}

		public static __TimeSpan FromMilliseconds(double value)
		{
			return new __TimeSpan { Ticks = global::System.Convert.ToInt64(value * TimeSpan.TicksPerMillisecond) };
		}

		public static implicit operator TimeSpan(__TimeSpan e)
		{
			return (TimeSpan)(object)e;
		}

		public double TotalSeconds
		{
			get
			{
				return this.Ticks / TimeSpan.TicksPerSecond;
			}
		}

		public double TotalMilliseconds
		{
			get
			{
				return this.Ticks / TimeSpan.TicksPerMillisecond;
			}
		}


		public int Hours
		{
			get
			{
				var v = global::System.Convert.ToInt32((double)this.Ticks / TimeSpan.TicksPerHour);

				return v % 24;
			}
		}

		public int Minutes
		{
			get
			{
				var v = global::System.Convert.ToInt32((double)this.Ticks / TimeSpan.TicksPerMinute);

				return v % 60;
			}
		}

		public int Seconds
		{
			get
			{
				var v = global::System.Convert.ToInt32((double)this.Ticks / TimeSpan.TicksPerSecond);

				return v % 60;
			}
		}

		public override string ToString()
		{
			return
				("" + Hours).PadLeft(2, '0') + ":" +
				("" + Minutes).PadLeft(2, '0') + ":" + 
				("" + Seconds).PadLeft(2, '0');
		}
	}
}
