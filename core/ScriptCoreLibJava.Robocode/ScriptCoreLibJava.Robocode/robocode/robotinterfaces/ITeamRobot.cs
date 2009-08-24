// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode.robotinterfaces;

namespace robocode.robotinterfaces
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/robotinterfaces/ITeamRobot.html
	[Script(IsNative = true)]
	public interface ITeamRobot
	{
		/// <summary>
		/// This method is called by the game to notify this robot about team events.
		/// </summary>
		ITeamEvents getTeamEventListener();

	}
}
