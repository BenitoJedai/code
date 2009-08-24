// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.io;
using java.lang;
using java.util;
using robocode;
using robocode.robotinterfaces;

namespace robocode
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/AdvancedRobot.html
	[Script(IsNative = true)]
	public class AdvancedRobot
	{
		/// <summary>
		/// 
		/// </summary>
		public AdvancedRobot()
		{
		}

		/// <summary>
		/// Registers a custom event to be called when a condition is met.
		/// </summary>
		public void addCustomEvent(Condition @condition)
		{
		}

		/// <summary>
		/// Clears out any pending events in the robot's event queue immediately.
		/// </summary>
		public void clearAllEvents()
		{
		}

		/// <summary>
		/// Executes any pending actions, or continues executing actions that are
		/// in process.
		/// </summary>
		public void execute()
		{
		}

		/// <summary>
		/// Do not call this method!
		/// </summary>
		public IAdvancedEvents getAdvancedEventListener()
		{
			return default(IAdvancedEvents);
		}

		/// <summary>
		/// Returns a vector containing all events currently in the robot's queue.
		/// </summary>
		public Vector getAllEvents()
		{
			return default(Vector);
		}

		/// <summary>
		/// Returns a vector containing all BulletHitBulletEvents currently in the
		/// robot's queue.
		/// </summary>
		public Vector getBulletHitBulletEvents()
		{
			return default(Vector);
		}

		/// <summary>
		/// Returns a vector containing all BulletHitEvents currently in the robot's
		/// queue.
		/// </summary>
		public Vector getBulletHitEvents()
		{
			return default(Vector);
		}

		/// <summary>
		/// Returns a vector containing all BulletMissedEvents currently in the
		/// robot's queue.
		/// </summary>
		public Vector getBulletMissedEvents()
		{
			return default(Vector);
		}

		/// <summary>
		/// Returns a file representing a data directory for the robot, which can be
		/// written to using <A HREF="../robocode/RobocodeFileOutputStream.html" title="class in robocode"><CODE>RobocodeFileOutputStream</CODE></A> or
		/// <A HREF="../robocode/RobocodeFileWriter.html" title="class in robocode"><CODE>RobocodeFileWriter</CODE></A>.
		/// </summary>
		public File getDataDirectory()
		{
			return default(File);
		}

		/// <summary>
		/// Returns a file in your data directory that you can write to using
		/// <A HREF="../robocode/RobocodeFileOutputStream.html" title="class in robocode"><CODE>RobocodeFileOutputStream</CODE></A> or <A HREF="../robocode/RobocodeFileWriter.html" title="class in robocode"><CODE>RobocodeFileWriter</CODE></A>.
		/// </summary>
		public File getDataFile(string @filename)
		{
			return default(File);
		}

		/// <summary>
		/// Returns the data quota available in your data directory, i.e. the amount
		/// of bytes left in the data directory for the robot.
		/// </summary>
		public long getDataQuotaAvailable()
		{
			return default(long);
		}

		/// <summary>
		/// Returns the distance remaining in the robot's current move measured in
		/// pixels.
		/// </summary>
		public double getDistanceRemaining()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the current priority of a class of events.
		/// </summary>
		public int getEventPriority(string @eventClass)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the direction that the robot's gun is facing, in radians.
		/// </summary>
		public double getGunHeadingRadians()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the angle remaining in the gun's turn, in degrees.
		/// </summary>
		public double getGunTurnRemaining()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the angle remaining in the gun's turn, in radians.
		/// </summary>
		public double getGunTurnRemainingRadians()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the direction that the robot's body is facing, in radians.
		/// </summary>
		public double getHeadingRadians()
		{
			return default(double);
		}

		/// <summary>
		/// Returns a vector containing all HitByBulletEvents currently in the
		/// robot's queue.
		/// </summary>
		public Vector getHitByBulletEvents()
		{
			return default(Vector);
		}

		/// <summary>
		/// Returns a vector containing all HitRobotEvents currently in the robot's
		/// queue.
		/// </summary>
		public Vector getHitRobotEvents()
		{
			return default(Vector);
		}

		/// <summary>
		/// Returns a vector containing all HitWallEvents currently in the robot's
		/// queue.
		/// </summary>
		public Vector getHitWallEvents()
		{
			return default(Vector);
		}

		/// <summary>
		/// Returns the direction that the robot's radar is facing, in radians.
		/// </summary>
		public double getRadarHeadingRadians()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the angle remaining in the radar's turn, in degrees.
		/// </summary>
		public double getRadarTurnRemaining()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the angle remaining in the radar's turn, in radians.
		/// </summary>
		public double getRadarTurnRemainingRadians()
		{
			return default(double);
		}

		/// <summary>
		/// Returns a vector containing all RobotDeathEvents currently in the robot's
		/// queue.
		/// </summary>
		public Vector getRobotDeathEvents()
		{
			return default(Vector);
		}

		/// <summary>
		/// Returns a vector containing all ScannedRobotEvents currently in the
		/// robot's queue.
		/// </summary>
		public Vector getScannedRobotEvents()
		{
			return default(Vector);
		}

		/// <summary>
		/// Returns a vector containing all StatusEvents currently in the robot's
		/// queue.
		/// </summary>
		public Vector getStatusEvents()
		{
			return default(Vector);
		}

		/// <summary>
		/// Returns the angle remaining in the robots's turn, in degrees.
		/// </summary>
		public double getTurnRemaining()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the angle remaining in the robot's turn, in radians.
		/// </summary>
		public double getTurnRemainingRadians()
		{
			return default(double);
		}

		/// <summary>
		/// Checks if the gun is set to adjust for the robot turning, i.e. to turn
		/// independent from the robot's body turn.
		/// </summary>
		public bool isAdjustGunForRobotTurn()
		{
			return default(bool);
		}

		/// <summary>
		/// Checks if the radar is set to adjust for the gun turning, i.e. to turn
		/// independent from the gun's turn.
		/// </summary>
		public bool isAdjustRadarForGunTurn()
		{
			return default(bool);
		}

		/// <summary>
		/// Checks if the radar is set to adjust for the robot turning, i.e. to turn
		/// independent from the robot's body turn.
		/// </summary>
		public bool isAdjustRadarForRobotTurn()
		{
			return default(bool);
		}

		/// <summary>
		/// This method is called when a custom condition is met.
		/// </summary>
		public void onCustomEvent(CustomEvent @event)
		{
		}

		/// <summary>
		/// This method is called if your robot dies.
		/// </summary>
		public void onDeath(DeathEvent @event)
		{
		}

		/// <summary>
		/// This method is called if the robot is using too much time between
		/// actions.
		/// </summary>
		public void onSkippedTurn(SkippedTurnEvent @event)
		{
		}

		/// <summary>
		/// Removes a custom event that was previously added by calling
		/// <A HREF="../robocode/AdvancedRobot.html#addCustomEvent(robocode.Condition)"><CODE>AdvancedRobot.addCustomEvent(Condition)</CODE></A>.
		/// </summary>
		public void removeCustomEvent(Condition @condition)
		{
		}

		/// <summary>
		/// Sets the robot to move ahead (forward) by distance measured in pixels
		/// when the next execution takes place.
		/// </summary>
		public void setAhead(double @distance)
		{
		}

		/// <summary>
		/// Sets the robot to move back by distance measured in pixels when the next
		/// execution takes place.
		/// </summary>
		public void setBack(double @distance)
		{
		}

		/// <summary>
		/// Sets the priority of a class of events.
		/// </summary>
		public void setEventPriority(string @eventClass, int @priority)
		{
		}

		/// <summary>
		/// Sets the gun to fire a bullet when the next execution takes place.
		/// </summary>
		public void setFire(double @power)
		{
		}

		/// <summary>
		/// Sets the gun to fire a bullet when the next execution takes place.
		/// </summary>
		public Bullet setFireBullet(double @power)
		{
			return default(Bullet);
		}

		/// <summary>
		/// Call this during an event handler to allow new events of the same
		/// priority to restart the event handler.
		/// </summary>
		public void setInterruptible(bool @interruptible)
		{
		}

		/// <summary>
		/// Sets the maximum turn rate of the robot measured in degrees if the robot
		/// should turn slower than <A HREF="../robocode/Rules.html#MAX_TURN_RATE"><CODE>Rules.MAX_TURN_RATE</CODE></A> (10 degress/turn).
		/// </summary>
		public void setMaxTurnRate(double @newMaxTurnRate)
		{
		}

		/// <summary>
		/// Sets the maximum velocity of the robot measured in pixels/turn if the
		/// robot should move slower than <A HREF="../robocode/Rules.html#MAX_VELOCITY"><CODE>Rules.MAX_VELOCITY</CODE></A> (8 pixels/turn).
		/// </summary>
		public void setMaxVelocity(double @newMaxVelocity)
		{
		}

		/// <summary>
		/// Sets the robot to resume the movement stopped by <A HREF="../robocode/Robot.html#stop()"><CODE>stop()</CODE></A>
		/// or <A HREF="../robocode/AdvancedRobot.html#setStop()"><CODE>AdvancedRobot.setStop()</CODE></A>, if any.
		/// </summary>
		public void setResume()
		{
		}

		/// <summary>
		/// This call is identical to <A HREF="../robocode/Robot.html#stop()"><CODE>stop()</CODE></A>, but returns immediately, and
		/// will not execute until you call <A HREF="../robocode/AdvancedRobot.html#execute()"><CODE>AdvancedRobot.execute()</CODE></A> or take an action that
		/// executes.
		/// </summary>
		public void setStop()
		{
		}

		/// <summary>
		/// This call is identical to <A HREF="../robocode/Robot.html#stop(boolean)"><CODE>stop(boolean)</CODE></A>, but
		/// returns immediately, and will not execute until you call
		/// <A HREF="../robocode/AdvancedRobot.html#execute()"><CODE>AdvancedRobot.execute()</CODE></A> or take an action that executes.
		/// </summary>
		public void setStop(bool @overwrite)
		{
		}

		/// <summary>
		/// Sets the robot's gun to turn left by degrees when the next execution
		/// takes place.
		/// </summary>
		public void setTurnGunLeft(double @degrees)
		{
		}

		/// <summary>
		/// Sets the robot's gun to turn left by radians when the next execution
		/// takes place.
		/// </summary>
		public void setTurnGunLeftRadians(double @radians)
		{
		}

		/// <summary>
		/// Sets the robot's gun to turn right by degrees when the next execution
		/// takes place.
		/// </summary>
		public void setTurnGunRight(double @degrees)
		{
		}

		/// <summary>
		/// Sets the robot's gun to turn right by radians when the next execution
		/// takes place.
		/// </summary>
		public void setTurnGunRightRadians(double @radians)
		{
		}

		/// <summary>
		/// Sets the robot's body to turn left by degrees when the next execution
		/// takes place.
		/// </summary>
		public void setTurnLeft(double @degrees)
		{
		}

		/// <summary>
		/// Sets the robot's body to turn left by radians when the next execution
		/// takes place.
		/// </summary>
		public void setTurnLeftRadians(double @radians)
		{
		}

		/// <summary>
		/// Sets the robot's radar to turn left by degrees when the next execution
		/// takes place.
		/// </summary>
		public void setTurnRadarLeft(double @degrees)
		{
		}

		/// <summary>
		/// Sets the robot's radar to turn left by radians when the next execution
		/// takes place.
		/// </summary>
		public void setTurnRadarLeftRadians(double @radians)
		{
		}

		/// <summary>
		/// Sets the robot's radar to turn right by degrees when the next execution
		/// takes place.
		/// </summary>
		public void setTurnRadarRight(double @degrees)
		{
		}

		/// <summary>
		/// Sets the robot's radar to turn right by radians when the next execution
		/// takes place.
		/// </summary>
		public void setTurnRadarRightRadians(double @radians)
		{
		}

		/// <summary>
		/// Sets the robot's body to turn right by degrees when the next execution
		/// takes place.
		/// </summary>
		public void setTurnRight(double @degrees)
		{
		}

		/// <summary>
		/// Sets the robot's body to turn right by radians when the next execution
		/// takes place.
		/// </summary>
		public void setTurnRightRadians(double @radians)
		{
		}

		/// <summary>
		/// Immediately turns the robot's gun to the left by radians.
		/// </summary>
		public void turnGunLeftRadians(double @radians)
		{
		}

		/// <summary>
		/// Immediately turns the robot's gun to the right by radians.
		/// </summary>
		public void turnGunRightRadians(double @radians)
		{
		}

		/// <summary>
		/// Immediately turns the robot's body to the left by radians.
		/// </summary>
		public void turnLeftRadians(double @radians)
		{
		}

		/// <summary>
		/// Immediately turns the robot's radar to the left by radians.
		/// </summary>
		public void turnRadarLeftRadians(double @radians)
		{
		}

		/// <summary>
		/// Immediately turns the robot's radar to the right by radians.
		/// </summary>
		public void turnRadarRightRadians(double @radians)
		{
		}

		/// <summary>
		/// Immediately turns the robot's body to the right by radians.
		/// </summary>
		public void turnRightRadians(double @radians)
		{
		}

		/// <summary>
		/// Does not return until a condition is met, i.e. when a
		/// <A HREF="../robocode/Condition.html#test()"><CODE>Condition.test()</CODE></A> returns <code>true</code>.
		/// </summary>
		public void waitFor(Condition @condition)
		{
		}

	}
}
