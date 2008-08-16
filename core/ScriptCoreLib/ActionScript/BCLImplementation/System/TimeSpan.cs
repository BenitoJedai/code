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

		public double TotalMilliseconds { get; set;  }

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
    }
}
