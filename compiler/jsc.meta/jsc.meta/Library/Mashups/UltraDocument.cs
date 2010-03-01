using System;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using System.Xml.Linq;

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

		public UltraDocument()
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

	class UltraWebService
	{
		public string QueryParam1;

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

			public void Method1(string PostParam1, Action<XElement> YieldReturn)
			{
				YieldReturn(
					new XElement("Document1")
				);
			}

		}

	}
}
