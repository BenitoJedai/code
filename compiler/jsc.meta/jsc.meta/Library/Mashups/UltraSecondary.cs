using System;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace jsc.meta.Library.Mashups
{
	class UltraSecondary
	{
		// todo: generate /sitemap now for the bots!

		// loaded in primary and used secondary
		interface IPage1
		{
			string Text { get; set; }
		}

		// loaded for /#/Page1
		class Page1 : IPage1
		{
			// todo:
			// secondary applications could be
			// windows forms or wpf application windows
			// and we could load them up via iframe?
			// or within our main page.
			// we should use vista like glass borders
			// if we load it up in an iframe
			// how do we communicate?

			// primary application will not implement this type
			// we can only reference via interface from the primary application

			public string Text { get; set; }

			public Page1(UltraSecondary c)
			{

			}

			#region if we want to create sub pages from our code we need to define these methods

			public static void Create(UltraSecondary c, Action<IPage1> yield)
			{
				// load code and yield
				var p = new Page1(c);

				if (yield != null)
					yield(p);
			}


			#endregion

		}

		// loaded for /#/Page2
		class Page2 : IPage1
		{
			// primary application will not implement this type
			// we can only reference via interface from the primary application

			public string Text { get; set; }

			public Page2(UltraSecondary c)
			{

			}

			#region if we want to create sub pages from our code we need to define these methods

			public static void Create(UltraSecondary c, Action<IPage1> yield)
			{
				// load code and yield
				var p = new Page1(c);

				if (yield != null)
					yield(p);
			}


			#endregion

		}

		public UltraSecondary(IHTMLElement e)
		{
			// default stuff

			if (Native.Document.location.hash == "#/Page2")
			{
				// runtime redirect
				Page1.Create(this,
					p =>
					{
						// we need to use async because the load may take its time.

						p.Text = "hello";
					}
				);
			}
			else if (Native.Document.location.hash == "")
			{
				// yay, primary application
			}

			#region generated code:

			if (Native.Document.location.hash == "#/Page1")
			{
				Page1.Create(this, null);
			}

			if (Native.Document.location.hash == "#/Page2")
			{
				Page1.Create(this, null);
			}


			#endregion
		}
	}

}
