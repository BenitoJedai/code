using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;
using System;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.DateTime))]
	internal class __DateTime
	{
		internal double InternalTotalSeconds;

		public double TotalSeconds
		{
			get
			{
				return InternalTotalSeconds;
			}
		}

		public int Second
		{
			get
			{
				return global::System.Convert.ToInt32(this.InternalTotalSeconds) % 60;
			}
		}

		public int Minute
		{
			get
			{
				return global::System.Convert.ToInt32(this.InternalTotalSeconds / 60) % 60;
			}
		}

		public int Hour
		{
			get
			{
				return global::System.Convert.ToInt32(this.InternalTotalSeconds / (60 * 60)) % 24;
			}
		}

		// int64 is really not supported, shall use bignumber instead?
		//internal long InternalTicks;

		//public long Ticks
		//{
		//    get
		//    {
		//        throw new 
		//        return InternalTicks;
		//    }
		//}

		public static DateTime Now
		{
			get
			{
				var v = new __DateTime
				{
					InternalTotalSeconds = Native.API.microtime(true)
					//InternalTicks = (long)Math.Floor(
					//    ticks_1970_1_1 + Native.API.microtime(true) * 100 * TicksPerMillisecond
					//)
				};


				return (DateTime)(object)v;
			}
		}

		//internal const long ticks_1970_1_1 = 621355968000000000;
		//internal const long TicksPerMillisecond = 0x10000;

		public static __TimeSpan operator -(__DateTime d1, __DateTime d2)
		{
			return new __TimeSpan { InternalTotalSeconds = d1.InternalTotalSeconds - d2.InternalTotalSeconds };
		}



		public string ToLongTimeString()
		{
			return
				("" + Hour).PadLeft(2, '0') + ":" +
				("" + Minute).PadLeft(2, '0') + ":" +
				("" + Second).PadLeft(2, '0');
		}





	}
}
