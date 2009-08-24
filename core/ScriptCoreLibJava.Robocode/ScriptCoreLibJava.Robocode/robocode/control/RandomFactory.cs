// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.util;

namespace robocode.control
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/control/RandomFactory.html
	[Script(IsNative = true)]
	public class RandomFactory
	{
		/// <summary>
		/// 
		/// </summary>
		public RandomFactory()
		{
		}

		/// <summary>
		/// Returns the random number generator used for generating a stream of
		/// random numbers.
		/// </summary>
		public Random getRandom()
		{
			return default(Random);
		}

		/// <summary>
		/// Resets the random number generator instance to be deterministic when
		/// generating random numbers.
		/// </summary>
		static public void resetDeterministic(long @seed)
		{
		}

		/// <summary>
		/// Sets the random number generator instance used for generating a
		/// stream of random numbers.
		/// </summary>
		static public void setRandom(Random @random)
		{
		}

	}
}
