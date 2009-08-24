// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode.control.snapshot;

namespace robocode.control.events
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/control/events/TurnEndedEvent.html
	[Script(IsNative = true)]
	public class TurnEndedEvent
	{
		/// <summary>
		/// Creates a new TurnEndedEvent.
		/// </summary>
		public TurnEndedEvent(ITurnSnapshot @turnSnapshot)
		{
		}

		/// <summary>
		/// Returns a snapshot of the turn that has ended.
		/// </summary>
		public ITurnSnapshot getTurnSnapshot()
		{
			return default(ITurnSnapshot);
		}

	}
}
