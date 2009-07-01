// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using org.w3c.dom;

namespace org.w3c.dom
{
	// http://java.sun.com/j2se/1.4.2/docs/api/org/w3c/dom/NodeList.html
	[Script(IsNative = true)]
	public interface NodeList
	{
		/// <summary>
		/// The number of nodes in the list.
		/// </summary>
		int getLength();

		/// <summary>
		/// Returns the <code>index</code>th item in the collection.
		/// </summary>
		Node item(int @index);

	}
}
