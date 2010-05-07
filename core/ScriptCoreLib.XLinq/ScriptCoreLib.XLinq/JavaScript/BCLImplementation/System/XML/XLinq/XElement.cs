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

		public __XName Name
		{
			get
			{
				return new __XName { InternalValue = this.InternalElement.nodeName };
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public __XElement()
			: this("item", null, null)
		{
		}

		public __XElement(XName name)
			: this(name, null, null)
		{
		}

		public __XElement(XName name, object item)
			: this(name, item, null)
		{
		}

		public __XElement(XName name, params object[] c)
			: this(name, null, c)
		{
		}

		public __XElement(XName name, object c0, object[] c)
		{
			InternalElementName = (__XName)(object)name;

			if (c0 != null)
				this.Add(c0);

			if (c != null)
				this.Add(c);
		}


		public string Value
		{
			get
			{
				return this.InternalElement.text;
			}
			set
			{
				this.RemoveAll();
				this.Add(value);
			}
		}

		public void RemoveAll()
		{
			if (this.InternalElement == null)
				return;

			var p = this.InternalElement.firstChild;

			while (p != null)
			{
				this.InternalElement.removeChild(p);

				p = this.InternalElement.firstChild;
			}
		}

		public XAttribute Attribute(XName name)
		{
			var e = InternalElement;

			if (null == e.getAttribute(name.LocalName))
				return null;

			// http://msdn.microsoft.com/en-us/library/ms757048(VS.85).aspx
			// there is no hasAttribute method in IE

			//if (e.hasAttribute(name.LocalName))
			return (XAttribute)(object)new __XAttribute(name, null) { InternalElement = this };
		}

		public IEnumerable<XAttribute> Attributes()
		{
			return this.InternalElement.attributes.Select(
				k =>
				{
					return (XAttribute)new __XAttribute(k.name, null)
					{
						InternalElement = this
					};
				}
			);
		}


		public static XElement Parse(string text)
		{
			var x = __XDocument.Parse(text);

			return x.Root;
		}
	}
}
