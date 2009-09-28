// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.event.DocumentEvent

using ScriptCoreLib;
using javax.swing.text;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/DocumentEvent.html
	[Script(IsNative = true)]
	public interface DocumentEvent
	{


		/// <summary>
		/// Gets the document that sourced the change event.
		/// </summary>
		Document getDocument();

		/// <summary>
		/// Returns the length of the change.
		/// </summary>
		int getLength();

		/// <summary>
		/// Returns the offset within the document of the start
		/// of the change.
		/// </summary>
		int getOffset();


	}
}
