// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.event.ListSelectionEvent

using ScriptCoreLib;
using java.lang;
using java.util;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/ListSelectionEvent.html
	[Script(IsNative = true)]
	public class ListSelectionEvent : EventObject
	{
		/// <summary>
		/// Represents a change in selection status between <code>firstIndex</code>
		/// and <code>lastIndex</code> inclusive
		/// (</code>firstIndex</code> is less than or equal to
		/// <code>lastIndex</code>).
		/// </summary>
		public ListSelectionEvent(object @source, int @firstIndex, int @lastIndex, bool @isAdjusting)
		{
		}

		/// <summary>
		/// Returns the index of the first row whose selection may have changed.
		/// </summary>
		public int getFirstIndex()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the index of the last row whose selection may have changed.
		/// </summary>
		public int getLastIndex()
		{
			return default(int);
		}

		/// <summary>
		/// Returns true if this is one of multiple change events.
		/// </summary>
		public bool getValueIsAdjusting()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string that displays and identifies this
		/// object's properties.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}
