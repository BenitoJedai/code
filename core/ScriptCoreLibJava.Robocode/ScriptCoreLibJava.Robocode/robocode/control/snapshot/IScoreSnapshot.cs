// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;

namespace robocode.control.snapshot
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/control/snapshot/IScoreSnapshot.html
	[Script(IsNative = true)]
	public interface IScoreSnapshot
	{
		/// <summary>
		/// Returns the current bullet damage score.
		/// </summary>
		double getCurrentBulletDamageScore();

		/// <summary>
		/// Returns the current bullet kill bonus.
		/// </summary>
		double getCurrentBulletKillBonus();

		/// <summary>
		/// Returns the current ramming damage score.
		/// </summary>
		double getCurrentRammingDamageScore();

		/// <summary>
		/// Returns the current ramming kill bonus.
		/// </summary>
		double getCurrentRammingKillBonus();

		/// <summary>
		/// Returns the current score.
		/// </summary>
		double getCurrentScore();

		/// <summary>
		/// Returns the current survival bonus.
		/// </summary>
		double getCurrentSurvivalBonus();

		/// <summary>
		/// Returns the current survival score.
		/// </summary>
		double getCurrentSurvivalScore();

		/// <summary>
		/// Returns the name of the contestant, i.e. a robot or team.
		/// </summary>
		string getName();

		/// <summary>
		/// Returns the total bullet damage score.
		/// </summary>
		double getTotalBulletDamageScore();

		/// <summary>
		/// Returns the total bullet kill bonus.
		/// </summary>
		double getTotalBulletKillBonus();

		/// <summary>
		/// Returns the total number of first places.
		/// </summary>
		int getTotalFirsts();

		/// <summary>
		/// Returns the total last survivor score.
		/// </summary>
		double getTotalLastSurvivorBonus();

		/// <summary>
		/// Returns the total ramming damage score.
		/// </summary>
		double getTotalRammingDamageScore();

		/// <summary>
		/// Returns the total ramming kill bonus.
		/// </summary>
		double getTotalRammingKillBonus();

		/// <summary>
		/// Returns the total score.
		/// </summary>
		double getTotalScore();

		/// <summary>
		/// Returns the total number of second places.
		/// </summary>
		int getTotalSeconds();

		/// <summary>
		/// Returns the total survival score.
		/// </summary>
		double getTotalSurvivalScore();

		/// <summary>
		/// Returns the total number of third places.
		/// </summary>
		int getTotalThirds();

	}
}
