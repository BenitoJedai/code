using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using org.w3c.dom;
using javax.xml.parsers;
using java.io;
using org.xml.sax;

namespace XmlExample
{
	[Script]
	public static class Extensions
	{
		public static Document ToDocument(this string e)
		{
			var doc = default(Document);
			try
			{
				var factory = DocumentBuilderFactory.newInstance();
				var builder = factory.newDocumentBuilder();
				doc = builder.parse(new InputSource( new StringReader(e)));
			}
			catch (csharp.ThrowableException ex)
			{
				((java.lang.Throwable)(object)ex).printStackTrace();
			}

			return doc;
		}
	}
}
