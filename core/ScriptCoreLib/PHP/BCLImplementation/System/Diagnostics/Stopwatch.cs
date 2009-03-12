using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Diagnostics
{
	[Script(Implements = typeof(global::System.Diagnostics.Stopwatch))]
	internal class __Stopwatch
	{
		DateTime InternalStart;
		DateTime InternalStop;

		public TimeSpan Elapsed
		{
			get
			{
				return InternalStop - InternalStart;
			}
		}

		public void Start()
		{
			this.InternalStart = DateTime.Now;
		}

		public void Stop()
		{
			this.InternalStop = DateTime.Now;
		}
	}
}
