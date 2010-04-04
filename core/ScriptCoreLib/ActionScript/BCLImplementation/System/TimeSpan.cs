using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
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


		public int Days
		{
			get
			{
				return Convert.ToInt32(
					TotalMilliseconds / (1000 * 60 * 60 * 24)
				);
			}
		}

		public int Hours
		{
			get
			{
				return Convert.ToInt32(
					TotalMilliseconds / (1000 * 60 * 60)
				);
			}
		}

		public int Minutes
		{
			get
			{
				return Convert.ToInt32(
					TotalMilliseconds / (1000 * 60)
				);
			}
		}


		public int Seconds
		{
			get
			{
				return Convert.ToInt32(
					TotalMilliseconds / (1000)
				);
			}
		}
	}
}
