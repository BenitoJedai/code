using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Xml.Linq
{
	[Script(Implements = typeof(global::System.Xml.Linq.XNode))]
	internal class __XNode : __XObject
	{
		internal org.w3c.dom.Node InternalNode;

		public override string ToString()
		{
			// http://faq.javaranch.com/java/DocumentToString

			//Source source = new DOMSource(node);
			//StringWriter stringWriter = new StringWriter();
			//Result result = new StreamResult(stringWriter);
			//TransformerFactory factory = TransformerFactory.newInstance();
			//Transformer transformer = factory.newTransformer();
			//transformer.transform(source, result);
			//return stringWriter.getBuffer().toString();

			return "<node />";
		}

	}
}
