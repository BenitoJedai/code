// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.io;
using java.lang;

namespace java.io
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/io/ObjectInput.html
	[Script(IsNative = true)]
	public interface ObjectInput : DataInput
	{
		/// <summary>
		/// Returns the number of bytes that can be read
		/// without blocking.
		/// </summary>
		int available();

		/// <summary>
		/// Closes the input stream.
		/// </summary>
		void close();

		/// <summary>
		/// Reads a byte of data.
		/// </summary>
		int read();

		/// <summary>
		/// Reads into an array of bytes.
		/// </summary>
		int read(byte[] @b);

		/// <summary>
		/// Reads into an array of bytes.
		/// </summary>
		int read(byte[] @b, int @off, int @len);

		/// <summary>
		/// Read and return an object.
		/// </summary>
		object readObject();

		/// <summary>
		/// Skips n bytes of input.
		/// </summary>
		long skip(long @n);

	}
}

