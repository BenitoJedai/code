// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.@event;
using java.util;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/ContainerListener.html
	[Script(IsNative = true)]
	public interface ContainerListener : EventListener
	{
		/// <summary>
		/// Invoked when a component has been added to the container.
		/// </summary>
		void componentAdded(ContainerEvent @e);

		/// <summary>
		/// Invoked when a component has been removed from the container.
		/// </summary>
		void componentRemoved(ContainerEvent @e);

	}
}

