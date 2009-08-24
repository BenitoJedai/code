// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode.robotinterfaces;

namespace robocode.robotinterfaces
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/robotinterfaces/IInteractiveRobot.html
	[Script(IsNative = true)]
	public interface IInteractiveRobot
	{
		/// <summary>
		/// This method is called by the game to notify this robot about interactive
		/// events, i.e. keyboard and mouse events.
		/// </summary>
		IInteractiveEvents getInteractiveEventListener();

	}
}
