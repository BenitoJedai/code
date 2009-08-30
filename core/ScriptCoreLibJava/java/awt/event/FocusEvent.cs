// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/FocusEvent.html
	[Script(IsNative = true)]
	public class FocusEvent : ComponentEvent
	{
		/// <summary>
		/// Constructs a <code>FocusEvent</code> object and identifies it
		/// as a permanent change in focus.
		/// </summary>
		public FocusEvent(Component @source, int @id)
			: base(source, id)
		{
		}

		/// <summary>
		/// Constructs a <code>FocusEvent</code> object and identifies
		/// whether or not the change is temporary.
		/// </summary>
		public FocusEvent(Component @source, int @id, bool @temporary)
			: base(source, id)
		{
		}

		/// <summary>
		/// Constructs a <code>FocusEvent</code> object with the
		/// specified temporary state and opposite <code>Component</code>.
		/// </summary>
		public FocusEvent(Component @source, int @id, bool @temporary, Component @opposite)
			: base(source, id)
		{
		}

		/// <summary>
		/// Returns the other Component involved in this focus change.
		/// </summary>
		public Component getOppositeComponent()
		{
			return default(Component);
		}

		/// <summary>
		/// Identifies the focus change event as temporary or permanent.
		/// </summary>
		public bool isTemporary()
		{
			return default(bool);
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

