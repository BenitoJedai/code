// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode.robotinterfaces;

namespace robocode.robotinterfaces
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/robotinterfaces/IAdvancedRobot.html
	[Script(IsNative = true)]
	public interface IAdvancedRobot
	{
		/// <summary>
		/// This method is called by the game to notify this robot about advanced
		/// robot event.
		/// </summary>
		IAdvancedEvents getAdvancedEventListener();

	}
}
