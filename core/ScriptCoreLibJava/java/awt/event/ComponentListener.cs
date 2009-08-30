// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.@event;
using java.util;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/ComponentListener.html
	[Script(IsNative = true)]
	public interface ComponentListener : EventListener
	{
		/// <summary>
		/// Invoked when the component has been made invisible.
		/// </summary>
		void componentHidden(ComponentEvent @e);

		/// <summary>
		/// Invoked when the component's position changes.
		/// </summary>
		void componentMoved(ComponentEvent @e);

		/// <summary>
		/// Invoked when the component's size changes.
		/// </summary>
		void componentResized(ComponentEvent @e);

		/// <summary>
		/// Invoked when the component has been made visible.
		/// </summary>
		void componentShown(ComponentEvent @e);

	}
}

