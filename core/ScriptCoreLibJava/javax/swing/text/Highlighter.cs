// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.text.Highlighter

using ScriptCoreLib;
using java.awt;
using java.lang;

namespace javax.swing.text
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/text/Highlighter.html
	[Script(IsNative = true)]
	public interface Highlighter
	{
		/// <summary>
		/// Changes the given highlight to span a different portion of
		/// the document.
		/// </summary>
		void changeHighlight(object @tag, int @p0, int @p1);

		/// <summary>
		/// Called when the UI is being removed from the
		/// interface of a JTextComponent.
		/// </summary>
		void deinstall(JTextComponent @c);

	

		/// <summary>
		/// Called when the UI is being installed into the
		/// interface of a JTextComponent.
		/// </summary>
		void install(JTextComponent @c);

		/// <summary>
		/// Renders the highlights.
		/// </summary>
		void paint(Graphics @g);

		/// <summary>
		/// Removes all highlights this highlighter is responsible for.
		/// </summary>
		void removeAllHighlights();

		/// <summary>
		/// Removes a highlight from the view.
		/// </summary>
		void removeHighlight(object @tag);

	}
}
