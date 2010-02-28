using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using java.applet;
using PromotionWebApplication1.Library;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Drawing;

namespace PromotionWebApplication1
{


	public sealed class UltraApplication
	{
		public UltraApplication(IHTMLElement e)
		{
			var DefaultTitle = "jsc solutions";


			Native.Document.title = DefaultTitle;

			StringActionAction GetTitleFromServer = new UltraWebService().GetTitleFromServer;



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

				var cc = new HTML.Pages.FromAssets.About();
				var MyPagesCurrent = default(IHTMLElement);


				//cc.About.onclick +=
				//    ee =>
				//    {
				//        ee.PreventDefault();


				//        var about = new Design.About();



				//        MyPagesCurrent.FadeOutCollapse(
				//            1, 100,
				//            delegate
				//            {
				//                MyPagesCurrent = about.Container;

				//                about.Container.AttachTo(MyPagesInternal);
				//                about.Container.FadeIn(1, 100, null);
				//            }
				//        );

		
				//    };


				//cc.Licensing.onclick +=
				//    ee =>
				//    {
				//        ee.PreventDefault();


				//        var licensing = new Design.Licensing();


				//        MyPagesCurrent.FadeOutCollapse(
				//            1, 100,
				//            delegate
				//            {

				//                MyPagesCurrent = licensing.Container;

				//                licensing.Container.AttachTo(MyPagesInternal);
				//                licensing.Container.FadeIn(1, 100, null);
				//            }
				//        );

				//    };

				cc.Container.AttachToDocument();
				cc.Container.FadeIn(500, 1000, null);


			}
			#endregion

			#region logo
			{
				if (Native.Document.location.hash == "#/audio")
				{
					new HTML.Pages.FromAssets.Audio().Container.AttachToDocument();
				}
				else
				{
					new PromotionWebApplication1.HTML.Audio.FromAssets.Track1 { controls = true }.AttachToDocument();
					new PromotionWebApplication1.HTML.Audio.FromWeb.Track1 { controls = true, autobuffer = true }.AttachToDocument();

					var cc = new HTML.Pages.FromAssets.Controls.Named.CenteredLogo_Kamma();

					cc.Container.AttachToDocument();

					// see: http://en.wikipedia.org/wiki/Perl_control_structures
					// "Unless" == "if not"  ;)

					IsMicrosoftInternetExplorer.YetIfNotThen(cc.TheLogoImage.BeginPulseAnimation).ButIfSoThen(cc.TheLogoImage.HideNowButShowAtDelay);
				}
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
