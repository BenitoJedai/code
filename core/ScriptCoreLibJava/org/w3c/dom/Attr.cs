// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using org.w3c.dom;

namespace org.w3c.dom
{
	// http://java.sun.com/j2se/1.4.2/docs/api/org/w3c/dom/Attr.html
	[Script(IsNative = true)]
	public interface Attr : Node
	{
		/// <summary>
		/// Returns the name of this attribute.
		/// </summary>
		string getName();

		/// <summary>
		/// The <code>Element</code> node this attribute is attached to or
		/// <code>null</code> if this attribute is not in use.
		/// </summary>
		Element getOwnerElement();

		/// <summary>
		/// If this attribute was explicitly given a value in the original
		/// document, this is <code>true</code>; otherwise, it is
		/// <code>false</code>.
		/// </summary>
		bool getSpecified();

		/// <summary>
		/// On retrieval, the value of the attribute is returned as a string.
		/// </summary>
		string getValue();

		/// <summary>
		/// On retrieval, the value of the attribute is returned as a string.
		/// </summary>
		void setValue(string @value);

	}
}
