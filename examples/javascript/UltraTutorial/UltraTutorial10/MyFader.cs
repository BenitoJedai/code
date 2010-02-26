using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;

namespace UltraTutorial10
{
	public static class MyFader
	{
		static public void AtDelay(this int ms, Action e)
		{
			new Timer(
				delegate
				{
					e();
				}
			).StartTimeout(ms);
		}

		static public void FadeIn(this IHTMLElement target, int waittime, int fadetime, Action done)
		{
			// if IE
			target.style.height = target.clientHeight + "px";

			target.style.Opacity = 0;

			waittime.AtDelay(
				delegate
				{
					Timer a = null;

					a = new Timer(
						delegate
						{

							target.style.Opacity = (a.Counter / a.TimeToLive);

							if (a.Counter == a.TimeToLive)
							{
								target.style.Opacity = 1;

								if (done != null)
									done();

							}

						}
						);


					a.StartInterval(fadetime / 25, 25);
				}
			);
		}
	}
}
