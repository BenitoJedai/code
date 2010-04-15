// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.xml.parsers.DocumentBuilderFactory

using ScriptCoreLib;
using java.lang;

namespace javax.xml.parsers
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/xml/parsers/DocumentBuilderFactory.html
	[Script(IsNative = true)]
	public abstract class DocumentBuilderFactory
	{
		/// <summary>
		/// 
		/// </summary>
		public DocumentBuilderFactory()
		{
		}

		/// <summary>
		/// Allows the user to retrieve specific attributes on the underlying
		/// implementation.
		/// </summary>
		abstract public object getAttribute(string @name);

		/// <summary>
		/// Indicates whether or not the factory is configured to produce
		/// parsers which converts CDATA nodes to Text nodes and appends it to
		/// the adjacent (if any) Text node.
		/// </summary>
		public bool isCoalescing()
		{
			return default(bool);
		}

		/// <summary>
		/// Indicates whether or not the factory is configured to produce
		/// parsers which expand entity reference nodes.
		/// </summary>
		public bool isExpandEntityReferences()
		{
			return default(bool);
		}

		/// <summary>
		/// Indicates whether or not the factory is configured to produce
		/// parsers which ignores comments.
		/// </summary>
		public bool isIgnoringComments()
		{
			return default(bool);
		}

		/// <summary>
		/// Indicates whether or not the factory is configured to produce
		/// parsers which ignore ignorable whitespace in element content.
		/// </summary>
		public bool isIgnoringElementContentWhitespace()
		{
			return default(bool);
		}

		/// <summary>
		/// Indicates whether or not the factory is configured to produce
		/// parsers which are namespace aware.
		/// </summary>
		public bool isNamespaceAware()
		{
			return default(bool);
		}

		/// <summary>
		/// Indicates whether or not the factory is configured to produce
		/// parsers which validate the XML content during parse.
		/// </summary>
		public bool isValidating()
		{
			return default(bool);
		}

		/// <summary>
		/// Creates a new instance of a <A HREF="../../../javax/xml/parsers/DocumentBuilder.html" title="class in javax.xml.parsers"><CODE>DocumentBuilder</CODE></A>
		/// using the currently configured parameters.
		/// </summary>
		abstract public DocumentBuilder newDocumentBuilder();

		/// <summary>
		/// Obtain a new instance of a
		/// <code>DocumentBuilderFactory</code>.
		/// </summary>
		static public DocumentBuilderFactory newInstance()
		{
			return default(DocumentBuilderFactory);
		}

		/// <summary>
		/// Allows the user to set specific attributes on the underlying
		/// implementation.
		/// </summary>
		abstract public void setAttribute(string @name, object @value);

		/// <summary>
		/// Specifies that the parser produced by this code will
		/// convert CDATA nodes to Text nodes and append it to the
		/// adjacent (if any) text node.
		/// </summary>
		public void setCoalescing(bool @coalescing)
		{
		}

		/// <summary>
		/// Specifies that the parser produced by this code will
		/// expand entity reference nodes.
		/// </summary>
		public void setExpandEntityReferences(bool @expandEntityRef)
		{
		}

		/// <summary>
		/// Specifies that the parser produced by this code will
		/// ignore comments.
		/// </summary>
		public void setIgnoringComments(bool @ignoreComments)
		{
		}

		/// <summary>
		/// Specifies that the parsers created by this  factory must eliminate
		/// whitespace in element content (sometimes known loosely as
		/// 'ignorable whitespace') when parsing XML documents (see XML Rec
		/// 2.10).
		/// </summary>
		public void setIgnoringElementContentWhitespace(bool @whitespace)
		{
		}

		/// <summary>
		/// Specifies that the parser produced by this code will
		/// provide support for XML namespaces.
		/// </summary>
		public void setNamespaceAware(bool @awareness)
		{
		}

		/// <summary>
		/// Specifies that the parser produced by this code will
		/// validate documents as they are parsed.
		/// </summary>
		public void setValidating(bool @validating)
		{
		}

	}
}
