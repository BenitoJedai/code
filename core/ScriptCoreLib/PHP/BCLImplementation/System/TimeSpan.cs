using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;
using System;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.TimeSpan))]
	internal class __TimeSpan
	{
		internal double InternalTotalSeconds;

		public __TimeSpan()
		{

		}

		//public long Ticks { get; set; }

		public static TimeSpan Parse(string e)
		{
			return default(TimeSpan);
		}

		public static __TimeSpan FromMilliseconds(double value)
		{
			return new __TimeSpan { InternalTotalSeconds = value * 0.001 };
		}

		public static implicit operator TimeSpan(__TimeSpan e)
		{
			return (TimeSpan)(object)e;
		}

		public double TotalSeconds
		{
			get
			{
				return InternalTotalSeconds;
			}
		}

		public double TotalMilliseconds
		{
			get
			{
				return global::System.Convert.ToInt32(this.InternalTotalSeconds * 1000) % 1000;
			}
		}

		public int Seconds
		{
			get
			{
				return global::System.Convert.ToInt32(this.InternalTotalSeconds) % 60;
			}
		}

		public int Minutes
		{
			get
			{
				return global::System.Convert.ToInt32(this.InternalTotalSeconds / 60) % 60;
			}
		}

		public int Hours
		{
			get
			{
				return global::System.Convert.ToInt32(this.InternalTotalSeconds / (60 * 60)) % 24;
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
