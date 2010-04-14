using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using javax.xml.transform;
using javax.xml.transform.dom;
using java.io;
using javax.xml.transform.stream;

namespace ScriptCoreLibJava.BCLImplementation.System.Xml.Linq
{
	[Script(Implements = typeof(global::System.Xml.Linq.XNode))]
	internal class __XNode : __XObject
	{
		internal org.w3c.dom.Node InternalNode;

		public override string ToString()
		{
			// http://faq.javaranch.com/java/DocumentToString
			var r = default(string);

			try
			{
				Source source = new DOMSource(this.InternalNode);
				StringWriter stringWriter = new StringWriter();
				Result result = new StreamResult(stringWriter);
				TransformerFactory factory = TransformerFactory.newInstance();
				Transformer transformer = factory.newTransformer();
				transformer.transform(source, result);

				r = stringWriter.getBuffer().toString();
			}
			catch
			{
				throw new NotSupportedException();
			}

			return r;
		}

	}
}
