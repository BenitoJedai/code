using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.XML.XLinq
{
	[Script(Implements = typeof(XAttribute))]
	internal class __XAttribute : __XObject
	{
		internal __XContainer InternalElement;

		public XName Name { get; set; }

		public string InternalValue;

		public string Value
		{
			get
			{
				if (this.InternalElement == null)
					return InternalValue;

				return (string)this.InternalElement.InternalElement.getAttribute(this.Name.LocalName);
			}
			set
			{
				this.InternalElement.InternalElement.setAttribute(this.Name.LocalName, value);
			}
		}

		public __XAttribute(XName name, object value)
		{
			this.Name = name;
			this.InternalValue = value as string;

		}

		public static implicit operator XAttribute(__XAttribute a)
		{
			return (XAttribute)(object)a;
		}
	}
}
