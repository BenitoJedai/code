// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.text.Element

using ScriptCoreLib;
using java.lang;

namespace javax.swing.text
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/text/Element.html
	[Script(IsNative = true)]
	public interface Element
	{
		/// <summary>
		/// Fetches the collection of attributes this element contains.
		/// </summary>
		AttributeSet getAttributes();

		/// <summary>
		/// Fetches the document associated with this element.
		/// </summary>
		Document getDocument();

		/// <summary>
		/// Fetches the child element at the given index.
		/// </summary>
		Element getElement(int @index);

		/// <summary>
		/// Gets the number of child elements contained by this element.
		/// </summary>
		int getElementCount();

		/// <summary>
		/// Gets the child element index closest to the given offset.
		/// </summary>
		int getElementIndex(int @offset);

		/// <summary>
		/// Fetches the offset from the beginning of the document
		/// that this element ends at.
		/// </summary>
		int getEndOffset();

		/// <summary>
		/// Fetches the name of the element.
		/// </summary>
		string getName();

		/// <summary>
		/// Fetches the parent element.
		/// </summary>
		Element getParentElement();

		/// <summary>
		/// Fetches the offset from the beginning of the document
		/// that this element begins at.
		/// </summary>
		int getStartOffset();

		/// <summary>
		/// Is this element a leaf element? An element that
		/// <i>may</i> have children, even if it currently
		/// has no children, would return <code>false</code>.
		/// </summary>
		bool isLeaf();

	}
}
