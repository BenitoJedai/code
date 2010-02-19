using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace PromotionWebApplication1.Library
{
	public static class MyFader
	{
		

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
