// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using org.w3c.dom;

namespace org.w3c.dom
{
	// http://java.sun.com/j2se/1.4.2/docs/api/org/w3c/dom/Node.html
	[Script(IsNative = true)]
	public interface Node
	{
		/// <summary>
		/// Adds the node <code>newChild</code> to the end of the list of children
		/// of this node.
		/// </summary>
		Node appendChild(Node @newChild);

		/// <summary>
		/// Returns a duplicate of this node, i.e., serves as a generic copy
		/// constructor for nodes.
		/// </summary>
		Node cloneNode(bool @deep);

		/// <summary>
		/// A <code>NamedNodeMap</code> containing the attributes of this node (if
		/// it is an <code>Element</code>) or <code>null</code> otherwise.
		/// </summary>
		NamedNodeMap getAttributes();

		/// <summary>
		/// A <code>NodeList</code> that contains all children of this node.
		/// </summary>
		NodeList getChildNodes();

		/// <summary>
		/// The first child of this node.
		/// </summary>
		Node getFirstChild();

		/// <summary>
		/// The last child of this node.
		/// </summary>
		Node getLastChild();

		/// <summary>
		/// Returns the local part of the qualified name of this node.
		/// </summary>
		string getLocalName();

		/// <summary>
		/// The namespace URI of this node, or <code>null</code> if it is
		/// unspecified.
		/// </summary>
		string getNamespaceURI();

		/// <summary>
		/// The node immediately following this node.
		/// </summary>
		Node getNextSibling();

		/// <summary>
		/// The name of this node, depending on its type; see the table above.
		/// </summary>
		string getNodeName();

		/// <summary>
		/// A code representing the type of the underlying object, as defined above.
		/// </summary>
		short getNodeType();

		/// <summary>
		/// The value of this node, depending on its type; see the table above.
		/// </summary>
		string getNodeValue();

		/// <summary>
		/// The <code>Document</code> object associated with this node.
		/// </summary>
		Document getOwnerDocument();

		/// <summary>
		/// The parent of this node.
		/// </summary>
		Node getParentNode();

		/// <summary>
		/// The namespace prefix of this node, or <code>null</code> if it is
		/// unspecified.
		/// </summary>
		string getPrefix();

		/// <summary>
		/// The node immediately preceding this node.
		/// </summary>
		Node getPreviousSibling();

		/// <summary>
		/// Returns whether this node (if it is an element) has any attributes.
		/// </summary>
		bool hasAttributes();

		/// <summary>
		/// Returns whether this node has any children.
		/// </summary>
		bool hasChildNodes();

		/// <summary>
		/// Inserts the node <code>newChild</code> before the existing child node
		/// <code>refChild</code>.
		/// </summary>
		Node insertBefore(Node @newChild, Node @refChild);

		/// <summary>
		/// Tests whether the DOM implementation implements a specific feature and
		/// that feature is supported by this node.
		/// </summary>
		bool isSupported(string @feature, string @version);

		/// <summary>
		/// Puts all <code>Text</code> nodes in the full depth of the sub-tree
		/// underneath this <code>Node</code>, including attribute nodes, into a
		/// "normal" form where only structure (e.g., elements, comments,
		/// processing instructions, CDATA sections, and entity references)
		/// separates <code>Text</code> nodes, i.e., there are neither adjacent
		/// <code>Text</code> nodes nor empty <code>Text</code> nodes.
		/// </summary>
		void normalize();

		/// <summary>
		/// Removes the child node indicated by <code>oldChild</code> from the list
		/// of children, and returns it.
		/// </summary>
		Node removeChild(Node @oldChild);

		/// <summary>
		/// Replaces the child node <code>oldChild</code> with <code>newChild</code>
		/// in the list of children, and returns the <code>oldChild</code> node.
		/// </summary>
		Node replaceChild(Node @newChild, Node @oldChild);

		/// <summary>
		/// The value of this node, depending on its type; see the table above.
		/// </summary>
		void setNodeValue(string @nodeValue);

		/// <summary>
		/// The namespace prefix of this node, or <code>null</code> if it is
		/// unspecified.
		/// </summary>
		void setPrefix(string @prefix);

	}
}
