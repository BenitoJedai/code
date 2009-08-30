// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.lang;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/HierarchyEvent.html
	[Script(IsNative = true)]
	public class HierarchyEvent : AWTEvent
	{
		/// <summary>
		/// Constructs an <code>HierarchyEvent</code> object to identify a
		/// change in the <code>Component</code> hierarchy.
		/// </summary>
		public HierarchyEvent(Component @source, int @id, Component @changed, Container @changedParent)
		{
		}

		/// <summary>
		/// Constructs an <code>HierarchyEvent</code> object to identify
		/// a change in the <code>Component</code> hierarchy.
		/// </summary>
		public HierarchyEvent(Component @source, int @id, Component @changed, Container @changedParent, long @changeFlags)
		{
		}

		/// <summary>
		/// Returns the Component at the top of the hierarchy which was
		/// changed.
		/// </summary>
		public Component getChanged()
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the parent of the Component returned by <code>
		/// getChanged()</code>.
		/// </summary>
		public Container getChangedParent()
		{
			return default(Container);
		}

		/// <summary>
		/// Returns a bitmask which indicates the type(s) of
		/// HIERARCHY_CHANGED events represented in this event object.
		/// </summary>
		public long getChangeFlags()
		{
			return default(long);
		}

		/// <summary>
		/// Returns the originator of the event.
		/// </summary>
		public Component getComponent()
		{
			return default(Component);
		}

		/// <summary>
		/// Returns a parameter string identifying this event.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

	}
}

