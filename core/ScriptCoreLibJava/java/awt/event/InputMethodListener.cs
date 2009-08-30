// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.@event;
using java.util;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/InputMethodListener.html
	[Script(IsNative = true)]
	public interface InputMethodListener : EventListener
	{
		/// <summary>
		/// Invoked when the caret within composed text has changed.
		/// </summary>
		void caretPositionChanged(InputMethodEvent @event);

		/// <summary>
		/// Invoked when the text entered through an input method has changed.
		/// </summary>
		void inputMethodTextChanged(InputMethodEvent @event);

	}
}

