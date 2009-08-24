// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode.control.snapshot;

namespace robocode.control.events
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/control/events/RoundStartedEvent.html
	[Script(IsNative = true)]
	public class RoundStartedEvent
	{
		/// <summary>
		/// Creates a new RoundStartedEvent.
		/// </summary>
		public RoundStartedEvent(ITurnSnapshot @startSnapshot, int @round)
		{
		}

		/// <summary>
		/// Returns the round number.
		/// </summary>
		public int getRound()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the start snapshot of the participating robots, initial starting positions etc.
		/// </summary>
		public ITurnSnapshot getStartSnapshot()
		{
			return default(ITurnSnapshot);
		}

	}
}
