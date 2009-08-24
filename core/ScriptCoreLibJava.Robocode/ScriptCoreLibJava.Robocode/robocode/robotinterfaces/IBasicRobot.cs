// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.io;
using java.lang;
using robocode.robotinterfaces;
using robocode.robotinterfaces.peer;

namespace robocode.robotinterfaces
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/robotinterfaces/IBasicRobot.html
	[Script(IsNative = true)]
	public interface IBasicRobot
	{
		/// <summary>
		/// This method is called by the game to notify this robot about basic
		/// robot event.
		/// </summary>
		IBasicEvents getBasicEventListener();

		/// <summary>
		/// This method is called by the game to invoke the
		/// <A HREF="http://java.sun.com/j2se/1.5.0/docs/api/java/lang/Runnable.html#run()" title="class or interface in java.lang"><CODE>run()</CODE></A> method of your robot, where the program
		/// of your robot is implemented.
		/// </summary>
		Runnable getRobotRunnable();

		/// <summary>
		/// Do not call this method!
		/// </summary>
		void setOut(PrintStream @out);

		/// <summary>
		/// Do not call this method!
		/// </summary>
		void setPeer(IBasicRobotPeer @peer);

	}
}
