using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM.HTML;

using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Ultra.Library.Delegates;

namespace ScriptCoreLib.JavaScript.Extensions
{
	/// <summary>
	/// Using these extensions will inject XLinq libraries into the compilation.
	/// </summary>
	public static class INodeExtensionsWithXLinq
	{
		/// <summary>
		/// Converts XML to HTML and appends all created nodes to the container.
		/// </summary>
		/// <param name="e"></param>
		/// <param name="value"></param>
		public static void Add(this INode e, XElement value)
		{
			var c = default(IHTMLDiv);

			if (e.ownerDocument != null)
			{
				c = (IHTMLDiv)e.ownerDocument.createElement("div");
			}
			else
			{
				c = new IHTMLDiv();
			}

			c.innerHTML = value.ToString();

			e.appendChild(c.firstChild);
		}



		public static void Add(this INode e, Action<XElementAction> factory)
		{
			factory(e.Add);
		}

		public static void WithContent(this IHTMLIFrame that, Action<IHTMLBody> y)
		{
			that.WhenDocumentReady(
				d =>
				{
					d.WithContent();
					d.WhenContentReady(y);
				}
			);
		}

		public static IHTMLDocument WithContent(this IHTMLDocument document, params object[] content)
		{
			document.open("text/html", "replace");

			var doc = new XElement("body");

			doc.Add(new XAttribute("style", "border: 0; margin: 0; padding: 0;"));

			doc.Add(content);

			document.write(doc.ToString());
			document.close();

			return document; 
		}
	}
}
