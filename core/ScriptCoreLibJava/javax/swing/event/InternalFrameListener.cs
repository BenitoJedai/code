// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.event.InternalFrameListener

using ScriptCoreLib;
using java.util;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/InternalFrameListener.html
	[Script(IsNative = true)]
	public interface InternalFrameListener : EventListener
	{
		/// <summary>
		/// Invoked when an internal frame is activated.
		/// </summary>
		void internalFrameActivated(InternalFrameEvent @e);

		/// <summary>
		/// Invoked when an internal frame has been closed.
		/// </summary>
		void internalFrameClosed(InternalFrameEvent @e);

		/// <summary>
		/// Invoked when an internal frame is in the process of being closed.
		/// </summary>
		void internalFrameClosing(InternalFrameEvent @e);

		/// <summary>
		/// Invoked when an internal frame is de-activated.
		/// </summary>
		void internalFrameDeactivated(InternalFrameEvent @e);

		/// <summary>
		/// Invoked when an internal frame is de-iconified.
		/// </summary>
		void internalFrameDeiconified(InternalFrameEvent @e);

		/// <summary>
		/// Invoked when an internal frame is iconified.
		/// </summary>
		void internalFrameIconified(InternalFrameEvent @e);

		/// <summary>
		/// Invoked when a internal frame has been opened.
		/// </summary>
		void internalFrameOpened(InternalFrameEvent @e);

	}
}
