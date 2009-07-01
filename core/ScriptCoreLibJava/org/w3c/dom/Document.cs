// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using org.w3c.dom;

namespace org.w3c.dom
{
	// http://java.sun.com/j2se/1.4.2/docs/api/org/w3c/dom/Document.html
	[Script(IsNative = true)]
	public interface Document : Node
	{
		/// <summary>
		/// Creates an <code>Attr</code> of the given name.
		/// </summary>
		Attr createAttribute(string @name);

		/// <summary>
		/// Creates an attribute of the given qualified name and namespace URI.
		/// </summary>
		Attr createAttributeNS(string @namespaceURI, string @qualifiedName);

		/// <summary>
		/// Creates a <code>CDATASection</code> node whose value is the specified
		/// string.
		/// </summary>
		CDATASection createCDATASection(string @data);

		/// <summary>
		/// Creates a <code>Comment</code> node given the specified string.
		/// </summary>
		Comment createComment(string @data);

		/// <summary>
		/// Creates an empty <code>DocumentFragment</code> object.
		/// </summary>
		DocumentFragment createDocumentFragment();

		/// <summary>
		/// Creates an element of the type specified.
		/// </summary>
		Element createElement(string @tagName);

		/// <summary>
		/// Creates an element of the given qualified name and namespace URI.
		/// </summary>
		Element createElementNS(string @namespaceURI, string @qualifiedName);

		/// <summary>
		/// Creates an <code>EntityReference</code> object.
		/// </summary>
		EntityReference createEntityReference(string @name);

		/// <summary>
		/// Creates a <code>ProcessingInstruction</code> node given the specified
		/// name and data strings.
		/// </summary>
		ProcessingInstruction createProcessingInstruction(string @target, string @data);

		/// <summary>
		/// Creates a <code>Text</code> node given the specified string.
		/// </summary>
		Text createTextNode(string @data);

		/// <summary>
		/// The Document Type Declaration (see <code>DocumentType</code>)
		/// associated with this document.
		/// </summary>
		DocumentType getDoctype();

		/// <summary>
		/// This is a convenience attribute that allows direct access to the child
		/// node that is the root element of the document.
		/// </summary>
		Element getDocumentElement();

		/// <summary>
		/// Returns the <code>Element</code> whose <code>ID</code> is given by
		/// <code>elementId</code>.
		/// </summary>
		Element getElementById(string @elementId);

		/// <summary>
		/// Returns a <code>NodeList</code> of all the <code>Elements</code> with a
		/// given tag name in the order in which they are encountered in a
		/// preorder traversal of the <code>Document</code> tree.
		/// </summary>
		NodeList getElementsByTagName(string @tagname);

		/// <summary>
		/// Returns a <code>NodeList</code> of all the <code>Elements</code> with a
		/// given local name and namespace URI in the order in which they are
		/// encountered in a preorder traversal of the <code>Document</code> tree.
		/// </summary>
		NodeList getElementsByTagNameNS(string @namespaceURI, string @localName);

		/// <summary>
		/// The <code>DOMImplementation</code> object that handles this document.
		/// </summary>
		DOMImplementation getImplementation();

		/// <summary>
		/// Imports a node from another document to this document.
		/// </summary>
		Node importNode(Node @importedNode, bool @deep);

	}
}
