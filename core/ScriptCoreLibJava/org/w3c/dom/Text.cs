// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/org.w3c.dom.Text

using ScriptCoreLib;

namespace org.w3c.dom
{
	// http://java.sun.com/j2se/1.4.2/docs/api/org/w3c/dom/Text.html
	[Script(IsNative = true)]
	public interface Text : CharacterData
	{
		/// <summary>
		/// Breaks this node into two nodes at the specified <code>offset</code>,
		/// keeping both in the tree as siblings.
		/// </summary>
		Text splitText(int @offset);

	}
}
