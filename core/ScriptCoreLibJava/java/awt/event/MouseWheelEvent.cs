// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/MouseWheelEvent.html
	[Script(IsNative = true)]
	public class MouseWheelEvent : MouseEvent
	{
		/// <summary>
		/// Constructs a <code>MouseWheelEvent</code> object with the
		/// specified source component, type, modifiers, coordinates,
		/// scroll type, scroll amount, and wheel rotation.
		/// </summary>
		public MouseWheelEvent(Component @source, int @id, long @when, int @modifiers, int @x, int @y, int @clickCount, bool @popupTrigger, int @scrollType, int @scrollAmount, int @wheelRotation) : base(source, id,when, modifiers, x, y, clickCount, popupTrigger)
		{
		}

		/// <summary>
		/// Returns the number of units that should be scrolled in response to this
		/// event.
		/// </summary>
		public int getScrollAmount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the type of scrolling that should take place in response to this
		/// event.
		/// </summary>
		public int getScrollType()
		{
			return default(int);
		}

		/// <summary>
		/// This is a convenience method to aid in the implementation of
		/// the common-case MouseWheelListener - to scroll a ScrollPane or
		/// JScrollPane by an amount which conforms to the platform settings.
		/// </summary>
		public int getUnitsToScroll()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the number of "clicks" the mouse wheel was rotated.
		/// </summary>
		public int getWheelRotation()
		{
			return default(int);
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

