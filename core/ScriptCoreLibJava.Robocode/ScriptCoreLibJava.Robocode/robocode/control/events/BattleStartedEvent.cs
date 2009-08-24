// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode;

namespace robocode.control.events
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/control/events/BattleStartedEvent.html
	[Script(IsNative = true)]
	public class BattleStartedEvent
	{
		/// <summary>
		/// Creates a new BattleStartedEvent.
		/// </summary>
		public BattleStartedEvent(BattleRules @battleRules, int @robotsCount, bool @isReplay)
		{
		}

		/// <summary>
		/// Returns the rules that will be used in the battle.
		/// </summary>
		public BattleRules getBattleRules()
		{
			return default(BattleRules);
		}

		/// <summary>
		/// Returns the number of robots participating in the battle.
		/// </summary>
		public int getRobotsCount()
		{
			return default(int);
		}

		/// <summary>
		/// Checks if this battle is a replay or real battle.
		/// </summary>
		public bool isReplay()
		{
			return default(bool);
		}

	}
}
