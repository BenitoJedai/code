﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
	[Script(
		Implements = typeof(global::System.DateTime)
		)
	]
	internal class __DateTime
	{
		internal Date InternalValue;


		public __DateTime()
			: this(-1, -1, -1, -1, -1, -1)
		{

		}

		public __DateTime(int year, int month, int day, int hour, int minute, int second)
		{
			this.InternalValue = new Date();

			if (year == -1)
			{

			}
			else
			{
				this.InternalValue.setFullYear(year, month - 1, day);
				this.InternalValue.setHours(hour, minute, second, 0);
			}
		}

		public static __DateTime Now
		{
			get
			{
				return new __DateTime();
			}
		}

		public override string ToString()
		{
			return "[DateTime]";
		}

		public long Ticks
		{
			get
			{
				return 0;
			}
		}



		public int Second { get { return Convert.ToInt32(this.InternalValue.getSeconds()); } }
		public int Minute { get { return Convert.ToInt32(this.InternalValue.getMinutes()); } }
		public int Hour { get { return  Convert.ToInt32(this.InternalValue.getHours()); } }
		public int Day { get { return Convert.ToInt32(this.InternalValue.getDate()); } }
		public int Month { get { return Convert.ToInt32(this.InternalValue.getMonth()) + 1; } }
		public int Year { get { return Convert.ToInt32(this.InternalValue.getFullYear()); } }

	}
}
