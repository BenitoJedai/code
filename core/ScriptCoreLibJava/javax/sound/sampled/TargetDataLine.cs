// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.sound.sampled.TargetDataLine

using ScriptCoreLib;

namespace javax.sound.sampled
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/sound/sampled/TargetDataLine.html
	[Script(IsNative = true)]
	public interface TargetDataLine : DataLine
	{
		/// <summary>
		/// Opens the line with the specified format, causing the line to acquire any
		/// required system resources and become operational.
		/// </summary>
		void open(AudioFormat @format);

		/// <summary>
		/// Opens the line with the specified format and requested buffer size,
		/// causing the line to acquire any required system resources and become
		/// operational.
		/// </summary>
		void open(AudioFormat @format, int @bufferSize);

		/// <summary>
		/// Reads audio data from the data line's input buffer.
		/// </summary>
		int read(sbyte[] @b, int @off, int @len);

	}
}
