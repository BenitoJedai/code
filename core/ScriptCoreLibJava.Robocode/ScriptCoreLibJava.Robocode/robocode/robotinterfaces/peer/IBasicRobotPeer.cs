// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.lang;
using robocode;

namespace robocode.robotinterfaces.peer
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/robotinterfaces/peer/IBasicRobotPeer.html
	[Script(IsNative = true)]
	public interface IBasicRobotPeer
	{
		/// <summary>
		/// Executes any pending actions, or continues executing actions that are
		/// in process.
		/// </summary>
		void execute();

		/// <summary>
		/// Immediately fires a bullet.
		/// </summary>
		Bullet fire(double @power);

		/// <summary>
		/// Returns the height of the current battlefield measured in pixels.
		/// </summary>
		double getBattleFieldHeight();

		/// <summary>
		/// Returns the width of the current battlefield measured in pixels.
		/// </summary>
		double getBattleFieldWidth();

		/// <summary>
		/// Returns the direction that the robot's body is facing, in radians.
		/// </summary>
		double getBodyHeading();

		/// <summary>
		/// Returns the angle remaining in the robot's turn, in radians.
		/// </summary>
		double getBodyTurnRemaining();

		/// <summary>
		/// This call <em>must</em> be made from a robot call to inform the game
		/// that the robot made a <code>get*</code> call like e.g.
		/// </summary>
		void getCall();

		/// <summary>
		/// Returns the distance remaining in the robot's current move measured in
		/// pixels.
		/// </summary>
		double getDistanceRemaining();

		/// <summary>
		/// Returns the robot's current energy.
		/// </summary>
		double getEnergy();

		/// <summary>
		/// Returns a graphics context used for painting graphical items for the robot.
		/// </summary>
		Graphics2D getGraphics();

		/// <summary>
		/// Returns the rate at which the gun will cool down, i.e. the amount of heat
		/// the gun heat will drop per turn.
		/// </summary>
		double getGunCoolingRate();

		/// <summary>
		/// Returns the direction that the robot's gun is facing, in radians.
		/// </summary>
		double getGunHeading();

		/// <summary>
		/// Returns the current heat of the gun.
		/// </summary>
		double getGunHeat();

		/// <summary>
		/// Returns the angle remaining in the gun's turn, in radians.
		/// </summary>
		double getGunTurnRemaining();

		/// <summary>
		/// Returns the robot's name.
		/// </summary>
		string getName();

		/// <summary>
		/// Returns the number of rounds in the current battle.
		/// </summary>
		int getNumRounds();

		/// <summary>
		/// Returns how many opponents that are left in the current round.
		/// </summary>
		int getOthers();

		/// <summary>
		/// Returns the direction that the robot's radar is facing, in radians.
		/// </summary>
		double getRadarHeading();

		/// <summary>
		/// Returns the angle remaining in the radar's turn, in radians.
		/// </summary>
		double getRadarTurnRemaining();

		/// <summary>
		/// Returns the number of the current round (0 to <A HREF="../../../robocode/robotinterfaces/peer/IBasicRobotPeer.html#getNumRounds()"><CODE>IBasicRobotPeer.getNumRounds()</CODE></A> - 1)
		/// in the battle.
		/// </summary>
		int getRoundNum();

		/// <summary>
		/// Returns the game time of the current round, where the time is equal to
		/// the current turn in the round.
		/// </summary>
		long getTime();

		/// <summary>
		/// Returns the velocity of the robot measured in pixels/turn.
		/// </summary>
		double getVelocity();

		/// <summary>
		/// Returns the X position of the robot. (0,0) is at the bottom left of the
		/// battlefield.
		/// </summary>
		double getX();

		/// <summary>
		/// Returns the Y position of the robot. (0,0) is at the bottom left of the
		/// battlefield.
		/// </summary>
		double getY();

		/// <summary>
		/// Immediately moves your robot forward or backward by distance measured in
		/// pixels.
		/// </summary>
		void move(double @distance);

		/// <summary>
		/// Rescan for other robots.
		/// </summary>
		void rescan();

		/// <summary>
		/// Sets the color of the robot's body.
		/// </summary>
		void setBodyColor(Color @color);

		/// <summary>
		/// Sets the color of the robot's bullets.
		/// </summary>
		void setBulletColor(Color @color);

		/// <summary>
		/// This call <em>must</em> be made from a robot call to inform the game
		/// that the robot made a <code>set*</code> call like e.g.
		/// </summary>
		void setCall();

		/// <summary>
		/// Sets the debug property with the specified key to the specified value.
		/// </summary>
		void setDebugProperty(string @key, string @value);

		/// <summary>
		/// Sets the gun to fire a bullet when the next execution takes place.
		/// </summary>
		Bullet setFire(double @power);

		/// <summary>
		/// Sets the color of the robot's gun.
		/// </summary>
		void setGunColor(Color @color);

		/// <summary>
		/// Sets the color of the robot's radar.
		/// </summary>
		void setRadarColor(Color @color);

		/// <summary>
		/// Sets the color of the robot's scan arc.
		/// </summary>
		void setScanColor(Color @color);

		/// <summary>
		/// Immediately turns the robot's body to the right or left by radians.
		/// </summary>
		void turnBody(double @radians);

		/// <summary>
		/// Immediately turns the robot's gun to the right or left by radians.
		/// </summary>
		void turnGun(double @radians);

	}
}
