using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace MatrixTransformBExample.js
{
	[Script]
	static class Extensions
	{
		public static void BlinkAt(this IHTMLElement e, int interval)
		{
			interval.AtIntervalWithCounter(
				counter =>
				{
					if (counter % 2 == 0)
					{
						e.style.Opacity = 0.3;
						return;
					}

					e.style.Opacity = 1;
				}
			);

		}

		public static void AtIntervalWithCounter(this int interval, Action<int> h)
		{
			var c = 0;
			new Timer(
				delegate
				{
					h(c);
					c++;
				}
			, 0, interval);
		}

		public static int Random(this int i)
		{
			return new Random().Next(i);
		}
	}
}
