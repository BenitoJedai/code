using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.TimeSpan))]
	internal class __TimeSpan
	{
		public long InternalTicks;

		public long Ticks { get { return InternalTicks; } }

		public double TotalMilliseconds
		{
			get
			{
				return (double)Ticks / (double)TimeSpan.TicksPerMillisecond;
			}
		}

		public override string ToString()
		{
			return TotalMilliseconds + "ms";
		}
	}
}
