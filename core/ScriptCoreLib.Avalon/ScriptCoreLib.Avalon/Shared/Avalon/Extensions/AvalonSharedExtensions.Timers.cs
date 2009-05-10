using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Avalon.Tween;
using System.IO;

namespace ScriptCoreLib.Shared.Avalon.Extensions
{
	partial class AvalonSharedExtensions
	{
		public static event Action<Delegate, long> TimerEvent;


		public static DispatcherTimer AtDelay(this int Milliseconds, Action Handler)
		{
			if (Handler == null)
				return null;

			var t = new DispatcherTimer
			{
				Interval = TimeSpan.FromMilliseconds(Milliseconds)
			};

			t.Tick +=
				delegate
				{
					var mark = DateTime.Now.Ticks;

					Handler();

					if (TimerEvent != null)
						TimerEvent(Handler, DateTime.Now.Ticks - mark);

					t.Stop();
				};

			t.Start();

			return t;
		}

		public static DispatcherTimer AtIntervalWithTimer(this int Milliseconds, Action<DispatcherTimer> Handler)
		{
			var t = new DispatcherTimer
			{
				Interval = TimeSpan.FromMilliseconds(Milliseconds)
			};

			t.Tick +=
				delegate
				{
					var mark = DateTime.Now.Ticks;

					Handler(t);

					if (TimerEvent != null)
						TimerEvent(Handler, DateTime.Now.Ticks - mark);
				};

			t.Start();

			return t;
		}

		public static DispatcherTimer AtIntervalWithCounter(this int Milliseconds, Action<int> Handler)
		{
			var t = new DispatcherTimer
			{
				Interval = TimeSpan.FromMilliseconds(Milliseconds)
			};

			int Counter = 0;

			t.Tick +=
				delegate
				{
					var mark = DateTime.Now.Ticks;



					Handler(Counter);

					if (TimerEvent != null)
						TimerEvent(Handler, DateTime.Now.Ticks - mark);

					Counter++;
				};

			t.Start();

			return t;
		}

		public static DispatcherTimer AtInterval(this int Milliseconds, IEnumerable<Action> Handler)
		{
			var e = Handler.GetEnumerator();
			return Milliseconds.AtIntervalWithTimer(
				t =>
				{
					if (e.MoveNext())
					{
						e.Current();
						return;
					}
					e.Dispose();
					e = null;
					t.Stop();
				}
			);
		}

		public static DispatcherTimer AtInterval(this int Milliseconds, Action Handler)
		{
			var t = new DispatcherTimer
			{
				Interval = TimeSpan.FromMilliseconds(Milliseconds)
			};

			t.Tick +=
				delegate
				{
					var mark = DateTime.Now.Ticks;

					Handler();

					if (TimerEvent != null)
						TimerEvent(Handler, DateTime.Now.Ticks - mark);
				};

			t.Start();

			return t;
		}
	}
}
