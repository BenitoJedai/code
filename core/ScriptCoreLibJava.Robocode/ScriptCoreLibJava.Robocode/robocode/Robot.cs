// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;
using robocode;
using robocode.robotinterfaces;

namespace robocode
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/Robot.html
	[Script(IsNative = true)]
	public class Robot : IInteractiveRobot, IPaintRobot, IBasicEvents2, IInteractiveEvents, IPaintEvents
	{
		/// <summary>
		/// Constructs a new robot.
		/// </summary>
		public Robot()
		{
		}

		/// <summary>
		/// Immediately moves your robot ahead (forward) by distance measured in
		/// pixels.
		/// </summary>
		public void ahead(double @distance)
		{
		}

		/// <summary>
		/// Immediately moves your robot backward by distance measured in pixels.
		/// </summary>
		public void back(double @distance)
		{
		}

		/// <summary>
		/// Do nothing this turn, meaning that the robot will skip it's turn.
		/// </summary>
		public void doNothing()
		{
		}

		/// <summary>
		/// Called by the system to 'clean up' after your robot.
		/// </summary>
		protected void finalize()
		{
		}

		/// <summary>
		/// Immediately fires a bullet.
		/// </summary>
		public void fire(double @power)
		{
		}

		/// <summary>
		/// Immediately fires a bullet.
		/// </summary>
		public Bullet fireBullet(double @power)
		{
			return default(Bullet);
		}

		/// <summary>
		/// This method is called by the game to notify this robot about basic
		/// robot event.}
		/// </summary>
		public IBasicEvents getBasicEventListener()
		{
			return default(IBasicEvents);
		}

		/// <summary>
		/// Returns the height of the current battlefield measured in pixels.
		/// </summary>
		public double getBattleFieldHeight()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the width of the current battlefield measured in pixels.
		/// </summary>
		public double getBattleFieldWidth()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the robot's current energy.
		/// </summary>
		public double getEnergy()
		{
			return default(double);
		}

		/// <summary>
		/// Returns a graphics context used for painting graphical items for the robot.
		/// </summary>
		public Graphics2D getGraphics()
		{
			return default(Graphics2D);
		}

		/// <summary>
		/// Returns the rate at which the gun will cool down, i.e. the amount of heat
		/// the gun heat will drop per turn.
		/// </summary>
		public double getGunCoolingRate()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the direction that the robot's gun is facing, in degrees.
		/// </summary>
		public double getGunHeading()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the current heat of the gun.
		/// </summary>
		public double getGunHeat()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the direction that the robot's body is facing, in degrees.
		/// </summary>
		public double getHeading()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the height of the robot measured in pixels.
		/// </summary>
		public double getHeight()
		{
			return default(double);
		}

		/// <summary>
		/// This method is called by the game to notify this robot about interactive
		/// events, i.e. keyboard and mouse events.}
		/// </summary>
		public IInteractiveEvents getInteractiveEventListener()
		{
			return default(IInteractiveEvents);
		}

		/// <summary>
		/// Returns the robot's name.
		/// </summary>
		public string getName()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the number of rounds in the current battle.
		/// </summary>
		public int getNumRounds()
		{
			return default(int);
		}

		/// <summary>
		/// Returns how many opponents that are left in the current round.
		/// </summary>
		public int getOthers()
		{
			return default(int);
		}

		/// <summary>
		/// This method is called by the game to notify this robot about painting
		/// events.}
		/// </summary>
		public IPaintEvents getPaintEventListener()
		{
			return default(IPaintEvents);
		}

		/// <summary>
		/// Returns the direction that the robot's radar is facing, in degrees.
		/// </summary>
		public double getRadarHeading()
		{
			return default(double);
		}

		/// <summary>
		/// This method is called by the game to invoke the
		/// <A HREF="http://java.sun.com/j2se/1.5.0/docs/api/java/lang/Runnable.html#run()" title="class or interface in java.lang"><CODE>run()</CODE></A> method of your robot, where the program
		/// of your robot is implemented.}
		/// </summary>
		public Runnable getRobotRunnable()
		{
			return default(Runnable);
		}

		/// <summary>
		/// Returns the current round number (0 to <A HREF="../robocode/Robot.html#getNumRounds()"><CODE>Robot.getNumRounds()</CODE></A> - 1) of
		/// the battle.
		/// </summary>
		public int getRoundNum()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the game time of the current round, where the time is equal to
		/// the current turn in the round.
		/// </summary>
		public long getTime()
		{
			return default(long);
		}

		/// <summary>
		/// Returns the velocity of the robot measured in pixels/turn.
		/// </summary>
		public double getVelocity()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the width of the robot measured in pixels.
		/// </summary>
		public double getWidth()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the X position of the robot. (0,0) is at the bottom left of the
		/// battlefield.
		/// </summary>
		public double getX()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the Y position of the robot. (0,0) is at the bottom left of the
		/// battlefield.
		/// </summary>
		public double getY()
		{
			return default(double);
		}

		/// <summary>
		/// This method is called after end of the battle, even when the battle is aborted.
		/// </summary>
		public void onBattleEnded(BattleEndedEvent @event)
		{
		}

		/// <summary>
		/// This method is called when one of your bullets hits another robot.
		/// </summary>
		public void onBulletHit(BulletHitEvent @event)
		{
		}

		/// <summary>
		/// This method is called when one of your bullets hits another bullet.
		/// </summary>
		public void onBulletHitBullet(BulletHitBulletEvent @event)
		{
		}

		/// <summary>
		/// This method is called when one of your bullets misses, i.e. hits a wall.
		/// </summary>
		public void onBulletMissed(BulletMissedEvent @event)
		{
		}

		/// <summary>
		/// This method is called if your robot dies.
		/// </summary>
		public void onDeath(DeathEvent @event)
		{
		}

		/// <summary>
		/// This method is called when your robot is hit by a bullet.
		/// </summary>
		public virtual void onHitByBullet(HitByBulletEvent @event)
		{
		}

		/// <summary>
		/// This method is called when your robot collides with another robot.
		/// </summary>
		public virtual void onHitRobot(HitRobotEvent @event)
		{
		}

		/// <summary>
		/// This method is called when your robot collides with a wall.
		/// </summary>
		public virtual void onHitWall(HitWallEvent @event)
		{
		}

		/// <summary>
		/// This method is called when a key has been pressed.
		/// </summary>
		public void onKeyPressed(KeyEvent @e)
		{
		}

		/// <summary>
		/// This method is called when a key has been released.
		/// </summary>
		public void onKeyReleased(KeyEvent @e)
		{
		}

		/// <summary>
		/// This method is called when a key has been typed (pressed and released).
		/// </summary>
		public void onKeyTyped(KeyEvent @e)
		{
		}

		/// <summary>
		/// This method is called when a mouse button has been clicked (pressed and
		/// released).
		/// </summary>
		public void onMouseClicked(MouseEvent @e)
		{
		}

		/// <summary>
		/// This method is called when a mouse button has been pressed and then
		/// dragged.
		/// </summary>
		public void onMouseDragged(MouseEvent @e)
		{
		}

		/// <summary>
		/// This method is called when the mouse has entered the battle view.
		/// </summary>
		public void onMouseEntered(MouseEvent @e)
		{
		}

		/// <summary>
		/// This method is called when the mouse has exited the battle view.
		/// </summary>
		public void onMouseExited(MouseEvent @e)
		{
		}

		/// <summary>
		/// This method is called when the mouse has been moved.
		/// </summary>
		public void onMouseMoved(MouseEvent @e)
		{
		}

		/// <summary>
		/// This method is called when a mouse button has been pressed.
		/// </summary>
		public void onMousePressed(MouseEvent @e)
		{
		}

		/// <summary>
		/// This method is called when a mouse button has been released.
		/// </summary>
		public void onMouseReleased(MouseEvent @e)
		{
		}

		/// <summary>
		/// This method is called when the mouse wheel has been rotated.
		/// </summary>
		public void onMouseWheelMoved(MouseWheelEvent @e)
		{
		}

		/// <summary>
		/// This method is called every time the robot is painted.
		/// </summary>
		public void onPaint(Graphics2D @g)
		{
		}

		/// <summary>
		/// This method is called when another robot dies.
		/// </summary>
		public void onRobotDeath(RobotDeathEvent @event)
		{
		}

		/// <summary>
		/// This method is called when your robot sees another robot, i.e. when the
		/// robot's radar scan "hits" another robot.
		/// </summary>
		public virtual void onScannedRobot(ScannedRobotEvent @event)
		{
		}

		/// <summary>
		/// This method is called every turn in a battle round in order to provide
		/// the robot status as a complete snapshot of the robot's current state at
		/// that specific time.
		/// </summary>
		public void onStatus(StatusEvent @e)
		{
		}

		/// <summary>
		/// This method is called if your robot wins a battle.
		/// </summary>
		public void onWin(WinEvent @event)
		{
		}

		/// <summary>
		/// Immediately resumes the movement you stopped by <A HREF="../robocode/Robot.html#stop()"><CODE>Robot.stop()</CODE></A>, if any.
		/// </summary>
		public void resume()
		{
		}

		/// <summary>
		/// The main method in every robot.
		/// </summary>
		public virtual void run()
		{
		}

		/// <summary>
		/// Scans for other robots.
		/// </summary>
		public void scan()
		{
		}

		/// <summary>
		/// Sets the gun to turn independent from the robot's turn.
		/// </summary>
		public void setAdjustGunForRobotTurn(bool @independent)
		{
		}

		/// <summary>
		/// Sets the radar to turn independent from the gun's turn.
		/// </summary>
		public void setAdjustRadarForGunTurn(bool @independent)
		{
		}

		/// <summary>
		/// Sets the radar to turn independent from the robot's turn.
		/// </summary>
		public void setAdjustRadarForRobotTurn(bool @independent)
		{
		}

		/// <summary>
		/// Sets all the robot's color to the same color in the same time, i.e. the
		/// color of the body, gun, radar, bullet, and scan arc.
		/// </summary>
		public void setAllColors(Color @color)
		{
		}

		/// <summary>
		/// Sets the color of the robot's body.
		/// </summary>
		public void setBodyColor(Color @color)
		{
		}

		/// <summary>
		/// Sets the color of the robot's bullets.
		/// </summary>
		public void setBulletColor(Color @color)
		{
		}

		/// <summary>
		/// Sets the color of the robot's body, gun, and radar in the same time.
		/// </summary>
		public void setColors(Color @bodyColor, Color @gunColor, Color @radarColor)
		{
		}

		/// <summary>
		/// Sets the color of the robot's body, gun, radar, bullet, and scan arc in
		/// the same time.
		/// </summary>
		public void setColors(Color @bodyColor, Color @gunColor, Color @radarColor, Color @bulletColor, Color @scanArcColor)
		{
		}

		/// <summary>
		/// Sets the debug property with the specified key to the specified value.
		/// </summary>
		public void setDebugProperty(string @key, string @value)
		{
		}

		/// <summary>
		/// Sets the color of the robot's gun.
		/// </summary>
		public void setGunColor(Color @color)
		{
		}

		/// <summary>
		/// Sets the color of the robot's radar.
		/// </summary>
		public void setRadarColor(Color @color)
		{
		}

		/// <summary>
		/// Sets the color of the robot's scan arc.
		/// </summary>
		public void setScanColor(Color @color)
		{
		}

		/// <summary>
		/// Immediately stops all movement, and saves it for a call to
		/// <A HREF="../robocode/Robot.html#resume()"><CODE>Robot.resume()</CODE></A>.
		/// </summary>
		public void stop()
		{
		}

		/// <summary>
		/// Immediately stops all movement, and saves it for a call to
		/// <A HREF="../robocode/Robot.html#resume()"><CODE>Robot.resume()</CODE></A>.
		/// </summary>
		public void stop(bool @overwrite)
		{
		}

		/// <summary>
		/// Immediately turns the robot's gun to the left by degrees.
		/// </summary>
		public void turnGunLeft(double @degrees)
		{
		}

		/// <summary>
		/// Immediately turns the robot's gun to the right by degrees.
		/// </summary>
		public void turnGunRight(double @degrees)
		{
		}

		/// <summary>
		/// Immediately turns the robot's body to the left by degrees.
		/// </summary>
		public void turnLeft(double @degrees)
		{
		}

		/// <summary>
		/// Immediately turns the robot's radar to the left by degrees.
		/// </summary>
		public void turnRadarLeft(double @degrees)
		{
		}

		/// <summary>
		/// Immediately turns the robot's radar to the right by degrees.
		/// </summary>
		public void turnRadarRight(double @degrees)
		{
		}

		/// <summary>
		/// Immediately turns the robot's body to the right by degrees.
		/// </summary>
		public void turnRight(double @degrees)
		{
		}

	}
}
