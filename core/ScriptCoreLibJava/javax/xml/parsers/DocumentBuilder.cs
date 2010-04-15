// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.xml.parsers.DocumentBuilder

using ScriptCoreLib;
using java.io;
using java.lang;
using org.w3c.dom;
using org.xml.sax;

namespace javax.xml.parsers
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/xml/parsers/DocumentBuilder.html
	[Script(IsNative = true)]
	public abstract class DocumentBuilder
	{
		/// <summary>
		/// 
		/// </summary>
		public DocumentBuilder()
		{
		}

		/// <summary>
		/// Obtain an instance of a <A HREF="../../../org/w3c/dom/DOMImplementation.html" title="interface in org.w3c.dom"><CODE>DOMImplementation</CODE></A> object.
		/// </summary>
		abstract public DOMImplementation getDOMImplementation();

		/// <summary>
		/// Indicates whether or not this parser is configured to
		/// understand namespaces.
		/// </summary>
		abstract public bool isNamespaceAware();

		/// <summary>
		/// Indicates whether or not this parser is configured to
		/// validate XML documents.
		/// </summary>
		abstract public bool isValidating();

		/// <summary>
		/// Obtain a new instance of a DOM <A HREF="../../../org/w3c/dom/Document.html" title="interface in org.w3c.dom"><CODE>Document</CODE></A> object
		/// to build a DOM tree with.
		/// </summary>
		abstract public Document newDocument();

		/// <summary>
		/// Parse the content of the given file as an XML document
		/// and return a new DOM <A HREF="../../../org/w3c/dom/Document.html" title="interface in org.w3c.dom"><CODE>Document</CODE></A> object.
		/// </summary>
		public Document parse(File @f)
		{
			return default(Document);
		}

		/// <summary>
		/// Parse the content of the given input source as an XML document
		/// and return a new DOM <A HREF="../../../org/w3c/dom/Document.html" title="interface in org.w3c.dom"><CODE>Document</CODE></A> object.
		/// </summary>
		abstract public Document parse(InputSource @is);

		/// <summary>
		/// Parse the content of the given <code>InputStream</code> as an XML
		/// document and return a new DOM <A HREF="../../../org/w3c/dom/Document.html" title="interface in org.w3c.dom"><CODE>Document</CODE></A> object.
		/// </summary>
		public Document parse(InputStream @is)
		{
			return default(Document);
		}

		/// <summary>
		/// Parse the content of the given <code>InputStream</code> as an XML
		/// document and return a new DOM <A HREF="../../../org/w3c/dom/Document.html" title="interface in org.w3c.dom"><CODE>Document</CODE></A> object.
		/// </summary>
		public Document parse(InputStream @is, string @systemId)
		{
			return default(Document);
		}

		/// <summary>
		/// Parse the content of the given URI as an XML document
		/// and return a new DOM <A HREF="../../../org/w3c/dom/Document.html" title="interface in org.w3c.dom"><CODE>Document</CODE></A> object.
		/// </summary>
		public Document parse(string @uri)
		{
			return default(Document);
		}

		/// <summary>
		/// Specify the <A HREF="../../../org/xml/sax/EntityResolver.html" title="interface in org.xml.sax"><CODE>EntityResolver</CODE></A> to be used to resolve
		/// entities present in the XML document to be parsed.
		/// </summary>
		abstract public void setEntityResolver(EntityResolver @er);

		/// <summary>
		/// Specify the <A HREF="../../../org/xml/sax/ErrorHandler.html" title="interface in org.xml.sax"><CODE>ErrorHandler</CODE></A> to be used to report
		/// errors present in the XML document to be parsed.
		/// </summary>
		abstract public void setErrorHandler(ErrorHandler @eh);

	}
}
