// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.io;
using java.lang;

namespace robocode
{
	// http://robocode.sourceforge.net/docs/robocode/robocode/MessageEvent.html
	[Script(IsNative = true)]
	public class MessageEvent
	{
		/// <summary>
		/// Called by the game to create a new MessageEvent.
		/// </summary>
		public MessageEvent(string @sender, Serializable @message)
		{
		}

		/// <summary>
		/// Returns the message itself.
		/// </summary>
		public Serializable getMessage()
		{
			return default(Serializable);
		}

		/// <summary>
		/// Returns the name of the sending robot.
		/// </summary>
		public string getSender()
		{
			return default(string);
		}

	}
}
