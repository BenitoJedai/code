using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Diagnostics
{
	[Script(Implements = typeof(global::System.Diagnostics.Stopwatch))]
	internal class __Stopwatch
	{
		public bool IsRunning
		{
			get;
			set;
		}

		DateTime InternalStart = DateTime.Now;
		DateTime InternalStop = DateTime.Now;

		public void Start()
		{
			IsRunning = true;
			InternalStart = DateTime.Now;
		}

		public void Stop()
		{
			IsRunning = false;
			InternalStop = DateTime.Now;
		}

		public TimeSpan Elapsed
		{
			get
			{
				return InternalStop - InternalStart;
			}
		}

		public long ElapsedMilliseconds
		{
			get
			{
				return Convert.ToInt64(Elapsed.TotalMilliseconds);
			}
		}

		public override string ToString()
		{
			return this.Elapsed.ToString();
		}
	}
}
