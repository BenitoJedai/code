// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.event.DocumentListener

using ScriptCoreLib;
using java.util;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/DocumentListener.html
	[Script(IsNative = true)]
	public interface DocumentListener : EventListener
	{
		/// <summary>
		/// Gives notification that an attribute or set of attributes changed.
		/// </summary>
		void changedUpdate(DocumentEvent @e);

		/// <summary>
		/// Gives notification that there was an insert into the document.
		/// </summary>
		void insertUpdate(DocumentEvent @e);

		/// <summary>
		/// Gives notification that a portion of the document has been
		/// removed.
		/// </summary>
		void removeUpdate(DocumentEvent @e);

	}
}
