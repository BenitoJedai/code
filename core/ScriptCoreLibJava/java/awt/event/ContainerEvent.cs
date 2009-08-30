// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/ContainerEvent.html
	[Script(IsNative = true)]
	public class ContainerEvent : ComponentEvent
	{
		/// <summary>
		/// Constructs a <code>ContainerEvent</code> object.
		/// </summary>
		public ContainerEvent(Component @source, int @id, Component @child) : base(source, id)
		{
		}

		/// <summary>
		/// Returns the component that was affected by the event.
		/// </summary>
		public Component getChild()
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the originator of the event.
		/// </summary>
		public Container getContainer()
		{
			return default(Container);
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

