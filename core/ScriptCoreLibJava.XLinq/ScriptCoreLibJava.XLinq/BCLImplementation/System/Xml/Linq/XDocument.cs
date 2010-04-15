using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml.Linq;
using org.xml.sax;
using java.io;

namespace ScriptCoreLibJava.BCLImplementation.System.Xml.Linq
{
	[Script(Implements = typeof(global::System.Xml.Linq.XDocument))]
	internal class __XDocument : __XContainer
	{
		public org.w3c.dom.Document InternalDocument;

		public XElement Root
		{
			get
			{
				return (XElement)(object)
					new __XElement
					{
						InternalValue = InternalDocument.getDocumentElement(),

					};
			}
		}

		public static __XDocument Parse(string text)
		{
			var r = default(__XDocument);

			try
			{
				// http://stackoverflow.com/questions/33262/how-do-i-load-an-org-w3c-dom-document-from-xml-in-a-string

				var f = javax.xml.parsers.DocumentBuilderFactory.newInstance();
				var b = f.newDocumentBuilder();

				var s = new InputSource(new StringReader(text));

				
				var doc = b.parse(s);

				r = new __XDocument
				{
					InternalDocument = doc,
					InternalValue = doc
				};

			}
			catch
			{
				throw new NotSupportedException();
			}

			return r;
		}
	}
}
