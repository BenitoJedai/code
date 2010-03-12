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


		public string Value
		{
			get
			{
				return this.InternalElement.text;
			}
		}
	}
}
