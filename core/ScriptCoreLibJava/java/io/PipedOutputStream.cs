// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/java.io.PipedOutputStream

using ScriptCoreLib;

namespace java.io
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/io/PipedOutputStream.html
	[Script(IsNative = true)]
	public class PipedOutputStream : OutputStream
	{
		/// <summary>
		/// Creates a piped output stream that is not yet connected to a
		/// piped input stream.
		/// </summary>
		public PipedOutputStream()
		{
		}

		/// <summary>
		/// Creates a piped output stream connected to the specified piped
		/// input stream.
		/// </summary>
		public PipedOutputStream(PipedInputStream @snk)
		{
		}

		/// <summary>
		/// Closes this piped output stream and releases any system resources
		/// associated with this stream.
		/// </summary>
		public void close()
		{
		}

		/// <summary>
		/// Connects this piped output stream to a receiver.
		/// </summary>
		public void connect(PipedInputStream @snk)
		{
		}

		/// <summary>
		/// Flushes this output stream and forces any buffered output bytes
		/// to be written out.
		/// </summary>
		public void flush()
		{
		}

		/// <summary>
		/// Writes <code>len</code> bytes from the specified byte array
		/// starting at offset <code>off</code> to this piped output stream.
		/// </summary>
		public void write(sbyte[] @b, int @off, int @len)
		{
		}

		/// <summary>
		/// Writes the specified <code>byte</code> to the piped output stream.
		/// </summary>
		public override void write(int @b)
		{
		}

	}
}
