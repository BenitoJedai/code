// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.sound.sampled.AudioInputStream

using ScriptCoreLib;
using java.io;

namespace javax.sound.sampled
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/sound/sampled/AudioInputStream.html
	[Script(IsNative = true)]
	public class AudioInputStream : InputStream
	{
		/// <summary>
		/// Constructs an audio input stream that has the requested format and length in sample frames,
		/// using audio data from the specified input stream.
		/// </summary>
		public AudioInputStream(InputStream @stream, AudioFormat @format, long @length)
		{
		}

		/// <summary>
		/// Constructs an audio input stream that reads its data from the target
		/// data line indicated.
		/// </summary>
		public AudioInputStream(TargetDataLine @line)
		{
		}

		/// <summary>
		/// Returns the maximum number of bytes that can be read (or skipped over) from this
		/// audio input stream without blocking.
		/// </summary>
		public int available()
		{
			return default(int);
		}

		/// <summary>
		/// Closes this audio input stream and releases any system resources associated
		/// with the stream.
		/// </summary>
		public void close()
		{
		}

		/// <summary>
		/// Obtains the audio format of the sound data in this audio input stream.
		/// </summary>
		public AudioFormat getFormat()
		{
			return default(AudioFormat);
		}

		/// <summary>
		/// Obtains the length of the stream, expressed in sample frames rather than bytes.
		/// </summary>
		public long getFrameLength()
		{
			return default(long);
		}

		/// <summary>
		/// Marks the current position in this audio input stream.
		/// </summary>
		public void mark(int @readlimit)
		{
		}

		/// <summary>
		/// Tests whether this audio input stream supports the <code>mark</code> and
		/// <code>reset</code> methods.
		/// </summary>
		public bool markSupported()
		{
			return default(bool);
		}

		/// <summary>
		/// Reads the next byte of data from the audio input stream.
		/// </summary>
		public override int read()
		{
			return default(int);
		}

		/// <summary>
		/// Reads some number of bytes from the audio input stream and stores them into
		/// the buffer array <code>b</code>.
		/// </summary>
		public int read(sbyte[] @b)
		{
			return default(int);
		}

		/// <summary>
		/// Reads up to a specified maximum number of bytes of data from the audio
		/// stream, putting them into the given byte array.
		/// </summary>
		public int read(sbyte[] @b, int @off, int @len)
		{
			return default(int);
		}

		/// <summary>
		/// Repositions this audio input stream to the position it had at the time its
		/// <code>mark</code> method was last invoked.
		/// </summary>
		public void reset()
		{
		}

		/// <summary>
		/// Skips over and discards a specified number of bytes from this
		/// audio input stream.
		/// </summary>
		public long skip(long @n)
		{
			return default(long);
		}

	}
}
