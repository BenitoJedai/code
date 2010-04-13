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
			: this(null)
		{
		}

		public __XElement(XName name)
		{
			InternalElementName = (__XName)(object)name;
		}

		public __XElement(XName name, object item)
		{
			InternalElementName = (__XName)(object)name;
			this.Add(item);
		}

		public __XElement(XName name, params object[] c)
		{
			InternalElementName = (__XName)(object)name;
			foreach (var item in c)
			{
				this.Add(item);
			}
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

			if (e.hasAttribute(name.LocalName))
			{
				return (XAttribute)(object)new __XAttribute(name, null) { InternalElement = this };

			}

			return null;
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


		public static __XElement Parse(string text)
		{
			var x = __XDocument.Parse(text);

			return x.Root;
		}
	}
}
