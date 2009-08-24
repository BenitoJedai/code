// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using robocode;

namespace robocode
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/ScannedRobotEvent.html
	[Script(IsNative = true)]
	public class ScannedRobotEvent
	{
		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/ScannedRobotEvent.html#ScannedRobotEvent(java.lang.String, double, double, double, double, double)"><CODE>ScannedRobotEvent.ScannedRobotEvent(String, double, double, double, double, double)</CODE></A> instead.</I>
		/// </summary>
		public ScannedRobotEvent()
		{
		}

		/// <summary>
		/// Called by the game to create a new ScannedRobotEvent.
		/// </summary>
		public ScannedRobotEvent(string @name, double @energy, double @bearing, double @distance, double @heading, double @velocity)
		{
		}

		/// <summary>
		/// Compares this event to another event regarding precedence.
		/// </summary>
		public int compareTo(Event @event)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the bearing to the robot you scanned, relative to your robot's
		/// heading, in degrees (-180 <= getBearing() < 180)
		/// </summary>
		public double getBearing()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the bearing to the robot you scanned, relative to your robot's
		/// heading, in radians (-PI <= getBearingRadians() < PI)
		/// </summary>
		public double getBearingRadians()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the distance to the robot (your center to his center).
		/// </summary>
		public double getDistance()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the energy of the robot.
		/// </summary>
		public double getEnergy()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the heading of the robot, in degrees (0 <= getHeading() < 360)
		/// </summary>
		public double getHeading()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the heading of the robot, in radians (0 <= getHeading() < 2 * PI)
		/// </summary>
		public double getHeadingRadians()
		{
			return default(double);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/ScannedRobotEvent.html#getEnergy()"><CODE>ScannedRobotEvent.getEnergy()</CODE></A> instead.</I>
		/// </summary>
		public double getLife()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the name of the robot.
		/// </summary>
		public string getName()
		{
			return default(string);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/ScannedRobotEvent.html#getBearing()"><CODE>ScannedRobotEvent.getBearing()</CODE></A> instead.</I>
		/// </summary>
		public double getRobotBearing()
		{
			return default(double);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/ScannedRobotEvent.html#getBearing()"><CODE>ScannedRobotEvent.getBearing()</CODE></A> instead.</I>
		/// </summary>
		public double getRobotBearingDegrees()
		{
			return default(double);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/ScannedRobotEvent.html#getBearingRadians()"><CODE>ScannedRobotEvent.getBearingRadians()</CODE></A> instead.</I>
		/// </summary>
		public double getRobotBearingRadians()
		{
			return default(double);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/ScannedRobotEvent.html#getDistance()"><CODE>ScannedRobotEvent.getDistance()</CODE></A> instead.</I>
		/// </summary>
		public double getRobotDistance()
		{
			return default(double);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/ScannedRobotEvent.html#getHeading()"><CODE>ScannedRobotEvent.getHeading()</CODE></A> instead.</I>
		/// </summary>
		public double getRobotHeading()
		{
			return default(double);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/ScannedRobotEvent.html#getHeading()"><CODE>ScannedRobotEvent.getHeading()</CODE></A> instead.</I>
		/// </summary>
		public double getRobotHeadingDegrees()
		{
			return default(double);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/ScannedRobotEvent.html#getHeadingRadians()"><CODE>ScannedRobotEvent.getHeadingRadians()</CODE></A> instead.</I>
		/// </summary>
		public double getRobotHeadingRadians()
		{
			return default(double);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/ScannedRobotEvent.html#getEnergy()"><CODE>ScannedRobotEvent.getEnergy()</CODE></A> instead.</I>
		/// </summary>
		public double getRobotLife()
		{
			return default(double);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/ScannedRobotEvent.html#getName()"><CODE>ScannedRobotEvent.getName()</CODE></A> instead.</I>
		/// </summary>
		public string getRobotName()
		{
			return default(string);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/ScannedRobotEvent.html#getVelocity()"><CODE>ScannedRobotEvent.getVelocity()</CODE></A> instead.</I>
		/// </summary>
		public double getRobotVelocity()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the velocity of the robot.
		/// </summary>
		public double getVelocity()
		{
			return default(double);
		}

	}
}
