using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Threading
{
	// http://referencesource.microsoft.com/#WindowsBase/src/Base/System/Windows/Threading/DispatcherTimer.cs
	// https://github.com/Reactive-Extensions/IL2JS/blob/master/System.Windows/System/Windows/Threading/DispatcherTimer.cs

	[Script(Implements = typeof(global::System.Windows.Threading.DispatcherTimer))]
	internal class __DispatcherTimer
	{
		// what about background threads?

		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150318
		// public TResult Invoke<TResult>(Func<TResult> callback);

		bool InternalIsEnabled = false;
		public bool IsEnabled
		{
			get { return InternalIsEnabled; }
			set
			{
				if (value == InternalIsEnabled)
					return;

				if (value)
				{
					Start();
				}
				else
				{
					Stop();
				}
			}
		}

		TimeSpan InternalInterval = new TimeSpan();

		public TimeSpan Interval
		{
			get { return InternalInterval; }
			set
			{
				InternalInterval = value;

				if (IsEnabled)
				{
					Stop();
					Start();
				}
			}
		}

		public event EventHandler Tick;

		public int InternalTimer;

		public void Start()
		{
			if (IsEnabled)
				return;

			InternalTimer = Native.window.setInterval(
				delegate
				{
					if (Tick != null)
						Tick(this, new EventArgs());
				}
				, Convert.ToInt32(Interval.TotalMilliseconds)
			);

			InternalIsEnabled = true;
		}

		public void Stop()
		{
			if (!IsEnabled)
				return;

			Native.window.clearInterval(InternalTimer);

			InternalIsEnabled = false;

		}

	}
}
