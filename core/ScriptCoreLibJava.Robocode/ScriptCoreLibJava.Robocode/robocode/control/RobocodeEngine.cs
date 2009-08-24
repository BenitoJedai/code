// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.io;
using java.lang;
using robocode.control;
using robocode.control.events;

namespace robocode.control
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/control/RobocodeEngine.html
	[Script(IsNative = true)]
	public class RobocodeEngine
	{
		/// <summary>
		/// Creates a new RobocodeEngine for controlling Robocode.
		/// </summary>
		public RobocodeEngine()
		{
		}

		/// <summary>
		/// Creates a new RobocodeEngine for controlling Robocode.
		/// </summary>
		public RobocodeEngine(File @robocodeHome)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Since 1.6.2. Use <A HREF="../../robocode/control/RobocodeEngine.html#RobocodeEngine(java.io.File)"><CODE>RobocodeEngine.RobocodeEngine(File)</CODE></A> and
		/// <A HREF="../../robocode/control/RobocodeEngine.html#addBattleListener(robocode.control.events.IBattleListener)"><CODE>addBattleListener()</CODE></A> instead.
		/// <p/>
		/// Creates a new RobocodeEngine for controlling Robocode.</I>
		/// </summary>
		public RobocodeEngine(File @robocodeHome, RobocodeListener @listener)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		public RobocodeEngine(IBattleListener @listener)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>Since 1.6.2. Use <A HREF="../../robocode/control/RobocodeEngine.html#RobocodeEngine()"><CODE>RobocodeEngine.RobocodeEngine()</CODE></A> and
		/// <A HREF="../../robocode/control/RobocodeEngine.html#addBattleListener(robocode.control.events.IBattleListener)"><CODE>addBattleListener()</CODE></A> instead.
		/// <p/>
		/// Creates a new RobocodeEngine for controlling Robocode. The JAR file of
		/// Robocode is used to determine the root directory of Robocode.</I>
		/// </summary>
		public RobocodeEngine(RobocodeListener @listener)
		{
		}

		/// <summary>
		/// Aborts the current battle if it is running.
		/// </summary>
		public void abortCurrentBattle()
		{
		}

		/// <summary>
		/// Adds a battle listener that must receive events occurring in battles.
		/// </summary>
		public void addBattleListener(IBattleListener @listener)
		{
		}

		/// <summary>
		/// Closes the RobocodeEngine and releases any allocated resources.
		/// </summary>
		public void close()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		protected void finalize()
		{
		}

		/// <summary>
		/// Returns the current working directory.
		/// </summary>
		public File getCurrentWorkingDir()
		{
			return default(File);
		}

		/// <summary>
		/// Returns all robots available from the local robot repository of Robocode.
		/// </summary>
		public RobotSpecification getLocalRepository()
		{
			return default(RobotSpecification);
		}

		/// <summary>
		/// Returns a selection of robots available from the local robot repository
		/// of Robocode.
		/// </summary>
		public RobotSpecification getLocalRepository(string @selectedRobots)
		{
			return default(RobotSpecification);
		}

		/// <summary>
		/// Returns the directory containing the robots.
		/// </summary>
		public File getRobotsDir()
		{
			return default(File);
		}

		/// <summary>
		/// Returns the installed version of Robocode.
		/// </summary>
		public string getVersion()
		{
			return default(string);
		}

		/// <summary>
		/// Print out all running threads to standard system out.
		/// </summary>
		static public void printRunningThreads()
		{
		}

		/// <summary>
		/// Removes a battle listener that has previously been added to this object.
		/// </summary>
		public void removeBattleListener(IBattleListener @listener)
		{
		}

		/// <summary>
		/// Runs the specified battle.
		/// </summary>
		public void runBattle(BattleSpecification @battleSpecification)
		{
		}

		/// <summary>
		/// Runs the specified battle.
		/// </summary>
		public void runBattle(BattleSpecification @battleSpecification, bool @waitTillOver)
		{
		}

		/// <summary>
		/// Runs the specified battle.
		/// </summary>
		public void runBattle(BattleSpecification @battleSpecification, string @initialPositions, bool @waitTillOver)
		{
		}

		/// <summary>
		/// Shows or hides the Robocode window.
		/// </summary>
		public void setVisible(bool @visible)
		{
		}

		/// <summary>
		/// Will block caller until current battle is over
		/// </summary>
		public void waitTillBattleOver()
		{
		}

	}
}
