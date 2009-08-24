// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using robocode.control.snapshot;

namespace robocode.control.snapshot
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/control/snapshot/IRobotSnapshot.html
	[Script(IsNative = true)]
	public interface IRobotSnapshot
	{
		/// <summary>
		/// Returns the color of the body.
		/// </summary>
		int getBodyColor();

		/// <summary>
		/// Returns the body heading in radians.
		/// </summary>
		double getBodyHeading();

		/// <summary>
		/// Returns the contestant index, which will not be changed during a battle.
		/// </summary>
		int getContestantIndex();

		/// <summary>
		/// Returns snapshot of debug properties.
		/// </summary>
		IDebugProperty getDebugProperties();

		/// <summary>
		/// Returns the energy level of the robot.
		/// </summary>
		double getEnergy();

		/// <summary>
		/// Returns the color of the gun.
		/// </summary>
		int getGunColor();

		/// <summary>
		/// Returns the gun heading in radians.
		/// </summary>
		double getGunHeading();

		/// <summary>
		/// Returns the gun heat of the robot.
		/// </summary>
		double getGunHeat();

		/// <summary>
		/// Returns the name of the robot.
		/// </summary>
		string getName();

		/// <summary>
		/// Returns snapshot of the output print stream for this robot.
		/// </summary>
		string getOutputStreamSnapshot();

		/// <summary>
		/// Returns the color of the radar.
		/// </summary>
		int getRadarColor();

		/// <summary>
		/// Returns the radar heading in radians.
		/// </summary>
		double getRadarHeading();

		/// <summary>
		/// Returns the color of the scan arc.
		/// </summary>
		int getScanColor();

		/// <summary>
		/// Returns snapshot of the current score for this robot.
		/// </summary>
		IScoreSnapshot getScoreSnapshot();

		/// <summary>
		/// Returns the short name of the robot.
		/// </summary>
		string getShortName();

		/// <summary>
		/// Returns the robot state.
		/// </summary>
		RobotState getState();

		/// <summary>
		/// Returns the name of the team, which can be the name of a robot if the contestant is not a team, but a robot.
		/// </summary>
		string getTeamName();

		/// <summary>
		/// Returns the velocity of the robot.
		/// </summary>
		double getVelocity();

		/// <summary>
		/// Returns the very short name of the robot.
		/// </summary>
		string getVeryShortName();

		/// <summary>
		/// Returns the X position of the robot.
		/// </summary>
		double getX();

		/// <summary>
		/// Returns the Y position of the robot.
		/// </summary>
		double getY();

		/// <summary>
		/// Checks if this robot is a <A HREF="../../../robocode/Droid.html" title="interface in robocode"><CODE>Droid</CODE></A>.
		/// </summary>
		bool isDroid();

		/// <summary>
		/// Checks if painting is enabled for this robot.
		/// </summary>
		bool isPaintEnabled();

		/// <summary>
		/// Checks if this robot is a <A HREF="../../../robocode/robotinterfaces/IPaintRobot.html" title="interface in robocode.robotinterfaces"><CODE>IPaintRobot</CODE></A> or is invoking getGraphics()
		/// </summary>
		bool isPaintRobot();

		/// <summary>
		/// Checks if RobocodeSG painting is enabled for this robot.
		/// </summary>
		bool isSGPaintEnabled();

	}
}
