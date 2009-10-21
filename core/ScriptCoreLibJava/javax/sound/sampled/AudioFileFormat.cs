// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.sound.sampled.AudioFileFormat

using ScriptCoreLib;
using java.lang;

namespace javax.sound.sampled
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/sound/sampled/AudioFileFormat.html
	[Script(IsNative = true)]
	public class AudioFileFormat
	{
		///// <summary>
		///// Constructs an audio file format object.
		///// </summary>
		//public AudioFileFormat(AudioFileFormat.Type @type, AudioFormat @format, int @frameLength)
		//{
		//}

		///// <summary>
		///// Constructs an audio file format object.
		///// </summary>
		//public AudioFileFormat(AudioFileFormat.Type @type, int @byteLength, AudioFormat @format, int @frameLength)
		//{
		//}

		/// <summary>
		/// Obtains the size in bytes of the entire audio file (not just its audio data).
		/// </summary>
		public int getByteLength()
		{
			return default(int);
		}

		/// <summary>
		/// Obtains the format of the audio data contained in the audio file.
		/// </summary>
		public AudioFormat getFormat()
		{
			return default(AudioFormat);
		}

		/// <summary>
		/// Obtains the length of the audio data contained in the file, expressed in sample frames.
		/// </summary>
		public int getFrameLength()
		{
			return default(int);
		}

		/// <summary>
		/// Obtains the audio file type, such as <code>WAVE</code> or <code>AU</code>.
		/// </summary>
		//public AudioFileFormat.Type getType()
		//{
		//    return default(AudioFileFormat.Type);
		//}

		/// <summary>
		/// Provides a string representation of the file format.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}
