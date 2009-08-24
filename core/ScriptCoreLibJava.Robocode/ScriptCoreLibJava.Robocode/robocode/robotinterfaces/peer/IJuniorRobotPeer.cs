// This source code was generated for ScriptCoreLib
using ScriptCoreLib;

namespace robocode.robotinterfaces.peer
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/robotinterfaces/peer/IJuniorRobotPeer.html
	[Script(IsNative = true)]
	public interface IJuniorRobotPeer
	{
		/// <summary>
		/// Moves this robot forward or backwards by pixels and turns this robot
		/// right or left by degrees at the same time.
		/// </summary>
		void turnAndMove(double @distance, double @radians);

	}
}
