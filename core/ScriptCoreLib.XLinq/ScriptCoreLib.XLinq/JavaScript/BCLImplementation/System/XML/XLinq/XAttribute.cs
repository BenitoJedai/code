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
		internal __XElement InternalElement;

		public XName Name { get; set; }

		public string Value
		{
			get
			{
				return (string)this.InternalElement.InternalElement.getAttribute(this.Name.LocalName);
			}
			set
			{
				this.InternalElement.InternalElement.setAttribute(this.Name.LocalName, value);
			}
		}
	}
}
