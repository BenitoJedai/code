// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using javax.swing;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/AncestorEvent.html
	[Script(IsNative = true)]
	public class AncestorEvent
	{
		/// <summary>
		/// Constructs an AncestorEvent object to identify a change
		/// in an ancestor-component's display-status.
		/// </summary>
		public AncestorEvent(JComponent @source, int @id, Container @ancestor, Container @ancestorParent)
		{
		}

		/// <summary>
		/// Returns the ancestor that the event actually occurred on.
		/// </summary>
		public Container getAncestor()
		{
			return default(Container);
		}

		/// <summary>
		/// Returns the parent of the ancestor the event actually occurred on.
		/// </summary>
		public Container getAncestorParent()
		{
			return default(Container);
		}

		/// <summary>
		/// Returns the component that the listener was added to.
		/// </summary>
		public JComponent getComponent()
		{
			return default(JComponent);
		}

	}
}
