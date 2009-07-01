// This source code was generated for ScriptCoreLib
using ScriptCoreLib;

namespace java.io
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/io/StringReader.html
	[Script(IsNative = true)]
	public class StringReader : Reader
	{
		public StringReader(string e)
		{

		}

		/// <summary>
		/// Close the stream.
		/// </summary>
		public override void close()
		{
			return;
		}

		/// <summary>
		/// Mark the present position in the stream.
		/// </summary>
		public void mark(int @readAheadLimit)
		{
			return;
		}

		/// <summary>
		/// Tell whether this stream supports the mark() operation, which it does.
		/// </summary>
		public bool markSupported()
		{
			return default(bool);
		}

		/// <summary>
		/// Read a single character.
		/// </summary>
		public int read()
		{
			return default(int);
		}

		/// <summary>
		/// Read characters into a portion of an array.
		/// </summary>
		public override int read(char[] @cbuf, int @off, int @len)
		{
			return default(int);
		}

		/// <summary>
		/// Tell whether this stream is ready to be read.
		/// </summary>
		public bool ready()
		{
			return default(bool);
		}

		/// <summary>
		/// Reset the stream to the most recent mark, or to the beginning of the
		/// string if it has never been marked.
		/// </summary>
		public void reset()
		{
			return;
		}

		/// <summary>
		/// Skip characters.
		/// </summary>
		public long skip(long @ns)
		{
			return default(long);
		}

	}
}
