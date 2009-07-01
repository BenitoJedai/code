// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using org.w3c.dom;

namespace org.w3c.dom
{
	// http://java.sun.com/j2se/1.4.2/docs/api/org/w3c/dom/Element.html
	[Script(IsNative = true)]
	public interface Element : Node
	{
		/// <summary>
		/// Retrieves an attribute value by name.
		/// </summary>
		string getAttribute(string @name);

		/// <summary>
		/// Retrieves an attribute node by name.
		/// </summary>
		Attr getAttributeNode(string @name);

		/// <summary>
		/// Retrieves an <code>Attr</code> node by local name and namespace URI.
		/// </summary>
		Attr getAttributeNodeNS(string @namespaceURI, string @localName);

		/// <summary>
		/// Retrieves an attribute value by local name and namespace URI.
		/// </summary>
		string getAttributeNS(string @namespaceURI, string @localName);

		/// <summary>
		/// Returns a <code>NodeList</code> of all descendant <code>Elements</code>
		/// with a given tag name, in the order in which they are encountered in
		/// a preorder traversal of this <code>Element</code> tree.
		/// </summary>
		NodeList getElementsByTagName(string @name);

		/// <summary>
		/// Returns a <code>NodeList</code> of all the descendant
		/// <code>Elements</code> with a given local name and namespace URI in
		/// the order in which they are encountered in a preorder traversal of
		/// this <code>Element</code> tree.
		/// </summary>
		NodeList getElementsByTagNameNS(string @namespaceURI, string @localName);

		/// <summary>
		/// The name of the element.
		/// </summary>
		string getTagName();

		/// <summary>
		/// Returns <code>true</code> when an attribute with a given name is
		/// specified on this element or has a default value, <code>false</code>
		/// otherwise.
		/// </summary>
		bool hasAttribute(string @name);

		/// <summary>
		/// Returns <code>true</code> when an attribute with a given local name and
		/// namespace URI is specified on this element or has a default value,
		/// <code>false</code> otherwise.
		/// </summary>
		bool hasAttributeNS(string @namespaceURI, string @localName);

		/// <summary>
		/// Removes an attribute by name.
		/// </summary>
		void removeAttribute(string @name);

		/// <summary>
		/// Removes the specified attribute node.
		/// </summary>
		Attr removeAttributeNode(Attr @oldAttr);

		/// <summary>
		/// Removes an attribute by local name and namespace URI.
		/// </summary>
		void removeAttributeNS(string @namespaceURI, string @localName);

		/// <summary>
		/// Adds a new attribute.
		/// </summary>
		void setAttribute(string @name, string @value);

		/// <summary>
		/// Adds a new attribute node.
		/// </summary>
		Attr setAttributeNode(Attr @newAttr);

		/// <summary>
		/// Adds a new attribute.
		/// </summary>
		Attr setAttributeNodeNS(Attr @newAttr);

		/// <summary>
		/// Adds a new attribute.
		/// </summary>
		void setAttributeNS(string @namespaceURI, string @qualifiedName, string @value);

	}
}
