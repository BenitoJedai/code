// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode.control.snapshot;

namespace robocode.control.snapshot
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/control/snapshot/IBulletSnapshot.html
	[Script(IsNative = true)]
	public interface IBulletSnapshot
	{
		/// <summary>
		/// 
		/// </summary>
		int getBulletId();

		/// <summary>
		/// Returns the color of the bullet.
		/// </summary>
		int getColor();

		/// <summary>
		/// Returns the explosion image index, which is different depending on the type of explosion.
		/// </summary>
		int getExplosionImageIndex();

		/// <summary>
		/// Returns the frame number to display, i.e. when the bullet explodes.
		/// </summary>
		int getFrame();

		/// <summary>
		/// Returns the X painting position of the bullet.
		/// </summary>
		double getPaintX();

		/// <summary>
		/// Returns the Y painting position of the bullet.
		/// </summary>
		double getPaintY();

		/// <summary>
		/// Returns the bullet power.
		/// </summary>
		double getPower();

		/// <summary>
		/// Returns the bullet state.
		/// </summary>
		BulletState getState();

		/// <summary>
		/// Returns the X position of the bullet.
		/// </summary>
		double getX();

		/// <summary>
		/// Returns the Y position of the bullet.
		/// </summary>
		double getY();

		/// <summary>
		/// Checks if the bullet has become an explosion.
		/// </summary>
		bool isExplosion();

	}
}
