using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Threading
{
	[Script(Implements = typeof(global::System.Windows.Threading.DispatcherTimer))]
	internal class __DispatcherTimer
	{
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

			InternalTimer = Native.Window.setInterval(
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

			Native.Window.clearInterval(InternalTimer);

			InternalIsEnabled = false;

		}

	}
}
