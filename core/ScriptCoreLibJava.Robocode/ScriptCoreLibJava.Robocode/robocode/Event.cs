// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode;

namespace robocode
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/Event.html
	[Script(IsNative = true)]
	public class Event
	{
		/// <summary>
		/// Called by the game to create a new Event.
		/// </summary>
		public Event()
		{
		}

		/// <summary>
		/// Compares this event to another event regarding precedence.
		/// </summary>
		public int compareTo(Event @event)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the priority of this event.
		/// </summary>
		public int getPriority()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the time this event occurred.
		/// </summary>
		public long getTime()
		{
			return default(long);
		}

		/// <summary>
		/// Called by the game to set the priority of an event to the priority your
		/// robot specified for this type of event (or the default priority).
		/// </summary>
		public void setPriority(int @newPriority)
		{
		}

		/// <summary>
		/// Could be caled by robot to assign the time to events which are not managed by game.
		/// </summary>
		public void setTime(long @newTime)
		{
		}

	}
}
