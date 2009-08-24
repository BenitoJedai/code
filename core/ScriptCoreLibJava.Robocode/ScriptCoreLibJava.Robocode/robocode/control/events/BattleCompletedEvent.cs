// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode;

namespace robocode.control.events
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/control/events/BattleCompletedEvent.html
	[Script(IsNative = true)]
	public class BattleCompletedEvent
	{
		/// <summary>
		/// Creates a new BattleCompletedEvent.
		/// </summary>
		public BattleCompletedEvent(BattleRules @battleRules, BattleResults[] results)
		{
		}

		/// <summary>
		/// Returns the rules that was used in the battle.
		/// </summary>
		public BattleRules getBattleRules()
		{
			return default(BattleRules);
		}

		/// <summary>
		/// Returns the unsorted battle results so that robot indexes can be used.
		/// </summary>
		public BattleResults getIndexedResults()
		{
			return default(BattleResults);
		}

		/// <summary>
		/// Returns the battle results sorted on score, meaning that robot indexes cannot be used.
		/// </summary>
		public BattleResults getSortedResults()
		{
			return default(BattleResults);
		}

	}
}
