// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.sound.sampled.DataLine

using ScriptCoreLib;

namespace javax.sound.sampled
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/sound/sampled/DataLine.html
	[Script(IsNative = true)]
	public interface DataLine : Line
	{
		/// <summary>
		/// Obtains the number of bytes of data currently available to the
		/// application for processing in the data line's internal buffer.
		/// </summary>
		int available();

		/// <summary>
		/// Drains queued data from the line by continuing data I/O until the
		/// data line's internal buffer has been emptied.
		/// </summary>
		void drain();

		/// <summary>
		/// Flushes queued data from the line.
		/// </summary>
		void flush();

		/// <summary>
		/// Obtains the maximum number of bytes of data that will fit in the data line's
		/// internal buffer.
		/// </summary>
		int getBufferSize();

		/// <summary>
		/// Obtains the current format (encoding, sample rate, number of channels,
		/// etc.) of the data line's audio data.
		/// </summary>
		AudioFormat getFormat();

		/// <summary>
		/// Obtains the current position in the audio data, in sample frames.
		/// </summary>
		int getFramePosition();

		/// <summary>
		/// Obtains the current volume level for the line.
		/// </summary>
		float getLevel();

		/// <summary>
		/// Obtains the current position in the audio data, in microseconds.
		/// </summary>
		long getMicrosecondPosition();

		/// <summary>
		/// Indicates whether the line is engaging in active I/O (such as playback
		/// or capture).
		/// </summary>
		bool isActive();

		/// <summary>
		/// Indicates whether the line is running.
		/// </summary>
		bool isRunning();

		/// <summary>
		/// Allows a line to engage in data I/O.
		/// </summary>
		void start();

		/// <summary>
		/// Stops the line.
		/// </summary>
		void stop();

	}
}
