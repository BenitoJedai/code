// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode.robotinterfaces;

namespace robocode.robotinterfaces
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/robotinterfaces/IPaintRobot.html
	[Script(IsNative = true)]
	public interface IPaintRobot
	{
		/// <summary>
		/// This method is called by the game to notify this robot about painting
		/// events.
		/// </summary>
		IPaintEvents getPaintEventListener();

	}
}
