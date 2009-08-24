// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode.control;

namespace robocode.control
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/control/BattleSpecification.html
	[Script(IsNative = true)]
	public class BattleSpecification
	{
		/// <summary>
		/// Creates a new BattleSpecification with the given number of rounds,
		/// battlefield size, and robots.
		/// </summary>
		public BattleSpecification(int @numRounds, BattlefieldSpecification @battlefieldSize, RobotSpecification[] robots)
		{
		}

		/// <summary>
		/// Creates a new BattleSpecification with the given settings.
		/// </summary>
		public BattleSpecification(int @numRounds, long @inactivityTime, double @gunCoolingRate, BattlefieldSpecification @battlefieldSize, RobotSpecification[] robots)
		{
		}

		/// <summary>
		/// Returns the battlefield size for this battle.
		/// </summary>
		public BattlefieldSpecification getBattlefield()
		{
			return default(BattlefieldSpecification);
		}

		/// <summary>
		/// Returns the gun cooling rate of the robots in this battle.
		/// </summary>
		public double getGunCoolingRate()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the allowed inactivity time for the robots in this battle.
		/// </summary>
		public long getInactivityTime()
		{
			return default(long);
		}

		/// <summary>
		/// Returns the number of rounds in this battle.
		/// </summary>
		public int getNumRounds()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the specifications of the robots participating in this battle.
		/// </summary>
		public RobotSpecification getRobots()
		{
			return default(RobotSpecification);
		}

	}
}
