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
		internal IXMLDocument InternalDocument;

		public static __XDocument Parse(string text)
		{
			var InternalValue = IXMLDocument.Parse(text);

			return InternalValue;
		}

		__XElement _Root;

		public XElement Root
		{
			get
			{
				if (_Root == null)
				{
					_Root = new __XElement(null, null) { InternalValue = InternalDocument.documentElement };
				}

				return (XElement)(object)_Root;
			}
		}

		public static implicit operator __XDocument(IXMLDocument InternalValue)
		{
			return new __XDocument
			{
				InternalDocument = InternalValue,
				InternalValue = InternalValue
			};
		}

		public static implicit operator XDocument(__XDocument doc)
		{
			return (XDocument)(object)(__XDocument)doc;
		}
	}
}
