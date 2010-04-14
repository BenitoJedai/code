using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml.Linq;

namespace ScriptCoreLibJava.BCLImplementation.System.Xml.Linq
{
	[Script(Implements = typeof(global::System.Xml.Linq.XElement))]
	internal class __XElement : __XNode
	{
		public org.w3c.dom.Element InternalElement
		{
			get
			{
				return (org.w3c.dom.Element)this.InternalNode;
			}
		}

		public static XElement Parse(string text)
		{
			return __XDocument.Parse(text).Root;
		}
	}
}
