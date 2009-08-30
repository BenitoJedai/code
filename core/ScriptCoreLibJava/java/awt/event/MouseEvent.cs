// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/MouseEvent.html
	[Script(IsNative = true)]
	public class MouseEvent : InputEvent
	{
		/// <summary>
		/// Constructs a <code>MouseEvent</code> object with the
		/// specified source component,
		/// type, modifiers, coordinates, and click count.
		/// </summary>
		public MouseEvent(Component @source, int @id, long @when, int @modifiers, int @x, int @y, int @clickCount, bool @popupTrigger)
		{
		}

		/// <summary>
		/// Constructs a <code>MouseEvent</code> object with the
		/// specified source component,
		/// type, modifiers, coordinates, and click count.
		/// </summary>
		public MouseEvent(Component @source, int @id, long @when, int @modifiers, int @x, int @y, int @clickCount, bool @popupTrigger, int @button)
		{
		}

		/// <summary>
		/// Returns which, if any, of the mouse buttons has changed state.
		/// </summary>
		public int getButton()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the number of mouse clicks associated with this event.
		/// </summary>
		public int getClickCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns a String describing the modifier keys and mouse buttons
		/// that were down during the event, such as "Shift", or "Ctrl+Shift".
		/// </summary>
		public string getMouseModifiersText(int @modifiers)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the x,y position of the event relative to the source component.
		/// </summary>
		public Point getPoint()
		{
			return default(Point);
		}

		/// <summary>
		/// Returns the horizontal x position of the event relative to the
		/// source component.
		/// </summary>
		public int getX()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the vertical y position of the event relative to the
		/// source component.
		/// </summary>
		public int getY()
		{
			return default(int);
		}

		/// <summary>
		/// Returns whether or not this mouse event is the popup menu
		/// trigger event for the platform.
		/// </summary>
		public bool isPopupTrigger()
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

		/// <summary>
		/// Translates the event's coordinates to a new position
		/// by adding specified <code>x</code> (horizontal) and <code>y</code>
		/// (vertical) offsets.
		/// </summary>
		public void translatePoint(int @x, int @y)
		{
		}

	}
}

