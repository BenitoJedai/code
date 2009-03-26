using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;
using System;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.DateTime))]
	internal class __DateTime
	{
		// http://aidanlister.com/repos/v/Duration.php

		internal double InternalTotalSeconds;

		public static __DateTime FromTotalSeconds(double e)
		{
			return new __DateTime { InternalTotalSeconds = e };
		}

		public int TotalSecondsInt32
		{
			get
			{
				return global::System.Convert.ToInt32(InternalTotalSeconds);
			}
		}

		public int Second
		{
			get
			{
				return TotalSecondsInt32 % 60;
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

		internal const long ticks_1970_1_1 = 621355968000000000;
		internal const long TicksPerMillisecond = 0x10000;

		public static __TimeSpan operator -(__DateTime d1, __DateTime d2)
		{
			return new __TimeSpan { InternalTotalSeconds = d1.InternalTotalSeconds - d2.InternalTotalSeconds };
		}





		public static bool IsLeapYear(int year)
		{
			if (year < 1)
				throw new Exception("ArgumentOutOfRange_Year");
			if (year > 0x270f)
				throw new Exception("ArgumentOutOfRange_Year");

			if ((year % 4) != 0)
			{
				return false;
			}
			if ((year % 100) == 0)
			{
				return ((year % 400) == 0);
			}
			return true;
		}


		public static int DaysInMonth(int year, int month)
		{
			if (month < 1)
				throw new Exception("ArgumentOutOfRange_Month");
			if (month > 12)
				throw new Exception("ArgumentOutOfRange_Month");


			int[] numArray = IsLeapYear(year) ? DaysToMonth366 : DaysToMonth365;

			return (numArray[month] - numArray[month - 1]);
		}

		static __DateTime()
		{
			DaysToMonth365 = new[] { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365 };
			DaysToMonth366 = new[] { 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335, 366 };

		}

		private static readonly int[] DaysToMonth366;
		private static readonly int[] DaysToMonth365;



		public int Month
		{
			get
			{
				var x = Native.API.getdate(this.TotalSecondsInt32);

				return (int)x["mon"];
			}
		}

		public int Day
		{
			get
			{
				var x = Native.API.getdate(this.TotalSecondsInt32);

				return (int)x["mday"];
			}
		}

		public int Year
		{
			get
			{
				var x = Native.API.getdate(this.TotalSecondsInt32);

				return (int)x["year"];
			}
		}




		public string ToLongTimeString()
		{
			return
				("" + Hour).PadLeft(2, '0') + ":" +
				("" + Minute).PadLeft(2, '0') + ":" +
				("" + Second).PadLeft(2, '0');
		}

		public string ToLongDateString()
		{
			return
				("" + Year).PadLeft(4, '0') + "." +
				("" + Month).PadLeft(2, '0') + "." +
				("" + Day).PadLeft(2, '0');
		}

		public override string ToString()
		{
			return ToLongDateString() + " " + ToLongTimeString();
		}

	}
}
