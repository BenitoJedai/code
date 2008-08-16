using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Threading
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

		public Timer InternalTimer;

		public void Start()
		{
			if (IsEnabled)
				return;

			InternalTimer = new Timer(Interval.TotalMilliseconds);
			InternalTimer.timer +=
				delegate
				{
					if (Tick != null)
						Tick(this, new EventArgs());
				};
			InternalTimer.start();

			InternalIsEnabled = true;
		}

		public void Stop()
		{
			if (!IsEnabled)
				return;

			InternalTimer.stop();

			InternalIsEnabled = false;

		}

	}
}
