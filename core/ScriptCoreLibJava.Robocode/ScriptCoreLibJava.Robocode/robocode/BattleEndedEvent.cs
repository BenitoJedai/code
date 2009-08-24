// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode;

namespace robocode
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/BattleEndedEvent.html
	[Script(IsNative = true)]
	public class BattleEndedEvent
	{
		/// <summary>
		/// Called by the game to create a new BattleEndedEvent.
		/// </summary>
		public BattleEndedEvent(bool @aborted, BattleResults @results)
		{
		}

		/// <summary>
		/// Returns the priority of this event.
		/// </summary>
		public int getPriority()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the battle results.
		/// </summary>
		public BattleResults getResults()
		{
			return default(BattleResults);
		}

		/// <summary>
		/// Checks if this battle was aborted.
		/// </summary>
		public bool isAborted()
		{
			return default(bool);
		}

	}
}
