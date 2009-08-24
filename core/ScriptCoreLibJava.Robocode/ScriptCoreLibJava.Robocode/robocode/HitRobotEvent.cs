// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using robocode;

namespace robocode
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/HitRobotEvent.html
	[Script(IsNative = true)]
	public class HitRobotEvent
	{
		/// <summary>
		/// Called by the game to create a new HitRobotEvent.
		/// </summary>
		public HitRobotEvent(string @name, double @bearing, double @energy, bool @atFault)
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
		/// Returns the bearing to the robot you hit, relative to your robot's
		/// heading, in degrees (-180 <= getBearing() < 180)
		/// </summary>
		public double getBearing()
		{
			return default(double);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/HitRobotEvent.html#getBearing()"><CODE>HitRobotEvent.getBearing()</CODE></A> instead.</I>
		/// </summary>
		public double getBearingDegrees()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the bearing to the robot you hit, relative to your robot's
		/// heading, in radians (-PI <= getBearingRadians() < PI)
		/// </summary>
		public double getBearingRadians()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the amount of energy of the robot you hit.
		/// </summary>
		public double getEnergy()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the name of the robot you hit.
		/// </summary>
		public string getName()
		{
			return default(string);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/HitRobotEvent.html#getName()"><CODE>HitRobotEvent.getName()</CODE></A> instead.</I>
		/// </summary>
		public string getRobotName()
		{
			return default(string);
		}

		/// <summary>
		/// Checks if your robot was moving towards the robot that was hit.
		/// </summary>
		public bool isMyFault()
		{
			return default(bool);
		}

	}
}
