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
		//public double TotalMilliseconds { get; set; }

		public static TimeSpan Parse(string e)
		{
			return default(TimeSpan);
		}

		//public static TimeSpan FromMilliseconds(double value)
		//{
		//    return new __TimeSpan { TotalMilliseconds = value };
		//}

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
	}
}
