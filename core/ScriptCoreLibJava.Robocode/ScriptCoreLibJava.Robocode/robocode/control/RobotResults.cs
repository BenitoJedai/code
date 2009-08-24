// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using robocode;
using robocode.control;

namespace robocode.control
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/control/RobotResults.html
	[Script(IsNative = true)]
	public class RobotResults
	{
		/// <summary>
		/// Constructs new RobotResults based on a <A HREF="../../robocode/control/RobotSpecification.html" title="class in robocode.control"><CODE>RobotSpecification</CODE></A> and <A HREF="../../robocode/BattleResults.html" title="class in robocode"><CODE>BattleResults</CODE></A>.
		/// </summary>
		public RobotResults(RobotSpecification @robot, BattleResults @results)
		{
		}

		/// <summary>
		/// Constructs new RobotResults.
		/// </summary>
		public RobotResults(RobotSpecification @robot, string @teamLeaderName, int @rank, double @score, double @survival, double @lastSurvivorBonus, double @bulletDamage, double @bulletDamageBonus, double @ramDamage, double @ramDamageBonus, int @firsts, int @seconds, int @thirds)
		{
		}

		/// <summary>
		/// Converts an array of <A HREF="../../robocode/BattleResults.html" title="class in robocode"><CODE>BattleResults</CODE></A> into an array of <A HREF="../../robocode/control/RobotResults.html" title="class in robocode.control"><CODE>RobotResults</CODE></A>.
		/// </summary>
		public RobotResults convertResults(BattleResults[] results)
		{
			return default(RobotResults);
		}

		/// <summary>
		/// Returns the robot these results are meant for.
		/// </summary>
		public RobotSpecification getRobot()
		{
			return default(RobotSpecification);
		}

	}
}
