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
using ScriptCoreLib.Shared.Drawing;
using java.applet;

namespace PromotionWebApplication1
{
#if UsingJava
	public sealed class AboutApplet : Applet
	{
		public void Method1(string p)
		{

		}
	}
#endif

	public sealed class UltraApplication
	{
		public UltraApplication(IHTMLElement e)
		{
			var DefaultTitle = "jsc solutions";


			Native.Document.title = DefaultTitle;

			StringActionAction GetTitleFromServer = new UltraWebService().GetTitleFromServer;

#if UsingJava
			var Applet1 = new AboutApplet();

			Applet1.Method1("hello world");

			var Applet1Element = Applet1.AttachAppletToDocument();

			Applet1Element.style.border = "1px solid red";
#endif

			GetTitleFromServer(
				n => Native.Document.title = n
			);

			#region Contact Us
			{
				// using wordpress as CMS are we?
				// http://en.support.wordpress.com/pages/hide-pages/
				// http://en.support.wordpress.com/pages/page-attributes/
				// we will build a snapshot of the site.
				// hidden pages need to be subpages :)

				var MyPages = new IHTMLDiv
				{

				};

				MyPages.style.overflow = IStyle.OverflowEnum.auto;
				MyPages.style.position = IStyle.PositionEnum.absolute;
				MyPages.style.width = "100%";
				MyPages.style.height = "100%";
				MyPages.AttachToDocument();

				var MyPagesInternal = new IHTMLDiv();

				MyPagesInternal.style.margin = "4em";
				MyPagesInternal.AttachTo(MyPages);

				var cc = new Pages.About();
				var MyPagesCurrent = default(IHTMLElement);


				cc.About.onclick +=
					ee =>
					{
						ee.PreventDefault();


						var about = new Design.About();



						MyPagesCurrent.FadeOutCollapse(
							1, 100,
							delegate
							{
								MyPagesCurrent = about.Container;

								about.Container.AttachTo(MyPagesInternal);
								about.Container.FadeIn(1, 100, null);
							}
						);

		
					};


				cc.Licensing.onclick +=
					ee =>
					{
						ee.PreventDefault();


						var licensing = new Design.Licensing();


						MyPagesCurrent.FadeOutCollapse(
							1, 100,
							delegate
							{

								MyPagesCurrent = licensing.Container;

								licensing.Container.AttachTo(MyPagesInternal);
								licensing.Container.FadeIn(1, 100, null);
							}
						);

					};

				cc.Container.AttachToDocument();
				cc.Container.FadeIn(500, 1000, null);


			}
			#endregion

			#region logo
			{
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


				var logo = new IHTMLImage("assets/ScriptCoreLib/jsc.png").AttachTo(a);

				logo.style.borderStyle = "none";
				logo.style.marginLeft = "-48px";
				logo.style.marginTop = "-48px";

				logo.style.SetSize(96, 96);

				// see: http://en.wikipedia.org/wiki/Perl_control_structures
				// "Unless" == "if not"  ;)

				IsMicrosoftInternetExplorer.YetIfNotThen(logo.BeginPulseAnimation).ButIfSoThen(logo.HideNowButShowAtDelay);
			}
			#endregion

			"UA-13087448-1".ToGoogleAnalyticsTracker(
				pageTracker =>
				{
					pageTracker._setDomainName(".jsc-solutions.net");
					pageTracker._trackPageview();
				}
			);


		}



		/// <summary>
		/// Microsoft Internet Explorer does not support using opacity on an image with an alpha layer.
		/// </summary>
		public static bool IsMicrosoftInternetExplorer
		{
			get
			{
				return (bool)new IFunction("/*@cc_on return true; @*/ return false;").apply(null);
			}
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
