using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.utils;

namespace FlashTreasureHunt.ActionScript
{
	[Script]
	public class TimeoutAction
	{
		// 5 minutes
		public const int LongOperation = 1000 * 60 * 5; 

		// initially disabled
		// active for only a small amount of time

		Action ContinueWhenDone_Before;
		Action ContinueWhenDone_e;

		public void ContinueWhenDone(Action e)
		{
			if (this.ContinueWhenDone_e != null)
				throw new Exception("Previous TimeoutAction has not yet been executed");

			this.ContinueWhenDone_e = e;

			// if Wait and Signal have already been called
			// and timeout is still active

			if (Wait_ms != null)
				if (!_SignalMissed)
				{
					Wait_ms.stop();
					Wait_ms = null;

					// note the overload - we just were save from the SignalWaisted event
					if (SignalWasExpected != null)
						SignalWasExpected();

					Continue();
				}
		}

		private void Continue()
		{
			if (ContinueWhenDone_Before != null)
			{
				ContinueWhenDone_Before();
				ContinueWhenDone_Before = null;
			}

			ContinueWhenDone_e();
			ContinueWhenDone_e = null;
		}

		Timer Wait_ms;
		bool _SignalMissed;

		public void Wait(int ms)
		{
			_SignalMissed = true;

			if (Wait_ms != null)
			{
				Wait_ms.stop();
				Wait_ms = null;
			}

			Wait_ms = ms.AtDelayDo(
				delegate
				{
					Wait_ms = null;

					if (_SignalMissed)
						if (SignalMissed != null)
							SignalMissed();

					if (ContinueWhenDone_e != null)
					{
						Continue();
					}
					else
					{
						if (SignalWaisted != null)
							SignalWaisted();
					}

				}
			);
		}

		public void Signal(Action BeforeContinue)
		{
			if (!_SignalMissed)
			{
				if (SignalNotExpected != null)
					SignalNotExpected();

				return;
			}

			_SignalMissed = false;

			if (Wait_ms != null)
			{
				this.ContinueWhenDone_Before = BeforeContinue;
	
				if (ContinueWhenDone_e != null)
				{
					Wait_ms.stop();
					Wait_ms = null;

					if (SignalWasExpected != null)
						SignalWasExpected();

					Continue();
				}
				else
				{
					// SignalWaisted will occur soon
				}
			}
			else
			{
				if (SignalNotExpected != null)
					SignalNotExpected();
			}
		}

		public event Action SignalMissed;
		public event Action SignalNotExpected;
		public event Action SignalWaisted;
		public event Action SignalWasExpected;
	}

}
