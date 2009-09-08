// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/java.io.OutputStream

using ScriptCoreLib;

namespace java.io
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/io/OutputStream.html
	[Script(IsNative = true)]
	public abstract class OutputStream
	{
		/// <summary>
		/// 
		/// </summary>
		public OutputStream()
		{
		}

		/// <summary>
		/// Closes this output stream and releases any system resources
		/// associated with this stream.
		/// </summary>
		public void close()
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
		/// Writes <code>b.length</code> bytes from the specified byte array
		/// to this output stream.
		/// </summary>
		public void write(sbyte[] @b)
		{
		}

		/// <summary>
		/// Writes <code>len</code> bytes from the specified byte array
		/// starting at offset <code>off</code> to this output stream.
		/// </summary>
		public void write(sbyte[] @b, int @off, int @len)
		{
		}

		/// <summary>
		/// Writes the specified byte to this output stream.
		/// </summary>
		abstract public void write(int @b);

	}
}
