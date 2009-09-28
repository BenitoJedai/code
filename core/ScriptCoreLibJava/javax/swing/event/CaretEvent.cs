// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.event.CaretEvent

using ScriptCoreLib;
using java.lang;
using java.util;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/CaretEvent.html
	[Script(IsNative = true)]
	public abstract class CaretEvent : EventObject
	{
		/// <summary>
		/// Creates a new CaretEvent object.
		/// </summary>
		public CaretEvent(object @source)
		{
		}

		/// <summary>
		/// Fetches the location of the caret.
		/// </summary>
		abstract public int getDot();

		/// <summary>
		/// Fetches the location of other end of a logical
		/// selection.
		/// </summary>
		abstract public int getMark();

	}
}
