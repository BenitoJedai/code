using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM.XML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.XML.XLinq
{
    [Script(Implements = typeof(XContainer))]
    internal class __XContainer : __XNode
    {

		public IXMLElement InternalElement
		{
			get
			{
				IXMLElement e = (IXMLElement)this.InternalValue;
				return e;
			}
		}

		public XElement Element(XName name)
		{
			return Elements(name).Single();
		}

		public IEnumerable<XElement> Elements(XName name)
		{
			var e = InternalElement;
			var a = new List<XElement>();

			foreach (var item in e.childNodes)
			{
				if (item.nodeType == ScriptCoreLib.JavaScript.DOM.INode.NodeTypeEnum.ElementNode)
					if (item.nodeName == name.LocalName)
						a.Add(
							(XElement)(object)new __XElement { InternalValue = item }
						);

			}

			return a;
		}
    }
}
