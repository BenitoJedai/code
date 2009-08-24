// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using robocode;

namespace robocode
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/HitByBulletEvent.html
	[Script(IsNative = true)]
	public class HitByBulletEvent
	{
		/// <summary>
		/// Called by the game to create a new HitByBulletEvent.
		/// </summary>
		public HitByBulletEvent(double @bearing, Bullet @bullet)
		{
		}

		/// <summary>
		/// Returns the bearing to the bullet, relative to your robot's heading,
		/// in degrees (-180 < getBearing() <= 180)
		/// <p/>
		/// If you were to turnRight(e.getBearing()), you would be facing the
		/// direction the bullet came from.
		/// </summary>
		public double getBearing()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the bearing to the bullet, relative to your robot's heading,
		/// in radians (-Math.PI < getBearingRadians() <= Math.PI)
		/// <p/>
		/// If you were to turnRightRadians(e.getBearingRadians()), you would be
		/// facing the direction the bullet came from.
		/// </summary>
		public double getBearingRadians()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the bullet that hit your robot.
		/// </summary>
		public Bullet getBullet()
		{
			return default(Bullet);
		}

		/// <summary>
		/// Returns the heading of the bullet when it hit you, in degrees
		/// (0 <= getHeading() < 360)
		/// <p/>
		/// Note: This is not relative to the direction you are facing.
		/// </summary>
		public double getHeading()
		{
			return default(double);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/HitByBulletEvent.html#getHeading()"><CODE>HitByBulletEvent.getHeading()</CODE></A> instead.</I>
		/// </summary>
		public double getHeadingDegrees()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the heading of the bullet when it hit you, in radians
		/// (0 <= getHeadingRadians() < 2 * PI)
		/// <p/>
		/// Note: This is not relative to the direction you are facing.
		/// </summary>
		public double getHeadingRadians()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the name of the robot that fired the bullet.
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

	}
}
