// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode;

namespace robocode.robotinterfaces
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/robotinterfaces/ITeamEvents.html
	[Script(IsNative = true)]
	public interface ITeamEvents
	{
		/// <summary>
		/// This method is called when your robot receives a message from a teammate.
		/// </summary>
		void onMessageReceived(MessageEvent @event);

	}
}
