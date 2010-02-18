using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using System.ComponentModel;
using PromotionWebApplication1.Library;
using ScriptCoreLib.JavaScript.Runtime;

namespace PromotionWebApplication1
{


	public sealed class UltraApplication
	{
		public UltraApplication(IHTMLElement e)
		{
			Native.Document.title = "jsc solutions";

			StringActionAction GetTitleFromServer = new UltraWebService().GetTitleFromServer;

			GetTitleFromServer(
				n => Native.Document.title = n
			);



			var c = new IHTMLDiv().AttachToDocument();

			c.style.position = IStyle.PositionEnum.absolute;
			c.style.left = "50%";
			c.style.top = "50%";

			var a = new IHTMLAnchor { href = "http://sketch.odopod.com/sketches/149253", target = "BackgroundFrame" }.AttachTo(c);

			//a.onclick +=
			//    ee =>
			//    {
			//        ee.PreventDefault();

			//        new IHTMLDiv { innerHTML = "<embed src='http://sketch.odopod.com/flash/OdoSketch.swf?sketchURL=/sketches/149253.xml&userURL=/users/28416&bgURL=/images/bigbg.jpg&mode=embed' AllowScriptAccess='always' bgcolor=#EDE7DB menu='false' quality='high' pluginspage='http://www.macromedia.com/go/getflashplayer' type='application/x-shockwave-flash' width='1000' height='400'></embed>" }
			//            .AttachToDocument();

			//        GetTitleFromServer(
			//            n => Native.Document.title = n
			//        );
			//    };


			var jsc = new IHTMLImage("assets/ScriptCoreLib/jsc.png").AttachTo(a);

			jsc.style.borderStyle = "none";
			jsc.style.marginLeft = "-48px";
			jsc.style.marginTop = "-48px";

			jsc.style.SetSize(96, 96);
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

			GoogleAnalytics();



		}

		private static void GoogleAnalytics()
		{
			var analytics = new IHTMLScript
			{
				type = "text/javascript",
				src = "http://www.google-analytics.com/ga.js"
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
	}

	public delegate void StringAction(string e);
	public delegate void StringActionAction(StringAction e);

	public sealed class UltraWebService
	{

		public void Hello(string data, StringAction result)
		{
			result(data + " hello");
			result(data + " world");
		}

		public void GetTitleFromServer(StringAction result)
		{
			var r = new Random();

			var Targets = new[]
			{
				"javascript",
				"java",
				"actionscript",
				"php"
			};

			result("jsc solutions - C# to " + Targets[r.Next(0, Targets.Length)]);

			// should we add timing information if we use Thread.Sleep to the results?

		}
	}
}
