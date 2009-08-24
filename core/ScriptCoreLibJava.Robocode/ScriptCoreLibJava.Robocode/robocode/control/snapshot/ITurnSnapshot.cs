// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode.control.snapshot;

namespace robocode.control.snapshot
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/control/snapshot/ITurnSnapshot.html
	[Script(IsNative = true)]
	public interface ITurnSnapshot
	{
		/// <summary>
		/// Returns a list of snapshots for the bullets that are currently on the battlefield.
		/// </summary>
		IBulletSnapshot getBullets();

		/// <summary>
		/// Returns a list of indexed scores grouped by teams, i.e. unordered.
		/// </summary>
		IScoreSnapshot getIndexedTeamScores();

		/// <summary>
		/// Returns a list of snapshots for the robots participating in the battle.
		/// </summary>
		IRobotSnapshot getRobots();

		/// <summary>
		/// Returns the current round of the battle.
		/// </summary>
		int getRound();

		/// <summary>
		/// Returns a list of sorted scores grouped by teams, ordered by position.
		/// </summary>
		IScoreSnapshot getSortedTeamScores();

		/// <summary>
		/// Returns the current TPS (turns per second).
		/// </summary>
		int getTPS();

		/// <summary>
		/// Returns the current turn in the battle round.
		/// </summary>
		int getTurn();

	}
}
