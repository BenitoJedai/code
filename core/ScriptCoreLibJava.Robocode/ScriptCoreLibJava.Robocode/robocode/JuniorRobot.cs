// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using robocode.robotinterfaces;

namespace robocode
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/JuniorRobot.html
	[Script(IsNative = true)]
	public class JuniorRobot
	{
		/// <summary>
		/// 
		/// </summary>
		public JuniorRobot()
		{
		}

		/// <summary>
		/// Moves this robot forward by pixels.
		/// </summary>
		public void ahead(int @distance)
		{
		}

		/// <summary>
		/// Moves this robot backward by pixels.
		/// </summary>
		public void back(int @distance)
		{
		}

		/// <summary>
		/// Turns the gun to the specified angle (in degrees) relative to body of this robot.
		/// </summary>
		public void bearGunTo(int @angle)
		{
		}

		/// <summary>
		/// Skips a turn.
		/// </summary>
		public void doNothing()
		{
		}

		/// <summary>
		/// Skips the specified number of turns.
		/// </summary>
		public void doNothing(int @turns)
		{
		}

		/// <summary>
		/// Fires a bullet with the default power of 1.
		/// </summary>
		public void fire()
		{
		}

		/// <summary>
		/// Fires a bullet with the specified bullet power, which is between 0.1 and 3
		/// where 3 is the maximum bullet power.
		/// </summary>
		public void fire(double @power)
		{
		}

		/// <summary>
		/// Do not call this method!
		/// </summary>
		public IBasicEvents getBasicEventListener()
		{
			return default(IBasicEvents);
		}

		/// <summary>
		/// Do not call this method!
		/// </summary>
		public Runnable getRobotRunnable()
		{
			return default(Runnable);
		}

		/// <summary>
		/// This event methods is called from the game when this robot has been hit
		/// by another robot's bullet.
		/// </summary>
		public void onHitByBullet()
		{
		}

		/// <summary>
		/// This event methods is called from the game when a bullet from this robot
		/// has hit another robot.
		/// </summary>
		public void onHitRobot()
		{
		}

		/// <summary>
		/// This event methods is called from the game when this robot has hit a wall.
		/// </summary>
		public void onHitWall()
		{
		}

		/// <summary>
		/// This event method is called from the game when the radar detects another
		/// robot.
		/// </summary>
		public void onScannedRobot()
		{
		}

		/// <summary>
		/// The main method in every robot.
		/// </summary>
		public void run()
		{
		}

		/// <summary>
		/// Sets the colors of the robot.
		/// </summary>
		public void setColors(int @bodyColor, int @gunColor, int @radarColor)
		{
		}

		/// <summary>
		/// Sets the colors of the robot.
		/// </summary>
		public void setColors(int @bodyColor, int @gunColor, int @radarColor, int @bulletColor, int @scanArcColor)
		{
		}

		/// <summary>
		/// Moves this robot forward by pixels and turns this robot left by degrees
		/// at the same time.
		/// </summary>
		public void turnAheadLeft(int @distance, int @degrees)
		{
		}

		/// <summary>
		/// Moves this robot forward by pixels and turns this robot right by degrees
		/// at the same time.
		/// </summary>
		public void turnAheadRight(int @distance, int @degrees)
		{
		}

		/// <summary>
		/// Moves this robot backward by pixels and turns this robot left by degrees
		/// at the same time.
		/// </summary>
		public void turnBackLeft(int @distance, int @degrees)
		{
		}

		/// <summary>
		/// Moves this robot backward by pixels and turns this robot right by degrees
		/// at the same time.
		/// </summary>
		public void turnBackRight(int @distance, int @degrees)
		{
		}

		/// <summary>
		/// Turns the gun left by degrees.
		/// </summary>
		public void turnGunLeft(int @degrees)
		{
		}

		/// <summary>
		/// Turns the gun right by degrees.
		/// </summary>
		public void turnGunRight(int @degrees)
		{
		}

		/// <summary>
		/// Turns the gun to the specified angle (in degrees).
		/// </summary>
		public void turnGunTo(int @angle)
		{
		}

		/// <summary>
		/// Turns this robot left by degrees.
		/// </summary>
		public void turnLeft(int @degrees)
		{
		}

		/// <summary>
		/// Turns this robot right by degrees.
		/// </summary>
		public void turnRight(int @degrees)
		{
		}

		/// <summary>
		/// Turns this robot to the specified angle (in degrees).
		/// </summary>
		public void turnTo(int @angle)
		{
		}

	}
}
