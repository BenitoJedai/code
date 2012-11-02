using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.TimeSpan))]
	internal class __TimeSpan
	{
		public __TimeSpan()
		{

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

		public static implicit operator TimeSpan(__TimeSpan e)
		{
			return (TimeSpan)(object)e;
		}


		public override string ToString()
		{
			return Days + "."
				+ ("" + Hours).PadLeft(2, '0') + ":"
				+ ("" + Minutes).PadLeft(2, '0') + ":"
				+ ("" + Seconds).PadLeft(2, '0');
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

		public int TotalHours
		{
			get
			{
				return Convert.ToInt32(
					TotalMilliseconds / (1000 * 60 * 60)
				);
			}
		}

		public int TotalMinutes
		{
			get
			{
				return Convert.ToInt32(
					TotalMilliseconds / (1000 * 60)
				);
			}
		}


		public int TotalSeconds
		{
			get
			{
				return Convert.ToInt32(
					TotalMilliseconds / (1000)
				);
			}
		}

		public int Seconds
		{
			get
			{
				return TotalSeconds % 60;
			}
		}


		public int Minutes
		{
			get
			{
				return TotalMinutes % 60;
			}
		}

		public int Hours
		{
			get
			{
				return TotalHours % 24;
			}
		}

		public int Days
		{
			get
			{
				return Convert.ToInt32(TotalDays);
			}
		}
	}
}
