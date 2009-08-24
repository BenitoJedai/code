// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using robocode.control;
using robocode.control.events;

namespace robocode.control
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/control/IRobocodeEngine.html
	[Script(IsNative = true)]
	public interface IRobocodeEngine
	{
		/// <summary>
		/// Aborts the current battle if it is running.
		/// </summary>
		void abortCurrentBattle();

		/// <summary>
		/// Adds a battle listener that must receive events occurring in battles.
		/// </summary>
		void addBattleListener(IBattleListener @listener);

		/// <summary>
		/// Closes the RobocodeEngine and releases any allocated resources.
		/// </summary>
		void close();

		/// <summary>
		/// Returns all robots available from the local robot repository of Robocode.
		/// </summary>
		RobotSpecification getLocalRepository();

		/// <summary>
		/// Returns a selection of robots available from the local robot repository
		/// of Robocode.
		/// </summary>
		RobotSpecification getLocalRepository(string @selectedRobotList);

		/// <summary>
		/// Returns the installed version of Robocode.
		/// </summary>
		string getVersion();

		/// <summary>
		/// Removes a battle listener that has previously been added to this object.
		/// </summary>
		void removeBattleListener(IBattleListener @listener);

		/// <summary>
		/// Runs the specified battle.
		/// </summary>
		void runBattle(BattleSpecification @battleSpecification);

		/// <summary>
		/// Runs the specified battle.
		/// </summary>
		void runBattle(BattleSpecification @battleSpecification, bool @waitTillOver);

		/// <summary>
		/// Runs the specified battle.
		/// </summary>
		void runBattle(BattleSpecification @battleSpecification, string @initialPositions, bool @waitTillOver);

		/// <summary>
		/// Shows or hides the Robocode window.
		/// </summary>
		void setVisible(bool @visible);

		/// <summary>
		/// Will block caller until current battle is over
		/// </summary>
		void waitTillBattleOver();

	}
}
