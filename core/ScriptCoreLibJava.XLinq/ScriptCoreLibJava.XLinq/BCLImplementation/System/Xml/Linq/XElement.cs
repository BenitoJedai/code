using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml.Linq;

namespace ScriptCoreLibJava.BCLImplementation.System.Xml.Linq
{
	[Script(Implements = typeof(global::System.Xml.Linq.XElement))]
	internal class __XElement : __XContainer
	{

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



		public static XElement Parse(string text)
		{
			return __XDocument.Parse(text).Root;
		}

		public string Value
		{
			get
			{
				var w = new StringBuilder();

				var f = this.InternalElement.getFirstChild();

				// http://faq.javaranch.com/java/GetNodeValue
				// http://java.sun.com/j2se/1.4.2/docs/api/constant-values.html#org.w3c.dom.Node.TEXT_NODE
				if (f.getNodeType() == 3)
				{
					w.Append(f.getNodeValue());
				}

				return w.ToString();
			}
			set
			{
				this.RemoveNodes();
				this.Add(Value);
			}
		}
	}
}
