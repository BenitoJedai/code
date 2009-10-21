// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.sound.sampled.LineListener

using ScriptCoreLib;
using java.util;

namespace javax.sound.sampled
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/sound/sampled/LineListener.html
	[Script(IsNative = true)]
	public interface LineListener : EventListener
	{
		/// <summary>
		/// Informs the listener that a line's state has changed.
		/// </summary>
		void update(LineEvent @event);

	}
}
