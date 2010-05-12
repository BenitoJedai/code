using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.XML;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.BCLImplementation.System.XML.XLinq;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.Extensions
{
	[Script]
	public static class XLinqExtensions
	{
		public static XDocument ToXDocument(this IXMLDocument doc)
		{
			return (__XDocument)doc;
		}

		public static XElement AsXElement(this IElement e)
		{
			return (XElement)(object)new __XElement((XName)null) { InternalValue = e };
		}
	}
}
