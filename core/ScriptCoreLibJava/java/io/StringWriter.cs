// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/java.io.StringWriter

using ScriptCoreLib;
using java.lang;

namespace java.io
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/io/StringWriter.html
	[Script(IsNative = true)]
	public class StringWriter : Writer
	{
		/// <summary>
		/// Create a new string writer, using the default initial string-buffer
		/// size.
		/// </summary>
		public StringWriter()
		{
		}

		/// <summary>
		/// Create a new string writer, using the specified initial string-buffer
		/// size.
		/// </summary>
		public StringWriter(int @initialSize)
		{
		}

		/// <summary>
		/// Closing a <tt>StringWriter</tt> has no effect.
		/// </summary>
		public override void close()
		{
		}

		/// <summary>
		/// Flush the stream.
		/// </summary>
		public override void flush()
		{
		}

		/// <summary>
		/// Return the string buffer itself.
		/// </summary>
		public StringBuffer getBuffer()
		{
			return default(StringBuffer);
		}

		/// <summary>
		/// Return the buffer's current value as a string.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

		/// <summary>
		/// Write a portion of an array of characters.
		/// </summary>
		public override void write(char[] @cbuf, int @off, int @len)
		{
		}

		/// <summary>
		/// Write a single character.
		/// </summary>
		public void write(int @c)
		{
		}

		/// <summary>
		/// Write a string.
		/// </summary>
		public void write(string @str)
		{
		}

		/// <summary>
		/// Write a portion of a string.
		/// </summary>
		public void write(string @str, int @off, int @len)
		{
		}

	}
}
