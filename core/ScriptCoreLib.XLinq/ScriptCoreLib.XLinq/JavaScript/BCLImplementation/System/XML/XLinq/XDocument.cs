using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM.XML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.XML.XLinq
{

	[Script(Implements = typeof(XDocument))]
	internal class __XDocument : __XContainer
	{
		IXMLDocument InternalDocument;

		public static __XDocument Parse(string text)
		{
			var InternalValue = IXMLDocument.Parse(text);

			return new __XDocument { InternalDocument = InternalValue, InternalValue = InternalValue };
		}

		__XElement _Root;

		public __XElement Root
		{
			get
			{
				if (_Root == null)
				{
					_Root = new __XElement { InternalValue = InternalDocument.documentElement };
				}

				return _Root;
			}
		}
	}
}
