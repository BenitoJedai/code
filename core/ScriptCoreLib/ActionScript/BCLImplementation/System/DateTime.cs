using System;
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
		internal Date InternalDate;

        public static __DateTime Now
        {
            get
            {
				return new __DateTime { InternalDate = new Date() };
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

		public int Year
		{
			get
			{
				return Convert.ToInt32(this.InternalDate.getFullYear());
			}
		}
    }
}
