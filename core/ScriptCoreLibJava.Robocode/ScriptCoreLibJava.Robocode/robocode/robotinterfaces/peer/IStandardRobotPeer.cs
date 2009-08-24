// This source code was generated for ScriptCoreLib
using ScriptCoreLib;

namespace robocode.robotinterfaces.peer
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/robotinterfaces/peer/IStandardRobotPeer.html
	[Script(IsNative = true)]
	public interface IStandardRobotPeer
	{
		/// <summary>
		/// Immediately resumes the movement you stopped by <A HREF="../../../robocode/robotinterfaces/peer/IStandardRobotPeer.html#stop(boolean)"><CODE>IStandardRobotPeer.stop(boolean)</CODE></A>,
		/// if any.
		/// </summary>
		void resume();

		/// <summary>
		/// Sets the gun to turn independent from the robot's turn.
		/// </summary>
		void setAdjustGunForBodyTurn(bool @independent);

		/// <summary>
		/// Sets the radar to turn independent from the robot's turn.
		/// </summary>
		void setAdjustRadarForBodyTurn(bool @independent);

		/// <summary>
		/// Sets the radar to turn independent from the gun's turn.
		/// </summary>
		void setAdjustRadarForGunTurn(bool @independent);

		/// <summary>
		/// Immediately stops all movement, and saves it for a call to
		/// <A HREF="../../../robocode/robotinterfaces/peer/IStandardRobotPeer.html#resume()"><CODE>IStandardRobotPeer.resume()</CODE></A>.
		/// </summary>
		void stop(bool @overwrite);

		/// <summary>
		/// Immediately turns the robot's radar to the right or left by radians.
		/// </summary>
		void turnRadar(double @radians);

	}
}
