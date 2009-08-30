// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.io;
using java.lang;

namespace java.io
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/io/ObjectOutput.html
	[Script(IsNative = true)]
	public interface ObjectOutput : DataOutput
	{
		/// <summary>
		/// Closes the stream.
		/// </summary>
		void close();

		/// <summary>
		/// Flushes the stream.
		/// </summary>
		void flush();

		/// <summary>
		/// Writes an array of bytes.
		/// </summary>
		void write(byte[] @b);

		/// <summary>
		/// Writes a sub array of bytes.
		/// </summary>
		void write(byte[] @b, int @off, int @len);

		/// <summary>
		/// Writes a byte.
		/// </summary>
		void write(int @b);

		/// <summary>
		/// Write an object to the underlying storage or stream.
		/// </summary>
		void writeObject(object @obj);

	}
}

