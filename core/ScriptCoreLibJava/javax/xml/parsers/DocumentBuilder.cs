using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using org.w3c.dom;
using java.io;
using org.xml.sax;

namespace javax.xml.parsers
{
	// http://java.sun.com/j2se/1.5.0/docs/api/javax/xml/parsers/DocumentBuilder.html
	[Script(IsNative = true)]
	public class DocumentBuilder
	{
		/// <summary>
		/// Parse the content of the given InputStream as an XML document and return a new DOM Document object.
		/// </summary>
		/// <param name="?"></param>
		/// <returns></returns>
		public Document parse(InputStream _is)
		{
			return default(Document);
		}


		/// <summary>
		/// Parse the content of the given input source as an XML document
		/// and return a new DOM <A HREF="../../../org/w3c/dom/Document.html" title="interface in org.w3c.dom"><CODE>Document</CODE></A> object.
		/// </summary>
		public Document parse(InputSource @is)
		{
			return default(Document);
		}
	}
}
