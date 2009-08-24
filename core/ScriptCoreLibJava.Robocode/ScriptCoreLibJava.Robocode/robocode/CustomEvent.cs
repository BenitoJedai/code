// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode;

namespace robocode
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/CustomEvent.html
	[Script(IsNative = true)]
	public class CustomEvent
	{
		/// <summary>
		/// Called by the game to create a new CustomEvent when a condition is met.
		/// </summary>
		public CustomEvent(Condition @condition)
		{
		}

		/// <summary>
		/// Called by the game to create a new CustomEvent when a condition is met.
		/// </summary>
		public CustomEvent(Condition @condition, int @priority)
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
		/// Returns the condition that fired, causing this event to be generated.
		/// </summary>
		public Condition getCondition()
		{
			return default(Condition);
		}

		/// <summary>
		/// Returns the priority of this event.
		/// </summary>
		public int getPriority()
		{
			return default(int);
		}

	}
}
