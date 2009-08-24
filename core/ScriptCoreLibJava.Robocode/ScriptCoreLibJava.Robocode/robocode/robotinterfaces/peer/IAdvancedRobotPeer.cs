// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.io;
using java.lang;
using java.util;
using robocode;

namespace robocode.robotinterfaces.peer
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/robotinterfaces/peer/IAdvancedRobotPeer.html
	[Script(IsNative = true)]
	public interface IAdvancedRobotPeer
	{
		/// <summary>
		/// Registers a custom event to be called when a condition is met.
		/// </summary>
		void addCustomEvent(Condition @condition);

		/// <summary>
		/// Clears out any pending events in the robot's event queue immediately.
		/// </summary>
		void clearAllEvents();

		/// <summary>
		/// Returns a vector containing all events currently in the robot's queue.
		/// </summary>
		List getAllEvents();

		/// <summary>
		/// Returns a vector containing all BulletHitBulletEvents currently in the
		/// robot's queue.
		/// </summary>
		List getBulletHitBulletEvents();

		/// <summary>
		/// Returns a vector containing all BulletHitEvents currently in the robot's
		/// queue.
		/// </summary>
		List getBulletHitEvents();

		/// <summary>
		/// Returns a vector containing all BulletMissedEvents currently in the
		/// robot's queue.
		/// </summary>
		List getBulletMissedEvents();

		/// <summary>
		/// Returns a file representing a data directory for the robot, which can be
		/// written to using <A HREF="../../../robocode/RobocodeFileOutputStream.html" title="class in robocode"><CODE>RobocodeFileOutputStream</CODE></A> or
		/// <A HREF="../../../robocode/RobocodeFileWriter.html" title="class in robocode"><CODE>RobocodeFileWriter</CODE></A>.
		/// </summary>
		File getDataDirectory();

		/// <summary>
		/// Returns a file in your data directory that you can write to using
		/// <A HREF="../../../robocode/RobocodeFileOutputStream.html" title="class in robocode"><CODE>RobocodeFileOutputStream</CODE></A> or <A HREF="../../../robocode/RobocodeFileWriter.html" title="class in robocode"><CODE>RobocodeFileWriter</CODE></A>.
		/// </summary>
		File getDataFile(string @filename);

		/// <summary>
		/// Returns the data quota available in your data directory, i.e. the amount
		/// of bytes left in the data directory for the robot.
		/// </summary>
		long getDataQuotaAvailable();

		/// <summary>
		/// Returns the current priority of a class of events.
		/// </summary>
		int getEventPriority(string @eventClass);

		/// <summary>
		/// Returns a vector containing all HitByBulletEvents currently in the
		/// robot's queue.
		/// </summary>
		List getHitByBulletEvents();

		/// <summary>
		/// Returns a vector containing all HitRobotEvents currently in the robot's
		/// queue.
		/// </summary>
		List getHitRobotEvents();

		/// <summary>
		/// Returns a vector containing all HitWallEvents currently in the robot's
		/// queue.
		/// </summary>
		List getHitWallEvents();

		/// <summary>
		/// Returns a vector containing all RobotDeathEvents currently in the robot's
		/// queue.
		/// </summary>
		List getRobotDeathEvents();

		/// <summary>
		/// Returns a vector containing all ScannedRobotEvents currently in the
		/// robot's queue.
		/// </summary>
		List getScannedRobotEvents();

		/// <summary>
		/// Returns a vector containing all StatusEvents currently in the robot's
		/// queue.
		/// </summary>
		List getStatusEvents();

		/// <summary>
		/// Checks if the gun is set to adjust for the robot turning, i.e. to turn
		/// independent from the robot's body turn.
		/// </summary>
		bool isAdjustGunForBodyTurn();

		/// <summary>
		/// Checks if the radar is set to adjust for the gun turning, i.e. to turn
		/// independent from the gun's turn.
		/// </summary>
		bool isAdjustRadarForBodyTurn();

		/// <summary>
		/// Checks if the radar is set to adjust for the robot turning, i.e. to turn
		/// independent from the robot's body turn.
		/// </summary>
		bool isAdjustRadarForGunTurn();

		/// <summary>
		/// Removes a custom event that was previously added by calling
		/// <A HREF="../../../robocode/robotinterfaces/peer/IAdvancedRobotPeer.html#addCustomEvent(robocode.Condition)"><CODE>IAdvancedRobotPeer.addCustomEvent(Condition)</CODE></A>.
		/// </summary>
		void removeCustomEvent(Condition @condition);

		/// <summary>
		/// Sets the priority of a class of events.
		/// </summary>
		void setEventPriority(string @eventClass, int @priority);

		/// <summary>
		/// Call this during an event handler to allow new events of the same
		/// priority to restart the event handler.
		/// </summary>
		void setInterruptible(bool @interruptible);

		/// <summary>
		/// Sets the maximum turn rate of the robot measured in degrees if the robot
		/// should turn slower than <A HREF="../../../robocode/Rules.html#MAX_TURN_RATE"><CODE>Rules.MAX_TURN_RATE</CODE></A> (10 degress/turn).
		/// </summary>
		void setMaxTurnRate(double @newMaxTurnRate);

		/// <summary>
		/// Sets the maximum velocity of the robot measured in pixels/turn if the
		/// robot should move slower than <A HREF="../../../robocode/Rules.html#MAX_VELOCITY"><CODE>Rules.MAX_VELOCITY</CODE></A> (8 pixels/turn).
		/// </summary>
		void setMaxVelocity(double @newMaxVelocity);

		/// <summary>
		/// Sets the robot to move forward or backward by distance measured in pixels
		/// when the next execution takes place.
		/// </summary>
		void setMove(double @distance);

		/// <summary>
		/// Sets the robot to resume the movement stopped by
		/// <A HREF="../../../robocode/robotinterfaces/peer/IStandardRobotPeer.html#stop(boolean)"><CODE>stop(boolean)</CODE></A> or
		/// <A HREF="../../../robocode/robotinterfaces/peer/IAdvancedRobotPeer.html#setStop(boolean)"><CODE>IAdvancedRobotPeer.setStop(boolean)</CODE></A>, if any.
		/// </summary>
		void setResume();

		/// <summary>
		/// This call is identical to <A HREF="../../../robocode/robotinterfaces/peer/IStandardRobotPeer.html#stop(boolean)"><CODE>stop(boolean)</CODE></A>, but returns immediately, and will not execute until you
		/// call <A HREF="../../../robocode/robotinterfaces/peer/IBasicRobotPeer.html#execute()"><CODE>execute()</CODE></A> or take an action that executes.
		/// </summary>
		void setStop(bool @overwrite);

		/// <summary>
		/// Sets the robot's body to turn right or left by radians when the next
		/// execution takes place.
		/// </summary>
		void setTurnBody(double @radians);

		/// <summary>
		/// Sets the robot's gun to turn right or left by radians when the next
		/// execution takes place.
		/// </summary>
		void setTurnGun(double @radians);

		/// <summary>
		/// Sets the robot's radar to turn right or left by radians when the next
		/// execution takes place.
		/// </summary>
		void setTurnRadar(double @radians);

		/// <summary>
		/// Does not return until a condition is met, i.e. when a
		/// <A HREF="../../../robocode/Condition.html#test()"><CODE>Condition.test()</CODE></A> returns <code>true</code>.
		/// </summary>
		void waitFor(Condition @condition);

	}
}
