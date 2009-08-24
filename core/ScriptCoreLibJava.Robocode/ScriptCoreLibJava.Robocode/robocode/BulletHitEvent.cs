// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using robocode;

namespace robocode
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/BulletHitEvent.html
	[Script(IsNative = true)]
	public class BulletHitEvent
	{
		/// <summary>
		/// Called by the game to create a new <code>BulletHitEvent</code>.
		/// </summary>
		public BulletHitEvent(string @name, double @energy, Bullet @bullet)
		{
		}

		/// <summary>
		/// Returns the bullet of yours that hit the robot.
		/// </summary>
		public Bullet getBullet()
		{
			return default(Bullet);
		}

		/// <summary>
		/// Returns the remaining energy of the robot your bullet has hit (after the
		/// damage done by your bullet).
		/// </summary>
		public double getEnergy()
		{
			return default(double);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/BulletHitEvent.html#getEnergy()"><CODE>BulletHitEvent.getEnergy()</CODE></A> instead.</I>
		/// </summary>
		public double getLife()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the name of the robot your bullet hit.
		/// </summary>
		public string getName()
		{
			return default(string);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/BulletHitEvent.html#getEnergy()"><CODE>BulletHitEvent.getEnergy()</CODE></A> instead.</I>
		/// </summary>
		public double getRobotLife()
		{
			return default(double);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/BulletHitEvent.html#getName()"><CODE>BulletHitEvent.getName()</CODE></A> instead.</I>
		/// </summary>
		public string getRobotName()
		{
			return default(string);
		}

	}
}
