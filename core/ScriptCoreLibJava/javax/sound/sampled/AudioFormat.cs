// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.sound.sampled.AudioFormat

using ScriptCoreLib;
using java.lang;

namespace javax.sound.sampled
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/sound/sampled/AudioFormat.html
	[Script(IsNative = true)]
	public class AudioFormat
	{
		///// <summary>
		///// Constructs an <code>AudioFormat</code> with the given parameters.
		///// </summary>
		//public AudioFormat(AudioFormat.Encoding @encoding, float @sampleRate, int @sampleSizeInBits, int @channels, int @frameSize, float @frameRate, bool @bigEndian)
		//{
		//}

		/// <summary>
		/// Constructs an <code>AudioFormat</code> with a linear PCM encoding and
		/// the given parameters.
		/// </summary>
		public AudioFormat(float @sampleRate, int @sampleSizeInBits, int @channels, bool @signed, bool @bigEndian)
		{
		}

		/// <summary>
		/// Obtains the number of channels.
		/// </summary>
		public int getChannels()
		{
			return default(int);
		}

		/// <summary>
		/// Obtains the type of encoding for sounds in this format.
		/// </summary>
		//public AudioFormat.Encoding getEncoding()
		//{
		//    return default(AudioFormat.Encoding);
		//}

		/// <summary>
		/// Obtains the frame rate in frames per second.
		/// </summary>
		public float getFrameRate()
		{
			return default(float);
		}

		/// <summary>
		/// Obtains the frame size in bytes.
		/// </summary>
		public int getFrameSize()
		{
			return default(int);
		}

		/// <summary>
		/// Obtains the sample rate.
		/// </summary>
		public float getSampleRate()
		{
			return default(float);
		}

		/// <summary>
		/// Obtains the size of a sample.
		/// </summary>
		public int getSampleSizeInBits()
		{
			return default(int);
		}

		/// <summary>
		/// Indicates whether the audio data is stored in big-endian or little-endian
		/// byte order.
		/// </summary>
		public bool isBigEndian()
		{
			return default(bool);
		}

		/// <summary>
		/// Indicates whether this format matches the one specified.
		/// </summary>
		public bool matches(AudioFormat @format)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string that describes the format, such as:
		/// "PCM SIGNED 22050 Hz 16 bit mono big-endian audio data".
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}
