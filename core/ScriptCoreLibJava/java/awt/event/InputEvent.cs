// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.@event;
using java.lang;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/InputEvent.html
	[Script(IsNative = true)]
	public class InputEvent : ComponentEvent
	{
		public InputEvent() :  base(null, 0)
		{

		}
		/// <summary>
		/// Consumes this event so that it will not be processed
		/// in the default manner by the source which originated it.
		/// </summary>
		public void consume()
		{
		}

		/// <summary>
		/// Returns the modifier mask for this event.
		/// </summary>
		public int getModifiers()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the extended modifier mask for this event.
		/// </summary>
		public int getModifiersEx()
		{
			return default(int);
		}

		/// <summary>
		/// Returns a String describing the extended modifier keys and
		/// mouse buttons, such as "Shift", "Button1", or "Ctrl+Shift".
		/// </summary>
		public string getModifiersExText(int @modifiers)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the timestamp of when this event occurred.
		/// </summary>
		public long getWhen()
		{
			return default(long);
		}

		/// <summary>
		/// Returns whether or not the Alt modifier is down on this event.
		/// </summary>
		public bool isAltDown()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether or not the AltGraph modifier is down on this event.
		/// </summary>
		public bool isAltGraphDown()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether or not this event has been consumed.
		/// </summary>
		public bool isConsumed()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether or not the Control modifier is down on this event.
		/// </summary>
		public bool isControlDown()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether or not the Meta modifier is down on this event.
		/// </summary>
		public bool isMetaDown()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether or not the Shift modifier is down on this event.
		/// </summary>
		public bool isShiftDown()
		{
			return default(bool);
		}

	}
}

