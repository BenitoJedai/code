using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting;

namespace ScriptCoreLib.JavaScript.Remoting.Extensions
{
	public static class PNodeExtensions
	{
		public static void AttachTo(this PNode e, PNode parent)
		{
			parent.appendChild(e);
		}

		public static void AttachToDocument(this PUltraComponent c1, PHTMLDocument doc)
		{
			c1.WhenReady(
				delegate
				{
					c1.Container.AttachToDocument(doc);
				}
			);
		}

		public static void AttachToDocument(this PNode c1, PHTMLDocument doc)
		{
			doc.get_body(
				body =>
				{
					c1.AttachTo(body);
				}
			);
		}


		public static void AttachTo(this PUltraComponent c1, PNode parent)
		{
			c1.WhenReady(
				delegate
				{
					parent.appendChild(c1.Container);
				}
			);
		}


		public static void Orphanize(this PNode n)
		{
			n.get_parentNode(
				p =>
				{
					p.removeChild(n);
				}
			);
		}

	
	}
}
