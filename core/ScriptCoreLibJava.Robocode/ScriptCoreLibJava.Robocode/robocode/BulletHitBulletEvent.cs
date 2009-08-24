// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode;

namespace robocode
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/BulletHitBulletEvent.html
	[Script(IsNative = true)]
	public class BulletHitBulletEvent
	{
		/// <summary>
		/// Called by the game to create a new <code>BulletHitEvent</code>.
		/// </summary>
		public BulletHitBulletEvent(Bullet @bullet, Bullet @hitBullet)
		{
		}

		/// <summary>
		/// Returns your bullet that hit another bullet.
		/// </summary>
		public Bullet getBullet()
		{
			return default(Bullet);
		}

		/// <summary>
		/// Returns the bullet that was hit by your bullet.
		/// </summary>
		public Bullet getHitBullet()
		{
			return default(Bullet);
		}

	}
}
