using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;

namespace PromotionWebApplication1.Library
{
	public delegate void TimerAction(Timer t);

	public static class MyExtensions
	{

		static public void AtInterval(this int ms, TimerAction e)
		{
			new Timer(
				t =>
				{
					e(t);
				}
			).StartInterval(ms);
		}

		static public void AtInterval(this int ms, Action e)
		{
			new Timer(
				delegate
				{
					e();
				}
			).StartInterval(ms);
		}

		static public void AtDelay(this int ms, Action e)
		{
			new Timer(
				delegate
				{
					e();
				}
			).StartTimeout(ms);
		}


		public static bool ButIfSoThen(this bool e, Action h)
		{
			if (e)
				h();

			return e;
		}

		public static bool YetIfNotThen(this bool e, Action h)
		{
			if (!e)
				h();

			return e;
		}


		public static void Invoke(this string src, string code)
		{
			var analytics = new IHTMLScript
			{
				type = "text/javascript",
				src = src
			};

			analytics.onload +=
				delegate
				{
					1.AtDelay(
						delegate
						{
							new IFunction(@"
try {
var pageTracker = _gat._getTracker('UA-13087448-1');
pageTracker._setDomainName('.jsc-solutions.net');
pageTracker._trackPageview();
} catch(err) { }
").apply(Native.Window);
						}
					);
				};


			analytics.AttachToDocument();
		}

		public static void HideNowButShowAtDelay(this IHTMLImage jsc)
		{
			jsc.style.visibility = IStyle.VisibilityEnum.hidden;

			jsc.InvokeOnComplete(
				delegate
				{
					500.AtDelay(
						delegate
						{
							jsc.style.visibility = IStyle.VisibilityEnum.visible;
						}
					);
				}
			);
		}

		public static void BeginPulseAnimation(this IHTMLImage jsc)
		{
			jsc.style.Opacity = 0;

			jsc.InvokeOnComplete(
				delegate
				{
					jsc.FadeIn(500, 1000,
						delegate
						{
							new Timer(
								t =>
								{
									jsc.style.Opacity = (Math.Cos(t.Counter * 0.1) + 1.0) * 0.5;
								}
							, 1, 1000 / 15);
						}
					);
				}
			);
		}
	}
}
