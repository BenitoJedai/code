using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.Remoting.DOM.HTML.Remoting
{
	public interface PHTMLDocument
	{
		// to be used from flash or java applet
		// to be defined as async API

		void createElement(string tag, PHTMLElementAction h);
		void createTextNode(string text, PTextNodeAction h);
		void createTextNode(string text);

		void get_documentElement(PHTMLElementAction e);
		void get_body(PHTMLElementAction e);
	}

	public class PIHTMLDocument : PHTMLDocument
	{
		// Some serius name mangling :)

		// P = Proxy
		// I = Interface

		internal IHTMLDocument InternalDocument;

		public void createElement(string tagName, PHTMLElementAction y)
		{
			PIHTMLElement i = InternalDocument.createElement(tagName);

			y(i);
		}

		public void createTextNode(string text, PTextNodeAction y)
		{
			var v = new PITextNode
			{
				InternalTextNode = InternalDocument.createTextNode(text)
			};

			y(v);
		}

		public void createTextNode(string text)
		{
			InternalDocument.createTextNode(text);
		}




		public void get_documentElement(PHTMLElementAction e)
		{
			PIHTMLElement i = this.InternalDocument.documentElement;

			e(i);
		}

		public void get_body(PHTMLElementAction e)
		{
			PIHTMLElement i = this.InternalDocument.body;

			e(i);
		}
	}
}
