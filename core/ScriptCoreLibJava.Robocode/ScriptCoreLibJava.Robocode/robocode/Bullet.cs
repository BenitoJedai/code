// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;

namespace robocode
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/Bullet.html
	[Script(IsNative = true)]
	public class Bullet
	{
		/// <summary>
		/// Called by the game to create a new <code>Bullet</code> object
		/// </summary>
		public Bullet(double @heading, double @x, double @y, double @power, string @ownerName, string @victimName, bool @isActive, int @bulletId)
		{
		}

		/// <summary>
		/// Returns the direction the bullet is/was heading, in degrees
		/// (0 <= getHeading() < 360).
		/// </summary>
		public double getHeading()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the direction the bullet is/was heading, in radians
		/// (0 <= getHeadingRadians() < 2 * Math.PI).
		/// </summary>
		public double getHeadingRadians()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the name of the robot that fired this bullet.
		/// </summary>
		public string getName()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the power of this bullet.
		/// </summary>
		public double getPower()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the velocity of this bullet.
		/// </summary>
		public double getVelocity()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the name of the robot that this bullet hit, or <code>null</code> if
		/// the bullet has not hit a robot.
		/// </summary>
		public string getVictim()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the X position of the bullet.
		/// </summary>
		public double getX()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the Y position of the bullet.
		/// </summary>
		public double getY()
		{
			return default(double);
		}

		/// <summary>
		/// Checks if this bullet is still active on the battlefield.
		/// </summary>
		public bool isActive()
		{
			return default(bool);
		}

	}
}
