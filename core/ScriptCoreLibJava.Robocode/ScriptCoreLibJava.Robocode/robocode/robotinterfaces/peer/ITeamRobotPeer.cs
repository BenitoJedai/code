// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.io;
using java.lang;
using java.util;

namespace robocode.robotinterfaces.peer
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/robotinterfaces/peer/ITeamRobotPeer.html
	[Script(IsNative = true)]
	public interface ITeamRobotPeer
	{
		/// <summary>
		/// Broadcasts a message to all teammates.
		/// </summary>
		void broadcastMessage(Serializable @message);

		/// <summary>
		/// Returns a vector containing all MessageEvents currently in the robot's
		/// queue.
		/// </summary>
		List getMessageEvents();

		/// <summary>
		/// Returns the names of all teammates, or <code>null</code> there is no
		/// teammates.
		/// </summary>
		string getTeammates();

		/// <summary>
		/// Checks if a given robot name is the name of one of your teammates.
		/// </summary>
		bool isTeammate(string @name);

		/// <summary>
		/// Sends a message to one (or more) teammates.
		/// </summary>
		void sendMessage(string @name, Serializable @message);

	}
}
