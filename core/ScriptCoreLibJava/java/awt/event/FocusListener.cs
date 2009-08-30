// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.@event;
using java.util;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/FocusListener.html
	[Script(IsNative = true)]
	public interface FocusListener : EventListener
	{
		/// <summary>
		/// Invoked when a component gains the keyboard focus.
		/// </summary>
		void focusGained(FocusEvent @e);

		/// <summary>
		/// Invoked when a component loses the keyboard focus.
		/// </summary>
		void focusLost(FocusEvent @e);

	}
}

