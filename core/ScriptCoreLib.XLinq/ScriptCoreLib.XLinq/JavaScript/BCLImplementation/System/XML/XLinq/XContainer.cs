using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.DOM.XML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.XML.XLinq
{
	[Script(Implements = typeof(XContainer))]
	internal abstract class __XContainer : __XNode
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
			return Elements(name).FirstOrDefault();
		}

		public IEnumerable<XElement> Elements()
		{
			var e = InternalElement;
			var a = new List<XElement>();

			foreach (var item in e.childNodes)
			{
				if (item.nodeType == ScriptCoreLib.JavaScript.DOM.INode.NodeTypeEnum.ElementNode)
					a.Add(
						(XElement)(object)new __XElement { InternalValue = item }
					);

			}

			return a;

		}

		public IEnumerable<XElement> Elements(XName name)
		{
			return this.Elements().Where(k => k.Name.LocalName == name.LocalName);

		}

		public __XName InternalElementName;

		public void Add(object content)
		{
			if (this.InternalValue == null)
			{
				var doc = new IXMLDocument(this.InternalElementName.LocalName);


				this.InternalValue = doc.documentElement;
			}

			{
				var e = content as string;

				if (e != null)
				{
					this.InternalValue.appendChild(
						this.InternalValue.ownerDocument.createTextNode(e)
					);
					return;
				}
			}

			#region XAttribute
			{
				var e = (__XAttribute)(object)(content as XAttribute);
				if (e != null)
				{
					var CurrentValue = e.Value;

					e.InternalElement = this;
					e.Value = CurrentValue;
					return;
				}
			}
			#endregion


			#region XElement
			{
				var e = (__XElement)(object)(content as XElement);
				if (e != null)
				{
					if (e.InternalValue == null)
					{
						e.InternalValue = this.InternalValue.ownerDocument.createElement(e.InternalElementName.LocalName);
					}
					else
					{
						__adoptNode(e);
					}

					this.InternalValue.appendChild(e.InternalValue);

					return;
				}
			}
			#endregion


			throw new NotImplementedException();
		}

		private void __adoptNode(__XElement e)
		{
			if (e.InternalValue.ownerDocument != this.InternalValue.ownerDocument)
			{
				var ownerDocument = this.InternalValue.ownerDocument;

				try
				{
					// IE does not implement adoptNode yet
					e.InternalValue = ownerDocument.adoptNode(e.InternalValue);
				}
				catch
				{

				}
			}
		}
	}
}
