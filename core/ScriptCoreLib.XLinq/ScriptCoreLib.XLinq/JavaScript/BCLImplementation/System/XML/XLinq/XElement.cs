using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM.XML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.XML.XLinq
{
	[Script(Implements = typeof(XElement))]
	internal class __XElement : __XContainer
	{
		__XName _Name;

		public __XName Name
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public __XElement()
			: this(null)
		{
		}

		public __XElement(XName name)
		{
			// implement
		}

		public IXMLElement InternalElement
		{
			get
			{
				IXMLElement e = (IXMLElement)this.InternalValue;
				return e;
			}
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

		public string Value
		{
			get
			{
				return this.InternalElement.text;
			}
		}
	}
}
