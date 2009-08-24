// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.io;
using java.lang;
using java.util;
using robocode;
using robocode.robotinterfaces;

namespace robocode
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/TeamRobot.html
	[Script(IsNative = true)]
	public class TeamRobot
	{
		/// <summary>
		/// 
		/// </summary>
		public TeamRobot()
		{
		}

		/// <summary>
		/// Broadcasts a message to all teammates.
		/// </summary>
		public void broadcastMessage(Serializable @message)
		{
		}

		/// <summary>
		/// Returns a vector containing all MessageEvents currently in the robot's
		/// queue.
		/// </summary>
		public Vector getMessageEvents()
		{
			return default(Vector);
		}

		/// <summary>
		/// Do not call this method!
		/// </summary>
		public ITeamEvents getTeamEventListener()
		{
			return default(ITeamEvents);
		}

		/// <summary>
		/// Returns the names of all teammates, or <code>null</code> there is no
		/// teammates.
		/// </summary>
		public string getTeammates()
		{
			return default(string);
		}

		/// <summary>
		/// Checks if a given robot name is the name of one of your teammates.
		/// </summary>
		public bool isTeammate(string @name)
		{
			return default(bool);
		}

		/// <summary>
		/// This method is called when your robot receives a message from a teammate.
		/// </summary>
		public void onMessageReceived(MessageEvent @event)
		{
		}

		/// <summary>
		/// Sends a message to one (or more) teammates.
		/// </summary>
		public void sendMessage(string @name, Serializable @message)
		{
		}

	}
}
