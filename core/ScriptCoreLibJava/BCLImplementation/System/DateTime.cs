using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.Runtime.InteropServices;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.DateTime))]
	internal class __DateTime
	{
		internal java.util.Calendar InternalValue;

		public __DateTime()
			: this(-1, -1, -1, -1, -1, -1)
		{

		}

		public __DateTime(int year, int month, int day, int hour, int minute, int second)
		{
			this.InternalValue = java.util.Calendar.getInstance();

			if (year == -1)
			{

			}
			else
			{
				this.InternalValue.set(year, month - 1, day, hour, minute, second);
			}
		}

		public static DateTime Now
		{
			get
			{

				return (DateTime)(object)new __DateTime();
			}
		}

		public int Second { get { return this.InternalValue.get(java.util.Calendar.SECOND); } }
		public int Minute { get { return this.InternalValue.get(java.util.Calendar.MINUTE); } }
		public int Hour { get { return this.InternalValue.get(java.util.Calendar.HOUR_OF_DAY); } }
		public int Day { get { return this.InternalValue.get(java.util.Calendar.DAY_OF_MONTH); } }
		public int Month { get { return this.InternalValue.get(java.util.Calendar.MONTH) + 1; } }
		public int Year { get { return this.InternalValue.get(java.util.Calendar.YEAR); } }

		public const long ticks_1970_1_1 = 621355968000000000;


		public long Ticks
		{
			get
			{
				// conversion needed

				var ms = this.InternalValue.getTimeInMillis();

				return ms * TimeSpan.TicksPerMillisecond + ticks_1970_1_1;
			}
		}



		public static __TimeSpan operator -(__DateTime d1, __DateTime d2)
		{
			return new __TimeSpan { InternalTicks = d1.Ticks - d2.Ticks };
		}
	}
}
