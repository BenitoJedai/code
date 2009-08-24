// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode;

namespace robocode.robotinterfaces
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/robotinterfaces/IBasicEvents.html
	[Script(IsNative = true)]
	public interface IBasicEvents
	{
		/// <summary>
		/// This method is called when one of your bullets hits another robot.
		/// </summary>
		void onBulletHit(BulletHitEvent @event);

		/// <summary>
		/// This method is called when one of your bullets hits another bullet.
		/// </summary>
		void onBulletHitBullet(BulletHitBulletEvent @event);

		/// <summary>
		/// This method is called when one of your bullets misses, i.e. hits a wall.
		/// </summary>
		void onBulletMissed(BulletMissedEvent @event);

		/// <summary>
		/// This method is called if your robot dies.
		/// </summary>
		void onDeath(DeathEvent @event);

		/// <summary>
		/// This method is called when your robot is hit by a bullet.
		/// </summary>
		void onHitByBullet(HitByBulletEvent @event);

		/// <summary>
		/// This method is called when your robot collides with another robot.
		/// </summary>
		void onHitRobot(HitRobotEvent @event);

		/// <summary>
		/// This method is called when your robot collides with a wall.
		/// </summary>
		void onHitWall(HitWallEvent @event);

		/// <summary>
		/// This method is called when another robot dies.
		/// </summary>
		void onRobotDeath(RobotDeathEvent @event);

		/// <summary>
		/// This method is called when your robot sees another robot, i.e. when the
		/// robot's radar scan "hits" another robot.
		/// </summary>
		void onScannedRobot(ScannedRobotEvent @event);

		/// <summary>
		/// This method is called every turn in a battle round in order to provide
		/// the robot status as a complete snapshot of the robot's current state at
		/// that specific time.
		/// </summary>
		void onStatus(StatusEvent @event);

		/// <summary>
		/// This method is called if your robot wins a battle.
		/// </summary>
		void onWin(WinEvent @event);

	}
}
