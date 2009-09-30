// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/java.awt.event.AdjustmentEvent

using ScriptCoreLib;
using java.awt;
using java.lang;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/AdjustmentEvent.html
	[Script(IsNative = true)]
	public class AdjustmentEvent : AWTEvent
	{
		/// <summary>
		/// Constructs an <code>AdjustmentEvent</code> object with the
		/// specified <code>Adjustable</code> source, event type,
		/// adjustment type, and value.
		/// </summary>
		public AdjustmentEvent(Adjustable @source, int @id, int @type, int @value)
		{
		}

		/// <summary>
		/// Constructs an <code>AdjustmentEvent</code> object with the
		/// specified Adjustable source, event type, adjustment type, and value.
		/// </summary>
		public AdjustmentEvent(Adjustable @source, int @id, int @type, int @value, bool @isAdjusting)
		{
		}

		/// <summary>
		/// Returns the <code>Adjustable</code> object where this event originated.
		/// </summary>
		public Adjustable getAdjustable()
		{
			return default(Adjustable);
		}

		/// <summary>
		/// Returns the type of adjustment which caused the value changed
		/// event.
		/// </summary>
		public int getAdjustmentType()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the current value in the adjustment event.
		/// </summary>
		public int getValue()
		{
			return default(int);
		}

		/// <summary>
		/// Returns <code>true</code> if this is one of multiple
		/// adjustment events.
		/// </summary>
		public bool getValueIsAdjusting()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string representing the state of this <code>Event</code>.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

	}
}
