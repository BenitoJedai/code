using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;


namespace PromotionWebApplication1.Library
{
	public static class MyFader
	{
		static public void FadeOutCollapse(this IHTMLElement target, int waittime, int fadetime, Action done)
		{
			target.FadeOut(waittime, fadetime,
				delegate
				{
					if (done != null)
						done();

					if (target != null)
						(fadetime / 25).AtInterval(
							t =>
							{
								var h = target.clientHeight;

								if (h > 6)
								{
									target.style.height = (h - 6) + "px";
								}
								else
								{
									target.Orphanize();

									t.Stop();
								}
							}
						);

				}
			);
		}

		static public void FadeOut(this IHTMLElement target, int waittime, int fadetime, Action done)
		{
			waittime.AtDelay(
				delegate
				{
					if (null == target)
					{
						done();
						return;
					}

					Timer a = null;

					a = new Timer(
						delegate
						{

							var Opacity = 1.0 - (a.Counter / a.TimeToLive);

							//Native.Document.title = "" + Opacity;

							target.style.Opacity = Opacity;

							if (a.Counter == a.TimeToLive)
							{
								target.style.Opacity = 0;

								if (done != null)
									done();

							}

						}
						);


					a.StartInterval(fadetime / 25, 25);
				}
			);
		}

		static public void FadeIn(this IHTMLElement target, int waittime, int fadetime, Action done)
		{
			// if IE
			var c = target.clientHeight;

			if (c < 20)
				c = 20;

			target.style.height = c + "px";

			target.style.Opacity = 0;
			target.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.empty;

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
