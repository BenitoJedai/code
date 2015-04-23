using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestShadowContentSelectByAttribute;
using TestShadowContentSelectByAttribute.Design;
using TestShadowContentSelectByAttribute.HTML.Pages;

namespace TestShadowContentSelectByAttribute
{
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{
			var x = new IHTMLButton { "x" }.AttachToDocument();

			var y = new IHTMLButton { "y" }.AttachToDocument();

			x.setAttribute("bar", "foo");
			y.setAttribute("foo", "bar");


			new IHTMLContent { }.AttachTo(Native.body.shadow).With(
				async content =>
				{
					// http://www.w3.org/TR/shadow-dom/#dfn-matching-criteria
					content.select = "[foo=bar]";

					await y.async.onclick;

					content.select = "[bar=foo]";
				}
			);
        }

	}
}
