using System;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace jsc.meta.Library.Mashups
{
	class UltraDocument
	{
		class MyApplet : java.applet.Applet
		{
			public override void init()
			{
				// java applet
			}
		}

		class MyFlash : Sprite
		{
			public MyFlash()
			{
				// flash object 
			}
		}

		public UltraDocument(IHTMLElement e)
		{
			// javascript code

			var j = new MyApplet();
			Native.Document.body.appendChild((INode)(object)j);

			var f = new MyFlash();
			Native.Document.body.appendChild((INode)(object)j);

			new UltraWebService.PageX().Method1(
				"I want My xml document",
				xml =>
				{
					// got it
				}
			);
		}
	}

	partial class UltraWebService
	{
		// private methods shall use /xml?WebMethod=000000 path

		public string QueryParam1;

		public UltraWebService()
		{

		}

		public UltraWebService(Uri Host)
		{
			// should we support multiple hosts?
		}

		public void Method1(string PostParam1, Action<string> YieldReturn)
		{
			// keyword yield return is async, cannot block at js at the moment!

			YieldReturn("hi");
		}

		public void Method1(string PostParam1, Action<string, Action> YieldReturnAndContinue)
		{
			// keyword yield return is async, cannot block at js at the moment!

			YieldReturnAndContinue("hi",
				delegate
				{
					// continue once the multipart response part 1 has been sent.

					// comet? :)
				}
			);
		}

		public class PageX
		{
			public string QueryParam1;

			// a method in this class shall be accessible as
			// http://localhost/PageX/?QueryParam1=foo&PostParam1=bar

			// public methods WILL expose their addres "/PageX/Method1?PostParam1=foo
			public void Method1(string PostParam1, Action<XElement> YieldReturn)
			{
				YieldReturn(
					new XElement("Document1")
				);
			}

			// nonpublic methods WONT expose their addres "/PageX/Method2?PostParam1=foo
			internal void Method2(string PostParam1, Action<XElement> YieldReturn)
			{
				YieldReturn(
					new XElement("Document1")
				);
			}

			// http://localhost/PageX/Method22 redirects to http://example.com
			// http://localhost/pagex/method22 redirects to http://example.com
			public void Method22(string PostParam1, Action<Uri> YieldReturn)
			{
				YieldReturn(
					new Uri("http://example.com")
				);
			}
		}


		// public methods WILL expose their addres "/Method7?PostParam1=foo
		public void Method7(string PostParam1, Action<XElement> YieldReturn)
		{
			YieldReturn(
				new XElement("Document1")
			);
		}


		// nonpublic methods WONT expose their addres "/Method8?PostParam1=foo
		internal void Method8(string PostParam1, Action<XElement> YieldReturn)
		{
			YieldReturn(
				new XElement("Document1")
			);
		}
	}
}
