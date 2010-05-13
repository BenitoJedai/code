using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.Extensions
{
	public static class INodeExtensions
	{
		public static IHTMLDiv WithinContainer(this INode e)
		{
			var x = new IHTMLDiv { e };

			x.style.width = "100%";
			x.style.height = "100%";

			return x;
		}


		public static IHTMLIFrame WhenContentReady(this IHTMLIFrame that, Action<IHTMLBody> y)
		{
			return that.WhenDocumentReady(
				doc =>
				{
					doc.WhenContentReady(y);
				}
			);
		}


		public static void WhenContentReady(this IHTMLDocument doc, Action<IHTMLBody> y)
		{
			if (doc.body != null)
			{
				y(doc.body);
			}
			else
			{
				new Timer(
					t =>
					{
						if (doc.body == null)
							return;

						t.Stop();

						y(doc.body);
					}
				).StartInterval(15);
			}
		}

		public static IHTMLIFrame WhenDocumentReady(this IHTMLIFrame that, Action<IHTMLDocument> y)
		{
			new Timer(
				t =>
				{
					if (that.contentWindow == null)
						return;

					if (that.contentWindow.document == null)
						return;

					t.Stop();

					y(that.contentWindow.document);
				}
			).StartInterval(15);

			return that;
		}

		public static void Clear(this INode e)
		{
			var p = e.firstChild;

			while (p != null)
			{
				e.removeChild(p);
				p = e.firstChild;
			}
		}

		public static void ReplaceWith(this INode e, INode value)
		{
			// http://msdn.microsoft.com/en-us/library/system.xml.linq.xnode.replacewith.aspx

			if (e.parentNode == null)
				return;

			e.parentNode.replaceChild(value, e);
		}

		public static void ReplaceContentWith(this INode parent, INode value)
		{
			parent.Clear();
			parent.Add(value);
		}
	}


}
