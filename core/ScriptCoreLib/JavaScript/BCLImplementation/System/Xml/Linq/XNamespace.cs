using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.XML.XLinq
{
	[Script(Implements = typeof(global::System.Xml.Linq.XNamespace))]
	internal class __XNamespace
	{
		public string NamespaceName { get; set; }

		public static __XNamespace Get(string namespaceName)
		{
			return new __XNamespace { NamespaceName = namespaceName };
		}

		public XName GetName(string localName)
		{
			return __XName.Get(localName, NamespaceName);
		}
	}
}
