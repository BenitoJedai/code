// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.sound.sampled.LineEvent

using ScriptCoreLib;
using java.lang;
using java.util;

namespace javax.sound.sampled
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/sound/sampled/LineEvent.html
	[Script(IsNative = true)]
	public class LineEvent : EventObject
	{
		///// <summary>
		///// Constructs a new event of the specified type, originating from the specified line.
		///// </summary>
		//public LineEvent(Line @line, LineEvent.Type @type, long @position)
		//{
		//}

		/// <summary>
		/// Obtains the position in the line's audio data when the event occurred, expressed in sample frames.
		/// </summary>
		public long getFramePosition()
		{
			return default(long);
		}

		/// <summary>
		/// Obtains the audio line that is the source of this event.
		/// </summary>
		public Line getLine()
		{
			return default(Line);
		}

		/// <summary>
		/// Obtains the event's type.
		/// </summary>
		//public LineEvent.Type getType()
		//{
		//    return default(LineEvent.Type);
		//}

		/// <summary>
		/// Obtains a string representation of the event.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}
