using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;
using System;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.DateTime))]
	internal class __DateTime
	{
		// int64 is really not supported, shall use bignumber instead?
		internal long InternalTicks;

		public long Ticks
		{
			get
			{
				return InternalTicks;
			}
		}

		public static DateTime Now
		{
			get
			{
				var v = new __DateTime
				{
					InternalTicks = (long)Math.Floor(
						ticks_1970_1_1 + Native.API.microtime(true) * 100 * TicksPerMillisecond
					)
				};


				return (DateTime)(object)v;
			}
		}

		internal const long ticks_1970_1_1 = 621355968000000000;
		internal const long TicksPerMillisecond = 0x10000;

		public static __TimeSpan operator -(__DateTime d1, __DateTime d2)
		{
			var t = d1.Ticks - d2.Ticks;

			return new __TimeSpan { Ticks = t };
		}



		public int Minute
		{
			get
			{
				return (int)((this.InternalTicks / 0x23c34600L) % 60L);
			}
		}
 

 


		public int Second
		{
			get
			{
				return (int)((this.InternalTicks / 0x989680L) % 60L);
			}
		}

		public int Hour
		{
			get
			{
				return (int)((this.InternalTicks / 0x861c46800L) % 0x18L);
			}
		}
 

 

 

	}
}
