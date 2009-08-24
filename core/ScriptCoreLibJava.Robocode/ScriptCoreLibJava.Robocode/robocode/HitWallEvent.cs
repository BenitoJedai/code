// This source code was generated for ScriptCoreLib
using ScriptCoreLib;

namespace robocode
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/HitWallEvent.html
	[Script(IsNative = true)]
	public class HitWallEvent
	{
		/// <summary>
		/// Called by the game to create a new HitWallEvent.
		/// </summary>
		public HitWallEvent(double @bearing)
		{
		}

		/// <summary>
		/// Returns the bearing to the wall you hit, relative to your robot's
		/// heading, in degrees (-180 <= getBearing() < 180)
		/// </summary>
		public double getBearing()
		{
			return default(double);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Use <A HREF="../robocode/HitWallEvent.html#getBearing()"><CODE>HitWallEvent.getBearing()</CODE></A> instead.</I>
		/// </summary>
		public double getBearingDegrees()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the bearing to the wall you hit, relative to your robot's
		/// heading, in radians (-PI <= getBearingRadians() < PI)
		/// </summary>
		public double getBearingRadians()
		{
			return default(double);
		}

	}
}
